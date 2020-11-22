using System;
using Asteroids.Core;
using Asteroids.Core.Ecs;
using Asteroids.Core.Messaging;
using Asteroids.Systems.Game.Components;
using Asteroids.Systems.Game.Messages;

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
            if (!other.Has<Asteroid>()) return;
            
            _world.Destroy(other); 
            _world.Destroy(bullet); 
                
            // TODO: Spawn explosion
            _world.Send(new PlaySound("sfx/explosions/3"));
        }

        private void PlayerCollision(Entity player, Entity other)
        {
        }
    }
}