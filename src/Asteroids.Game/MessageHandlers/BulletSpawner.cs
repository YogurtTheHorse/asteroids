using Asteroids.Core;
using Asteroids.Core.Messaging;
using Asteroids.Core.Utils;
using Asteroids.Systems.Game.Components;
using Asteroids.Systems.Game.Components.Entities;
using Asteroids.Systems.Game.Components.Physics;
using Asteroids.Systems.Game.Components.Rendering;
using Asteroids.Systems.Game.Content.PolygonLoading;
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
        private readonly PolygonLoader _polygonLoader;
        private readonly World _world;

        public BulletSpawner(Texture2D bulletTexture, PolygonLoader polygonLoader, World world)
        {
            _bulletTexture = bulletTexture;
            _polygonLoader = polygonLoader;
            _world = world;
        }

        protected override void Handle(SpawnBullet message)
        {
            PolygonRenderer renderer = _polygonLoader.Load("polygons/bullet");
            
            _world
                .CreateEntity()
                .Attach(new Transform
                {
                    Position = message.Position,
                    Rotation = message.Rotation
                })
                .Attach(new Rigidbody
                {
                    Velocity = new Vector2(0, -Velocity).Rotate(message.Rotation)
                })
                .Attach(new SpriteRenderer
                {
                    Texture = _bulletTexture
                })
                .Attach(renderer)
                .Attach(new Collider(new Polygon2(renderer.Vertices)))
                .Attach(new Lifetime(BulletLifeTime))
                .Attach(new Bullet());

            _world.Send(new PlaySound("sfx/lasers/6"));
        }
    }
}