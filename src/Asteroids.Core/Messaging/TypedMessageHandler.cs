namespace Asteroids.Core.Messaging
{
    /// <summary>
    /// Message handler that filters message type.
    /// </summary>
    /// <typeparam name="T">Allowed message type.</typeparam>
    public abstract class TypedMessageHandler<T> : IMessageHandler where T : Message
    {
        public void Handle(Message message)
        {
            if (message is T typed)
            {
                Handle(typed); // not a recursion 'cuz will use less generic method.
            }
        }

        protected abstract void Handle(T message);
    }
}