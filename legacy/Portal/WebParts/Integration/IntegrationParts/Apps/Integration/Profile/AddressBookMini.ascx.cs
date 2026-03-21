using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;

using WCMS.Common;
using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Diagnostics;

using WCMS.Framework.Utilities;

using WCMS.WebSystem.Apps.Integration;
using WCMS.WebSystem.Apps.Integration;

namespace WCMS.WebSystem.WebParts.Profile
{
    public partial class AddressBookMiniController : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var watch = PerformanceLog.StartLog();
            var context = new WContext(this);

            try
            {
                var element = context.Element;
                var paramSet = element.GetParameterSet(true);

                var itemTemplate = paramSet.GetParameterValue(ParameterKeys.ItemTemplate);
                var containerTemplate = paramSet.GetParameterValue(ParameterKeys.ContainerTemplate);
                var emptyTemplate = paramSet.GetParameterValue(ParameterKeys.EmptyTemplate);
                var keyword = paramSet.GetParameterValue(ParameterKeys.CustomFilter);

                // 0 - filter by Month, 1 to 12 specific celebrants month
                var celebrantsFilter = DataUtil.GetInt32(element.GetParameterValue(ParameterKeys.CelebrantsFilter), -1);
                var maxItems = DataUtil.GetInt32(element.GetParameterValue(ParameterKeys.MaxItems), -1);
                var mainPageUrl = element.GetParameterValue(ParameterKeys.MainPageUrl, "#");
                var groupPath = element.GetParameterValue(ParameterKeys.ParentGroup);
                var userProfileUrlFormat = element.GetParameterValue("UserProfileUrlFormat", "");
                WebGroup group = null;

                if (celebrantsFilter > 12)
                    celebrantsFilter = -1;
                else if (celebrantsFilter == 0)
                    celebrantsFilter = DateTime.Now.Month;

                if (!string.IsNullOrEmpty(groupPath) && (group = WebGroup.SelectNode(groupPath)) != null)
                {
                    var groupId = group.Id;
                    var userDetailsFormat = !string.IsNullOrEmpty(userProfileUrlFormat) ? userProfileUrlFormat : MemberConstants.UserProfilePageFormat;
                    var viewerIsAdmin = WSession.Current.IsAdministrator;
                    var loweredKeyword = string.IsNullOrEmpty(keyword) ? string.Empty : keyword.ToLower();
                    var members = MemberLink.Provider.GetList(MemberAccountStatus.Approved, celebrantsFilter);
                    var users = WebUser.GetList(groupId, 1);

                    MemberLink link = null;
                    var orderedResult = (from i in users
                                         where i != null && (link = members.FirstOrDefault(m => m.UserId == i.Id)) != null
                                            && (string.IsNullOrEmpty(loweredKeyword) ||
                                                 (DataUtil.HasMatch(i.UserName, loweredKeyword) ||
                                                     DataUtil.HasMatch(i.FirstName, loweredKeyword) ||
                                                     DataUtil.HasMatch(i.LastName, loweredKeyword) ||
                                                     DataUtil.HasMatch(i.MiddleName, loweredKeyword) ||
                                                     DataUtil.HasMatch(i.Email, loweredKeyword) ||
                                                     DataUtil.HasMatch(i.MobileNumber, loweredKeyword) ||
                                                     DataUtil.HasMatch(link.ExternalIdNo, loweredKeyword)))
                                         select new
                                         {
                                             i.Id,
                                             i.UserName,
                                             i.Email,
                                             DisplayName = AccountHelper.GetPrefixedName(i),
                                             PhotoPath = link.GetPhotoPathIfNull("200x200"),
                                             UserProfileUrl = string.Format(userDetailsFormat, i.Id),
                                             link.ExternalIdNo,
                                             link.MembershipDate,
                                             i.LastUpdate
                                         }).ToArray();


                    var sb = new StringBuilder();
                    NamedValueProvider values = null;

                    var count = orderedResult.Length;
                    if (count > 0)
                    {
                        var maxIndex = count - 1;
                        maxItems = count > maxItems ? maxItems : count;

                        var indexes = new int[maxIndex + 1];
                        for (int i = 0; i <= maxIndex; i++)
                            indexes[i] = i;
                        DataUtil.Shuffle(indexes);

                        for (int i = 0; i < maxItems; i++)
                        {
                            var item = orderedResult[indexes[i]];
                            values = new NamedValueProvider();
                            values.Add(TemplateKeys.ProfileUrl, item.UserProfileUrl);
                            values.Add(TemplateKeys.DisplayName, item.DisplayName);
                            values.Add(TemplateKeys.PhotoUrl, item.PhotoPath);
                            sb.Append(values.Substitute(itemTemplate));
                        }

                        if (!string.IsNullOrEmpty(containerTemplate))
                        {
                            if (celebrantsFilter > 0 && !string.IsNullOrEmpty(mainPageUrl) && !mainPageUrl.Equals("#"))
                            {
                                var query = new QueryParser(mainPageUrl);
                                query.Set("Celebrants", celebrantsFilter);
                                mainPageUrl = query.BuildQuery();
                            }

                            values = new NamedValueProvider();
                            values.Add(ParameterKeys.MainPageUrl, mainPageUrl);
                            values.Add(TemplateKeys.CelebrantsMonth, TimeUtil.GetMonthName(celebrantsFilter > 0 ? celebrantsFilter : 1));
                            values.Add(Substituter.DefaultKey, sb);
                            literalOutput.Text = Substituter.Substitute(containerTemplate, values);
                        }
                        else
                        {
                            literalOutput.Text = sb.ToString();
                        }
                    }
                    else if (!string.IsNullOrEmpty(emptyTemplate))
                    {
                        values = new NamedValueProvider();
                        values.Add(ParameterKeys.MainPageUrl, mainPageUrl);
                        values.Add(TemplateKeys.CelebrantsMonth, TimeUtil.GetMonthName());
                        literalOutput.Text = Substituter.Substitute(emptyTemplate, values);
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.WriteLog(ex);
            }

            PerformanceLog.EndLog(string.Format("AddressBook Mini: {0}/{1}", context.ObjectId, context.RecordId), watch, context.PageId);
        }
    }
}