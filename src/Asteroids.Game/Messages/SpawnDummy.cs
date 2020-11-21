using Asteroids.Core.Messaging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids.Systems.Game.Messages
{
    public class SpawnDummy : Message
    {
        public Vector2 Position { get; set; }

        public Texture2D Texture { get; set; }
    }
}