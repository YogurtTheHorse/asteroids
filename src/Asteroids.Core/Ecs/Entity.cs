using System;
using System.Collections.Generic;
using System.Linq;
using Asteroids.Core.Exceptions;

namespace Asteroids.Core.Ecs
{
    /// <summary>
    /// Represents some object of an ECS world. In current implementation entity is used as components container with
    /// some unique id given be world. 
    /// </summary>
    /// <seealso cref="World.CreateEntity"/>
    public class Entity
    {
        /// <summary>
        /// Unique ID by which entity could by accessed.
        /// </summary>
        public int Id { get; }
        
        /// <summary>
        /// Is entity marked as destroyed and will be removed from world after this frame.
        /// </summary>
        public bool IsDestroyed { get; internal set; }
        
        private readonly List<Component> _components;

        public Entity(int id)
        {
            Id = id;

            _components = new List<Component>();
        }

        /// <summary>
        /// Adds component to entity and checks its requirements.
        /// </summary>
        /// <param name="component">Component to attach to entity.</param>
        /// <returns>Modified entity.</returns>
        /// <exception cref="ComponentNotFoundException">
        /// Thrown when component requirements weren't satisfied.
        /// </exception>
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

        /// <summary>
        /// Removes component from entity. Before de-attaching checks is it possible, as component may be required by
        /// others. 
        /// </summary>
        /// <param name="component">Component instance to remove.</param>
        /// <returns>Modified entity.</returns>
        /// <exception cref="CoreException">
        /// Thrown when component cannot be removed due dependencies of other components.</exception>
        /// <exception cref="ComponentNotFoundException">
        /// Thrown when component wasn't attached to entity. 
        /// </exception>
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

        /// <summary>
        /// Checks whether entity has component of a given type.
        /// </summary>
        /// <typeparam name="T">Type of component to check.</typeparam>
        /// <returns>True if there is a component of provided type attached to entity.</returns>
        /// <see cref="Has"/>
        public bool Has<T>() where T : Component => Has(typeof(T));

        /// <summary>
        /// Checks whether entity has component of type.
        /// </summary>
        /// <param name="componentType">Type of checked component.</param>
        /// <returns>True if there is a component of provided type attached to entity.</returns>
        public bool Has(Type componentType) => _components.Any(componentType.IsInstanceOfType);

        /// <summary>
        /// Gets component by its type.
        /// </summary>
        /// <typeparam name="T">Type of component.</typeparam>
        /// <returns>Component of a given type.</returns>
        /// <exception cref="ComponentNotFoundException">Thrown when component wasn't found on entity.</exception>
        public T Get<T>() where T : Component
        {
            var component = TryGet<T>();

            if (component is null)
            {
                throw new ComponentNotFoundException(typeof(T), Id);
            }

            return component;
        }

        /// <summary>
        /// Tries to find a component by type.
        /// </summary>
        /// <typeparam name="T">Type of component.</typeparam>
        /// <returns>Found component and null otherwise.</returns>
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

        public IEnumerable<T> GetAll<T>() where T : Component => _components
            .Where(c => c is T)
            .Cast<T>();
    }
}