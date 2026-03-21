using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCMS.Common
{
    public class ObjectPair<T1, T2>
    {
        public ObjectPair(T1 object1, T2 object2)
        {
            this.Object1 = object1;
            this.Object2 = object2;
        }

        public T1 Object1 { get; set; }
        public T2 Object2 { get; set; }
    }
}
