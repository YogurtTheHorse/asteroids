using Asteroids.Core;
using Asteroids.Systems;
using Asteroids.Systems.Game;
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
                .Register(new DummySystem(_world))
                .Register(new DummySpawner(_world, Content.Load<Texture2D>("asteroid")));
        }

        protected override void Update(GameTime gameTime)
        {
            _world.Update(gameTime);
            
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            _world.Draw(gameTime);

            base.Draw(gameTime);
        }
    }
}