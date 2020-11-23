using Microsoft.Xna.Framework;

namespace Asteroids.Core.Ecs.Systems
{
    /// <summary>
    /// System for handling updates.
    /// </summary>
    public interface IUpdateSystem : IBaseSystem
    {
        /// <summary>
        /// Called every frame.
        /// </summary>
        /// <param name="gameTime">Time passed from last time.</param>
        void Update(GameTime gameTime);
    }
}