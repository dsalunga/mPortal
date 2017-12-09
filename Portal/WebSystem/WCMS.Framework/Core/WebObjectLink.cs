namespace WCMS.Framework.Core
{
    class WebObjectLink : IWebObject
    {
        [ObjectColumn(true)]
        public int Id { get; set; }

        [ObjectColumn]
        public int LeftObjectId { get; set; }

        [ObjectColumn]
        public int LeftRecordId { get; set; }

        [ObjectColumn]
        public int RightObjectId { get; set; }

        [ObjectColumn]
        public int RightRecordId { get; set; }

        #region IWebObject Members


        public int OBJECT_ID
        {
            get { return -1; }
        }

        #endregion
    }
}
