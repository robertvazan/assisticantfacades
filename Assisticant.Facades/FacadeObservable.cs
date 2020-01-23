// Part of Assisticant.Facades: https://blog.machinezoo.com/easy-wpf-control-authoring-with
using Assisticant.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assisticant.Facades
{
    class FacadeObservable : FacadeMember
    {
        readonly FacadeMember UnderlyingMember;
        readonly PropertyInfo ValueProperty;
        public override string Name { get { return UnderlyingMember.Name; } }
        public override Type MemberType { get { return ValueProperty.PropertyType; } }
        public override Type DeclaringType { get { return UnderlyingMember.DeclaringType; } }
        public override bool CanWrite { get { return ValueProperty.CanWrite; } }

        FacadeObservable(FacadeMember underlying)
        {
            UnderlyingMember = underlying;
            ValueProperty = underlying.MemberType.GetProperty("Value");
        }

        public override object GetValue(object model) { throw new NotSupportedException(); }
        public override void SetValue(object model, object value) { ValueProperty.SetValue(UnderlyingMember.GetValue(model), value); }

        public static FacadeMember Unwrap(FacadeMember member)
        {
            if (member.MemberType.IsGenericType && typeof(Observable<>).IsAssignableFrom(member.MemberType.GetGenericTypeDefinition()))
                return new FacadeObservable(member);
            return member;
        }
    }
}
