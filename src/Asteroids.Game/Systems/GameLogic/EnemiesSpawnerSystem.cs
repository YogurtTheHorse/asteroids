using System.Linq;
using Asteroids.Core;
using Asteroids.Core.Ecs;
using Asteroids.Core.Ecs.Systems;
using Asteroids.Systems.Game.Components.Entities;
using Asteroids.Systems.Game.MessageHandlers;
using Asteroids.Systems.Game.Messages;
using Microsoft.Xna.Framework;

namespace Asteroids.Systems.Game.Systems.GameLogic
{
    public class EnemiesSpawnerSystem : AbstractSystem, IUpdateSystem
    {
        private float _timeToSpawn = 0f;

        public int MinEnemiesToSpawn { get; set; } = 2;

        public int MaxEnemiesToSpawn { get; set; } = 5;

        public float MinTimeToSpawn { get; set; } = 15f;
        
        public float MaxTimeToSpawn { get; set; } = 25f;
        
        public float UfoProbability { get; set; } = 0.1f;
        
        public EnemiesSpawnerSystem(World world) : base(world)
        {
        }

        public void Update(GameTime delta)
        {
            if (!World.Entities.With<Player>().Any()) return;
            
            if (_timeToSpawn <= 0)
            {
                int enemiesToSpawn = World.Random.Next(MinEnemiesToSpawn, MaxEnemiesToSpawn);
                for (int i = 0; i < enemiesToSpawn; i++)
                {
                    SpawnEnemy();
                }

                _timeToSpawn = MinTimeToSpawn + (float) World.Random.NextDouble() * (MaxTimeToSpawn - MinTimeToSpawn);
            }

            _timeToSpawn -= (float)delta.ElapsedGameTime.TotalSeconds;
        }

        private void SpawnEnemy()
        {
            World.Send(new SpawnAsteroid
            {
                Size = AsteroidSpawner.MaxSize
            });
        }

        public void Restart()
        {
            _timeToSpawn = 0;
        }
    }
}