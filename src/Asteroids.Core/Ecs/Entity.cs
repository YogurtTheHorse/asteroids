using System;
using System.Collections.Generic;
using System.Linq;
using Asteroids.Core.Exceptions;

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
            foreach (var requiredComponent in component.RequiredComponents)
            {
                if (!Has(requiredComponent))
                {
                    throw new ComponentNotFoundException(requiredComponent, Id);
                }
            }
            
            _components.Add(component);
            return this;
        }

        public Entity DeAttach(Component component)
        {
            // TODO: Add check this component was last of that type

            foreach (var attachedComponent in _components)
            foreach (var requiredComponent in attachedComponent.RequiredComponents)
            {
                if (requiredComponent.IsInstanceOfType(component))
                {
                    throw new CoreException(
                        $"{component.GetType().Name} can't be removed, because it's required by {requiredComponent.Name}"
                    );
                }
            }

            if (!_components.Remove(component))
            {
                throw new ComponentNotFoundException(component.GetType(), Id);
            }

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
                throw new ComponentNotFoundException(typeof(T), Id);
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