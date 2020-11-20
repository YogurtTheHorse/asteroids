namespace Asteroids.Core
{
    /// <summary>
    /// System for rendering components.
    /// </summary>
    public interface IDrawSystem
    {
        /// <summary>
        /// Called every draw call.
        /// </summary>
        void Draw();
    }
}