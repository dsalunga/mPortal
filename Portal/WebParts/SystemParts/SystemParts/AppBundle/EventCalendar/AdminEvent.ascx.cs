using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using WCMS.Common.Utilities;
using WCMS.Framework;
using WCMS.Framework.Core;
using WCMS.Framework.Utilities;
using WCMS.WebSystem.Controls;

namespace WCMS.WebSystem.WebParts.EventCalendar
{
    public partial class AdminEvent : UserControl
    {
        protected ISaveInFolder SaveInFolder1;
        protected TabControl TabControl1;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Setup tab navigation
                this.InitTabControl();
                this.Initialize();

                int calendarId = -1;

                CalendarEvent item = null;

                var context = new WContext(this);
                var element = context.ParameterizedObject;

                int eventId = DataHelper.GetId(Request, "EventId");
                if (eventId > 0 && (item = CalendarEvent.Get(eventId)) != null)
                {
                    txtSubject.Text = item.Subject;
                    txtMessage.Value = item.Message;
                    if (item.LocationId > 0)
                    {
                        cboLocations.SelectedValue = item.LocationId.ToString();
                        txtLocation.Text = string.Empty;
                    }
                    else
                    {
                        txtLocation.Text = item.LocationString;
                        chkOtherLocation.Checked = true;
                    }

                    calendarId = item.CalendarId;

                    cboCategory.SelectedValue = item.CategoryId.ToString();
                    txtStartDate.Text = item.StartDate.ToString("dd-MMM-yyyy hh:mm tt");
                    txtEndDate.Text = item.EndDate.ToString("dd-MMM-yyyy hh:mm tt");

                    if (item.RepeatUntil == WConstants.DateTimeMinValue)
                        chkNoEnd.Checked = true;
                    else
                        txtRepeatUntil.Text = item.RepeatUntil.ToString("dd-MMM-yyyy hh:mm tt");

                    rblRecurrence.SelectedValue = item.RecurrenceId.ToString();
                    hRecipients.Value = item.ReminderTo;
                    cboReminderBefore.SelectedValue = item.ReminderBefore.ToString();
                    SaveInFolder1.FolderId = SaveInFolder1.GetFolder(item.OBJECT_ID, item.Id);

                    rblSendVia.SelectedValue = item.SendReminderVia.ToString();

                    // Select Template from TemplateId or Category
                    if (item.TemplateId > 0)
                    {
                        if (cboTemplates.Items.FindByValue(item.TemplateId.ToString()) != null)
                            cboTemplates.SelectedValue = item.TemplateId.ToString();
                    }
                    else if (item.CalendarId > 0)
                    {
                        var category = item.Category;
                        if (category != null && category.TemplateId > 0)
                            if (cboTemplates.Items.FindByValue(category.TemplateId.ToString()) != null)
                                cboTemplates.SelectedValue = category.TemplateId.ToString();
                    }

                    // Populate the weekdays
                    if (item.Weekdays > 0)
                    {
                        int weekdays = item.Weekdays;
                        for (int i = 64; i >= 1; i /= 2)
                        {
                            if (weekdays / i >= 1)
                            {
                                cblWeekdays.Items.FindByValue(i.ToString()).Selected = true;
                                weekdays -= i;
                            }
                        }
                    }

                    cmdDelete.Visible = true;
                    cmdSendReminder.Visible = true;

                    gridRecipients.DataBind();
                }
                else
                {
                    var defaultRecipients = element.GetParameterValue("DefaultRecipients");
                    if (!string.IsNullOrEmpty(defaultRecipients))
                    {
                        hRecipients.Value = defaultRecipients;
                        gridRecipients.DataBind();
                    }
                }

                // Set Calendar

                if (calendarId == -1)
                    calendarId = context.GetId("CalendarId");

                // Check if in EditMode
                if (context.ContextType == WContextTypes.EditMode)
                {
                    if (calendarId == -1)
                    {
                        var tmpCalendarId = DataHelper.GetId(element.GetParameterValue("CalendarId", "-1"));
                        if (tmpCalendarId > 0)
                            calendarId = tmpCalendarId;
                    }

                    cboCalendar.Enabled = false;
                }

                ListItem listItem = null;
                if (calendarId > 0 && (listItem = cboCalendar.Items.FindByValue(calendarId.ToString())) != null)
                    listItem.Selected = true;

                // Doesn't work?
                TabControl1.SelectedIndex = 0;

                // BaseGroup
                var baseGroup = element.GetParameterValue("BaseGroup", string.Empty);
                if (!string.IsNullOrEmpty(baseGroup))
                    hBaseGroup.Value = baseGroup;
            }
        }

        protected void TabControl1_SelectedTabChanged(object oSender, TabEventArgs args)
        {
            switch (args.TabName)
            {
                case "tabGeneral":
                    TabControl1.SelectedIndex = 0;
                    MultiView1.SetActiveView(viewGeneral);
                    break;

                case "tabRecurrence":
                    TabControl1.SelectedIndex = 1;
                    MultiView1.SetActiveView(viewRecurrence);
                    break;

                case "tabReminder":
                    TabControl1.SelectedIndex = 2;
                    MultiView1.SetActiveView(viewReminder);
                    break;

                case "tabMisc":
                    TabControl1.SelectedIndex = 3;
                    MultiView1.SetActiveView(viewMisc);
                    break;
            }
        }

        protected void cmdCancel_Click(object sender, EventArgs e)
        {
            int eventId = DataHelper.GetId(Request, "EventId");

            if (eventId > 0)
                this.ReturnToEventView();
            else
                this.Return();
        }

        private void Return()
        {
            var query = new WQuery(this);
            query.Remove("Load");
            query.Remove("EventId");
            query.Redirect();
        }

        private void ReturnToEventView(int eventId = -1)
        {
            var query = new WQuery(this);
            if (eventId > 0)
                query.Set("EventId", eventId);

            query.LoadAndRedirect("AdminEventView.ascx");
        }

        private void InitTabControl()
        {
            TabControl1.AddTab("tabGeneral", "General", true);
            TabControl1.AddTab("tabRecurrence", "Recurrence", true);
            TabControl1.AddTab("tabReminder", "Reminder", true);
            TabControl1.AddTab("tabMisc", "Misc", true);
        }

        private void Initialize()
        {
            string dateString = Request["Date"];
            DateTime date = DateTimeHelper.ParseTicks(dateString);

            cboLocations.DataSource = CalendarLocation.GetList();
            cboLocations.DataBind();

            cboCalendar.DataSource = CalendarItem.Provider.GetList();
            cboCalendar.DataBind();

            rblRecurrence.DataSource = CalendarRecurrence.GetList();
            rblRecurrence.DataBind();
            rblRecurrence.SelectedIndex = 0;

            for (int i = 1; i <= 64; i *= 2)
            {
                cblWeekdays.Items.Add(new ListItem
                {
                    Value = i.ToString(),
                    Text = WeekdaysEnum.GetName(i)
                });
            }

            cboCategory.DataSource = CalendarCategory.GetList();
            cboCategory.DataBind();

            cboReminderBefore.Items.AddRange(
                new ListItem[] { 
                    new ListItem("No reminder", "-1"),
                    new ListItem("At start", "0"),
                    //new ListItem("5 minutes", "5"),
                    new ListItem("15 minutes", "15"),
                    new ListItem("30 minutes", "30"),
                    new ListItem("1 hour", "60"),
                    new ListItem("2 hours", "120"),
                    new ListItem("6 hours", (6*60).ToString()),
                    new ListItem("12 hours", (60*12).ToString()),
                    new ListItem("1 day", (60*24).ToString()),
                    new ListItem("2 days", (60*48).ToString()),
                    new ListItem("3 days", (60*24*3).ToString()),
                    new ListItem("1 week", (60*24*7).ToString()),
                    new ListItem("2 weeks", (60*24*14).ToString()),
                    new ListItem("3 weeks", (60*24*21).ToString()),
                });
            cboReminderBefore.SelectedIndex = 0;

            DateTime endDate = date.TimeOfDay.Ticks == 0 ? date : date.AddMinutes(30);

            txtStartDate.Text = DateTimeHelper.ToCompactDateTime(date, "dd-MMM-yyyy", "dd-MMM-yyyy hh:mm tt");
            txtEndDate.Text = DateTimeHelper.ToCompactDateTime(endDate, "dd-MMM-yyyy", "dd-MMM-yyyy hh:mm tt");
            txtRepeatUntil.Text = txtEndDate.Text;
            //txtReminderToEmail.Text = WebRegistry.SelectNode("/Apps/EventCalendar/DefaultReminderToEmail").Value;

            // Template / Theme
            cboTemplates.DataSource = CalendarTemplate.Provider.GetList();
            cboTemplates.DataBind();
        }

        protected void cmdUpdate_Click(object sender, EventArgs e)
        {
            int eventId = DataHelper.GetId(Request, "EventId");

            CalendarEvent item = eventId > 0 ? CalendarEvent.Get(eventId) : new CalendarEvent();
            if (item == null)
                item = new CalendarEvent();

            int weekdays = 0;
            for (int i = 1; i <= 64; i *= 2)
                if (cblWeekdays.Items.FindByValue(i.ToString()).Selected)
                    weekdays += i;

            var recurrenceId = DataHelper.GetId(rblRecurrence.SelectedValue);
            if (recurrenceId == RecurrenceType.Weekly && weekdays == 0)
            {
                lblStatus.InnerHtml = "You have selected a Weekly recurrence but no Weekdays were selected. Kindly select at least one Weekday.";
                return;
            }

            var categoryId = DataHelper.GetId(cboCategory.SelectedValue);
            var templateId = DataHelper.GetInt32(cboTemplates.SelectedValue);
            var startDate = Convert.ToDateTime(txtStartDate.Text.Trim());
            var endDate = Convert.ToDateTime(txtEndDate.Text.Trim());

            if (endDate < startDate)
            {
                lblStatus.InnerHtml = "End Date must be equal or later than Start Date.";
                return;
            }

            item.Subject = txtSubject.Text.Trim();
            item.Message = txtMessage.Value.Trim();
            item.LocationString = txtLocation.Text.Trim();
            item.CategoryId = categoryId;
            item.StartDate = startDate;
            item.EndDate = endDate;
            item.RecurrenceId = recurrenceId;
            item.RepeatUntil = chkNoEnd.Checked ? WConstants.DateTimeMinValue : Convert.ToDateTime(txtRepeatUntil.Text.Trim());
            item.ReminderTo = hRecipients.Value; //txtReminderToEmail.Text.Trim().Replace(",", ";");
            item.ReminderBefore = Convert.ToInt32(cboReminderBefore.SelectedValue);
            item.SendReminderVia = DataHelper.GetInt32(rblSendVia.SelectedValue);
            item.Weekdays = weekdays;

            if (categoryId > 0)
            {
                var category = CalendarCategory.Get(categoryId);
                if (category != null && category.TemplateId > 0 && templateId == category.TemplateId)
                    item.TemplateId = -1;
                else
                    item.TemplateId = templateId;
            }
            else
            {
                item.TemplateId = templateId;
            }

            // Set Calendar
            if (cboCalendar.Items.Count > 0)
                item.CalendarId = DataHelper.GetId(cboCalendar.SelectedValue);

            if (!chkOtherLocation.Checked)
            {
                item.LocationId = DataHelper.GetId(cboLocations.SelectedValue);
                item.LocationString = string.Empty;
            }
            else
            {
                item.LocationId = -1;
                item.LocationString = txtLocation.Text.Trim();
            }

            if (item.LocationId > 0 && item.Location != null)
            {
                DateTime nextOccurence = item.GetNextOccurence();
                DateTime eventEnd = nextOccurence.Add(item.Length);
                if (!item.Location.IsAvailable(nextOccurence, eventEnd, item))
                {
                    lblLocationAvailability.InnerHtml = "Not available";
                    lblLocationAvailability.Style["color"] = "red";
                    return;
                }
            }
            else if (string.IsNullOrEmpty(item.LocationString))
            {
                lblLocationAvailability.InnerHtml = "Please select a valid location";
                lblLocationAvailability.Style["color"] = "red";
                return;
            }

            item.Update();

            // Save a link on WFS (WebFS)
            if (panelSaveTo.Visible)
                SaveInFolder1.Update(item.Subject, CalendarEvent.ID, item.Id);

            this.ReturnToEventView(item.Id);
        }

        protected void cmdDelete_Click(object sender, EventArgs e)
        {
            int eventId = DataHelper.GetId(Request, "EventId");
            CalendarEvent.Delete(eventId);

            this.Return();
        }

        protected void cmdSendReminder_Click(object sender, EventArgs e)
        {
            int eventId = DataHelper.GetId(Request, "EventId");
            CalendarEvent evnt = null;

            if (eventId > 0 && (evnt = CalendarEvent.Get(eventId)) != null)
            {
                EmailReminder reminder = new EmailReminder(evnt);
                if (reminder.Send())
                {
                    lblStatus.InnerHtml = "Reminder sent at " + DateTime.Now;
                    return;
                }
                else
                {
                    lblStatus.InnerHtml = "Could not send the reminder. Probably the event has occurred in the past. Check the occurrence settings properly.";
                }
            }
            else
            {
                lblStatus.InnerHtml = "This event is invalid.";
            }
        }

        protected void cmdCheckLocation_Click(object sender, EventArgs e)
        {
            int locationId = DataHelper.GetId(cboLocations.SelectedValue);
            CalendarLocation location = CalendarLocation.Get(locationId);
            if (location != null)
            {
                int eventId = DataHelper.GetId(Request, "EventId");

                CalendarEvent item = eventId > 0 ? CalendarEvent.Get(eventId) : new CalendarEvent();
                if (item == null)
                    item = new CalendarEvent();

                int weekdays = 0;
                for (int i = 1; i <= 64; i *= 2)
                    if (cblWeekdays.Items.FindByValue(i.ToString()).Selected)
                        weekdays += i;

                item.Subject = txtSubject.Text.Trim();
                item.Message = txtMessage.Value.Trim();
                item.LocationString = txtLocation.Text.Trim();
                item.CategoryId = DataHelper.GetId(cboCategory.SelectedValue);
                item.StartDate = Convert.ToDateTime(txtStartDate.Text.Trim());
                item.EndDate = Convert.ToDateTime(txtEndDate.Text.Trim());
                item.RecurrenceId = DataHelper.GetId(rblRecurrence.SelectedValue);
                item.RepeatUntil = Convert.ToDateTime(txtRepeatUntil.Text.Trim());
                item.ReminderTo = hRecipients.Value; //txtReminderToEmail.Text.Trim().Replace(",", ";");
                item.ReminderBefore = Convert.ToInt32(cboReminderBefore.SelectedValue);
                item.Weekdays = weekdays;

                if (!chkOtherLocation.Checked)
                {
                    item.LocationId = DataHelper.GetId(cboLocations.SelectedValue);
                    item.LocationString = string.Empty;
                }
                else
                {
                    item.LocationId = -1;
                    item.LocationString = txtLocation.Text.Trim();
                }

                DateTime nextOccurence = item.GetNextOccurence();
                DateTime eventEnd = nextOccurence.Add(item.Length);
                if (location.IsAvailable(nextOccurence, eventEnd, item))
                {
                    lblLocationAvailability.InnerHtml = "Available";
                    lblLocationAvailability.Style["color"] = "green";
                }
                else
                {
                    lblLocationAvailability.InnerHtml = "Not available";
                    lblLocationAvailability.Style["color"] = "red";
                }

                // Update location status
                foreach (ListItem listItem in cboLocations.Items)
                {
                    int locId = DataHelper.GetId(listItem.Value);
                    CalendarLocation loc = null;
                    if (locId > 0 && (loc = CalendarLocation.Get(locId)) != null)
                        if (!loc.IsAvailable(nextOccurence, eventEnd, item))
                            listItem.Text = loc.Name + "*";
                }
            }
        }

        public DataSet GetRecipients(string customRecipients)
        {
            var includeList = DataHelper.ParseDelimitedStringToList(customRecipients, AccountConstants.AccountDelimiter);

            List<WebUser> includeUserList = new List<WebUser>();
            List<WebGroup> includeGroupList = new List<WebGroup>();

            List<string> includeEmailList = new List<string>();
            List<string> includeMobileList = new List<string>();

            if (includeList.Count > 0)
            {
                // Convert includeList to lists for Users and Groups
                foreach (string include in includeList)
                {
                    var account = AccountHelper.FromAccountString(include);
                    if (account != null)
                    {
                        if (account.OBJECT_ID == WebObjects.WebUser)
                            includeUserList.Add(account as WebUser);
                        else if (account.OBJECT_ID == WebObjects.WebGroup)
                            includeGroupList.Add(account as WebGroup);
                    }
                    else
                    {
                        if (Validator.IsRegexMatch(include, RegexPresets.Email))
                            includeEmailList.Add(include);
                        else if (Validator.IsRegexMatch(include, RegexPresets.MobileNumber))
                            includeMobileList.Add(include);
                    }
                }
            }

            var result = from g in includeGroupList
                         select new
                         {
                             Id = g.ToUniqueShortString(),
                             g.Name,
                             Email = "",
                             MobileNumber = ""
                         };

            if (includeUserList.Count > 0)
            {
                result = result.Union(from user in includeUserList
                                      select new
                                      {
                                          Id = user.ToUniqueShortString(),
                                          Name = user.FirstAndLastName,
                                          user.Email,
                                          user.MobileNumber
                                      });
            }

            if (includeEmailList.Count > 0)
            {
                result = result.Union(from c in includeEmailList
                                      select new
                                      {
                                          Id = c,
                                          Name = "",
                                          Email = c,
                                          MobileNumber = ""
                                      });
            }

            if (includeMobileList.Count > 0)
            {
                result = result.Union(from c in includeMobileList
                                      select new
                                      {
                                          Id = c,
                                          Name = "",
                                          Email = "",
                                          MobileNumber = c
                                      });
            }

            return DataHelper.ToDataSet(result);
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "Custom_Delete":
                    QueryParser query = new QueryParser(this);
                    string shortString = e.CommandArgument.ToString();

                    // Check if present in custom recipients list
                    var includedList = DataHelper.ParseDelimitedStringToList(hRecipients.Value.Trim(), AccountConstants.AccountDelimiter);
                    if (includedList.Count > 0)
                    {
                        foreach (var included in includedList)
                        {
                            if (included == shortString)
                            {
                                includedList.Remove(included);
                                hRecipients.Value = DataHelper.ToDelimitedString(includedList, AccountConstants.AccountDelimiter);
                                break;
                            }
                        }
                    }

                    gridRecipients.DataBind();
                    break;
            }
        }

        protected void cmdAdd_Click(object sender, EventArgs e)
        {
            if (AddRecipients(txtAdd.Text.Trim()))
                txtAdd.Text = string.Empty;
        }

        private bool AddRecipients(string newAccounts)
        {
            var includeList = DataHelper.ParseDelimitedStringToList(hRecipients.Value, AccountConstants.AccountDelimiter);

            bool accountAdded = false;

            if (!string.IsNullOrEmpty(newAccounts))
            {
                var accountList = DataHelper.ParseDelimitedStringToList(newAccounts, AccountConstants.AccountDelimiter);
                if (accountList.Count > 0)
                {
                    //List<WebGroup> groups = new List<WebGroup>();
                    foreach (string item in accountList)
                    {
                        string shortString = string.Empty;

                        var account = AccountHelper.FromAccountString(item);
                        if (account != null)
                        {
                            INameWebObject a = null;
                            if (includeList.FirstOrDefault(i => (a = AccountHelper.FromAccountString(i)) != null && (a.OBJECT_ID == account.OBJECT_ID && a.Id == account.Id)) == null)
                            {
                                if (account.OBJECT_ID == WebObjects.WebGroup)
                                    shortString = (account as WebGroup).ToUniqueString();
                                else if (account.OBJECT_ID == WebObjects.WebUser)
                                    shortString = (account as WebUser).UserName;
                            }
                        }
                        else
                        {
                            if (Validator.IsRegexMatch(item, RegexPresets.Email) || Validator.IsRegexMatch(item, RegexPresets.MobileNumber))
                                shortString = item;
                            else
                                continue;
                        }

                        if (!string.IsNullOrWhiteSpace(shortString))
                        {
                            if (!accountAdded)
                                accountAdded = true;

                            includeList.Add(shortString);
                        }
                    }
                }
            }

            if (accountAdded)
            {
                hRecipients.Value = DataHelper.ToDelimitedString(includeList, AccountConstants.AccountDelimiter);

                gridRecipients.DataBind();

                return true;
            }
            else
            {
                lblStatus.InnerHtml = "Account does not exist or invalid recipient type.";
            }

            return false;
        }

        protected void cmdReset_Click(object sender, EventArgs e)
        {
            txtAdd.Text = string.Empty;

            hRecipients.Value = string.Empty;
            gridRecipients.DataBind();
        }
    }
}