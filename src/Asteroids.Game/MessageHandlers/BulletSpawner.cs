using Asteroids.Core;
using Asteroids.Core.Messaging;
using Asteroids.Core.Utils;
using Asteroids.Systems.Game.Components;
using Asteroids.Systems.Game.Messages;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpMath2;

namespace Asteroids.Systems.Game.MessageHandlers
{
    public class BulletSpawner : TypedMessageHandler<SpawnBullet>
    {
        public float BulletLifeTime { get; set; } = 0.75f;

        public float Velocity { get; set; } = 500f;

        private readonly Texture2D _bulletTexture;
        private readonly World _world;

        public BulletSpawner(Texture2D bulletTexture, World world)
        {
            _bulletTexture = bulletTexture;
            _world = world;
        }

        protected override void Handle(SpawnBullet message)
        {
            _world
                .CreateEntity()
                .Attach(new Transform
                {
                    Position = message.Position,
                    Rotation = message.Rotation
                })
                .Attach(new Rigidbody
                {
                    Velocity = new Vector2(Velocity, 0).Rotate(message.Rotation)
                })
                .Attach(new SpriteRenderer
                {
                    Texture = _bulletTexture
                })
                .Attach(new PolygonRenderer
                {
                    Vertices = new[] {Vector2.Zero, new Vector2(-5f, 0)}
                })
                .Attach(new Collider(new Polygon2(new Vector2[]
                {
                    new(-1, 0),
                    new(0, 1f),
                    new(1f, 0),
                    new(0, -1),
                })))
                .Attach(new Lifetime(BulletLifeTime))
                .Attach(new Bullet());

            _world.Send(new PlaySound("sfx/lasers/6"));
        }
    }
}