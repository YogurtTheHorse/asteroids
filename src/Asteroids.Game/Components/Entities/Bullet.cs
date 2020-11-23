using Asteroids.Core.Ecs;
using Asteroids.Systems.Game.Components.Rendering;

namespace Asteroids.Systems.Game.Components.Entities
{
    public class Bullet : Component
    {
        public Bullet() => this
            .Require<Transform>()
            .Require<Renderer>()
            .Require<Lifetime>();
    }
}