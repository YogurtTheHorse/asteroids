using Asteroids.Core.Ecs;
using Asteroids.Systems.Game.Enums;
using Microsoft.Xna.Framework;

namespace Asteroids.Systems.Game.Components
{
    public class LabelComponent : Component
    {
        public string Label { get; set; } = string.Empty;

        public Align Align { get; set; } = Align.TopLeft;
        
        public Color Color { get; set; } = Color.White;

        public LabelComponent() => Require<Transform>();
    }
}