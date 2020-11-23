using System;
using Asteroids.Core;
using Asteroids.Core.Messaging;
using Asteroids.Core.Utils;
using Asteroids.Systems.Game.Components;
using Asteroids.Systems.Game.Components.Physics;
using Asteroids.Systems.Game.Components.Rendering;
using Asteroids.Systems.Game.Content;
using Asteroids.Systems.Game.Messages;
using Microsoft.Xna.Framework;

namespace Asteroids.Systems.Game.MessageHandlers
{
    public class ExplosionsSpawner : TypedMessageHandler<Explosion>
    {
        public float ParticlesSpeed { get; set; } = 40;

        private readonly JsonLoader _jsonLoader;
        private readonly World _world;

        public ExplosionsSpawner(JsonLoader jsonLoader, World world)
        {
            _jsonLoader = jsonLoader;
            _world = world;
        }
        
        protected override void Handle(Explosion message)
        {
            _world.Send(new PlaySound("sfx/explosions/3"));

            var animation = _jsonLoader.Load<Animation>("animations/explosion");

            _world
                .CreateEntity()
                .Attach(new Transform
                {
                    Position = message.Position,
                    Scale = message.Size / 4f
                })
                .Attach(new Lifetime(animation.Duration))
                .Attach(new SpriteRenderer())
                .Attach(animation);
            
            for (var i = 0; i < 3 * message.Size; i++)
            {
                _world
                    .CreateEntity()
                    .Attach(new Transform()
                    {
                        Position = message.Position
                    })
                    .Attach(new PolygonRenderer
                    {
                        Vertices = new[] {Vector2.Zero, Vector2.UnitX}
                    })
                    .Attach(new Rigidbody
                    {
                        Velocity = Vector2.UnitX.Rotate((float) _world.Random.NextDouble() * MathF.PI * 2) *
                                   ParticlesSpeed
                    })
                    .Attach(new Lifetime(1f));
            }
        }
    }
}