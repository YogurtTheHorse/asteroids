using System;
using System.Linq;
using Asteroids.Core;
using Asteroids.Core.Ecs;
using Asteroids.Core.Ecs.Systems;
using Asteroids.Core.Utils;
using Asteroids.Systems.Game.Components;
using Asteroids.Systems.Game.Components.Entities;
using Asteroids.Systems.Game.Components.Physics;
using Asteroids.Systems.Game.Enums;
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
        public float ShootImpact { get; set; } = 5f;

        public float RotationSpeed { get; set; } = MathF.PI * 1.5f;

        public float Acceleration { get; set; } = 200f;

        public PlayerSystem(World world) : base(world)
        {
            world.Register<KeyPressed>(KeyPressedHandler);
        }

        public override void Update(Entity player, GameTime gameTime)
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
            Entity? playerEntity = World.Entities.FirstOrDefault(f => f.Has<Player>());

            if (playerEntity is null)
            {
                return;
            }

            var transform = playerEntity.Get<Transform>();
            var rigidbody = playerEntity.Get<Rigidbody>();
            var player = playerEntity.Get<Player>();

            switch (message.Key)
            {
                case Keys.Z:
                    rigidbody.Velocity -= new Vector2(0f, -1f).Rotate(transform.Rotation) * ShootImpact;
                    World.Send(new SpawnBullet
                    {
                        Position = transform.Position,
                        Rotation = transform.Rotation
                    });
                    break;

                case Keys.X when player.LasersCount > 0:
                    player.LasersCount--;
                    World.Send(new SpawnBullet
                    {
                        BulletType = BulletType.Laser,
                        Position = transform.Position,
                        Rotation = transform.Rotation
                    });
                    break;
            }
        }
    }
}