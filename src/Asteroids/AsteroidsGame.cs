using Asteroids.Core;
using Asteroids.Systems.Game.Components;
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
                .Register(new KeepInScreenSystem(GraphicsDevice, _world));

            _world
                .CreateEntity()
                .Attach(new Transform())
                .Attach(new Rigidbody())
                .Attach(new SpriteRenderer
                {
                    Texture = Content.Load<Texture2D>("ship")
                })
                .Attach(new Player());
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

            base.Draw(gameTime);
        }
    }
}