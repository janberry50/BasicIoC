using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicIoc
{
    internal interface ITestInterface
    {
        Boolean Execute();
    }

    internal interface IConstructorInterface1 { }
    internal interface IConstructorInterface2 { }
    internal class ConstructorClass1 : IConstructorInterface1 { }
    internal class ConstructorClass2 : IConstructorInterface2 { }

    internal class TestClass : ITestInterface
    {
        public Guid Guid { get; set; }

        public TestClass()
        {
            this.Guid = Guid.NewGuid();
        }
        public Boolean Execute() => true;
    }

    internal class ConstructorTestClass
    {
        public ConstructorTestClass(IConstructorInterface1 iface1, IConstructorInterface2 iface2) { }
    }

    internal abstract class AbstractConstructorTestClass
    {
        public AbstractConstructorTestClass(IConstructorInterface1 iface1, IConstructorInterface2 iface2) { }
    }
}
