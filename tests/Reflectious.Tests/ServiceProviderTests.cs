using System;
using Moq;
using Xunit;

namespace Reflectious.Tests
{
    public class ServiceProviderTests
    {
        [Fact]
        public void MultiCtors_WithNewInstance_UsesEmptyCtor()
        {
            var sp = new ActivatorServiceProvider();
            
            var result = Reflect.Type<TwoCtorStub>()
                .WithNewInstance()
                .GetMethod(nameof(TwoCtorStub.GetValue))
                .Invoke(new ParamStub1(), new ParamStub2());
            
            Assert.Equal("EmptyCtor_ParamStub1Value_ParamStub2Value_", result);
        }

        [Fact]
        public void MultiCtors_WithNewInstanceWithMethodParametersFromProvider_UsesEmptyCtor()
        {
            var sp = new ActivatorServiceProvider();
            
            var result = Reflect.Type<TwoCtorStub>()
                .WithNewInstance()
                .GetMethod(nameof(TwoCtorStub.GetValue))
                .WithParameters<ParamStub1, ParamStub2>()
                .FromServiceProvider(sp)
                .Invoke();
            
            Assert.Equal("EmptyCtor_ParamStub1Value_ParamStub2Value_", result);
        }

        [Fact]
        public void MutiCtors_SpecifiedCtorWithServiceProvider_UsesCorrectCtor()
        {
            var sp = new ActivatorServiceProvider();
            
            var result = Reflect.Type<TwoCtorStub>()
                .WithNewInstance()
                .UsingConstructor<ParamStub1>()
                .WithArgumentsFromServiceProvider(sp)
                .GetMethod(nameof(TwoCtorStub.GetValue))
                .Invoke(new ParamStub1(), new ParamStub2());
            
            Assert.Equal("ParamCtor_ParamStub1Value_ParamStub2Value_", result);
        }
        
        [Fact]
        public void MultiCtors_CreateInstance_UsesEmptyCtor()
        {
            var instance = Reflect.Type<TwoCtorStub>()
                .CreateInstance();
            
            Assert.Equal("EmptyCtor_", instance.Prefix);
        }
        
        [Fact]
        public void OneCtor_WithServiceProvider_GetsInstanceFromProvider()
        {            
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(sp => sp.GetService(typeof(ParamStub1))).Returns(new ParamStub1());

            var instance = Reflect.Type<OneCtorStub>()
                .GetConstructor()
                .FromServiceProvider(spMock.Object)
                .Invoke();
            
            spMock.Verify(sp => sp.GetService(typeof(ParamStub1)));
        }
        
        [Fact]
        public void MultiCtors_SpecifiedParamsWithServiceProvider_GetsInstanceFromProvider()
        {            
            var spMock = new Mock<IServiceProvider>();

            var instance = Reflect.Type<TwoCtorStub>()
                .GetConstructor()
                .WithParameters(typeof(ParamStub1))
                .FromServiceProvider(spMock.Object)
                .Invoke();
            
            spMock.Verify(sp => sp.GetService(typeof(ParamStub1)));
        }
        
        [Fact]
        public void MultiCtors_WithNewInstanceFromServiceProvider_GetsInstanceFromProvider()
        {            
            var spMock = new Mock<IServiceProvider>();
            spMock.Setup(sp => sp.GetService(typeof(TwoCtorStub))).Returns(new TwoCtorStub());

            var prefix = Reflect.Type<TwoCtorStub>()
                .WithNewInstance()
                .FromServiceProvider(spMock.Object)
                .GetProperty(s => s.Prefix)
                .GetValue();
            
            spMock.Verify(sp => sp.GetService(typeof(TwoCtorStub)));
            Assert.Equal("EmptyCtor_", prefix);
        }

        private class OneCtorStub
        {
            public string Prefix { get; }

            public OneCtorStub(ParamStub1 stub1)
            {
                Prefix = "OnlyCtor_";
            }
            
            public string GetValue(ParamStub1 paramStub1, ParamStub2 paramStub2)
            {
                return Prefix + paramStub1.Value + paramStub2.Value;
            }
        }

        private class TwoCtorStub
        {
            public string Prefix { get; }

            public TwoCtorStub()
            {
                Prefix = "EmptyCtor_";
            }
            
            public TwoCtorStub(ParamStub1 stub1)
            {
                Prefix = "ParamCtor_";
            }
            
            public string GetValue(ParamStub1 paramStub1, ParamStub2 paramStub2)
            {
                return Prefix + paramStub1.Value + paramStub2.Value;
            }
        }

        private class ParamStub1
        {
            public string Value { get; } = "ParamStub1Value_";
        }

        private class ParamStub2
        {
            public string Value { get; } = "ParamStub2Value_";
        }
    }

    internal class ActivatorServiceProvider : IServiceProvider
    {
        public object GetService(Type serviceType)
        {
            return Activator.CreateInstance(serviceType);
        }
    }
}