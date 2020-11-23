using Asteroids.Core.Messaging;
using Asteroids.Systems.Game.Enums;
using Microsoft.Xna.Framework;

namespace Asteroids.Systems.Game.Messages
{
    public class SpawnEnemy : Message
    {
        public EnemyType EnemyType { get; set; } 
        
        public int Size { get; set; }
        
        public Vector2? Position { get; set; }
    }
}