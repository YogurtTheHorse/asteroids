using System.Linq;
using Asteroids.Core;
using Asteroids.Core.Ecs;
using Asteroids.Core.Ecs.Systems;
using Asteroids.Systems.Game.Components.Entities;
using Asteroids.Systems.Game.Enums;
using Asteroids.Systems.Game.MessageHandlers;
using Asteroids.Systems.Game.Messages;
using Microsoft.Xna.Framework;

namespace Asteroids.Systems.Game.Systems.GameLogic
{
    public class EnemiesSpawnerSystem : WorldSystem, IUpdateSystem
    {
        private float _timeToSpawn = 0f;

        public int MinEnemiesToSpawn { get; set; } = 3;

        public int MaxEnemiesToSpawn { get; set; } = 4;

        public float MinTimeToSpawn { get; set; } = 15f;

        public float MaxTimeToSpawn { get; set; } = 25f;

        public float UfoProbability { get; set; } = 0.3f;

        public EnemiesSpawnerSystem(World world) : base(world)
        {
        }

        public void Update(GameTime gameTime)
        {
            if (!World.Entities.With<Player>().Any()) return;

            if (_timeToSpawn <= 0 || World.Entities.With<Enemy>().Count() == 0)
            {
                int enemiesToSpawn = World.Random.Next(MinEnemiesToSpawn, MaxEnemiesToSpawn);
                for (int i = 0; i < enemiesToSpawn; i++)
                {
                    SpawnEnemy();
                }

                _timeToSpawn = MinTimeToSpawn + (float) World.Random.NextDouble() * (MaxTimeToSpawn - MinTimeToSpawn);
            }

            _timeToSpawn -= (float) gameTime.ElapsedGameTime.TotalSeconds;
        }

        private void SpawnEnemy()
        {
            EnemyType enemyType = World.Random.NextDouble() < UfoProbability
                ? EnemyType.Ufo
                : EnemyType.Asteroid;

            World.Send(new SpawnEnemy
            {
                EnemyType = enemyType,
                Size = EnemiesSpawnerHandler.MaxSize
            });
        }

        public void Restart()
        {
            _timeToSpawn = 0;
        }
    }
}