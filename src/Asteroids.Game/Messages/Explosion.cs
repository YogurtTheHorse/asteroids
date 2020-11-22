using Asteroids.Core.Messaging;
using Microsoft.Xna.Framework;

namespace Asteroids.Systems.Game.Messages
{
    public class Explosion : Message
    {
        public Vector2 Position { get; set; }

        public int Size { get; set; } = 4;
    }
}