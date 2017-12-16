using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.WebSystem.WebParts.Article
{
    /// <summary>
    /// Article Link Column Constants
    /// </summary>
    public struct LinkColumns
    {
        public const int SID = 1;
        public const int STitle = 2;
        public const int SImage = 3;
        public const int SDescription = 4;
        public const int SDate = 5;
        public const int SContent = 6;
        public const int SAuthor = 7;
        public const int ItemLink = 8;
        public const int BackLink = 9;
        public const int ItemDiv = 11;
        public const int BackDiv = 13;

        public const int ItemStyleOrClass = 10;
        public const int BackStyleOrClass = 12;
        public const int PageStyleOrClass = 14;
    }

    /// <summary>
    /// Instance means, instance of the WebPart
    /// </summary>
    public struct SubscriptionModes
    {
        public const int AllGroups = 0;
        public const int GroupSpecific = 1;
        public const int ByInstance = 2;
        public const int AllGroupsPlusInstance = 3;
    }

    public struct ArticleConstants
    {
        public const string StyleFormat = @"style=""{0}"" ";
        public const string ClassFormat = @"class=""{0}""";
        public const string LinkFormat = "<a href=\"{0}\">{1}</a>";
        public const string ListLinkFormat = "<a href=\"{0}\"><div style=\"{1}\">{2}</div></a>";

        public const string DateFormatString = "{0:MMMM d, yyyy}";
        public const string ShortDateFormat = "dd-MMM";
        public const string ShortDateFormatFull = "dd-MMM-yy";

        public const string ListTemplate = "<h2>$(Page:Title)</h2><div>$(Content)</div>";
        public const string ItemTemplate = "";
        public const string DetailsTemplate = "";

        public const string ListTemplateKey = "ListTemplate";
        public const string ItemTemplateKey = "ItemTemplate";
        public const string FirstItemTemplateKey = "FirstItemTemplate";
        public const string DetailsTemplateKey = "DetailsTemplate";
        public const string DateFormatStringKey = "DateFormat";

        public const int MaxDisplayItem = 5;
        public const string MaxDisplayItemKey = "MaxDisplayItem";

        public const string ModeKey = "SubscriptionMode";
        public const string GroupKey = "SubscriptionGroup";
        public const string UsePageParameterKey = "UsePageParameter";

        public const string ParameterSetNameKey = "ParameterSetName";
        public const string IgnoreGroupsKey = "IgnoreGroups";
        public const string EmailTemplateFileKey = "EmailTemplateFile";

        public const string ArticleId = "ArticleId";

        // Tags
        public const string TagKey = "Tag";
    }
}
