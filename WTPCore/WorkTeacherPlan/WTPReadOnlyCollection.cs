using CollectionsPattern;

namespace WTPCore.WorkTeacherPlan
{
    interface ICascadeRowContainer
    {
        ICascadeRow CascadeRow
        {
            get;
        }
    }
    internal class WTPReadOnlyCollection<T> : CascadeCollection<T>
        where T : BaseObject, ICascadeRowContainer
    {
        
        internal void Add(T Item)
        {
            lst.Add(Item);
        }
        
        internal T[] ToArray()
        {            
            return lst.ToArray();
        }
        public override void Remove(T value, bool Dispose)
        {
            base.Remove(value, false);
        }
        public override ICascadeRow GetDeletedDataRow(T Value)
        {
            return Value.CascadeRow;
        }
    }
}
