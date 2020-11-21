using Asteroids.Core.Messaging;

namespace Asteroids.Systems.Game.Messages
{
    public class SpawnAsteroid : Message
    {
        public int Size { get; set; }
    }
}