using Asteroids.Core.Messaging;
using Asteroids.Systems.Game.Enums;
using Microsoft.Xna.Framework;

namespace Asteroids.Systems.Game.Messages
{
    public class SpawnBullet : Message
    {
        public Vector2 Position { get; set; }
        
        public float Rotation { get; set; }

        public BulletType BulletType { get; set; } = BulletType.Regular;
    }
}