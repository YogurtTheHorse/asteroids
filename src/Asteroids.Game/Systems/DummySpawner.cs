using Asteroids.Core;
using Asteroids.Core.Ecs.Systems;
using Asteroids.Systems.Game.Messages;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Asteroids.Systems.Game.Systems
{
    public class DummySpawner : AbstractSystem, IUpdateSystem
    {
        private readonly Texture2D _dummyTexture;

        public DummySpawner(World world, Texture2D dummyTexture) : base(world)
        {
            _dummyTexture = dummyTexture;
        }

        public void Update(GameTime delta)
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                World.Send(new SpawnDummy
                {
                    Position = new Vector2(Mouse.GetState().Position.X, Mouse.GetState().Position.Y),
                    Texture = _dummyTexture
                });
            }
        }
    }
}