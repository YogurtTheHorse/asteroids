using System;

namespace Asteroids.Core.Messaging
{
    /// <summary>
    /// Delegate describing typed handler.
    /// </summary>
    /// <param name="message">Message handler is receivng.</param>
    /// <typeparam name="T">Message type.</typeparam>
    /// <remarks>
    /// I use delegates because they are more generic and lightweight than actions in generic case, but in
    /// implementations i'll use actions as they are delegates implementations, too.
    /// </remarks>
    public delegate void MessageHandlerType<in T>(T message) where T : Message;
    
    public class TypedMessageHandler<T> : MessageHandler where T : Message
    {
        private readonly MessageHandlerType<T> _handler;

        public TypedMessageHandler(MessageHandlerType<T> handler)
        {
            _handler = handler;
        }
        
        public override void Handle(Message message)
        {
            if (message is T typed)
            {
                _handler(typed);
            }
        }
    }
}