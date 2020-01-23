using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Assisticant.Facades
{
    abstract class FacadeMapping
    {
        public readonly DependencyProperty ViewProperty;
        public readonly FacadeMember ModelProperty;

        public abstract void Update(object model, object value);

        public FacadeMapping(DependencyProperty controlProp, FacadeMember modelProp)
        {
            ViewProperty = controlProp;
            ModelProperty = modelProp;
        }

        public void UpdateFrom(object model, DependencyObject view) { Update(model, view.GetValue(ViewProperty)); }

        public override string ToString() { return ModelProperty.ToString(); }
    }
}
