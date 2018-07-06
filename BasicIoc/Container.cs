using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace BasicIoc
{
    public class Container : IContainer
    {
        private readonly IDictionary<String, RegisteredObject> _registeredObjects;

        public Container()
        {
            this._registeredObjects = new Dictionary<String, RegisteredObject>();
        }

        public void Register<TAbstraction, TImplementation>() where TImplementation : class, TAbstraction
        {
            this.AddTransientObject(typeof(TAbstraction), typeof(TImplementation));
        }

        public void Register<TAbstraction, TImplementation>(Boolean asSingleton) where TImplementation : class, TAbstraction
        {
            if (asSingleton)
            {
                this.AddSingletonObject(typeof(TAbstraction), typeof(TImplementation));
            }
            else
            {
                this.Register<TAbstraction, TImplementation>();
            }
        }

        public Object Resolve<T>()
        {
            return this.ResolveType(typeof(T));
        }

        private Object ResolveType(Type typeToResolve)
        {
            Object result = null;

            if (typeToResolve.IsInterface)
            {
                if (this._registeredObjects.TryGetValue(typeToResolve.Name, out var regObj))
                {
                    result = regObj.GetInstance();
                }
            }
            else
            {
                var constructorParams = this.ResolveConstructors(typeToResolve);

                if(!ReferenceEquals(constructorParams, null))
                {
                    result = this.CreateInstance(typeToResolve, constructorParams.ToArray());
                }
            }

            return result;
        }

        private void AddTransientObject(Type abstraction, Type implementation)
        {
            this._registeredObjects.Add(abstraction.Name, new TransientRegisteredObject(implementation));
        }

        private void AddSingletonObject(Type abstraction, Type implementation)
        {
            this._registeredObjects.Add(abstraction.Name, new SingletonRegisteredObject(implementation));
        }

        private Object CreateInstance(Type type, params Object[] parameters)
        {
            return Activator.CreateInstance(type, parameters);
        }

        private IEnumerable<Object> ResolveConstructors(Type typeToResolve)
        {
            IEnumerable<Object> result = null;

            if (!typeToResolve.IsAbstract)
            {
                var constructors = typeToResolve.GetConstructors();

                if (constructors.Length > 0)
                {
                    foreach (var c in constructors)
                    {
                        var parameters = this.ResolveConstructor(c);

                        if (!ReferenceEquals(parameters, null))
                        {
                            result = parameters;
                            break;
                        }
                    }
                }
            }

            return result;
        }

        private IEnumerable<Object> ResolveConstructor(ConstructorInfo constructor)
        {
            IEnumerable<Object> result = null;

            var parameters = constructor.GetParameters();
            var resolvedParameters = new List<Object>();
            var allResolved = true;

            foreach (var p in parameters)
            {
                var resolvedParameter = this.ResolveType(p.ParameterType);

                if (ReferenceEquals(resolvedParameter, null))
                {
                    allResolved = false;
                    break;
                }
                else
                {
                    resolvedParameters.Add(resolvedParameter);
                }
            }

            if(allResolved)
            {
                result = resolvedParameters;
            }

            return result;
        }
    }
}
