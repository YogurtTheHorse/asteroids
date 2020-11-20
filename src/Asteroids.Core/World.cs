using System;
using System.Collections.Generic;
using System.Linq;
using Asteroids.Core.Ecs;
using Asteroids.Core.Ecs.Systems;
using Asteroids.Core.Messaging;
using Microsoft.Xna.Framework;

namespace Asteroids.Core
{
    /// <summary>
    /// Class controlling ECS pattern.
    /// </summary>
    public class World
    {
        private readonly List<IUpdateSystem> _updateSystems;
        private readonly List<IDrawSystem> _drawSystems;
        private readonly List<Entity> _entities;
        private int _entitiesCounter;

        private readonly List<MessageHandler> _messageHandlers;
        private readonly Queue<Message> _messagesQueue;

        public World()
        {
            _updateSystems = new List<IUpdateSystem>();
            _drawSystems = new List<IDrawSystem>();
            _entities = new List<Entity>();
            _messagesQueue = new Queue<Message>();
            _messageHandlers = new List<MessageHandler>();

            _entitiesCounter = 0;
        }

        public IEnumerable<Entity> Entities => _entities.AsReadOnly();

        public World Register(IUpdateSystem updateSystem)
        {
            _updateSystems.Add(updateSystem);

            return this;
        }

        public World Register(IDrawSystem drawSystem)
        {
            _drawSystems.Add(drawSystem);

            return this;
        }

        public World Register(MessageHandler handler)
        {
            _messageHandlers.Add(handler);

            return this;
        }

        public World Register<T>(MessageHandlerType<T> handle) where T : Message
        {
            _messageHandlers.Add(new TypedMessageHandler<T>(handle));

            return this;
        }

        public Entity CreateEntity()
        {
            var entity =
                new Entity(_entitiesCounter++); // ha-ha C++ style (this comment is making this code readable btw)

            _entities.Add(entity);

            return entity;
        }

        public void Update(GameTime gameTime)
        {
            Message[] messages = _messagesQueue.ToArray(); // we do that to avoid loops with recursive answers to queue
            _messagesQueue.Clear();

            foreach (Message message in messages)
            foreach (MessageHandler handler in _messageHandlers)
            {
                // todo: add error handling
                handler.Handle(message);
            }

#if DEBUG
            if (_messagesQueue.Any())
            {
                // TODO: Log warning about possible message loop 
            }
#endif

            foreach (IUpdateSystem updateSystem in _updateSystems)
            {
                // todo: add error handling
                updateSystem.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime)
        {
            foreach (IDrawSystem drawSystem in _drawSystems)
            {
                // todo: add error handling
                drawSystem.Draw();
            }
        }

        public void Send(Message message)
        {
            _messagesQueue.Enqueue(message);
        }
    }
}