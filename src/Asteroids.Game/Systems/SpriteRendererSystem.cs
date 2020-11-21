﻿using Asteroids.Core;
using Asteroids.Core.Ecs;
using Asteroids.Core.Ecs.Systems;
using Asteroids.Systems.Game.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids.Systems.Game.Systems
{
    public class SpriteRendererSystem : AbstractSystem, IDrawSystem
    {
        private readonly SpriteBatch _spritesBatch;

        public SpriteRendererSystem(GraphicsDevice graphicsDevice, World world) : base(world)
        {
            _spritesBatch = new SpriteBatch(graphicsDevice);
        }

        public void Draw()
        {
            _spritesBatch.Begin();

            World.Entities
                .With<SpriteRenderer>()
                .ForEach(DrawEntity);

            _spritesBatch.End();

            void DrawEntity(Entity e)
            {
                var transform = e.Get<Transform>();
                var sprite = e.Get<SpriteRenderer>();

                if (sprite.Texture is null)
                {
                    return;
                }
                    
                Vector2 origin = sprite.Origin * new Vector2(sprite.Texture.Width, sprite.Texture.Height); 
                    
                _spritesBatch.Draw(
                    sprite.Texture,
                    transform.Position,
                    sprite.TextureRectangle,
                    sprite.Color,
                    transform.Rotation,
                    origin,
                    sprite.Scale,
                    sprite.Effects,
                    sprite.LayerDepth
                );
            }
        }
    }
}