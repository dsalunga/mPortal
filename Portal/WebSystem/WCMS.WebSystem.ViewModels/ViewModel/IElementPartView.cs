using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.WebSystem.ViewModel
{
    public interface IElementPartView
    {
        int ObjectID { get; set; }
        int RecordId { get; set; }

        void Initialize();
    }
}
