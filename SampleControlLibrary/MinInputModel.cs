// Part of Assisticant.Facades: https://blog.machinezoo.com/easy-wpf-control-authoring-with
using Assisticant.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleControlLibrary
{
    class MinInputModel
    {
        public readonly Observable<double> Input = new Observable<double>();
    }
}
