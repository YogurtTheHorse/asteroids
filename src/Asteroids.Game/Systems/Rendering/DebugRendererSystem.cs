﻿using Asteroids.Core;
using Asteroids.Core.Utils;
using Asteroids.Systems.Game.Components;
using Asteroids.Systems.Game.Components.Rendering;
using Asteroids.Systems.Game.Messages;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Primitives2D;

namespace Asteroids.Systems.Game.Systems.Rendering
{
    public class DebugRendererSystem : RendererSystemBase<Renderer>
    {
        public DebugRendererSystem(GraphicsDevice graphicsDevice, World world) : base(graphicsDevice, world)
        {
            Enabled = false;
            world.Register<KeyPressed>(k =>
            {
                if (k.Key != Keys.F5) return;

                Enabled = !Enabled;
            });
        }

        protected override void DrawAt(Transform transform, Renderer renderer)
        {
            Vector2[] corners = GetBoundariesCorners(transform, renderer.GetRect());
            (float minX, float minY, float maxX, float maxY) = corners.GetBoundaries();

            SpriteBatch.DrawRectangle(
                new Rectangle((int)minX, (int)minY, (int)(maxX - minX), (int)(maxY - minY)),
                Color.Yellow
            );

            for (var i = 1; i <= corners.Length; i++)
            {
                SpriteBatch.DrawLine(
                    corners[i - 1],
                    corners[i % corners.Length],
                    Color.Green
                );
            }

            SpriteBatch.DrawLine(
                transform.Position - new Vector2(5),
                transform.Position + new Vector2(5),
                Color.Yellow
            );

            SpriteBatch.DrawLine(
                transform.Position - new Vector2(5, -5),
                transform.Position + new Vector2(5, -5),
                Color.Yellow
            );
        }
    }
}