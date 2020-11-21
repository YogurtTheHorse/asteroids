using System;
using System.Collections.Generic;
using System.Linq;

namespace Asteroids.Core.Ecs
{
    /// <summary>
    /// Extensions to work with entities.
    /// </summary>
    /// <remarks>
    /// Yeah, it's absolutely not optimal. It will require a lot of calls to Entity.Get on entities et cetera, but this
    /// project is not about good performance. Especially, this part, so please take this in concern. If I had to
    /// write high performance code my ECS system would be significantly differ from what I done here. 
    /// </remarks>
    public static class EntitiesCollectionExtensions
    {
        /// <summary>
        /// Filters entities so only entities with specific components will be present. 
        /// </summary>
        /// <param name="entities">List of entities to filter.</param>
        /// <typeparam name="T">Type of component to filter.</typeparam>
        /// <returns>Filtered entities.</returns>
        public static IEnumerable<Entity> With<T>(this IEnumerable<Entity> entities) where T : Component =>
            entities.Where(e => e.Has<T>());
        
        /// <summary>
        /// Filters entities so only entities with-out specific components will be present. 
        /// </summary>
        /// <param name="entities">List of entities to filter.</param>
        /// <typeparam name="T">Type of component to filter.</typeparam>
        /// <returns>Filtered entities.</returns>
        public static IEnumerable<Entity> Without<T>(this IEnumerable<Entity> entities) where T : Component =>
            entities.Where(e => !e.Has<T>());

        /// <summary>
        /// Executes action for every entity in collection.
        /// </summary>
        /// <param name="entities">Entities to eun action on.</param>
        /// <param name="action">Action to run.</param>
        public static void ForEach(this IEnumerable<Entity> entities, Action<Entity> action)
        {
            foreach (Entity entity in entities)
            {
                action(entity);
            }
        } 
    }
}