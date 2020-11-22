using Asteroids.Core.Ecs;

namespace Asteroids.Systems.Game.Components
{
    public class Bullet : Component
    {
        public Bullet() => this
            .Require<Transform>()
            .Require<Renderer>()
            .Require<Lifetime>();
    }
}