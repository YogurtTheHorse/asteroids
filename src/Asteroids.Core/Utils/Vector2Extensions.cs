using System;
using Microsoft.Xna.Framework;

namespace Asteroids.Core
{
    public static class Vector2Extensions
    {
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