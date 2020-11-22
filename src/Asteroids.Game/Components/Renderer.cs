using System.Drawing;
using Asteroids.Core.Ecs;
using Color = Microsoft.Xna.Framework.Color;

namespace Asteroids.Systems.Game.Components
{
    /// <summary>
    /// Base class for components describing renderer data.
    /// </summary>
    public abstract class Renderer : Component
    {
        /// <summary>
        /// Base color of renderer.
        /// </summary>
        public Color Color { get; set; }= Color.White;

        public Renderer() => Require<Transform>();

        public abstract RectangleF GetRect();
    }
}