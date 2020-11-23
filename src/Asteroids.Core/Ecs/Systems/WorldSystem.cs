namespace Asteroids.Core.Ecs.Systems
{
    /// <summary>
    /// Abstract system as base class to hold world and implement interface.
    /// </summary>
    public abstract class WorldSystem : IBaseSystem
    {
        protected readonly World World;
        
        /// <inheritdoc />
        public bool Enabled { get; set; } = true;

        protected WorldSystem(World world)
        {
            World = world;
        }
    }
}