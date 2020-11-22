using Asteroids.Core.Ecs;

namespace Asteroids.Systems.Game.Components
{
    public class ScoreComponent : Component
    {
        public ScoreComponent() => Require<Transform>().Require<LabelComponent>();
    }
}