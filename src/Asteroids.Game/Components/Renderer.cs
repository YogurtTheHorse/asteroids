using Asteroids.Core.Ecs;
using Microsoft.Xna.Framework;

namespace Asteroids.Systems.Game.Components
{
    public abstract class Renderer : Component
    {
        public Color Color { get; set; }= Color.White;

        public Renderer() => Require<Transform>();
    }
}