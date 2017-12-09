using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.Framework
{
    public interface ITask
    {
        void Initialize(ITaskRequest task);
        void Execute();
    }
}
