using System;

namespace Asteroids
{
    /// <summary>
    /// Main program class with startup logic.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Entry point of the program.
        /// </summary>
        /// <param name="args">Console arguments.</param>
        public static void Main(string[] args)
        {
            using var game = new AsteroidsGame();
            
            game.Run();
        }
    }
}
