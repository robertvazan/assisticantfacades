using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Assisticant.Facades
{
    class FacadeProperty : FacadeMember
    {
        readonly PropertyInfo ModelProperty;
        public override string Name { get { return ModelProperty.Name; } }
        public override Type MemberType { get { return ModelProperty.PropertyType; } }
        public override Type DeclaringType { get { return ModelProperty.DeclaringType; } }
        public override bool CanWrite { get { return ModelProperty.CanWrite; } }

        public FacadeProperty(PropertyInfo modelProp) { ModelProperty = modelProp; }

        public override object GetValue(object model) { return ModelProperty.GetValue(model); }
        public override void SetValue(object model, object value) { ModelProperty.SetValue(model, value); }
    }
}
