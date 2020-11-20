namespace Asteroids.Core.Messaging
{
    public abstract class MessageHandler
    {
        public abstract void Handle(Message message);
    }
}