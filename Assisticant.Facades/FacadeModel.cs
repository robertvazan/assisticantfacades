using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Assisticant.Facades
{
    public static class FacadeModel
    {
        public static object Wrap(object model) { return ForView.Wrap(model); }

        public static void Wrap(object model, DependencyObject target)
        {
            var element = target as FrameworkElement;
            if (element != null)
                element.DataContext = Wrap(model);
        }

        public static void UpdateAll(object model, DependencyObject view)
        {
            foreach (var member in FacadeType.Create(view.GetType(), model.GetType()).Members)
                member.UpdateFrom(model, view);
        }

        public static void Update(object model, DependencyObject view, DependencyPropertyChangedEventArgs change)
        {
            FacadeMapping member;
            if (FacadeType.Create(view.GetType(), model.GetType()).ByViewProperty.TryGetValue(change.Property, out member))
                member.Update(model, change.NewValue);
        }
    }
}
