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
    class FacadeMember
    {
        public readonly DependencyProperty ViewProperty;
        readonly PropertyInfo ModelProperty;
        readonly FieldInfo ModelField;
        readonly Type MemberType;
        readonly PropertyInfo ValueProperty;
        Type ModelValueType { get { return ValueProperty != null ? ValueProperty.PropertyType : ModelProperty != null ? ModelProperty.PropertyType : ModelField.FieldType; } }

        public FacadeMember(DependencyProperty controlProp, PropertyInfo modelProp)
        {
            ViewProperty = controlProp;
            ModelProperty = modelProp;
            MemberType = ModelProperty.PropertyType;
            ValueProperty = UnwrapValueProperty();
            CheckTypeCompatibility();
            if (ValueProperty == null && !ModelProperty.CanWrite)
                throw new ArgumentException("Model property must be writable: " + this);
        }

        public FacadeMember(DependencyProperty controlProp, FieldInfo field)
        {
            ViewProperty = controlProp;
            ModelField = field;
            MemberType = ModelField.FieldType;
            ValueProperty = UnwrapValueProperty();
            CheckTypeCompatibility();
            if (ValueProperty == null && ModelField.IsInitOnly)
                throw new ArgumentException("Model field must be writable: " + this);
        }

        public void Update(object model, object value)
        {
            if (ValueProperty != null)
            {
                object member = null;
                if (ModelProperty != null)
                    member = ModelProperty.GetValue(model);
                if (ModelField != null)
                    member = ModelField.GetValue(model);
                ValueProperty.SetValue(member, value);
            }
            else
            {
                if (ModelProperty != null)
                    ModelProperty.SetValue(model, value);
                if (ModelField != null)
                    ModelField.SetValue(model, value);
            }
        }

        public void UpdateFrom(object model, DependencyObject view) { Update(model, view.GetValue(ViewProperty)); }

        PropertyInfo UnwrapValueProperty()
        {
            if (MemberType.IsGenericType && typeof(Observable<>).IsAssignableFrom(MemberType.GetGenericTypeDefinition()))
                return MemberType.GetProperty("Value");
            return null;
        }

        void CheckTypeCompatibility()
        {
            if (ModelValueType != ViewProperty.PropertyType)
                throw new ArgumentException("Type in model is incompatible with type in view: " + this);
        }

        public override string ToString() { return String.Format("{0}.{1}", ModelProperty != null ? ModelProperty.DeclaringType : ModelField.DeclaringType, ViewProperty.Name); }
    }
}
