using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.Json;
using Asteroids.Systems.Game.Components;
using Microsoft.Xna.Framework;

namespace Asteroids.PolygonLoading
{
    public class PolygonLoader
    {
        private readonly string _contentRoot;

        public PolygonLoader(string contentRoot)
        {
            _contentRoot = contentRoot;
        }

        public PolygonRenderer Load(string polygonName)
        {
            string path = Path.Combine(_contentRoot, polygonName + ".json");

            using var jsonFile = new StreamReader(path);
            string jsonContent = jsonFile.ReadToEnd();
            
            var rawPolygon = JsonSerializer.Deserialize<RawPolygon>(jsonContent);

            if (rawPolygon is null)
            {
                throw new Exception($"Unnable to deserialize raw polygon from file: {path}");
            }

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