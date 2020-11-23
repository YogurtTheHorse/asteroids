namespace Asteroids.Core.Exceptions
{
    /// <summary>
    /// Exception thrown when system wasn't found in world.
    /// </summary>
    public class EntityNotFoundException : CoreException
    {
        public EntityNotFoundException(int entityId) :base($"Entity {entityId} wasn't found") { }
    }
}