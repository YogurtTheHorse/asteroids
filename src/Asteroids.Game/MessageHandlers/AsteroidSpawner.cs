using System;
using Asteroids.Core;
using Asteroids.Core.Messaging;
using Asteroids.Core.Utils;
using Asteroids.Systems.Game.Components;
using Asteroids.Systems.Game.Messages;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
            var x = (float) _world.Random.NextDouble() * _graphicsDevice.Viewport.Width;
            var y = (float) _world.Random.NextDouble() * _graphicsDevice.Viewport.Height;
            var directionAngle = (float) (_world.Random.NextDouble() - 0.5d) * 4 * MathF.PI;
            var startingVelocity = MaxVelocity / message.Size;
            
            // TODO: Check is asteroid intersect ship.
            
            _world
                .CreateEntity()
                .Attach(new Transform
                {
                    Position = new Vector2(x, y)
                })
                .Attach(new Rigidbody
                {
                    Velocity = new Vector2(startingVelocity, 0).Rotate(directionAngle),
                    AngularVelocity = MathF.PI
                })
                .Attach(new SpriteRenderer
                {
                    Texture = _asteroidTexture,
                    Scale = message.Size / 4f
                })
                .Attach(new Asteroid());
        }
    }
}