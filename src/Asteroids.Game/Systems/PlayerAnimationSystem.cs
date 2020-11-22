using Asteroids.Core;
using Asteroids.Core.Ecs;
using Asteroids.Core.Ecs.Systems;
using Asteroids.Systems.Game.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids.Systems.Game.Systems
{
    public class PlayerAnimationSystem : EntityProcessingSystem<Player>
    {
        private readonly ContentManager _contentLoader;

        public PlayerAnimationSystem(ContentManager contentLoader, World world) : base(world)
        {
            _contentLoader = contentLoader;
        }

        public override void Update(Entity entity, GameTime delta)
        {
            var player = entity.Get<Player>();
            var rigidbody = entity.Get<Rigidbody>();

            if (rigidbody.Acceleration.Length() > 0 && player.ExhaustRenderer is null)
            {
                player.ExhaustRenderer = new SpriteRenderer()
                {
                    Texture = _contentLoader.Load<Texture2D>("ship/ship_exhaust_1")
                };
                entity.Attach(player.ExhaustRenderer);
            }
            else if (player.ExhaustRenderer is not null && rigidbody.Acceleration == Vector2.Zero)
            {
                entity.DeAttach(player.ExhaustRenderer);
                player.ExhaustRenderer = null;
            }

        }
    }
}