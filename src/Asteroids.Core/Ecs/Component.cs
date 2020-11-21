using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Asteroids.Core.Ecs
{
    /// <summary>
    /// This is one of three main parts of ECS system, describing some data of entity which will be used by system.
    /// </summary>
    public abstract class Component
    {
        private readonly List<Type> _requiredTypes;
        
        /// <summary>
        /// Collection of components' types required by component. Dependencies list.  
        /// </summary>
        public ReadOnlyCollection<Type> RequiredComponents => _requiredTypes.AsReadOnly();

        protected Component()
        {
            _requiredTypes = new List<Type>();
        }

        /// <summary>
        /// Places some component type to requirements.
        /// </summary>
        /// <typeparam name="T">Type of component this component depends on.</typeparam>
        /// <returns>Modified component (for flow).</returns>
        /// <seealso cref="Entity.Attach"/>
        public Component Require<T>() where T : Component
        {
            _requiredTypes.Add(typeof(T));
            return this;
        }
    }
}