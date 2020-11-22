using Asteroids.Core.Ecs;

namespace Asteroids.Systems.Game.Components
{
    public class ScoreComponent : Component
    {
        public int Score { get; set; } = 0;
        
        public ScoreComponent()
        {
            Require<Transform>().Require<LabelComponent>();
        }
    }
}