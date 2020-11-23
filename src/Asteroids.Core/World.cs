using System;
using System.Collections.Generic;
using System.Linq;
using Asteroids.Core.Ecs;
using Asteroids.Core.Ecs.Systems;
using Asteroids.Core.Exceptions;
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

        private readonly List<IMessageHandler> _messageHandlers;
        private readonly Queue<Message> _messagesQueue;

        /// <summary>
        /// Current entities of the world.
        /// </summary>
        public IEnumerable<Entity> Entities => _entities.AsReadOnly();

        /// <summary>
        /// Native world width.
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// Native world height.
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// Public random initialized by world.
        /// </summary>
        public Random Random { get; }

        public World(int width, int height)
        {
            Width = width;
            Height = height;
            _updateSystems = new List<IUpdateSystem>();
            _drawSystems = new List<IDrawSystem>();
            _entities = new List<Entity>();
            _messagesQueue = new Queue<Message>();
            _messageHandlers = new List<IMessageHandler>();

            _entitiesCounter = 0;

            Random = new Random();
        }

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

        public World Register(IMessageHandler handler)
        {
            _messageHandlers.Add(handler);

            return this;
        }

        /// <summary>
        /// Automatically created <see cref="MessageHandlerType{T}"/> and registers it as message handler.
        /// </summary>
        /// <seealso cref="Register(Asteroids.Core.Messaging.IMessageHandler)"/>
        public World Register<T>(MessageHandlerType<T> handle) where T : Message
        {
            _messageHandlers.Add(new InlineTypedMessageHandler<T>(handle));

            return this;
        }

        /// <summary>
        /// Creates empty entity with unique id.
        /// </summary>
        /// <returns>Created entity.</returns>
        public Entity CreateEntity()
        {
            // ha-ha C++ style (this comment is making this code readable btw)
            var entity = new Entity(_entitiesCounter++); 

            _entities.Add(entity);

            return entity;
        }

        public void Update(GameTime gameTime)
        {
            Message[] messages = _messagesQueue.ToArray(); // we do that to avoid loops with recursive answers to queue
            _messagesQueue.Clear();

            foreach (Message message in messages)
            foreach (IMessageHandler handler in _messageHandlers)
            {
                try
                {
                    handler.Handle(message);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            foreach (IUpdateSystem updateSystem in _updateSystems)
            {
                if (!updateSystem.Enabled) continue;
                try
                {
                    updateSystem.Update(gameTime);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            for (int i = 0; i < _entities.Count; i++)
            {
                if (_entities[i].IsDestroyed)
                {
                    _entities.RemoveAt(i);
                    i--;
                }
            }
        }

        public void Draw(GameTime gameTime)
        {
            foreach (IDrawSystem drawSystem in _drawSystems)
            {
                if (!drawSystem.Enabled) continue;

                try
                {
                    drawSystem.Draw(gameTime);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        /// <summary>
        /// Sends message to all message handlers before next update frame.
        /// </summary>
        public void Send(Message message)
        {
            _messagesQueue.Enqueue(message);
        }

        /// <summary>
        /// Marks entity as destroyed and removes it from entities collection right after update.
        /// </summary>
        public void Destroy(int entityId)
        {
            var entity = _entities.FirstOrDefault(e => e.Id == entityId);

            if (entity is null)
            {
                throw new EntityNotFoundException(entityId);
            }

            entity.IsDestroyed = true;
        }

        /// <inheritdoc cref="Destroy(int)"/>
        public void Destroy(Entity entity)
        {
            if (entity.IsDestroyed) return;

            Destroy(entity.Id);
        }

        /// <summary>
        /// Gets system attached to world by its type.
        /// </summary>
        public T Get<T>() where T : IBaseSystem
        {
            foreach (IDrawSystem drawSystem in _drawSystems)
            {
                if (drawSystem is T typed)
                {
                    return typed;
                }
            }

            foreach (IUpdateSystem updateSystem in _updateSystems)
            {
                if (updateSystem is T typed)
                {
                    return typed;
                }
            }

            throw new CoreException($"System {typeof(T).Name} is not present in world.");
        }
    }
}