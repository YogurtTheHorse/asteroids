namespace Asteroids.Core.Ecs.Systems
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