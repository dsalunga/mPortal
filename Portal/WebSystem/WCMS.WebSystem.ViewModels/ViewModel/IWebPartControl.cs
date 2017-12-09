using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.WebSystem.ViewModel
{
    public interface IWebPartControl : IObjectValueProvider
    {
        string PageTitleOverride { get; }
    }
}
