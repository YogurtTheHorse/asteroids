using Asteroids.Core;
using Asteroids.Core.Messaging;
using Asteroids.Systems.Game.Enums;
using Asteroids.Systems.Game.Messages;
using Asteroids.Systems.Game.Systems;
using Microsoft.Xna.Framework.Input;

namespace Asteroids.Systems.Game.MessageHandlers
{
    public class RendererSystemSwitcher : TypedMessageHandler<KeyPressed>
    {
        private readonly SpriteRendererSystem _spriteSystem;
        private readonly PolygonRendererSystem _polygonalSystem;

        public RendererSystem EnabledSystem { get; set; } = RendererSystem.Polygonal;
        

        public RendererSystemSwitcher(World world)
        {
            _spriteSystem = world.Get<SpriteRendererSystem>();
            _polygonalSystem = world.Get<PolygonRendererSystem>();
            
            RefreshRenderers();
        }
        
        protected override void Handle(KeyPressed keyPressed)
        {
            if (keyPressed.Key != Keys.LeftControl) return;

            EnabledSystem = EnabledSystem == RendererSystem.Polygonal
                ? RendererSystem.Sprite
                : RendererSystem.Polygonal;

            RefreshRenderers();
        }

        private void RefreshRenderers()
        {
            var spriteEnabled = EnabledSystem == RendererSystem.Sprite;

            _spriteSystem.Enabled = spriteEnabled;
            _polygonalSystem.Enabled = !spriteEnabled;
        }
    }
}