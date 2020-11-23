using Asteroids.Core.Ecs;
using Asteroids.Systems.Game.Components.Rendering;
using Asteroids.Systems.Game.Enums;

namespace Asteroids.Systems.Game.Components.Entities
{
    public class Bullet : Component
    {
        public BulletType BulletType { get; set; } = BulletType.Regular;
        
        public Bullet() => this
            .Require<Transform>()
            .Require<Renderer>()
            .Require<Lifetime>();
    }
}