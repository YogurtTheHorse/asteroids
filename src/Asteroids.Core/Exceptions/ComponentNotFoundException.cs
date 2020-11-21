using System;

namespace Asteroids.Core.Exceptions
{
    /// <summary>
    /// Exception thrown when component wasn't found on specific entity.
    /// </summary>
    public class ComponentNotFoundException : CoreException
    {
        public ComponentNotFoundException(Type componentType, int id) : base($"Entity doesn't have attached {componentType.Name}")
        {
            
        }
    }
}