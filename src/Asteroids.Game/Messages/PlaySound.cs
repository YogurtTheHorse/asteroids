using Asteroids.Core.Messaging;

namespace Asteroids.Systems.Game.Messages
{
    public class PlaySound : Message
    {
        public PlaySound(string sound)
        {
            Sound = sound;
        }

        public string Sound { get; }
    }
}