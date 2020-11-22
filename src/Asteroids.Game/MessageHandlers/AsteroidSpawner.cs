using System;
using System.Linq;
using Asteroids.Core;
using Asteroids.Core.Ecs;
using Asteroids.Core.Messaging;
using Asteroids.Core.Utils;
using Asteroids.Systems.Game.Components;
using Asteroids.Systems.Game.Messages;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpMath2;

namespace Asteroids.Systems.Game.MessageHandlers
{
    public class AsteroidSpawner : TypedMessageHandler<SpawnAsteroid>
    {
        public float MaxVelocity { get; set; } = 200f;
        
        private readonly GraphicsDevice _graphicsDevice;
        private readonly Texture2D _asteroidTexture;
        private readonly World _world;

        public AsteroidSpawner(GraphicsDevice graphicsDevice, Texture2D asteroidTexture, World world)
        {
            _graphicsDevice = graphicsDevice;
            _asteroidTexture = asteroidTexture;
            _world = world;
        }
        
        protected override void Handle(SpawnAsteroid message)
        {
            Vector2 position = message.Position ?? new Vector2(
                (float) _world.Random.NextDouble() * _graphicsDevice.Viewport.Width,
                (float) _world.Random.NextDouble() * _graphicsDevice.Viewport.Height
            );
            float directionAngle = (float) (_world.Random.NextDouble() - 0.5d) * 4 * MathF.PI;
            float startingVelocity = MaxVelocity / message.Size;
            float scale = message.Size / 8f;
            
            // TODO: Check is asteroid intersect ship.

            var vertices = new[]
            {
                new Vector2(-127, -7),
                new Vector2(-92, -51),
                new Vector2(-72, -88),
                new Vector2(-61, -92),
                new Vector2(-41, -107),
                new Vector2(22, -107),
                new Vector2(39, -102),
                new Vector2(70, -74),
                new Vector2(94, -76),
                new Vector2(125, -12),
                new Vector2(126, 9),
                new Vector2(90, 56),
                new Vector2(80, 62),
                new Vector2(64, 90),
                new Vector2(39, 101),
                new Vector2(25, 109),
                new Vector2(-15, 101),
                new Vector2(-97, 60),
                new Vector2(-127, 22)
            };
            
            _world
                .CreateEntity()
                .Attach(new Transform
                {
                    Position = position,
                    Scale = scale
                })
                .Attach(new Rigidbody
                {
                    Velocity = new Vector2(startingVelocity, 0).Rotate(directionAngle),
                    AngularVelocity = MathF.PI
                })
                .Attach(new SpriteRenderer
                {
                    Texture = _asteroidTexture
                })
                .Attach(new PolygonRenderer
                {
                    Vertices = vertices,
                    Loop = true
                })
                .Attach(new Collider(new Polygon2(vertices.Select(v => v * scale).ToArray())))
                .Attach(new Asteroid
                {
                    Size = message.Size
                });
        }
    }
}