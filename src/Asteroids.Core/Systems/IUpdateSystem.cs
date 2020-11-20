using Microsoft.Xna.Framework;

namespace Asteroids.Core
{
    /// <summary>
    /// System for handling updates.
    /// </summary>
    public interface IUpdateSystem
    {
        /// <summary>
        /// Called every frame.
        /// </summary>
        /// <param name="delta">Time passed from last time.</param>
        void Update(GameTime delta);
    }
}