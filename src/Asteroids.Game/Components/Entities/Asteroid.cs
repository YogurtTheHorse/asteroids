using Asteroids.Core.Ecs;
using Asteroids.Systems.Game.Components.Physics;
using Asteroids.Systems.Game.Components.Rendering;

namespace Asteroids.Systems.Game.Components.Entities
{
    public class Asteroid : Enemy
    {
        public int Size { get; set; } = 4;
        
        public Asteroid() => this
            .Require<Rigidbody>()
            .Require<Renderer>();
    }
}