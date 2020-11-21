using Asteroids.Core.Messaging;
using Microsoft.Xna.Framework.Input;

namespace Asteroids.Systems.Game.Messages
{
    public class KeyPressed : Message
    {
        public Keys Key { get; set; }
    }
}