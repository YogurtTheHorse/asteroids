using Asteroids.Core.Messaging;
using Microsoft.Xna.Framework;

namespace Asteroids.Systems.Game.Messages
{
    public class SpawnAsteroid : Message
    {
        public int Size { get; set; }
        
        public Vector2? Position { get; set; }
    }
}