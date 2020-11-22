using Asteroids.Core.Messaging;
using Asteroids.Systems.Game.Messages;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;

namespace Asteroids.Systems.Game.MessageHandlers
{
    public class SoundManager : TypedMessageHandler<PlaySound>
    {
        private readonly ContentManager _contentManager;

        public SoundManager(ContentManager contentManager)
        {
            _contentManager = contentManager;
        }
        
        protected override void Handle(PlaySound message)
        {
            SoundEffectInstance soundInstance = _contentManager.Load<SoundEffect>(message.Sound).CreateInstance();
            soundInstance.Play();
        }
    }
}