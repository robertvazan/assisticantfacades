using Assisticant.Facades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SampleControlLibrary
{
    public class MinInput : Freezable
    {
        internal readonly MinInputModel Model = new MinInputModel();

        public static readonly DependencyProperty InputProperty = DependencyProperty.Register("Input", typeof(double), typeof(MinInput));
        public double Input { get { return (double)GetValue(InputProperty); } set { SetValue(InputProperty, value); } }

        public MinInput() { FacadeModel.UpdateAll(Model, this); }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            FacadeModel.Update(Model, this, e);
            base.OnPropertyChanged(e);
        }

        protected override Freezable CreateInstanceCore()
        {
            return new MinInput();
        }
    }
}
