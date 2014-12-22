using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Assisticant.Facades
{
    class FacadeMapping
    {
        public readonly DependencyProperty ViewProperty;
        readonly FacadeMember ModelProperty;

        public FacadeMapping(DependencyProperty controlProp, FacadeMember modelProp)
        {
            ViewProperty = controlProp;
            ModelProperty = modelProp;
            if (ModelProperty.MemberType != ViewProperty.PropertyType)
                throw new ArgumentException("Type in model is incompatible with type in view: " + this);
            if (!ModelProperty.CanWrite)
                throw new ArgumentException("Model field/property must be writable: " + this);
        }

        public void Update(object model, object value) { ForView.Unwrap<object>(value, unwrapped => ModelProperty.SetValue(model, unwrapped)); }
        public void UpdateFrom(object model, DependencyObject view) { Update(model, view.GetValue(ViewProperty)); }

        public override string ToString() { return ModelProperty.ToString(); }
    }
}
