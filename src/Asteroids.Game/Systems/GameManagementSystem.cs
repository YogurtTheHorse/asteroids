using System.Linq;
using Asteroids.Core;
using Asteroids.Core.Ecs;
using Asteroids.Core.Ecs.Systems;
using Asteroids.Systems.Game.Components;
using Asteroids.Systems.Game.Enums;
using Asteroids.Systems.Game.Messages;
using Asteroids.Systems.Game.PolygonLoading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpMath2;

namespace Asteroids.Systems.Game.Systems
{
    public class GameManagementSystem : EntityProcessingSystem<ScoreComponent>
    {
        public int Score { get; set; }

        private bool _gameStarted = false;

        private readonly ContentManager _content;
        private readonly PolygonLoader _polygonLoader;

        public GameManagementSystem(ContentManager content, PolygonLoader polygonLoader, World world) : base(world)
        {
            _content = content;
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

            World
                .CreateEntity()
                .Attach(new Transform
                {
                    Position = new Vector2(World.Width / 2f, World.Height / 2f)
                })
                .Attach(new Rigidbody())
                .Attach(new SpriteRenderer
                {
                    Texture = _content.Load<Texture2D>("ship/ship")
                })
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

            if (!World.Entities.With<Player>().Any() && _gameStarted)
            {
                StopGame();
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