using Asteroids.Core;
using Asteroids.Systems.Game.Components;
using Asteroids.Systems.Game.Components.Rendering;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids.Systems.Game.Systems.Rendering
{
    public class SpriteRendererSystem : RendererSystemBase<SpriteRenderer>
    {
        public SpriteRendererSystem(GraphicsDevice graphicsDevice, World world) : base(graphicsDevice, world) { }

        protected override void DrawAt(Transform transform, SpriteRenderer sprite)
        {
            if (sprite.Texture is null)
            {
                return;
            }
                    
            Vector2 origin = sprite.Origin * new Vector2(sprite.Texture.Width, sprite.Texture.Height); 
                    
            SpriteBatch.Draw(
                sprite.Texture,
                transform.Position,
                sprite.TextureRectangle,
                sprite.Color,
                transform.Rotation,
                origin,
                transform.Scale,
                sprite.Effects,
                sprite.LayerDepth
            );
        }
    }
}