using System;
using System.Linq;
using Asteroids.Core;
using Asteroids.Core.Ecs;
using Asteroids.Core.Ecs.Systems;
using Asteroids.Core.Utils;
using Asteroids.Systems.Game.Components;
using Asteroids.Systems.Game.Components.Entities;
using Asteroids.Systems.Game.Components.Physics;
using Asteroids.Systems.Game.MessageHandlers;
using Asteroids.Systems.Game.Messages;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Asteroids.Systems.Game.Systems.Input
{
    /// <summary>
    /// System that handles keyboard states and control player.
    /// </summary>
    public class PlayerSystem : EntityProcessingSystem<Player>
    {
        public float RotationSpeed { get; set; } = MathF.PI;

        public float Acceleration { get; set; } = 200f;

        public PlayerSystem(World world) : base(world)
        {
            world.Register<KeyPressed>(KeyPressedHandler);
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
                rigidbody.Acceleration = new Vector2(0f, -1f).Rotate(transform.Rotation) * Acceleration;
            }
            else
            {
                rigidbody.Acceleration = Vector2.Zero;
            }
        }

        private void KeyPressedHandler(KeyPressed message)
        {
            Entity? player = World.Entities.FirstOrDefault(f => f.Has<Player>());

            if (player is null)
            {
                return;
            }
            
            var transform = player.Get<Transform>();

            switch (message.Key)
            {
                case Keys.Z:
                    World.Send(new SpawnBullet
                    {
                        Position = transform.Position,
                        Rotation = transform.Rotation
                    });
                    break;

                case Keys.X:
                    World.Send(new SpawnAsteroid
                    {
                        Size = AsteroidSpawner.MaxSize
                    });
                    break;
            }
        }
    }
}