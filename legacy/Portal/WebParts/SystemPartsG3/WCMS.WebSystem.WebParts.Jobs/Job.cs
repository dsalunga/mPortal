using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Framework.Core;

using WCMS.WebSystem.WebParts.Jobs.Providers;

namespace WCMS.WebSystem.WebParts.Jobs
{
    public class Job : WebObjectBase, ISelfManager
    {
        private static IJobProvider _provider;

        static Job()
        {
            _provider = new JobSqlProvider();
        }

        public string Title { get; set; }
        public string Description { get; set; }

        public override int OBJECT_ID
        {
            get { return -1; }
        }

        public static IJobProvider Provider { get { return _provider; } }


        public bool Delete()
        {
            throw new NotImplementedException();
        }

        public int Update()
        {
            throw new NotImplementedException();
        }
    }
}
