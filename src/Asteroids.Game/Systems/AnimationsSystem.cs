using System.Linq;
using Asteroids.Core;
using Asteroids.Core.Ecs;
using Asteroids.Core.Ecs.Systems;
using Asteroids.Systems.Game.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids.Systems.Game.Systems
{
    public class AnimationsSystem : EntityProcessingSystem<Animation>
    {
        private readonly ContentManager _content;

        public AnimationsSystem(ContentManager content, World world) : base(world)
        {
            _content = content;
        }

        public override void Update(Entity entity, GameTime delta)
        {
            var animation = entity.Get<Animation>();

            if (animation.Clips.Length == 0)
            {
                return;
            }

            animation.Time += (float)delta.ElapsedGameTime.TotalSeconds;

            if (animation.Time > animation.Duration)
            {
                if (animation.Loop)
                {
                    animation.Time -= animation.Duration;
                }
                else
                {
                    return;
                }
            }
            
            var renderer = entity.Get<SpriteRenderer>();
            var currentClip = 0;

            for (var t = 0f; t + animation.Clips[currentClip].Duration < animation.Time; t += animation.Clips[currentClip].Duration)
            {
                currentClip++;
            }

            renderer.Texture = _content.Load<Texture2D>(animation.Clips[currentClip].Name);
        }
    }
}