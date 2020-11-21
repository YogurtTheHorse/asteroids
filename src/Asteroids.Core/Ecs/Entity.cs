using System;
using System.Collections.Generic;
using System.Linq;

namespace Asteroids.Core.Ecs
{
    public class Entity
    {
        public int Id { get; }
        
        private readonly List<Component> _components;

        public Entity(int id)
        {
            Id = id;

            _components = new List<Component>();
        }

        public Entity Attach(Component component)
        {
            _components.Add(component);
            return this;
        }

        public Entity DeAttach(Component component)
        {
            _components.Remove(component);

            return this;
        }

        public bool Has<T>() where T : Component => Has(typeof(T));

        /// <summary>
        /// Checks whether entity has component of type.
        /// </summary>
        /// <param name="componentType">Type of checked component.</param>
        /// <returns>True if there is a component of provided type attached to entity.</returns>
        public bool Has(Type componentType) => _components.Any(componentType.IsInstanceOfType);

        public T Get<T>() where T : Component
        {
            var component = TryGet<T>();

            if (component is null)
            {
                throw new KeyNotFoundException($"Component of type {typeof(T).Name} is not present on entity {Id}");
            }

            return component;
        }

        public T? TryGet<T>() where T : Component
        {
            foreach (Component component in _components)
            {
                if (component is T typed)
                {
                    return typed;
                }
            }

            return null;
        }
    }
}