using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;

using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.Content
{
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class ContentService
    {
        // To use HTTP GET, add [WebGet] attribute. (Default ResponseFormat is WebMessageFormat.Json)
        // To create an operation that returns XML,
        //     add [WebGet(ResponseFormat=WebMessageFormat.Xml)],
        //     and include the following line in the operation body:
        //         WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
        [OperationContract]
        public void DoWork()
        {
            // Add your operation implementation here
            return;
        }

        // Add more operations here and mark them with [OperationContract]

        [OperationContract]
        public string GetContent(int id)
        {
            WebContent item = id > 0 ? WebContent.Get(id) : null;
            if (item != null)
                return item.Content;

            return string.Empty;
        }

        [OperationContract]
        public string GetContentByTitle(string title)
        {
            WebContent item = string.IsNullOrEmpty(title) ? null : WebContent.Provider.Get(title);
            if (item != null)
                return item.Content;

            return string.Empty;
        }

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        public string GetContentJson(int id)
        {
            //WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
            return GetContent(id);
        }

        [OperationContract]
        [WebGet(ResponseFormat = WebMessageFormat.Json)]
        public string GetContentByTitleJson(string title)
        {
            //WebOperationContext.Current.OutgoingResponse.ContentType = "text/xml";
            return GetContentByTitle(title);
        }
    }
}
