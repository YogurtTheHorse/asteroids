using Asteroids.Core;
using Asteroids.Systems.Game.Components;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Primitives2D;

namespace Asteroids.Systems.Game.Systems
{
    public class PolygonRendererSystem : RendererSystemBase<PolygonRenderer>
    {
        public PolygonRendererSystem(GraphicsDevice graphicsDevice, World world) : base(graphicsDevice, world) { }

        protected override void DrawAt(Transform transform, PolygonRenderer renderer)
        {
            int end = renderer.Loop
                ? renderer.Vertices.Length
                : renderer.Vertices.Length - 1;

            for (var i = 1; i <= end; ++i)
            {
                SpriteBatch.DrawLine(
                    transform.ToWorld(renderer.Vertices[i - 1]),
                    transform.ToWorld(renderer.Vertices[i % renderer.Vertices.Length]),
                    renderer.Color,
                    renderer.Thickness
                );
            }
        }
    }
}