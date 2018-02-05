using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WCMS.Framework.Core;
using WCMS.WebSystem.Apps.Integration.Providers;

namespace WCMS.WebSystem.Apps.Integration
{
    public class MCComposer : NamedWebObject, ISelfManager
    {
        private static IMCComposerProvider _provider = new MCComposerProvider();

        public MCComposer()
        {
            Entry = string.Empty;
            Locale = string.Empty;
            Work = string.Empty;
            Description = string.Empty;
            PhotoFile = string.Empty;
            NickName = string.Empty;

            Active = 1;
            CompetitionId = -1;
        }

        public string Entry { get; set; }
        public string Locale { get; set; }
        public string Work { get; set; }
        public string Description { get; set; }
        public string PhotoFile { get; set; }
        public string NickName { get; set; }
        public int Active { get; set; }
        public int CompetitionId { get; set; }

        public bool IsActive
        {
            get { return Active == 1; }
            set { Active = value ? 1 : 0; }
        }

        public static IMCComposerProvider Provider { get { return _provider; } }

        public override int OBJECT_ID { get { return -1; } }

        public bool Delete()
        {
            return _provider.Delete(this.Id);
        }

        public int Update()
        {
            return _provider.Update(this);
        }
    }
}
