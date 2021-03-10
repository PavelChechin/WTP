using System;
using System.Linq;
using WTPCore.Data;

namespace WTPCore.Factory
{
    public class WTPBaseFactory : WTPFactory
    {
      
        static Type[] BaseDataTypes;

        static WTPBaseFactory()
        {
            var types = typeof(WTPFactory).Assembly.GetTypes();
            BaseDataTypes =
                types.Where(t => t.Namespace == "WTPCore.Data.Tables.Base")
                .ToArray();

            Instance = new WTPBaseFactory();
        }

        public static WTPBaseFactory Instance
        {
            get;
            private set;
        }

        public WTPBaseFactory()
        {
            
        }
        
        protected override Type[] GetTypes()
        {
            return BaseDataTypes;
        }
        public override IWTPDATAROW CreateRow(Type RowType)
        {
            Type containerType = GetClassType(RowType);
            object newRow = Activator.CreateInstance(containerType);
            return (IWTPDATAROW)newRow;
        }     
    }
}
