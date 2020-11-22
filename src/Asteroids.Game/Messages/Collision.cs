using Asteroids.Core.Ecs;
using Asteroids.Core.Messaging;

namespace Asteroids.Systems.Game.Messages
{
    public class Collision : Message
    {
        public Collision(Entity a, Entity b)
        {
            A = a;
            B = b;
        }

        public Entity A { get; }
        
        public Entity B { get; }
    }
}