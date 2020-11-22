using Asteroids.Core;
using Asteroids.Core.Ecs;
using Asteroids.Core.Ecs.Systems;
using Asteroids.Systems.Game.Components;
using Microsoft.Xna.Framework;

namespace Asteroids.Systems.Game.Systems
{
    public class ScoreSystem : EntityProcessingSystem<ScoreComponent>
    {
        public ScoreSystem(World world) : base(world) { }

        public override void Update(Entity entity, GameTime delta)
        {
            var label = entity.Get<LabelComponent>();
            var score = entity.Get<ScoreComponent>();

            label.Label = $"Score: {score.Score}";
        }
    }
}