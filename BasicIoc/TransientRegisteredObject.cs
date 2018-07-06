using System;

namespace BasicIoc
{
    internal class TransientRegisteredObject : RegisteredObject
    {
        public TransientRegisteredObject(Type implementation): base(implementation)
        {            
        }

        public override Object GetInstance(params Object[] parameters)
        {
            Object result = null;

            if(parameters.Length == 0)
            {
                result = Activator.CreateInstance(this.ImplementationType);
            }
            else
            {
                result = Activator.CreateInstance(this.ImplementationType, parameters);
            }

            return result;
        }
    }
}
