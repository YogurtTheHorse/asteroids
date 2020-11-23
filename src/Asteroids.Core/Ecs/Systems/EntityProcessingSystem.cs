using Microsoft.Xna.Framework;

namespace Asteroids.Core.Ecs.Systems
{
    public abstract class EntityProcessingSystem<T> : AbstractSystem, IUpdateSystem where T : Component
    {
        public EntityProcessingSystem(World world) : base(world) { }

        public virtual void Update(GameTime gameTime) => World
            .Entities
            .With<T>()
            .ForEach(e => Update(e, gameTime));

        public abstract void Update(Entity entity, GameTime delta);
    }
}