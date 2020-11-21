using System;
using Asteroids.Core.Ecs;
using Microsoft.Xna.Framework;

namespace Asteroids.Systems.Game.Components
{
    public class PolygonRenderer : Renderer
    {
        public Vector2[] Vertices { get; set; } = Array.Empty<Vector2>();

        public bool Loop { get; set; } = false;
        
        public float Thickness { get; set; } = 1f;

        public PolygonRenderer() => Require<Transform>();
    }
}