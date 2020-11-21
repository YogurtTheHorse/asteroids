using System;
using Asteroids.Core.Ecs;
using Asteroids.Core.Exceptions;
using Asteroids.Core.Tests.TestData;
using Xunit;

namespace Asteroids.Core.Tests.Ecs
{
    public class EntityTests
    {
        [Fact]
        public void Attach_AddComponent_WillAddComponent()
        {
            var entity = new Entity(0);

            entity.Attach(new FooComponent());
            
            Assert.True(entity.Has<FooComponent>());
        } 
        
        [Fact]
        public void DeAttach_DeAttachingComponent_WillBeRemoved()
        {
            var component = new FooComponent();
            var entity = new Entity(0);
            entity.Attach(component);

            entity.DeAttach(component);
            
            Assert.False(entity.Has<FooComponent>());
        }

        [Fact]
        public void DeAttach_DeAttachingMissingComponent_WillThrow()
        {
            var entity = new Entity(0);

            Action act = () => entity.DeAttach(new FooComponent());
            
            Assert.Throws<ComponentNotFoundException>(act);
        }

        [Fact]
        public void Attach_AttachingWithoutRequirements_WillThrow()
        {
            var entity = new Entity(0);

            Action act = () => entity.Attach(new ComponentWithFooRequirement());
            
            Assert.Throws<ComponentNotFoundException>(act);
        }

        [Fact]
        public void Attach_AttachingWithRequirements_WillAddComponent()
        {
            var entity = new Entity(0);
            entity.Attach(new FooComponent());
            entity.Attach(new ComponentWithFooRequirement());
            
            Assert.True(entity.Has<ComponentWithFooRequirement>());
            Assert.True(entity.Has<FooComponent>());
        }

        [Fact]
        public void DeAttach_DeAttachingDependency_WillThrow()
        {
            var entity = new Entity(0);
            entity.Attach(new FooComponent());
            entity.Attach(new ComponentWithFooRequirement());

            Action act = () => entity.DeAttach(new FooComponent());
            
            Assert.Throws<CoreException>(act);
        }
    }
}