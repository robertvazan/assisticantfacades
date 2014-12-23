using Assisticant.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FacadeSample
{
    class MainModel
    {
        public readonly Observable<string> FirstName = new Observable<string>("Robert");
        public readonly Observable<string> LastName = new Observable<string>("Važan");
        public readonly Observable<bool> IsReversed = new Observable<bool>();

        public readonly Observable<double> Input1 = new Observable<double>(3);
        public readonly Observable<double> Input2 = new Observable<double>(5);
        public readonly Observable<double> Input3 = new Observable<double>(2);
        public readonly Observable<double> Input4 = new Observable<double>(7);
    }
}
