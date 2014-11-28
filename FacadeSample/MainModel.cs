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
    }
}
