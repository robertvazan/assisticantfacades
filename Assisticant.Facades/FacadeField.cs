// Part of Assisticant.Facades: https://blog.machinezoo.com/easy-wpf-control-authoring-with
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Assisticant.Facades
{
    class FacadeField : FacadeMember
    {
        readonly FieldInfo ModelField;
        public override string Name { get { return ModelField.Name; } }
        public override Type MemberType { get { return ModelField.FieldType; } }
        public override Type DeclaringType { get { return ModelField.DeclaringType; } }
        public override bool CanWrite { get { return !ModelField.IsInitOnly; } }

        public FacadeField(FieldInfo field) { ModelField = field; }

        public override object GetValue(object model) { return ModelField.GetValue(model); }
        public override void SetValue(object model, object value) { ModelField.SetValue(model, value); }
    }
}
