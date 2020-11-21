using Asteroids.Core.Ecs;
using Microsoft.Xna.Framework;

namespace Asteroids.Systems.Game.Components
{
    public class Transform : Component
    {
        public Vector2 Position { get; set; }
        
        public float Rotation { get; set; }
    }
}