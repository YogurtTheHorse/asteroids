using Asteroids.Core;
using Asteroids.Core.Ecs;
using Asteroids.Core.Ecs.Systems;
using Asteroids.Systems.Game.Components;
using Asteroids.Systems.Game.Components.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids.Systems.Game.Systems.Rendering
{
    /// <summary>
    /// System that keeps entities inside screen.
    /// </summary>
    public class KeepInScreenSystem : EntityProcessingSystem<Transform>
    {
        private readonly GraphicsDevice _graphicsDevice;
        
        public KeepInScreenSystem(GraphicsDevice graphicsDevice, World world) : base(world)
        {
            _graphicsDevice = graphicsDevice;
        }

        public override void Update(Entity entity, GameTime delta)
        {
            var transform = entity.Get<Transform>();

            if (transform.KeepOnScreen)
            {
                PutBackOnScreen(transform);
            }
        }

        private void PutBackOnScreen(Transform transform)
        {
            int height = World.Height;
            int width = World.Width;

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