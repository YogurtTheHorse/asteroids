using System;
using Microsoft.Xna.Framework;

namespace Asteroids.Core.Utils
{
    /// <summary>
    /// Extensions for Vector2.
    /// </summary>
    public static class Vector2Extensions
    {
        /// <summary>
        /// Rotates vector around (0, 0) on specific angle.
        /// </summary>
        /// <param name="v">Vector to rotate.</param>
        /// <param name="radians">Value of angle to rotate.</param>
        /// <returns>Rotated vector.</returns>
        public static Vector2 Rotate(this Vector2 v, float radians)
        {
            float sin = MathF.Sin(radians);
            float cos = MathF.Cos(radians);
         
            float tx = v.X;
            float ty = v.Y;
 
            return new Vector2(cos * tx - sin * ty, sin * tx + cos * ty);

        }
    }
}