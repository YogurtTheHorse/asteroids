using Asteroids.Core;
using Asteroids.Core.Ecs;
using Microsoft.Xna.Framework;

namespace Asteroids.Systems.Game.Components
{
    public class Transform : Component
    {
        public Vector2 Position { get; set; }
        
        public float Rotation { get; set; }

        public Vector2 ToWorld(Vector2 local) => Position + local.Rotate(Rotation);
    }
}