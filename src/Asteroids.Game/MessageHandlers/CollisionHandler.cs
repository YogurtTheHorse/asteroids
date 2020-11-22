using System;
using Asteroids.Core;
using Asteroids.Core.Ecs;
using Asteroids.Core.Messaging;
using Asteroids.Core.Utils;
using Asteroids.Systems.Game.Components;
using Asteroids.Systems.Game.Messages;
using Microsoft.Xna.Framework;

namespace Asteroids.Systems.Game.MessageHandlers
{
    public class CollisionHandler : TypedMessageHandler<Collision>
    {
        public float ParticlesSpeed { get; set; } = 40;

        private readonly World _world;

        public CollisionHandler(World world)
        {
            _world = world;
        }

        protected override void Handle(Collision message)
        {
            if (message.A.Has<Player>())
            {
                PlayerCollision(message.A, message.B);
            }
            else if (message.B.Has<Player>())
            {
                PlayerCollision(message.B, message.A);
            }
            else if (message.A.Has<Bullet>())
            {
                BulletCollision(message.A, message.B);
            }
            else if (message.B.Has<Bullet>())
            {
                BulletCollision(message.B, message.A);
            }
        }

        private void BulletCollision(Entity bullet, Entity other)
        {
            if (!other.Has<Asteroid>() || other.IsDestroyed) return;

            _world.Destroy(other);
            _world.Destroy(bullet);

            var asteroid = other.Get<Asteroid>();

            SpawnAsteroidParts(other, asteroid);

            for (var i = 0; i < 3 * asteroid.Size; i++)
            {
                _world
                    .CreateEntity()
                    .Attach(new Transform()
                    {
                        Position = bullet.Get<Transform>().Position
                    })
                    .Attach(new PolygonRenderer
                    {
                        Vertices = new[] {Vector2.Zero, Vector2.UnitX}
                    })
                    .Attach(new Rigidbody
                    {
                        Velocity = Vector2.UnitX.Rotate((float) _world.Random.NextDouble() * MathF.PI * 2) *
                                   ParticlesSpeed
                    })
                    .Attach(new Lifetime(1f));
            }

            _world.Send(new PlaySound("sfx/explosions/3"));
        }

        private void SpawnAsteroidParts(Entity other, Asteroid asteroid)
        {
            var asteroidTransform = other.Get<Transform>();
            if (asteroid.Size == 1)
            {
                return;
            }
            
            Vector2 radius = Vector2.UnitX * asteroid.Size * 10f;


            for (var i = 0; i < asteroid.Size; i++)
            {
                Vector2 pos = asteroidTransform.Position + radius.Rotate(MathF.PI * 2 / asteroid.Size * i);

                _world.Send(new SpawnAsteroid
                {
                    Size = asteroid.Size - 1,
                    Position = pos
                });
            }
        }

        private void PlayerCollision(Entity player, Entity other)
        {
            if (!other.Has<Asteroid>()) return;

            _world.Destroy(player);

            _world.Send(new PlaySound("sfx/misc/error"));
        }
    }
}