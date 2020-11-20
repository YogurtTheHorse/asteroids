using Asteroids.Core;
using Asteroids.Core.Systems;
using Microsoft.Xna.Framework;

namespace Asteroids.Systems.Game
{
    public class DummySystem : EntityProcessingSystem<DummySystem.Dummy>
    {
        public class Dummy : Component { }

        public DummySystem(World world) : base(world) { }

        public override void Update(Entity entity, GameTime delta)
        {
            var transform = entity.Get<Transform>();

            transform.Rotation += (float)delta.ElapsedGameTime.TotalSeconds;
        }
    }
}