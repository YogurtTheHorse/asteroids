using System.Drawing;
using System.Linq;
using Asteroids.Core;
using Asteroids.Core.Ecs;
using Asteroids.Core.Ecs.Systems;
using Asteroids.Core.Utils;
using Asteroids.Systems.Game.Components;
using Asteroids.Systems.Game.Components.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids.Systems.Game.Systems.Rendering
{
    public abstract class RendererSystemBase<T> : WorldSystem, IDrawSystem where T : Renderer
    {
        private readonly GraphicsDevice _graphicsDevice;
        protected SpriteBatch SpriteBatch { get; }

        protected RendererSystemBase(GraphicsDevice graphicsDevice, World world) : base(world)
        {
            _graphicsDevice = graphicsDevice;
            SpriteBatch = new SpriteBatch(graphicsDevice);
        }

        public void Draw(GameTime _)
        {
            SpriteBatch.Begin();

            World
                .Entities
                .With<T>()
                .ForEach(DrawEntity);

            SpriteBatch.End();
        }

        protected Vector2[] GetBoundariesCorners(Transform transform, RectangleF boundaries) => new[]
            {
                new Vector2(boundaries.Right, boundaries.Bottom),
                new Vector2(boundaries.Left, boundaries.Bottom),
                new Vector2(boundaries.Left, boundaries.Top),
                new Vector2(boundaries.Right, boundaries.Top),
            }
            .Select(transform.ToWorld)
            .ToArray();

        protected abstract void DrawAt(Transform transform, T renderer);

        private void DrawEntity(Entity entity)
        {
            var transform = entity.Get<Transform>();
            
            foreach (T renderer in entity.GetAll<T>())
            {
                DrawRenderer(renderer, transform);
            }
        }

        private void DrawRenderer(T renderer, Transform transform)
        {
            int width = World.Width;
            int height = World.Height;

            Vector2[] corners = GetBoundariesCorners(transform, renderer.GetRect());
            (float minX, float minY, float maxX, float maxY) = corners.GetBoundaries();

            DrawAt(transform, renderer);

            if (minX < 0)
            {
                DrawAt(transform.Translate(width, 0), renderer);
            }

            if (minY < 0)
            {
                DrawAt(transform.Translate(0, height), renderer);
            }

            if (maxX > width)
            {
                DrawAt(transform.Translate(-width, 0), renderer);
            }

            if (maxY > height)
            {
                DrawAt(transform.Translate(0, -height), renderer);
            }

            // TODO: Add corner cases
            // TODO: Add corner cases
        }
    }
}