using System;

namespace Asteroids.Core.Exceptions
{
    public class CoreException : Exception
    {
        public CoreException(string message) : base(message) { }
    }
}