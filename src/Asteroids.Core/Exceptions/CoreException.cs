using System;

namespace Asteroids.Core.Exceptions
{
    /// <summary>
    /// Base exception class for core engine exceptions.
    /// </summary>
    public class CoreException : Exception
    {
        public CoreException(string message) : base(message) { }
    }
}