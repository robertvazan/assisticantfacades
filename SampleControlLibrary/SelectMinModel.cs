using Assisticant.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleControlLibrary
{
    class SelectMinModel
    {
        public readonly ObservableList<MinInput> Inputs = new ObservableList<MinInput>();
        IEnumerable<MinInputModel> InputModels { get { return Inputs.Select(i => i.Model); } }
        public double Min { get { return InputModels.Any() ? InputModels.Min(m => m.Input.Value) : 0.0; } }
    }
}
