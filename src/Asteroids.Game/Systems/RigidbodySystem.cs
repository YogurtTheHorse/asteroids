using System;
using Asteroids.Core;
using Asteroids.Core.Ecs;
using Asteroids.Core.Ecs.Systems;
using Asteroids.Systems.Game.Components;
using Microsoft.Xna.Framework;

namespace Asteroids.Systems.Game.Systems
{
    public class RigidbodySystem : EntityProcessingSystem<Rigidbody>
    {
        public RigidbodySystem(World world) : base(world)
        {
        }

        public override void Update(Entity body, GameTime gameTime)
        {
            var transform = body.Get<Transform>();
            var rigidbody = body.Get<Rigidbody>();
            var delta = (float) gameTime.ElapsedGameTime.TotalSeconds;

            rigidbody.Velocity += rigidbody.Acceleration * delta;
            rigidbody.AngularVelocity += rigidbody.AngularAcceleration * delta;

            rigidbody.Velocity = Vector2.Min(rigidbody.Velocity, rigidbody.MaxVelocity);

            transform.Position += rigidbody.Velocity * delta;
            transform.Rotation += rigidbody.AngularVelocity * delta;
        }
    }
}