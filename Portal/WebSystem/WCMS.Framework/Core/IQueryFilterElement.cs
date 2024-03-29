﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.Framework.Core
{
    public interface IQueryFilterElement<T>
    {
        string Name { get; set; }
        T Value { get; set; }
        T NullValue { get; set; }
    }
}
