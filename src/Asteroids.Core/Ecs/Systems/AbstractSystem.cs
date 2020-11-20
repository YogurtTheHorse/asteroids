namespace Asteroids.Core.Systems
{
    public abstract class AbstractSystem : IBaseSystem
    {
        protected readonly World World;
        
        public bool Enabled { get; set; } = true;

        protected AbstractSystem(World world)
        {
            World = world;
        }
    }
}