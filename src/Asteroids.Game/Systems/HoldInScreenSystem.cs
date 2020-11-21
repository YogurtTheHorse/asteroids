using Asteroids.Core;
using Asteroids.Core.Ecs;
using Asteroids.Core.Ecs.Systems;
using Asteroids.Systems.Game.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids.Systems.Game.Systems
{
    public class HoldInScreenSystem : EntityProcessingSystem<Transform>
    {
        private readonly GraphicsDevice _graphicsDevice;
        public HoldInScreenSystem(GraphicsDevice graphicsDevice, World world) : base(world)
        {
            _graphicsDevice = graphicsDevice;
        }

        public override void Update(Entity entity, GameTime delta)
        {
            var height = _graphicsDevice.Viewport.Height;
            var width = _graphicsDevice.Viewport.Width;
            var transform = entity.Get<Transform>();

            if (transform.Position.X > width)
            {
                transform.Position = new Vector2(0, transform.Position.Y);
            }

            if (transform.Position.Y > height)
            {
                transform.Position = new Vector2(transform.Position.X, 0);
            }

            if (transform.Position.X < 0)
            {
                transform.Position = new Vector2(width, transform.Position.Y);
            }

            if (transform.Position.Y < 0)
            {
                transform.Position = new Vector2(transform.Position.X, height);
            }
        }
    }
}