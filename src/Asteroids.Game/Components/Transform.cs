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

        public float Scale { get; set; } = 1f;

        public bool KeepOnScreen { get; set; } = true;

        public Vector2 ToWorld(Vector2 local) => Position + (local * Scale).Rotate(Rotation);

        public Transform Translate(Vector2 v) =>
            new()
            {
                Position = Position + v,
                Rotation = Rotation,
                Scale = Scale
            };

        public Transform Translate(float x, float y) => Translate(new Vector2(x, y));
    }
}