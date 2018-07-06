using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicIoc
{
    public interface IContainer
    {
        void Register<TAbstraction, TImplementation>() where TImplementation: class, TAbstraction;
        void Register<TAbstraction, TImplementation>(Boolean asSingleton) where TImplementation : class, TAbstraction;

        Object Resolve<T>();
    }
}
