// Part of Assisticant.Facades: https://blog.machinezoo.com/easy-wpf-control-authoring-with
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assisticant.Facades
{
    class FacadeListAdaptor<T> : IList
    {
        readonly IList<T> InnerList;
        public int Count { get { return InnerList.Count; } }
        public bool IsFixedSize { get { return false; } }
        public bool IsReadOnly { get { return InnerList.IsReadOnly; } }
        public bool IsSynchronized { get { return false; } }
        public object SyncRoot { get { return this; } }
        public object this[int index] { get { return InnerList[index]; } set { InnerList[index] = (T)value; } }

        public FacadeListAdaptor(IList<T> inner) { InnerList = inner; }

        public int Add(object value) { InnerList.Add((T)value); return InnerList.Count - 1; }
        public void Clear() { InnerList.Clear(); }
        public bool Contains(object value) { return InnerList.Contains((T)value); }
        public int IndexOf(object value) { return InnerList.IndexOf((T)value); }
        public void Insert(int index, object value) { InnerList.Insert(index, (T)value); }
        public void Remove(object value) { InnerList.Remove((T)value); }
        public void RemoveAt(int index) { InnerList.RemoveAt(index); }
        public void CopyTo(Array array, int index) { InnerList.CopyTo((T[])array, index); }
        public IEnumerator GetEnumerator() { return InnerList.GetEnumerator(); }
    }
}
