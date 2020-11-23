using System;
using Asteroids.Core;
using Asteroids.Core.Ecs;
using Asteroids.Core.Ecs.Systems;
using Asteroids.Systems.Game.Components;
using Asteroids.Systems.Game.Components.Rendering;
using Asteroids.Systems.Game.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids.Systems.Game.Systems.Rendering
{
    public class FontRendererSystem : WorldSystem, IDrawSystem
    {
        public SpriteFont Font { get; set; }
        
        private readonly SpriteBatch _spriteBatch;

        public FontRendererSystem(SpriteFont font, GraphicsDevice graphicsDevice, World world) : base(world)
        {
            Font = font;
            _spriteBatch = new SpriteBatch(graphicsDevice);
        }

        public void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            
            World
                .Entities
                .With<LabelComponent>()
                .ForEach(DrawString);
            
            _spriteBatch.End();
        }

        private void DrawString(Entity entity)
        {
            var label = entity.Get<LabelComponent>();
            Vector2 size = Font.MeasureString(label.Label);

            Vector2 offset =
                label.Align switch
                {
                    Align.Center => -size / 2,
                    Align.TopLeft => Vector2.Zero,
                    _ => throw new InvalidOperationException($"Got unsupported align: {label.Align}")
                };
            
            _spriteBatch.DrawString(Font, label.Label, entity.Get<Transform>().Position + offset, label.Color);
        }
    }
}