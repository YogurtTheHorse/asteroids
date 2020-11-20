using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;

namespace Asteroids.Core
{
    /// <summary>
    /// Class controlling ECS pattern.
    /// </summary>
    public class World
    {
        private List<IUpdateSystem> _updateSystems;
        private List<IDrawSystem> _drawSystems;
        private List<Entity> _entities;
        private int _entitiesCounter ;

        public World()
        {
            _updateSystems = new List<IUpdateSystem>();
            _drawSystems = new List<IDrawSystem>();
            _entities = new List<Entity>();
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

        public Entity CreateEntity()
        {
            var entity = new Entity(_entitiesCounter++); // ha-ha C++ style (this comment is making this code readable btw)
            
            _entities.Add(entity);

            return entity;
        }

        public void Update(GameTime gameTime)
        {
            foreach (IUpdateSystem updateSystem in _updateSystems)
            {
                updateSystem.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime)
        {
            foreach (IDrawSystem drawSystem in _drawSystems)
            {
                drawSystem.Draw();
            }
        }
    }
}