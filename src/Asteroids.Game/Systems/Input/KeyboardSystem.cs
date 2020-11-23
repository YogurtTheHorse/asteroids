using System;
using Asteroids.Core;
using Asteroids.Core.Ecs.Systems;
using Asteroids.Systems.Game.Messages;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Asteroids.Systems.Game.Systems.Input
{
    public class KeyboardSystem : AbstractSystem, IUpdateSystem
    {
        private KeyboardState _oldKeyboardState; 
        
        public KeyboardSystem(World world) : base(world)
        {
        }

        public void Update(GameTime _)
        {
            KeyboardState newState = Keyboard.GetState();

            foreach (Keys key in Enum.GetValues(typeof(Keys)))
            {
                if (newState.IsKeyDown(key) && _oldKeyboardState.IsKeyUp(key))
                {
                    World.Send(new KeyPressed
                    {
                        Key = key
                    });
                }
            }

            _oldKeyboardState = newState;
        }
    }
}