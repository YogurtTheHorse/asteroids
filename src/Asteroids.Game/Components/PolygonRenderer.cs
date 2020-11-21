using System;
using Microsoft.Xna.Framework;

namespace Asteroids.Systems.Game.Components
{
    /// <summary>
    /// Data for rendering entity as polygon.
    /// </summary>
    public class PolygonRenderer : Renderer
    {
        public Vector2[] Vertices { get; set; } = Array.Empty<Vector2>();

        /// <summary>
        /// Should polygon be rendered as a loop or as a simple chain.
        /// </summary>
        public bool Loop { get; set; } = false;
        
        /// <summary>
        /// Line thickness.
        /// </summary>
        public float Thickness { get; set; } = 1f;

        public PolygonRenderer() => Require<Transform>();
    }
}