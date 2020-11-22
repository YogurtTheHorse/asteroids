﻿using System;
using Asteroids.Core;
using Asteroids.Systems.Game.Components;
using Asteroids.Systems.Game.MessageHandlers;
using Asteroids.Systems.Game.Messages;
using Asteroids.Systems.Game.PolygonLoading;
using Asteroids.Systems.Game.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpMath2;

namespace Asteroids
{
    /// <summary>
    /// Core game logics
    /// </summary>
    public class AsteroidsGame : Game
    {
        private PolygonLoader _polygonLoader;
        private GraphicsDeviceManager _graphics;
        private readonly World _world;
        private RenderTarget2D _nativeRenderTarget;
        private SpriteBatch _spriteBatch;

        public AsteroidsGame()
        {
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            _graphics = new GraphicsDeviceManager(this);
            _polygonLoader = new PolygonLoader(Content.RootDirectory);
            _world = new World(640, 480);
        }

        protected override void Initialize()
        {
            base.Initialize();

            SpriteFont spriteFont = Content.Load<SpriteFont>("fonts/Eneminds Bold");
            SpriteFont vectorFont = Content.Load<SpriteFont>("fonts/VectorBattle");

            SetScale(2);
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
                .Register(new ExplosionsSpawner(_world))
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
        }

        private void SetScale(int scale)
        {
            _graphics.PreferredBackBufferWidth = _world.Width * scale;
            _graphics.PreferredBackBufferHeight = _world.Height * scale;
            _graphics.ApplyChanges();
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

            _spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise);
            _spriteBatch.Draw(
                _nativeRenderTarget,
                new Rectangle(0, 0, GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height),
                Color.White
            );
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}