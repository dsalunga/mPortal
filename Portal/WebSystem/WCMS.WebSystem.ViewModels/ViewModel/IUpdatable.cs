using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.WebSystem.ViewModel
{
    public interface IUpdatable
    {
        bool Update();

        string UpdateText { get; }
        string CancelText { get; }
    }
}
