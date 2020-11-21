using System.Linq;
using Asteroids.Core;
using Asteroids.Core.Ecs;
using Asteroids.Core.Ecs.Systems;
using Asteroids.Core.Utils;
using Asteroids.Systems.Game.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids.Systems.Game.Systems
{
    public abstract class RendererSystemBase<T> : AbstractSystem, IDrawSystem where T : Renderer
    {
        private readonly GraphicsDevice _graphicsDevice;
        protected SpriteBatch SpriteBatch { get; }

        protected RendererSystemBase(GraphicsDevice graphicsDevice, World world) : base(world)
        {
            _graphicsDevice = graphicsDevice;
            SpriteBatch = new SpriteBatch(graphicsDevice);
        }

        public void Draw()
        {
            SpriteBatch.Begin();

            World
                .Entities
                .With<T>()
                .ForEach(DrawEntity);

            SpriteBatch.End();
        }

        private Vector2[] GetBoundariesCorners(Vector2 center, Vector2 boundaries, float angle) => new[]
            {
                new Vector2(-boundaries.X / 2, -boundaries.Y / 2),
                new Vector2(boundaries.X / 2, -boundaries.Y / 2),
                new Vector2(-boundaries.X / 2, boundaries.Y / 2),
                new Vector2(boundaries.X / 2, boundaries.Y / 2)
            }
            .Select(v => center + v.Rotate(angle))
            .ToArray();

        private void DrawEntity(Entity entity)
        {
            var width = _graphicsDevice.Viewport.Width;
            var height = _graphicsDevice.Viewport.Height;

            var renderer = entity.Get<T>();
            var transform = entity.Get<Transform>();
            var boundaries = renderer.GetBoundaries();
            var corners = GetBoundariesCorners(transform.Position, boundaries, transform.Rotation);

            var minX = corners.Min(v => v.X);
            var minY = corners.Min(v => v.Y);
            var maxX = corners.Max(v => v.X);
            var maxY = corners.Max(v => v.Y);

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
        }

        protected abstract void DrawAt(Transform transform, T renderer);
    }
}