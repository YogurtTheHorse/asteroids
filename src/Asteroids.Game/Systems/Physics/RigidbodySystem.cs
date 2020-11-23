using System;
using Asteroids.Core;
using Asteroids.Core.Ecs;
using Asteroids.Core.Ecs.Systems;
using Asteroids.Systems.Game.Components;
using Asteroids.Systems.Game.Components.Physics;
using Microsoft.Xna.Framework;

namespace Asteroids.Systems.Game.Systems.Physics
{
    /// <summary>
    /// System that handles velocities of entities with rigidbody.
    /// </summary>
    public class RigidbodySystem : EntityProcessingSystem<Rigidbody>
    {
        public RigidbodySystem(World world) : base(world) { }

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

            if (transform.Rotation > MathF.PI * 2)
            {
                transform.Rotation -= MathF.PI * 2;
            }

            if (transform.Rotation < -MathF.PI * 2)
            {
                transform.Rotation += MathF.PI * 2;
            }
        }
    }
}