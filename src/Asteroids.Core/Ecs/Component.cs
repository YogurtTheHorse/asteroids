using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Asteroids.Core.Ecs
{
    public abstract class Component
    {
        private readonly List<Type> _requiredTypes;

        protected Component()
        {
            _requiredTypes = new List<Type>();
        }

        public Component Require<T>()
        {
            _requiredTypes.Add(typeof(T));
            return this;
        }
        
        public ReadOnlyCollection<Type> RequiredComponents => _requiredTypes.AsReadOnly();
    }
}