using Asteroids.Core.Ecs;
using Microsoft.Xna.Framework;

namespace Asteroids.Systems.Game.Components
{
    public class Rigidbody : Component
    {
        public Vector2 Velocity { get; set; } = Vector2.Zero;

        public Vector2 MaxVelocity { get; set; } = new(float.PositiveInfinity);

        public float AngularVelocity { get; set; } = 0f;
        
        public Vector2 Acceleration { get; set; } = Vector2.Zero;

        public float AngularAcceleration { get; set; } = 0f;

        public Rigidbody() => this
            .Require<Transform>();
    }
}