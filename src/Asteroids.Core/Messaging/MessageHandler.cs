namespace Asteroids.Core.Messaging
{
    /// <summary>
    /// Interface for generic message handlers.
    /// </summary>
    public interface IMessageHandler
    {
        /// <summary>
        /// Handles a generic message.
        /// </summary>
        /// <param name="message">Message to handle.</param>
        void Handle(Message message);
    }
}