using System;
using WTPCore.Data.Interfaces.Base;
using CollectionsPattern;

namespace WTPCore.WorkTeacherPlan
{
    public class WTPParam : BaseRow<WTPParams, IWTPPARAM, WTPParam>
    {
        public WTPParam(WTPParams Params, IWTPPARAM DataRow)
            : base(DataRow, Params)
        {
            ValueType = GetValueType(DataRow.WTPPARAM_TYPE);
            if (ValueType != typeof(object))
                DefaultValue = Activator.CreateInstance(ValueType);
            else
                DefaultValue = null;
        }
        public object DefaultValue
        {
            get;
            private set;
        }
        public Type ValueType
        {
            get;
            private set;
        }

        public bool IsDefaultValue(object Value)
        {
            if (Value == null)
                return true;
            else if (DefaultValue != null)
                return DefaultValue.Equals(Value);
            else
                return false;
        }

        public static Type GetValueType(string TypeName)
        {
            switch (TypeName.ToLower())
            {
                case "int32":
                case "int": return typeof(int);
                case "int16":
                case "short": return typeof(short);
                case "int64":
                case "long": return typeof(long);
                case "double": return typeof(double);
                case "single":
                case "float": return typeof(float);
                default: return typeof(object);

            }
        }
    }
}
