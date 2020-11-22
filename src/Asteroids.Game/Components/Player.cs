using Asteroids.Core.Ecs;

namespace Asteroids.Systems.Game.Components
{
    /// <summary>
    /// Player data.
    /// </summary>
    public class Player : Component
    {
        public Player() => this
            .Require<Transform>()
            .Require<Rigidbody>()
            .Require<Renderer>();

        public SpriteRenderer ExhaustRenderer { get; set; }
    }
}