using Asteroids.Core.Exceptions;

namespace Asteroids.Core
{
    public class EntityNotFoundException : CoreException
    {
        public EntityNotFoundException(int entityId) :base($"Entity {entityId} wasn't find") { }
    }
}