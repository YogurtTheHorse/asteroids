using Asteroids.Core.Ecs;

namespace Asteroids.Core.Tests.TestData
{
    public class ComponentWithFooRequirement : Component
    {
        public ComponentWithFooRequirement() => Require<FooComponent>();
    }
}