using Assisticant.Facades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace SampleControlLibrary
{
    public class SelectMin : Control
    {
        readonly SelectMinModel Model = new SelectMinModel();

        public static readonly DependencyProperty InputsProperty = DependencyProperty.Register("Inputs", typeof(MinInputCollection), typeof(SelectMin));
        public MinInputCollection Inputs { get { return (MinInputCollection)GetValue(InputsProperty); } set { SetValue(InputsProperty, value); } }

        static SelectMin()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SelectMin), new FrameworkPropertyMetadata(typeof(SelectMin)));
        }

        public SelectMin()
        {
            Inputs = new MinInputCollection();
            FacadeModel.UpdateAll(Model, this);
        }

        public override void OnApplyTemplate()
        {
            FacadeModel.Wrap(Model, GetTemplateChild("Root"));
            base.OnApplyTemplate();
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs args)
        {
            FacadeModel.Update(Model, this, args);
            base.OnPropertyChanged(args);
        }
    }
}
