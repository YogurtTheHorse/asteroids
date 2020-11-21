using System;
using Asteroids.Core;
using Asteroids.Core.Ecs;
using Asteroids.Core.Ecs.Systems;
using Asteroids.Systems.Game.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Asteroids.Systems.Game.Systems
{
    public class PlayerSystem : EntityProcessingSystem<Player>, IUpdateSystem
    {
        public float RotationSpeed { get; set; } = MathF.PI;

        public float Acceleration { get; set; } = 200f;

        public PlayerSystem(World world) : base(world)
        {
        }

        public override void Update(Entity player, GameTime delta)
        {
            var rigidbody = player.Get<Rigidbody>();
            var transform = player.Get<Transform>();
            
            var keyboardSate = Keyboard.GetState();
            
            rigidbody.AngularVelocity = 0;

            if (keyboardSate.IsKeyDown(Keys.Left))
            {
                rigidbody.AngularVelocity -= RotationSpeed;
            }

            if (keyboardSate.IsKeyDown(Keys.Right))
            {
                rigidbody.AngularVelocity += RotationSpeed;
            }

            if (keyboardSate.IsKeyDown(Keys.Up))
            {
                rigidbody.Acceleration = new Vector2(0, 1f).Rotate(transform.Rotation) * Acceleration;
            }
            else
            {
                rigidbody.Acceleration = Vector2.Zero;   
            }

        }
    }
}