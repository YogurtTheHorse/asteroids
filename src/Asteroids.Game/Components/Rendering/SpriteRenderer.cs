using System.Drawing;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace Asteroids.Systems.Game.Components.Rendering
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

        public float LayerDepth { get; set; }

        public SpriteEffects Effects { get; set; } = SpriteEffects.None;

        public override RectangleF GetRect()
        {
            if (Texture is null)
            {
                return RectangleF.Empty;
            }

            var width = TextureRectangle?.Width ?? Texture.Width;
            var height = TextureRectangle?.Height ?? Texture.Height;

            return new RectangleF(
                (Origin.X - 0.5f) * width - width / 2f,
                (Origin.Y - 0.5f) * height - height / 2f,
                width,
                height
            );
        }
    }
}