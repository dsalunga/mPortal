using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Common.Utilities;
using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.Content
{
    public struct ContentConstants
    {
        public const string USER_EDITOR_CONFIG = "Content.IsPlainTextDefault";
        public const string IsContentActive = "IsContentActive";
    }

    public class ContentHelper
    {
        public static bool IsPlainTextDefault()
        {
            if (WSession.Current.IsLoggedIn)
            {
                var user = WSession.Current.User;

                return DataHelper.GetBool(user.GetParameterValue(ContentConstants.USER_EDITOR_CONFIG), false);
            }

            return false;
        }

        public static string GetElementContent(WContext context)
        {
            string contentText = string.Empty;
            var objectContent = WebObjectContent
                    .GetByObjectId(context.ObjectId, context.RecordId);

            if (objectContent != null)
            {
                var content = objectContent.Content;
                if (content != null && !content.IsDraft)
                {
                    // var element = context.Element;
                    // var activeContent = element.GetParameterValue("IsContentActive", null);
                    if (content.IsActiveContent) // if (DataHelper.IsTrue(activeContent)){
                    {
                        var providers = context.ValueProvider;
                        contentText = Substituter.Substitute(content.Content, providers);
                    }
                    else
                    {
                        contentText = content.Content;
                    }
                }
            }

            return contentText;
        }
    }
}
