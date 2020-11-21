using System;

namespace Asteroids.Core.Exceptions
{
    public class ComponentNotFoundException : CoreException
    {
        public ComponentNotFoundException(Type componentType, int id) : base($"Entity doesn't have attached {componentType.Name}")
        {
            
        }
    }
}