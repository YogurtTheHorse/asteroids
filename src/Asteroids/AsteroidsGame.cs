using System;
using Asteroids.Core;
using Asteroids.Systems.Game.Components;
using Asteroids.Systems.Game.MessageHandlers;
using Asteroids.Systems.Game.Messages;
using Asteroids.Systems.Game.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids
{
    /// <summary>
    /// Core game logics
    /// </summary>
    public class AsteroidsGame : Game
    {
        private GraphicsDeviceManager _graphics;
        private readonly World _world;

        public AsteroidsGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            _world = new World();

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();

            _world
                .Register(new SpriteRendererSystem(GraphicsDevice, _world))
                .Register(new PolygonRendererSystem(GraphicsDevice, _world))
                .Register(new RigidbodySystem(_world))
                .Register(new PlayerSystem(_world))
                .Register(new KeepInScreenSystem(GraphicsDevice, _world))
                .Register(new KeyboardSystem(_world))
                .Register(new LifeTimeSystem(_world))
                .Register(new DebugRendererSystem(GraphicsDevice, _world));

            _world
                .Register(new AsteroidSpawner(
                    GraphicsDevice,
                    Content.Load<Texture2D>("asteroid"),
                    _world
                ))
                .Register(new BulletSpawner(Content.Load<Texture2D>("laser"), _world))
                .Register(new RendererSystemSwitcher(_world));

            _world
                .CreateEntity()
                .Attach(new Transform())
                .Attach(new Rigidbody())
                .Attach(new SpriteRenderer
                {
                    Texture = Content.Load<Texture2D>("ship")
                })
                .Attach(new PolygonRenderer()
                {
                    Vertices = new []
                    {
                        new Vector2(25f, 0),
                        new Vector2(-5f, -10f),
                        new Vector2(-5f, 10f),
                    },
                    Loop = true
                })
                .Attach(new Player());
            
            _world.Send(new SpawnAsteroid
            {
                Size = _world.Random.Next(1, 4)
            });
        }

        protected override void Update(GameTime gameTime)
        {
            _world.Update(gameTime);
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            
            _world.Draw(gameTime);
            
            Console.WriteLine($"FPS: {1 / gameTime.ElapsedGameTime.TotalSeconds}");

            base.Draw(gameTime);
        }
    }
}