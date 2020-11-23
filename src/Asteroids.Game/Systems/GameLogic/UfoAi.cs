using System.Linq;
using Asteroids.Core;
using Asteroids.Core.Ecs;
using Asteroids.Core.Ecs.Systems;
using Asteroids.Systems.Game.Components;
using Asteroids.Systems.Game.Components.Entities;
using Asteroids.Systems.Game.Components.Physics;
using Microsoft.Xna.Framework;

namespace Asteroids.Systems.Game.Systems.GameLogic
{
    public class UfoAi : EntityProcessingSystem<Ufo>
    {
        public float UfoAcceleration { get; set; } = 100f;
        
        public UfoAi(World world) : base(world)
        {
        }

        public override void Update(Entity entity, GameTime gameTime)
        {
            Entity? player = World.Entities.With<Player>().FirstOrDefault();
            
            if (player is null) return;

            Vector2 playerPosition = player.Get<Transform>().Position;

            var rigidbody = entity.Get<Rigidbody>();
            var transform = entity.Get<Transform>();

            Vector2 direction = playerPosition - transform.Position;
            direction.Normalize();
            rigidbody.Acceleration = direction * UfoAcceleration;
        }
    }
}