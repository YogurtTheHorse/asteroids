namespace Asteroids.Core.Messaging
{
    /// <summary>
    /// Delegate describing typed handler.
    /// </summary>
    /// <param name="message">Message handler is receiving.</param>
    /// <typeparam name="T">Message type.</typeparam>
    /// <remarks>
    /// I use delegates because they are more generic and lightweight than actions in generic case, but in
    /// implementations i'll use actions as they are delegates implementations, too.
    /// </remarks>
    public delegate void MessageHandlerType<in T>(T message) where T : Message;
    
    /// <summary>
    /// Handy implementation of message handler to put delegate as handler.
    /// </summary>
    /// <typeparam name="T">Message type to handle.</typeparam>
    /// <seealso cref="TypedMessageHandler{T}"/>
    public class InlineTypedMessageHandler<T> : TypedMessageHandler<T> where T : Message
    {
        private readonly MessageHandlerType<T> _handler;

        public InlineTypedMessageHandler(MessageHandlerType<T> handler)
        {
            _handler = handler;
        }

        /// <inheritdoc />
        protected override void Handle(T message)
        {
            _handler(message);
        }
    }
}