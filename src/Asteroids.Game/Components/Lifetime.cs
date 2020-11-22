using Asteroids.Core.Ecs;

namespace Asteroids.Systems.Game.Components
{
    public class Lifetime : Component
    {
        public float TimeToLive { get; set; }

        public Lifetime(float timeToLive)
        {
            TimeToLive = timeToLive;
        }
    }
}