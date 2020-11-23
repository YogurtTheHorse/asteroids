using System.Linq;
using Asteroids.Systems.Game.Components.Rendering;
using Microsoft.Xna.Framework;

namespace Asteroids.Systems.Game.Content.PolygonLoading
{
    public class PolygonLoader
    {
        private readonly JsonLoader _jsonLoader;

        public PolygonLoader(JsonLoader jsonLoader)
        {
            _jsonLoader = jsonLoader;
        }

        public PolygonRenderer Load(string polygonName)
        {
            var rawPolygon = _jsonLoader.Load<RawPolygon>(polygonName);

            return new PolygonRenderer()
            {
                Vertices = rawPolygon
                    .Vertices
                    .Select(x => new Vector2(x[0], x[1]))
                    .ToArray(),
                Loop = rawPolygon.Loop
            };
        }
    }
}