using Asteroids.Core.Ecs;
using Asteroids.Systems.Game.Components.Physics;
using Asteroids.Systems.Game.Components.Rendering;

namespace Asteroids.Systems.Game.Components.Entities
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

        public SpriteRenderer? ExhaustRenderer { get; set; }
    }
}