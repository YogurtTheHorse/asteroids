using Asteroids.Core.Ecs;

namespace Asteroids.Systems.Game.Components
{
    /// <summary>
    /// Player data.
    /// </summary>
    public class Player : Component
    {
        /// <summary>
        /// Score player got.
        /// </summary>
        public int Score { get; set; } = 0;
        
        public Player() => this
            .Require<Transform>()
            .Require<Rigidbody>()
            .Require<Renderer>();
    }
}