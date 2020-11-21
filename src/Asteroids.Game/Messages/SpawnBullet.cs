using Asteroids.Core.Messaging;
using Microsoft.Xna.Framework;

namespace Asteroids.Systems.Game.Messages
{
    public class SpawnBullet : Message
    {
        public Vector2 Position { get; set; }
    }
}