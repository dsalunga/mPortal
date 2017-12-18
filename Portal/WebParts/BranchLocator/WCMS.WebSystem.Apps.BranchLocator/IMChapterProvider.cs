using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WCMS.Framework.Core;

namespace WCMS.WebSystem.Apps.BranchLocator
{
    public interface IMChapterProvider : IDataProvider<MChapter>
    {
        IEnumerable<MChapter> GetList(int parentId);
        IEnumerable<MChapter> GetListByLocaleId(int localeId);
        MChapter GetByLocaleId(int localeId);
        MChapter Get(string name, int parentId = -2);
    }
}
