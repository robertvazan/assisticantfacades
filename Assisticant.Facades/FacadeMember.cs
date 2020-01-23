using Assisticant.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Assisticant.Facades
{
    abstract class FacadeMember
    {
        public abstract string Name { get; }
        public abstract Type MemberType { get; }
        public abstract Type DeclaringType { get; }
        public abstract bool CanWrite { get; }

        public abstract object GetValue(object model);
        public abstract void SetValue(object model, object value);

        public override string ToString() { return String.Format("{0}.{1}", DeclaringType, Name); }
    }
}
