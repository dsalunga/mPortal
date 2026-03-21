using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCMS.Framework.Core.SqlProvider
{
    public class WebCategoryProvider : GenericSqlDataProviderBase<WebCategory>, IWebCategoryProvider
    {
        protected override string SelectProcedure { get { return "WebCategory_Get"; } }
        protected override string DeleteProcedure { get { return "WebCategory_Del"; } }

        protected override WebCategory From(IDataReader r, WebCategory source)
        {
            WebCategory item = new WebCategory();

            return item;
        }

        public IEnumerable<WebCategory> GetList(int objectId, int parentId = -2)
        {
            List<WebCategory> items = new List<WebCategory>();

            return items;
        }

        public override int Update(WebCategory item)
        {
            return item.Id;
        }
    }
}
