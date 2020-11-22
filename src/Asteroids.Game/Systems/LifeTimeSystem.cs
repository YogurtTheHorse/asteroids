using Asteroids.Core;
using Asteroids.Core.Ecs;
using Asteroids.Core.Ecs.Systems;
using Asteroids.Systems.Game.Components;
using Microsoft.Xna.Framework;

namespace Asteroids.Systems.Game.Systems
{
    public class LifeTimeSystem : EntityProcessingSystem<Lifetime>

    {
        public LifeTimeSystem(World world) : base(world) {}

        public override void Update(Entity entity, GameTime delta)
        {
            var lifetime = entity.Get<Lifetime>();
            lifetime.TimeToLive -= (float) delta.ElapsedGameTime.TotalSeconds;

            if (lifetime.TimeToLive < 0)
            {
                World.Destroy(entity.Id);
            }
        }
    }
}