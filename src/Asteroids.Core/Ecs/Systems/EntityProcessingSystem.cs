using Microsoft.Xna.Framework;

namespace Asteroids.Core.Ecs.Systems
{
    /// <summary>
    /// System that automatically finds all entities with specified component and calls Update on them.
    /// </summary>
    /// <typeparam name="T">Component to look for.</typeparam>
    public abstract class EntityProcessingSystem<T> : WorldSystem, IUpdateSystem where T : Component
    {
        public EntityProcessingSystem(World world) : base(world) { }

        /// <inheritdoc />
        public virtual void Update(GameTime gameTime) => World
            .Entities
            .With<T>()
            .ForEach(e => Update(e, gameTime));

        /// <summary>
        /// Calls update on entity with {T} component.
        /// </summary>
        /// <param name="entity">Entity with component.</param>
        /// <param name="gameTime">Game time.</param>
        public abstract void Update(Entity entity, GameTime gameTime);
    }
}