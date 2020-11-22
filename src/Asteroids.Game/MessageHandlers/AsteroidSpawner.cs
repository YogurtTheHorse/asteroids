using System;
using System.Linq;
using Asteroids.Core;
using Asteroids.Core.Ecs;
using Asteroids.Core.Messaging;
using Asteroids.Core.Utils;
using Asteroids.PolygonLoading;
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
        private readonly PolygonLoader _polygonLoader;
        private readonly World _world;

        public AsteroidSpawner(GraphicsDevice graphicsDevice, Texture2D asteroidTexture, PolygonLoader polygonLoader, World world)
        {
            _graphicsDevice = graphicsDevice;
            _asteroidTexture = asteroidTexture;
            _polygonLoader = polygonLoader;
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

            PolygonRenderer polygon = _polygonLoader.Load("polygons/asteroids/big");
            
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
                .Attach(polygon)
                .Attach(new Collider(new Polygon2(polygon.Vertices.Select(v => v * scale).ToArray())))
                .Attach(new Asteroid
                {
                    Size = message.Size
                });
        }
    }
}