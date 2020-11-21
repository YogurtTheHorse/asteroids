using Asteroids.Core.Ecs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Asteroids.Systems.Game.Components
{
    /// <summary>
    /// Describes sprite to draw. All comments about properties can be found at <see cref="SpriteBatch.Draw"/>.
    /// </summary>
    public class SpriteRenderer : Renderer
    {
        public Texture2D? Texture { get; set; }
        
        public Rectangle? TextureRectangle { get; set; }

        /// <summary>
        /// By default centered.
        /// </summary>
        public Vector2 Origin { get; set; } = new(0.5f);

        public float Scale { get; set; } = 1f;
        
        public float LayerDepth { get; set; }

        public SpriteEffects Effects { get; set; } = SpriteEffects.None;
    }
}