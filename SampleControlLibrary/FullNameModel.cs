// Part of Assisticant.Facades: https://blog.machinezoo.com/easy-wpf-control-authoring-with
using Assisticant.Facades;
using Assisticant.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleControlLibrary
{
    class FullNameModel
    {
        public readonly Observable<string> First = new Observable<string>();
        public readonly Observable<string> Last = new Observable<string>();
        public readonly Observable<bool> IsReversed = new Observable<bool>();

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
