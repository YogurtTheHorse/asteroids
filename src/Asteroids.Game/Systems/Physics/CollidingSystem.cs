using System.Linq;
using Asteroids.Core;
using Asteroids.Core.Ecs;
using Asteroids.Core.Ecs.Systems;
using Asteroids.Systems.Game.Components;
using Asteroids.Systems.Game.Components.Physics;
using Asteroids.Systems.Game.Messages;
using Microsoft.Xna.Framework;
using SharpMath2;

namespace Asteroids.Systems.Game.Systems.Physics
{
    public class CollidingSystem : AbstractSystem, IUpdateSystem
    {
        public CollidingSystem(World world) : base(world)
        {
        }

        private void CheckCollision(Entity a, Entity b)
        {
            var colliderA = a.Get<Collider>();
            var transformA = a.Get<Transform>();
            var colliderB = b.Get<Collider>();
            var transformB = b.Get<Transform>();

            bool intersects = Polygon2.Intersects(
                colliderA.Shape,
                colliderB.Shape,
                transformA.Position,
                transformB.Position,
                new Rotation2(transformA.Rotation),
                new Rotation2(transformB.Rotation),
                true
            );

            if (intersects)
            {
                World.Send(new Collision(a, b));
            }
        }

        public void Update(GameTime gameTime)
        {
            var entities = World.Entities.With<Collider>().ToArray();

            for (var i = 0; i < entities.Length; i++)
            for (var j = 0; j < i; j++)
            {
                CheckCollision(entities[i], entities[j]);
            }
        }
    }
}