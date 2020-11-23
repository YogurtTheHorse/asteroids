using System;
using Asteroids.Core;
using Asteroids.Systems.Game.Components;
using Asteroids.Systems.Game.Content;
using Asteroids.Systems.Game.MessageHandlers;
using Asteroids.Systems.Game.Messages;
using Asteroids.Systems.Game.PolygonLoading;
using Asteroids.Systems.Game.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Asteroids
{
    /// <summary>
    /// Core game logics
    /// </summary>
    public class AsteroidsGame : Game
    {
        private readonly PolygonLoader _polygonLoader;
        private readonly JsonLoader _jsonLoader;
        private readonly GraphicsDeviceManager _graphics;
        private readonly World _world;
        private RenderTarget2D _nativeRenderTarget = null!;
        private SpriteBatch _spriteBatch = null!;

        private int _scale = 2;
        private bool _fullscreen = false;

        public AsteroidsGame()
        {
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics = new GraphicsDeviceManager(this);
            _jsonLoader = new JsonLoader(Content.RootDirectory);
            _polygonLoader = new PolygonLoader(_jsonLoader);
            _world = new World(480, 360);
        }

        protected override void Initialize()
        {
            base.Initialize();

            SpriteFont spriteFont = Content.Load<SpriteFont>("fonts/Eneminds Bold");
            SpriteFont vectorFont = Content.Load<SpriteFont>("fonts/VectorBattle");

            SetScale(_scale);
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _nativeRenderTarget = new RenderTarget2D(GraphicsDevice, _world.Width, _world.Height);

            _world
                .Register(new BackgroundSystem(
                    GraphicsDevice,
                    Content.Load<Texture2D>("bg/bg"),
                    Content.Load<Texture2D>("bg/stars"),
                    _world
                ))
                .Register(new SpriteRendererSystem(GraphicsDevice, _world))
                .Register(new PolygonRendererSystem(GraphicsDevice, _world))
                .Register(new RigidbodySystem(_world))
                .Register(new PlayerSystem(_world))
                .Register(new KeepInScreenSystem(GraphicsDevice, _world))
                .Register(new KeyboardSystem(_world))
                .Register(new LifeTimeSystem(_world))
                .Register(new DebugRendererSystem(GraphicsDevice, _world))
                .Register(new PlayerAnimationSystem(Content, _world))
                .Register(new EnemiesSpawnerSystem(_world))
                .Register(new GameManagementSystem(Content, _polygonLoader, _world))
                .Register(new ExplosionsSpawner(_jsonLoader, _world))
                .Register(new AnimationsSystem(Content, _world))
                .Register(new FontRendererSystem(spriteFont, GraphicsDevice, _world))
                .Register(new CollidingSystem(_world));

            _world
                .Register(new AsteroidSpawner(
                    GraphicsDevice,
                    Content,
                    _polygonLoader,
                    _world
                ))
                .Register(new BulletSpawner(
                    Content.Load<Texture2D>("ship/bullet"),
                    _polygonLoader,
                    _world
                ))
                .Register(new CollisionHandler(_world))
                .Register(new SoundManager(Content))
                .Register(new RendererSystemSwitcher(spriteFont, vectorFont, _world));

            _world.Register<KeyPressed>(keyPressed =>
            {
                switch (keyPressed.Key)
                {
                    case Keys.N when _scale > 1:
                        SetScale(_scale - 1);
                        break;

                    case Keys.M:
                        SetScale(_scale + 1);
                        break;

                    case Keys.F:
                        SetFullscreen(!_fullscreen);
                        break;
                }
            });
        }

        private void SetScale(int scale)
        {
            _scale = scale;
            _graphics.PreferredBackBufferWidth = _world.Width * scale;
            _graphics.PreferredBackBufferHeight = _world.Height * scale;
            _graphics.IsFullScreen = false;
            _graphics.ApplyChanges();
        }

        private void SetFullscreen(bool fullscreen)
        {
            _fullscreen = fullscreen;
            if (fullscreen)
            {
                _scale = Math.Min(
                    GraphicsDevice.DisplayMode.Width / _world.Width,
                    GraphicsDevice.DisplayMode.Height / _world.Height
                );

                _graphics.PreferredBackBufferWidth = GraphicsDevice.DisplayMode.Width;
                _graphics.PreferredBackBufferHeight = GraphicsDevice.DisplayMode.Height;
                _graphics.IsFullScreen = true;
                _graphics.ApplyChanges();
            }
            else
            {
                SetScale(_scale);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            _world.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.SetRenderTarget(_nativeRenderTarget);
            GraphicsDevice.Clear(Color.Black);

            _world.Draw(gameTime);

            GraphicsDevice.SetRenderTarget(null);

            _spriteBatch.Begin(
                SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                DepthStencilState.None,
                RasterizerState.CullCounterClockwise
            );

            int width = _world.Width * _scale;
            int height = _world.Height * _scale;

            _spriteBatch.Draw(
                _nativeRenderTarget,
                new Rectangle(
                    (GraphicsDevice.Viewport.Width - width) / 2,
                    (GraphicsDevice.Viewport.Height - height) / 2,
                    width,
                    height
                ),
                Color.White
            );
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}