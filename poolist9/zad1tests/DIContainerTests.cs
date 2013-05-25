using System;
using NUnit.Framework;
using zad1;

namespace zad1tests {
    interface ITestClass { }
    class TestClass : ITestClass { }

    [TestFixture]
    public class DIContainerTests {
        [Test]
        public void Should_properly_resolve_types() {
            IDIContainer c = new DIContainer();

            c.RegisterType<ITestClass, TestClass>();
            var result = c.Resolve<ITestClass>();

            Assert.IsInstanceOf<TestClass>(result);
        }

        [Test]
        public void Should_properly_create_singletons() {
            IDIContainer c = new DIContainer();

            c.RegisterType<ITestClass, TestClass>();
            c.RegisterType<ITestClass>(true);
            var result1 = c.Resolve<ITestClass>();
            var result2 = c.Resolve<ITestClass>();

            Assert.AreSame(result1, result2);
        }

        [Test]
        [ExpectedException(typeof(InterfaceNotRegisteredException))]
        public void Should_throw_exception_on_not_registered_type() {
            IDIContainer c = new DIContainer();

            c.Resolve<TestClass>();
        }

        [Test]
        [ExpectedException(typeof(InterfaceNotRegisteredException))]
        public void Should_throw_exception_on_setting_singleton_for_not_registered_type() {
            IDIContainer c = new DIContainer();

            c.RegisterType<TestClass>(true);
        }
    }
}
