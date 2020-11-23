using Asteroids.Core;
using Asteroids.Core.Messaging;
using Asteroids.Core.Utils;
using Asteroids.Systems.Game.Components;
using Asteroids.Systems.Game.Components.Entities;
using Asteroids.Systems.Game.Components.Physics;
using Asteroids.Systems.Game.Components.Rendering;
using Asteroids.Systems.Game.Content;
using Asteroids.Systems.Game.Content.PolygonLoading;
using Asteroids.Systems.Game.Enums;
using Asteroids.Systems.Game.Messages;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpMath2;

namespace Asteroids.Systems.Game.MessageHandlers
{
    public class BulletSpawner : TypedMessageHandler<SpawnBullet>
    {
        public float BulletLifeTime { get; set; } = 0.5f;
        
        public float LaserLifeTime { get; set; } = 0.75f;

        public float Velocity { get; set; } = 500f;

        private readonly Texture2D _bulletTexture;
        private readonly Texture2D _laserTexture;
        private readonly PolygonLoader _polygonLoader;
        private readonly World _world;

        public BulletSpawner(ContentManager content, World world)
        {
            _bulletTexture = content.Load<Texture2D>("ship/bullet");
            _laserTexture = content.Load<Texture2D>("ship/bullet_long");
            
            _polygonLoader = new PolygonLoader(new JsonLoader(content));
            _world = world;
        }

        protected override void Handle(SpawnBullet message)
        {
            string polygonPath = message.BulletType switch
            {
                BulletType.Laser => "polygons/long_bullet",
                _ => "polygons/bullet"
            };
            
            PolygonRenderer renderer = _polygonLoader.Load(polygonPath);
            
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
                    Texture = message.BulletType switch
                    {
                        BulletType.Laser => _laserTexture,
                        _ => _bulletTexture
                    }
                })
                .Attach(renderer)
                .Attach(new Collider(new Polygon2(renderer.Vertices)))
                .Attach(new Lifetime(message.BulletType switch
                {
                    BulletType.Laser => LaserLifeTime,
                    _ => BulletLifeTime
                }))
                .Attach(new Bullet
                {
                    BulletType = message.BulletType
                });

            _world.Send(new PlaySound(message.BulletType switch
            {
                BulletType.Laser => "sfx/lasers/7",
                _ => "sfx/lasers/6"
            }));
        }
    }
}