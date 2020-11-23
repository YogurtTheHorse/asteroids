using System;
using System.Linq;
using Asteroids.Core;
using Asteroids.Core.Ecs;
using Asteroids.Core.Messaging;
using Asteroids.Core.Utils;
using Asteroids.Systems.Game.Components;
using Asteroids.Systems.Game.Components.Entities;
using Asteroids.Systems.Game.Components.Physics;
using Asteroids.Systems.Game.Components.Rendering;
using Asteroids.Systems.Game.Content.PolygonLoading;
using Asteroids.Systems.Game.Messages;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpMath2;

namespace Asteroids.Systems.Game.MessageHandlers
{
    public class AsteroidSpawner : TypedMessageHandler<SpawnAsteroid>
    {
        public const int MaxSize = 4; 
     
        public float MaxVelocity { get; set; } = 200f;
        public float SafeDistance { get; set; } = 150f;
        
        private readonly GraphicsDevice _graphicsDevice;
        private readonly ContentManager _content;
        private readonly PolygonLoader _polygonLoader;
        private readonly World _world;

        public AsteroidSpawner(GraphicsDevice graphicsDevice, ContentManager content, PolygonLoader polygonLoader, World world)
        {
            _graphicsDevice = graphicsDevice;
            _content = content;
            _polygonLoader = polygonLoader;
            _world = world;
        }
        
        protected override void Handle(SpawnAsteroid message)
        {
            Vector2 position = message.Position ?? GeneratePosition();
            float directionAngle = (float) (_world.Random.NextDouble() - 0.5d) * 4 * MathF.PI;
            float startingVelocity = MaxVelocity / message.Size;
            
            // TODO: Check is asteroid intersect ship.
            string name = $"planetoids/planetoid_{MaxSize - message.Size + 1}";

            PolygonRenderer polygon = _polygonLoader.Load($"polygons/{name}");
            
            _world
                .CreateEntity()
                .Attach(new Transform
                {
                    Position = position
                })
                .Attach(new Rigidbody
                {
                    Velocity = new Vector2(startingVelocity, 0).Rotate(directionAngle),
                    AngularVelocity = MathF.PI
                })
                .Attach(new SpriteRenderer
                {
                    Texture = _content.Load<Texture2D>(name)
                })
                .Attach(polygon)
                .Attach(new Collider(new Polygon2(polygon.Vertices)))
                .Attach(new Asteroid
                {
                    Size = message.Size
                });
        }

        private Vector2 GeneratePosition()
        {
            Entity? player = _world.Entities.With<Player>().FirstOrDefault();
            var transform = player?.Get<Transform>();
            
            Vector2 position;

            do
            {
                position = new Vector2(
                    (float) _world.Random.NextDouble() * _world.Width,
                    (float) _world.Random.NextDouble() * _world.Height
                );
            } while (transform != null && (transform.Position - position).Length() < SafeDistance);

            return position;
        }
    }
}