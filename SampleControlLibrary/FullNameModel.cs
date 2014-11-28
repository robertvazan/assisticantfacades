using Assisticant.Facades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleControlLibrary
{
    class FullNameModel
    {
        public FacadeProperty<string> First;
        public FacadeProperty<string> Last;
        public FacadeProperty<bool> IsReversed;

        public string Full
        {
            get
            {
                var first = First.Value ?? "";
                var last = Last.Value ?? "";
                return !IsReversed.Value
                    ? first + " " + last
                    : last + " " + first;
            }
        }
    }
}
