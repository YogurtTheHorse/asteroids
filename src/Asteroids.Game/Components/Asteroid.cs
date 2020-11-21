using Asteroids.Core.Ecs;

namespace Asteroids.Systems.Game.Components
{
    public class Asteroid : Component
    {
        public int Size { get; set; } = 4;
        
        public Asteroid() => this
            .Require<Rigidbody>()
            .Require<Renderer>();
    }
}