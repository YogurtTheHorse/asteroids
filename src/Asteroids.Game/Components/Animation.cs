using System;
using System.Linq;
using Asteroids.Core.Ecs;

namespace Asteroids.Systems.Game.Components
{
    public class Animation : Component
    {
        public bool Loop { get; set; } = false;

        public Clip[] Clips { get; set; } = Array.Empty<Clip>();

        public float Time { get; set; } = 0f;

        public float Duration => Clips.Sum(c => c.Duration);

        public Animation() => Require<SpriteRenderer>();
        
        public struct Clip
        {
            public string Name { get; set; }
            
            public float Duration { get; set; }
        }
    }
}