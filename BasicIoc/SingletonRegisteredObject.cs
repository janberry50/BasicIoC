using System;

namespace BasicIoc
{
    internal class SingletonRegisteredObject : RegisteredObject
    {
        private Object _instance;

        public SingletonRegisteredObject(Type implementation): base(implementation)
        {
        }

        public override Object GetInstance(params Object[] parameters)
        {
            if(ReferenceEquals(this._instance, null))
            {
                this.CreateInstance(parameters);
            }

            return this._instance;
        }

        private void CreateInstance(params Object[] parameters)
        {
            if (parameters.Length == 0)
            {
                this._instance = Activator.CreateInstance(this.ImplementationType);
            }
            else
            {
                this._instance = Activator.CreateInstance(this.ImplementationType, parameters);
            }
        }
    }
}
