namespace Asteroids.Core.Exceptions
{
    public class EntityNotFoundException : CoreException
    {
        public EntityNotFoundException(int entityId) :base($"Entity {entityId} wasn't found") { }
    }
}