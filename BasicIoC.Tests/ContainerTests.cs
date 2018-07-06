using System;
using BasicIoc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BasicIoC.Tests
{
    [TestClass]
    public class ContainerTests
    {
        [TestMethod]
        public void InterfaceIsResolved()
        {
            IContainer container = new Container();
            container.Register<ITestInterface, TestClass>();

            Assert.IsInstanceOfType(container.Resolve<ITestInterface>(), typeof(TestClass));
        }

        [TestMethod]
        public void InterfaceIsResolvedAsSingleton()
        {
            IContainer container = new Container();
            container.Register<ITestInterface, TestClass>(true);

            var regObj1 = container.Resolve<ITestInterface>();
            var regObj2 = container.Resolve<ITestInterface>();

            Assert.AreSame(regObj1, regObj2);
        }

        [TestMethod]
        public void ConcreteClassWithNoConstructorIsResolved()
        {
            IContainer container = new Container();

            Assert.IsInstanceOfType(container.Resolve<TestClass>(), typeof(TestClass));
        }

        [TestMethod]
        public void ConcreteClassWithConstructorWhereNoParametersAreRegisteredIsNull()
        {
            IContainer container = new Container();

            Assert.IsNull(container.Resolve<ConstructorTestClass>());
        }

        [TestMethod]
        public void ConcreteClassWithConstructorWhereSomeParametersAreRegisteredIsNull()
        {
            IContainer container = new Container();
            container.Register<IConstructorInterface1, ConstructorClass1>();

            Assert.IsNull(container.Resolve<ConstructorTestClass>());
        }

        [TestMethod]
        public void ConcreteClassWithConstructorWhereAllParametersAreRegisteredIsResolved()
        {
            IContainer container = new Container();
            container.Register<IConstructorInterface1, ConstructorClass1>();
            container.Register<IConstructorInterface2, ConstructorClass2>();

            var obj = container.Resolve<ConstructorTestClass>();

            Assert.IsNotNull(obj);
            Assert.IsInstanceOfType(obj, typeof(ConstructorTestClass));
        }

        [TestMethod]
        public void AbstractClassIsNotResolved()
        {
            IContainer container = new Container();
            container.Register<IConstructorInterface1, ConstructorClass1>();
            container.Register<IConstructorInterface2, ConstructorClass2>();

            Assert.IsNull(container.Resolve<AbstractConstructorTestClass>());
        }
    }
}
