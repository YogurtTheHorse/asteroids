using Asteroids.Core;
using Asteroids.Core.Ecs;
using Asteroids.Core.Ecs.Systems;
using Asteroids.Systems.Renders;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids.Systems.Game
{
    public class DummySystem : EntityProcessingSystem<DummySystem.Dummy>
    {
        public class Dummy : Component
        {
            public Dummy() => this
                .Require<Transform>()
                .Require<SpriteRenderer>();
        }

        public DummySystem(World world) : base(world)
        {
            world.Register<SpawnDummy>(d => CreateDummy(d.Position, d.Texture));
        }

        public override void Update(Entity entity, GameTime delta)
        {
            var transform = entity.Get<Transform>();

            transform.Rotation += (float)delta.ElapsedGameTime.TotalSeconds;
        }

        private void CreateDummy(Vector2 position, Texture2D texture) => World
            .CreateEntity()
            .Attach(new Transform
            {
                Position = position
            })
            .Attach(new SpriteRenderer
            {
                Texture = texture,
                Scale = 0.1f
            })
            .Attach(new Dummy());
    }
}