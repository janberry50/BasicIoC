using System;

namespace BasicIoc
{
    internal abstract class RegisteredObject
    {
        public Type ImplementationType { get; }
        public abstract Object GetInstance(params Object[] parameters);

        public RegisteredObject(Type implementation)
        {
            this.ImplementationType = implementation;
        }
    }
}
