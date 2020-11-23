using System;
using System.IO;
using System.Text.Json;
using Microsoft.Xna.Framework.Content;

namespace Asteroids.Systems.Game.Content
{
    public class JsonLoader
    {
        private readonly string _contentRoot;
        
        public JsonLoader(ContentManager contentManager) : this(contentManager.RootDirectory) { }

        public JsonLoader(string contentRoot)
        {
            _contentRoot = contentRoot;
        }

        public T Load<T>(string polygonName)
        {
            string path = Path.Combine(_contentRoot, polygonName + ".json");

            using var jsonFile = new StreamReader(path);
            string jsonContent = jsonFile.ReadToEnd();
            
            var obj = JsonSerializer.Deserialize<T>(jsonContent);

            if (obj is null)
            {
                throw new Exception($"Unnable to deserialize {typeof(T).Name} from file: {path}");
            }

            return obj;
        }
    }
}