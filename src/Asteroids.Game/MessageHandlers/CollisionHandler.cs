using System;
using Asteroids.Core.Messaging;
using Asteroids.Systems.Game.Messages;

namespace Asteroids.Systems.Game.MessageHandlers
{
    public class CollisionHandler : TypedMessageHandler<Collision>
    {
        protected override void Handle(Collision message)
        {
            Console.WriteLine($"Collision detected between {message.A.Id} and {message.B.Id}");
        }
    }
}