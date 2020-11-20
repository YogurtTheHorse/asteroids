using System;
using System.Collections.Generic;
using System.Linq;

namespace Asteroids.Core
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
        public static IEnumerable<Entity> With<T>(this IEnumerable<Entity> entites) where T : Component =>
            entites.Where(e => e.Has<T>());
        
        public static IEnumerable<Entity> Without<T>(this IEnumerable<Entity> entites) where T : Component =>
            entites.Where(e => e.HasNot<T>());

        public static void ForEach(this IEnumerable<Entity> entities, Action<Entity> action)
        {
            foreach (Entity entity in entities)
            {
                action(entity);
            }
        } 
    }
}