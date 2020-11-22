using System;
using System.Linq;
using Asteroids.Core;
using Asteroids.Core.Ecs;
using Asteroids.Core.Ecs.Systems;
using Asteroids.Systems.Game.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids.Systems.Game.Systems
{
    public class BackgroundSystem : AbstractSystem, IDrawSystem
    {
        public float ParallaxMaxShift { get; set; } = 5f;

        private readonly GraphicsDevice _graphicsDevice;
        private readonly Texture2D _firstLayer;
        private readonly Texture2D _secondLayer;
        private readonly SpriteBatch _spriteBatch;
        private Vector2 _oldPlayerPosition = Vector2.Zero;

        public BackgroundSystem(GraphicsDevice graphicsDevice, Texture2D firstLayer, Texture2D secondLayer, World world) : base(world)
        {
            _graphicsDevice = graphicsDevice;
            _firstLayer = firstLayer;
            _secondLayer = secondLayer;
            _spriteBatch = new SpriteBatch(graphicsDevice);
        }
        
        public void Draw(GameTime gameTime)
        {
            int width = World.Width;
            int height = World.Height;
            
            Entity? player = World.Entities.With<Player>().FirstOrDefault();
            Vector2 playerPosition = player?.Get<Transform>().Position ?? _oldPlayerPosition;
            _oldPlayerPosition = playerPosition;
            Vector2 playerToCenter = playerPosition - new Vector2(width / 2f, height / 2f);
            Vector2 absPlayerToCenter = new Vector2(MathF.Abs(playerToCenter.X), MathF.Abs(playerToCenter.Y)) / new Vector2(width, height);
            Vector2 parallaxShift = new Vector2(MathF.Cos(absPlayerToCenter.X), MathF.Sin(absPlayerToCenter.Y)) * ParallaxMaxShift;
            
            _spriteBatch.Begin();
            
            for (var i = 0; i < width; i += _firstLayer.Width)
            for (var j = 0; j < height; j += _firstLayer.Height)
            {
                _spriteBatch.Draw(
                    _firstLayer,
                    new Vector2(i, j),
                    null,
                    Color.White,
                    0,
                    Vector2.Zero,
                    1f,
                    SpriteEffects.None,
                    0f
                );
                _spriteBatch.Draw(
                    _secondLayer,
                    new Vector2(i, j) - parallaxShift,
                    null,
                    Color.White,
                    0,
                    Vector2.Zero,
                    1f,
                    SpriteEffects.None,
                    1f
                );
            }
            _spriteBatch.End();
        }
    }
}