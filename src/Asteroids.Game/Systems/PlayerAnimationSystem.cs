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
        private SpriteRenderer? _exhaustRenderer = null; 
        private readonly ContentManager _contentLoader;

        public PlayerAnimationSystem(ContentManager contentLoader, World world) : base(world)
        {
            _contentLoader = contentLoader;
        }

        public override void Update(Entity entity, GameTime delta)
        {
            var spriteRenderer = entity.Get<SpriteRenderer>();
            var rigidbody = entity.Get<Rigidbody>();

            if (rigidbody.Acceleration.Length() > 0 && _exhaustRenderer is null)
            {
                _exhaustRenderer = new SpriteRenderer()
                {
                    Texture = _contentLoader.Load<Texture2D>("ship/ship_exhaust_1")
                };
                entity.Attach(_exhaustRenderer);
            }
            else if (_exhaustRenderer is not null && rigidbody.Acceleration == Vector2.Zero)
            {
                entity.DeAttach(_exhaustRenderer);
                _exhaustRenderer = null;
            }

        }
    }
}