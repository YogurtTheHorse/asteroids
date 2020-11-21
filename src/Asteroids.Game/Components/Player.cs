using Asteroids.Core.Ecs;

namespace Asteroids.Systems.Game.Components
{
    public class Player : Component
    {
        public int Score { get; set; } = 0;

        public Player() => this
            .Require<Transform>()
            .Require<Rigidbody>()
            .Require<Renderer>();
    }
}