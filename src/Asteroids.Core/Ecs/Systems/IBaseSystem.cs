namespace Asteroids.Core.Ecs.Systems
{
    /// <summary>
    /// Base interface of game systems. Pretty clean for now.
    /// </summary>
    public interface IBaseSystem
    {
        /// <summary>
        /// Should world process the system.
        /// </summary>
        bool Enabled { get; set; }
    }
}