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
    public class FullName : Control
    {
        readonly FullNameModel Model = new FullNameModel();

        public static readonly DependencyProperty FirstProperty = DependencyProperty.Register("First", typeof(string), typeof(FullName));
        public string First { get { return (string)GetValue(FirstProperty); } set { SetValue(FirstProperty, value); } }

        public static readonly DependencyProperty LastProperty = DependencyProperty.Register("Last", typeof(string), typeof(FullName));
        public string Last { get { return (string)GetValue(LastProperty); } set { SetValue(LastProperty, value); } }

        public static readonly DependencyProperty IsReversedProperty = DependencyProperty.Register("IsReversed", typeof(bool), typeof(FullName));
        public bool IsReversed { get { return (bool)GetValue(IsReversedProperty); } set { SetValue(IsReversedProperty, value); } }

        static FullName()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FullName), new FrameworkPropertyMetadata(typeof(FullName)));
        }

        public FullName()
        {
            FacadeModel.UpdateAll(Model, this);
        }

        public override void OnApplyTemplate()
        {
            FacadeModel.Wrap(Model, GetTemplateChild("Root"));
            base.OnApplyTemplate();
        }

        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs args)
        {
            base.OnPropertyChanged(args);
            FacadeModel.Update(Model, this, args);
        }
    }
}
