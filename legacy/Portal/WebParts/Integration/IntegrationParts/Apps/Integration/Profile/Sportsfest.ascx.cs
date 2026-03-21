using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core.Shared;
using WCMS.WebSystem.Apps.Integration;
using WCMS.WebSystem.Apps.Integration;

namespace WCMS.WebSystem.WebParts.Profile
{
    public partial class SportsfestController : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                for (int i = 4; i < 81; i++)
                    cboAge.Items.Add(i.ToString());

                WContext context = new WContext(this);
                var element = context.Element;

                string groupsPath = element.GetParameterValue(MemberConstants.LocaleGroupPathKey, MemberConstants.LocaleGroupPath);
                var groups = WebGroup.SelectNode(groupsPath).Children;
                if (groups.Count() > 0)
                {
                    // Set Locale Group
                    foreach (var group in groups)
                    {
                        var spaceIdx = group.Name.IndexOf(" ");
                        cboGroupColor.Items.Add(group.Name.Substring(0, spaceIdx));
                    }
                }

                var when = element.GetParameterValue("When");
                if (!string.IsNullOrEmpty(when))
                {
                    lblWhen.InnerHtml = when;
                    panelWhen.Visible = true;
                }

                var where = element.GetParameterValue("Where");
                if (!string.IsNullOrEmpty(where))
                {
                    lblWhere.InnerHtml = where;
                    panelWhere.Visible = true;
                }

                var whereExtra = element.GetParameterValue("Where-Extra");
                if (!string.IsNullOrEmpty(whereExtra))
                {
                    lblWhereExtra.Visible = true;
                    lblWhereExtra.InnerHtml = whereExtra;
                }

                var title = element.GetParameterValue("Title");
                if (!string.IsNullOrEmpty(title))
                {
                    lblTitle.InnerHtml = title;

                    linkReturn.InnerHtml = string.Format("Return to {0}", title);

                    this.Page.Title = title;
                }

                var user = WSession.Current.User;
                if (user != null)
                {
                    txtName.Text = user.FirstAndLastName;
                    txtMobile.Text = user.MobileNumber;

                    var group = MemberHelper.GetLocaleGroup(user.Id);
                    if (!string.IsNullOrEmpty(group))
                    {
                        var spaceIdx = group.IndexOf(" ");
                        var groupColor = group.Substring(0, spaceIdx);

                        ListItem item = null;
                        if (!string.IsNullOrEmpty(groupColor) && (item = cboGroupColor.Items.FindByText(groupColor)) != null)
                            item.Selected = true;
                    }
                }
            }
        }

        protected void cmdSubmit_Click(object sender, EventArgs e)
        {
            int age = 0;
            if (!int.TryParse(cboAge.SelectedItem.Text, out age) || age < 1)
            {
                lblStatus.Text = "Please select your age.";
                return;
            }

            if (cboGroupColor.SelectedIndex == 0)
            {
                lblStatus.Text = "Please select your Group Color.";
                return;
            }

            string sports = rblSet1.SelectedIndex >= 0 && (rblSet1.SelectedIndex + 1 < rblSet1.Items.Count) ? rblSet1.SelectedValue : string.Empty;
            if (rblSet2.SelectedIndex >= 0 && (rblSet2.SelectedIndex + 1 < rblSet2.Items.Count))
            {
                if (sports.Length > 0)
                    sports += ", ";

                sports += rblSet2.SelectedValue;
            }

            //if (string.IsNullOrEmpty(sports))
            //{
            //    lblStatus.Text = "Please select at least one sport.";
            //    return;
            //}

            if (!chkAgree1.Checked || !chkAgree2.Checked)
            {
                lblStatus.Text = "Please agree to all terms by ticking the 2 checkboxes.";
                return;
            }


            string name = txtName.Text.Trim();

            var item = Sportsfest.Provider.Get(name);
            if (item == null)
            {
                item = new Sportsfest();
                item.Name = name;
                item.GroupColor = cboGroupColor.Text;
                item.ShirtSize = cboShirtSize.SelectedValue;
                item.Age = age;
                item.Mobile = txtMobile.Text.Trim();
                item.Locale = txtLocale.Text.Trim();
                item.Suggestion = txtSuggestion.Text.Trim();
                item.CountryCode = DataUtil.GetId(cboCountry.SelectedValue);
                item.Sports = sports;
                item.Update();

                panelRegistration.Visible = false;
                panelDone.Visible = true;


                lblSports.InnerHtml = string.IsNullOrEmpty(sports) ? "NONE" : sports;
                linkReturn.HRef = (new WContext(this)).BuildQuery();
            }
            else
            {
                lblStatus.Text = "Sorry, you can register only once.";
                return;
            }
        }

        public IEnumerable<Country> GetCountries()
        {
            return Country.GetList();
        }
    }
}