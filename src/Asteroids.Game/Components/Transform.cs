using Asteroids.Core.Ecs;
using Asteroids.Core.Utils;
using Microsoft.Xna.Framework;

namespace Asteroids.Systems.Game.Components
{
    /// <summary>
    /// Data about positioning in world.
    /// </summary>
    /// <remarks>
    /// Coordinate system starts in upper left corner. 
    /// </remarks>
    public class Transform : Component
    {
        public Vector2 Position { get; set; }
        
        public float Rotation { get; set; }

        public Vector2 ToWorld(Vector2 local) => Position + local.Rotate(Rotation);
    }
}