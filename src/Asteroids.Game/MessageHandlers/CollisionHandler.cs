using System;
using Asteroids.Core;
using Asteroids.Core.Ecs;
using Asteroids.Core.Messaging;
using Asteroids.Core.Utils;
using Asteroids.Systems.Game.Components;
using Asteroids.Systems.Game.Components.Entities;
using Asteroids.Systems.Game.Messages;
using Asteroids.Systems.Game.Systems;
using Asteroids.Systems.Game.Systems.GameLogic;
using Microsoft.Xna.Framework;

namespace Asteroids.Systems.Game.MessageHandlers
{
    public class CollisionHandler : TypedMessageHandler<Collision>
    {
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
            if (other.IsDestroyed) return;
            
            if (!other.Has<Enemy>()) return;

            _world.Destroy(other);
            _world.Destroy(bullet);

            var asteroid = other.TryGet<Asteroid>();

            if (asteroid == null) // ufo
            {
                _world
                    .Get<GameManagementSystem>()
                    .Score += 150;
            }
            else
            {
                SpawnAsteroidParts(other, asteroid);

                _world
                    .Get<GameManagementSystem>()
                    .Score += 10 * asteroid.Size;
            }

            _world.Send(new Explosion
            {
                Position = bullet.Get<Transform>().Position,
                Size = asteroid?.Size ?? 4
            });
        }

        private void SpawnAsteroidParts(Entity other, Asteroid asteroid)
        {
            var asteroidTransform = other.Get<Transform>();
            if (asteroid.Size == 1)
            {
                return;
            }
            
            Vector2 radius = Vector2.UnitX * asteroid.Size * 5f;

            for (var i = 1; i < asteroid.Size; i++)
            {
                Vector2 pos = asteroidTransform.Position + radius.Rotate(MathF.PI * 2 / asteroid.Size * i);

                _world.Send(new SpawnEnemy
                {
                    Size = asteroid.Size - i,
                    Position = pos
                });
            }
        }

        private void PlayerCollision(Entity player, Entity other)
        {
            if (!other.Has<Enemy>()) return;

            _world.Destroy(player);

            _world.Send(new PlaySound("sfx/misc/error"));
        }
    }
}