using System.Linq;
using Asteroids.Core;
using Asteroids.Core.Ecs;
using Asteroids.Core.Ecs.Systems;
using Asteroids.Systems.Game.Components;
using Asteroids.Systems.Game.Components.Entities;
using Asteroids.Systems.Game.Components.Physics;
using Asteroids.Systems.Game.Components.Rendering;
using Asteroids.Systems.Game.Content.PolygonLoading;
using Asteroids.Systems.Game.Enums;
using Asteroids.Systems.Game.Messages;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpMath2;

namespace Asteroids.Systems.Game.Systems.GameLogic
{
    public class GameManagementSystem : EntityProcessingSystem<ScoreComponent>
    {
        public int Score { get; set; }
        public float LaserRechargeTime { get; set; } = 7f;

        private bool _gameStarted = false;
        private float _timeToGiveLaser = 0f;

        private readonly PolygonLoader _polygonLoader;

        public GameManagementSystem(ContentManager content, PolygonLoader polygonLoader, World world) : base(world)
        {
            _polygonLoader = polygonLoader;
            
            World.Register<KeyPressed>(Handle);

            Start();
        }

        private void Handle(KeyPressed message)
        {
            if (message.Key != Keys.R) return;
            
            World.Entities.ForEach(World.Destroy);
            World.Get<EnemiesSpawnerSystem>().Restart();
            
            Start();
        }

        public void Start()
        {
            Score = 0;
            World.Entities.With<LabelComponent>().ForEach(World.Destroy);

            PolygonRenderer polygon = _polygonLoader.Load("polygons/ship");
            _timeToGiveLaser = LaserRechargeTime;

            World
                .CreateEntity()
                .Attach(new Transform
                {
                    Position = new Vector2(World.Width / 2f, World.Height / 2f)
                })
                .Attach(new Rigidbody())
                .Attach(new SpriteRenderer())
                .Attach(polygon)
                .Attach(new Collider(new Polygon2(polygon.Vertices)))
                .Attach(new Player());

            World
                .CreateEntity()
                .Attach(new Transform
                {
                    Position = new Vector2(10f)
                })
                .Attach(new LabelComponent())
                .Attach(new ScoreComponent());

            _gameStarted = true;
        }

        public override void Update(GameTime delta)
        {
            base.Update(delta);

            Entity? playerEntity = World.Entities.With<Player>().FirstOrDefault();
            _timeToGiveLaser -= (float) delta.ElapsedGameTime.TotalSeconds;

            if (playerEntity is null && _gameStarted)
            {
                StopGame();
            }
            else if (playerEntity != null && _timeToGiveLaser < 0 )
            {
                var player = playerEntity.Get<Player>();
                
                if (player.LasersCount < 3)
                {
                    _timeToGiveLaser = LaserRechargeTime;
                    player.LasersCount++;

                    if (player.LasersCount == 1)
                    {
                        World.Send(new PlaySound("sfx/charged"));
                    }
                }
            }
        }

        private void StopGame()
        {
            World.Entities.ForEach(World.Destroy);
            _gameStarted = false;

            World
                .CreateEntity()
                .Attach(new Transform
                {
                    Position = new Vector2(World.Width / 2, World.Height / 2)
                })
                .Attach(new LabelComponent
                {
                    Align = Align.Center
                })
                .Attach(new ScoreComponent());
        }

        public override void Update(Entity entity, GameTime delta)
        {
            var label = entity.Get<LabelComponent>();

            label.Label = _gameStarted
                ? $"Score: {Score}"
                : $"You died. Your score: {Score}\nPress R to start again";
        }
    }
}