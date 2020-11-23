using Asteroids.Core;
using Asteroids.Core.Ecs;
using Asteroids.Core.Ecs.Systems;
using Asteroids.Systems.Game.Components.Entities;
using Asteroids.Systems.Game.Components.Physics;
using Asteroids.Systems.Game.Components.Rendering;
using Asteroids.Systems.Game.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace Asteroids.Systems.Game.Systems.GameLogic
{
    public class PlayerAnimationSystem : EntityProcessingSystem<Player>
    {
        private readonly JsonLoader _jsonLoader;

        public PlayerAnimationSystem(ContentManager contentLoader, World world) : base(world)
        {
            _jsonLoader = new JsonLoader(contentLoader);
        }

        public override void Update(Entity entity, GameTime delta)
        {
            var player = entity.Get<Player>();
            var rigidbody = entity.Get<Rigidbody>();

            if (rigidbody.Acceleration.Length() > 0 && player.ExhaustRenderer is null)
            {
                player.ExhaustRenderer = new SpriteRenderer();
                player.ExhaustAnimation = _jsonLoader.Load<Animation>("animations/exhaust");
                player.ExhaustAnimation.Renderer = player.ExhaustRenderer;
                
                entity.Attach(player.ExhaustRenderer);
                entity.Attach(player.ExhaustAnimation);
            }
            else if (player.ExhaustRenderer is not null && rigidbody.Acceleration == Vector2.Zero)
            {
                entity.DeAttach(player.ExhaustAnimation!);
                entity.DeAttach(player.ExhaustRenderer);
                
                player.ExhaustRenderer = null;
                player.ExhaustAnimation = null;
            }

        }
    }
}