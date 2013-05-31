using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DI {
    [Serializable()]
    public class InterfaceNotRegisteredException : Exception {
        public InterfaceNotRegisteredException() : base() { }

        protected InterfaceNotRegisteredException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }

    [Serializable()]
    public class InfiniteInjectionException : Exception {
        public InfiniteInjectionException() : base() { }

        protected InfiniteInjectionException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) { }
    }

    [AttributeUsage(AttributeTargets.Constructor)]
    public class DInject : Attribute { }

    public interface IDIContainer {
        void RegisterType<T>(bool singleton);
        void RegisterType<From, To>(bool singleton) where To : From;
        void RegisterInstance<T>(T instance);

        T Resolve<T>();
    }

    public class DIContainer : IDIContainer {
        private Dictionary<Type, object> _singletons;
        private Dictionary<Type, Type> _registers;
        private List<Type> _temp;

        public DIContainer() {
            this._singletons = new Dictionary<Type, object>();
            this._registers = new Dictionary<Type, Type>();
            this._temp = new List<Type>();
        }

        private void Singletonize<T>(bool singleton) {
            if(singleton)
                this._singletons[typeof(T)] = null;
            else
                this._singletons.Remove(typeof(T));
        }

        public void RegisterType<T>(bool singleton) {
            this._registers[typeof(T)] = typeof(T);
            this.Singletonize<T>(singleton);
        }

        public void RegisterType<From, To>(bool singleton) where To : From {
            this._registers[typeof(From)] = typeof(To);
            this.Singletonize<From>(singleton);
        }

        public void RegisterInstance<T>(T instance) {
            this._singletons[typeof(T)] = instance;
        }

        private object CreateInstance(Type type) {
            this._temp.Add(type);
            ConstructorInfo[] constructors = this._registers[type].GetConstructors();
            ConstructorInfo constructor = null;
            ParameterInfo[] parameters = null;
            int i = -1;
            foreach(ConstructorInfo cinfo in constructors) {
                ParameterInfo[] pinfo = cinfo.GetParameters();
                IEnumerable<DInject> attrs = cinfo.GetCustomAttributes<DInject>(false);
                if(attrs.Count() == 1) {
                    constructor = cinfo;
                    parameters = pinfo;
                    break;
                }
                if(pinfo.Length > i) {
                    i = pinfo.Length;
                    constructor = cinfo;
                    parameters = pinfo;
                }
            }
            object[] values = new object[parameters.Length];
            for(i = 0; i < parameters.Length; ++i) {
                values[i] = this.Resolve(parameters[i].ParameterType);
            }
            return constructor.Invoke(values);
        }

        private object Resolve(Type type) {
            if(!this._registers.ContainsKey(type) && !this._singletons.ContainsKey(type))
                throw new InterfaceNotRegisteredException();
            if(this._singletons.ContainsKey(type)) {
                if(this._singletons[type] == null) {
                    if(this._temp.Contains(type))
                        throw new InfiniteInjectionException();
                    this._singletons[type] = this.CreateInstance(type);
                }
                return this._singletons[type];
            }
            if(this._temp.Contains(type))
                throw new InfiniteInjectionException();
            return this.CreateInstance(type);
        }

        public T Resolve<T>() {
            this._temp.Clear();
            return (T)this.Resolve(typeof(T));
        }
    }
}
