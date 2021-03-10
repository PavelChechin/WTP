using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections;
using WTPCore.Data;

namespace WTPCore.Factory
{
    public abstract class WTPFactory
    {

        public static ReadOnlyCollection<Type> SInterfaces
        {
            get;
            private set;
        }
        public ReadOnlyCollection<Type> Interfaces
        {
            get
            {
                return SInterfaces;
            }
        }
        public ReadOnlyCollection<Type> Types
        {
            get;
            private set;
        }

        Hashtable Interfaces2Types = new Hashtable();
        Hashtable Types2Interfaces = new Hashtable();
        
        static WTPFactory()
        {
            //Загрузили все типы этой сборки один раз
            var types = typeof(WTPFactory).Assembly.GetTypes();
            //Получили все интересующие нас интерфейсы
            var ar1 = types.Where(t => t.IsInterface
                && t.GetInterfaces().Contains(typeof(IWTPDATAROW)));
            SInterfaces = new ReadOnlyCollection<Type>(ar1.ToArray());                                    
        }

        public WTPFactory()
        {
            var ar2 = GetTypes();
            Types = new ReadOnlyCollection<Type>(ar2);
            //Сопоставили типы и интерфейсы
            foreach (Type t in ar2)
            {
                Type i = t
                    .FindInterfaces((T, Filter) => { return Interfaces.Contains(T); }, Interfaces)
                    .FirstOrDefault();
                Interfaces2Types.Add(i, t);
                Types2Interfaces.Add(t, i);
            }
        }

        protected abstract Type[] GetTypes();
        //{
        
        //}
        /// <summary>
        /// Возвращает тип интерфейса по типу класса
        /// </summary>
        /// <param name="ClassType"></param>
        /// <returns></returns>
        public Type GetInterfaceType<T>()
        {
            return (Type)Types2Interfaces[typeof(T)];
        }
        /// <summary>
        /// Возвращает тип интерфейса по типу класса
        /// </summary>
        /// <param name="ClassType"></param>
        /// <returns></returns>
        public Type GetInterfaceType(Type ClassType)
        {
            return (Type)Types2Interfaces[ClassType];
        }
        /// <summary>
        /// Возвращает тип класса по типу интерфейса
        /// </summary>
        /// <param name="InterfaceType"></param>
        /// <returns></returns>
        public Type GetClassType<T>() where T : IWTPDATAROW
        {
            return (Type)Interfaces2Types[typeof(T)];
        }
        /// <summary>
        /// Возвращает тип класса по типу интерфейса
        /// </summary>
        /// <param name="InterfaceType"></param>
        /// <returns></returns>
        public Type GetClassType(Type InterfaceType)
        {
            return (Type)Interfaces2Types[InterfaceType];
        }
        
        /// <summary>
        /// Создание новой строки по типу интерфейса
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T CreateRow<T>() where T: IWTPDATAROW
        {
            return (T)CreateRow(typeof(T));
        }

        public abstract IWTPDATAROW CreateRow(Type RowType);
    }
}
