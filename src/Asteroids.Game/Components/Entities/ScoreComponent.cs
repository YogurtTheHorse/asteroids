using Asteroids.Core.Ecs;
using Asteroids.Systems.Game.Components.Rendering;

namespace Asteroids.Systems.Game.Components.Entities
{
    public class ScoreComponent : Component
    {
        public ScoreComponent() => Require<Transform>().Require<LabelComponent>();
    }
}