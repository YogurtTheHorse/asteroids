using Asteroids.Core.Ecs;
using SharpMath2;

namespace Asteroids.Systems.Game.Components.Physics
{
    public class Collider : Component
    {
        public Polygon2 Shape { get; }

        public Collider(Polygon2 shape)
        {
            Shape = shape;

            Require<Transform>();
        }
    }
}