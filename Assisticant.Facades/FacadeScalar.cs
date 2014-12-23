using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Assisticant.Facades
{
    class FacadeScalar : FacadeMapping
    {
        public FacadeScalar(DependencyProperty controlProp, FacadeMember modelProp)
            : base(controlProp, modelProp)
        {
            if (ModelProperty.MemberType != ViewProperty.PropertyType)
                throw new ArgumentException("Type in model is incompatible with type in view: " + this);
            if (!ModelProperty.CanWrite)
                throw new ArgumentException("Model field/property must be writable: " + this);
        }

        public override void Update(object model, object value) { ForView.Unwrap<object>(value, unwrapped => ModelProperty.SetValue(model, unwrapped)); }
    }
}
