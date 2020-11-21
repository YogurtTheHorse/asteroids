using Asteroids.Core;
using Asteroids.Core.Ecs;
using Asteroids.Core.Ecs.Systems;
using Asteroids.Systems.Game.Components;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Primitives2D;

namespace Asteroids.Systems.Game.Systems
{
    public class PolygonRendererSystem : AbstractSystem, IDrawSystem
    {
        private readonly SpriteBatch _drawBatch;

        public PolygonRendererSystem(GraphicsDevice graphicsDevice, World world) : base(world)
        {
            _drawBatch = new SpriteBatch(graphicsDevice);
        }

        public void Draw()
        {
            _drawBatch.Begin();

            World
                .Entities
                .With<PolygonRenderer>()
                .ForEach(DrawPolygon);

            _drawBatch.End();

            void DrawPolygon(Entity e) // local function is faster than lambda, so... why not.
            {
                var renderer = e.Get<PolygonRenderer>();
                var transform = e.Get<Transform>();
                var end = renderer.Loop
                    ? renderer.Vertices.Length
                    : renderer.Vertices.Length - 1;

                for (int i = 1; i <= end; ++i)
                {
                    _drawBatch.DrawLine(
                        transform.ToWorld(renderer.Vertices[i - 1]),
                        transform.ToWorld(renderer.Vertices[i % renderer.Vertices.Length]),
                        renderer.Color,
                        renderer.Thickness
                    );
                }
            }
        }
    }
}