using Asteroids.Systems.Game.Components.Rendering;

namespace Asteroids.Systems.Game.Components.Entities
{
    public class Ufo : Enemy
    {
        public Ufo() => Require<Renderer>();
    }
}