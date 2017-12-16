using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.Common;
using WCMS.Common.Utilities;

using WCMS.Framework;
using WCMS.Framework.Core;

using WCMS.Framework.Utilities;

namespace WCMS.WebSystem.WebParts.Incident
{
    public class IncidentHelper
    {
        private const string STATUS_FS = "<span style=\"color: {0}; font-weight: bold\">{1}</span>";
        private const string SLA_FS = "<div style=\"width: 10px; height: 10px; background-color: {0}; float: left; margin-top: 4px; margin-right: 2px;\" title=\"{3}\"></div><a href=\"{2}\"><strong>{1}</strong></a>";
        private const string SLA_RUNNING_TIME_FS = "<div style=\"width: 10px; height: 10px; background-color: {0}; float: left; margin-top: 4px; margin-right: 2px;\"></div>{1}{2}";

        public static bool IsSupportUser(ParameterizedWebObject set, WebUser user)
        {
            if (set != null && user != null)
            {
                var group = GetBaseSupportGroup(set);
                if (group != null)
                    return user.IsMemberOf(group.Id);
            }

            return false;
        }

        public static bool IsSupportUser(WContext context, WebUser user)
        {
            if (user != null)
            {
                var set = context.Element.GetParameterSet();
                if (set != null)
                    return IsSupportUser(set, user);
            }

            return false;
        }

        public static string GetGroupName(int groupId)
        {
            var item = WebGroup.Get(groupId);
            return item != null ? item.Name : string.Empty;
        }

        public static string GetCategoryName(int categoryId)
        {
            var item = IncidentCategory.Provider.Get(categoryId);
            return item != null ? item.Name : string.Empty;
        }

        public static string GetTypeName(int typeId)
        {
            IncidentType item = typeId > 0 ? IncidentType.Provider.Get(typeId) : null;
            return item != null ? item.Name : string.Empty;
        }

        public static string FormatStatus(int status)
        {
            var statusString = TicketStatus.GetText(status);
            switch (status)
            {
                case TicketStatus.Completed:
                    return string.Format(STATUS_FS, "#FF0080", statusString);

                case TicketStatus.InProgress:
                    return string.Format(STATUS_FS, "#00f", statusString);

                case TicketStatus.Closed:
                    return string.Format(STATUS_FS, "#555", statusString);

                case TicketStatus.OnHold:
                    return string.Format(STATUS_FS, "#FF00FF", statusString);

                default:
                    return string.Format(STATUS_FS, "#000", statusString);
            }
        }

        public static string FormatIncidentSLA(IncidentTicket item, WContext context, TicketSlaInfo sla)
        {
            context.Set("TicketId", item.Id);

            var type = IncidentType.Provider.Get(item.TypeId);

            if (item.Status == TicketStatus.Closed || item.Status == TicketStatus.Completed || item.Status == TicketStatus.OnHold)
            {
                return string.Format(SLA_FS, "#777", item.TicketGuid, context.BuildQuery(), "SLA - Not applicable");
            }
            else
            {
                var utcNow = DateTime.UtcNow;
                var utcSubmitted = item.DateCreated.ToUniversalTime();

                if (type == null || type.UseSLA)
                {
                    var dateCreatedUtc = item.DateCreated.ToUniversalTime();
                    var slaDuration = sla.GetSlaDuration(item);
                    var warningLevelMinutes = sla.GetWarningLevelMinutes(slaDuration);

                    if (utcNow >= dateCreatedUtc.AddHours(slaDuration))
                        return string.Format(SLA_FS, "#F44365", item.TicketGuid, context.BuildQuery(), "SLA - Critical");
                    else if (utcNow > dateCreatedUtc.AddMinutes(warningLevelMinutes))
                        return string.Format(SLA_FS, "#ffff00", item.TicketGuid, context.BuildQuery(), "SLA - Warning");
                }
                else if (item.ETA > WConstants.DateTimeMinValue)
                {
                    var etaUtc = item.ETA.ToUniversalTime();
                    if (utcNow >= etaUtc)
                        return string.Format(SLA_FS, "#F44365", item.TicketGuid, context.BuildQuery(), "SLA - Critical");
                    else if (IsInWarningLevel(utcNow, utcSubmitted, etaUtc, sla.WarningPercentage))
                        return string.Format(SLA_FS, "#ffff00", item.TicketGuid, context.BuildQuery(), "SLA - Warning");
                }
            }

            return string.Format(SLA_FS, "#25EF2B", item.TicketGuid, context.BuildQuery(), "SLA - On-track");
        }

        public static bool IsInWarningLevel(DateTime utcNow, DateTime utcSubmitted, DateTime etaUtc, double warningPercentage)
        {
            if (etaUtc > utcNow)
                return utcNow >= utcSubmitted.AddMinutes(etaUtc.Subtract(utcSubmitted).TotalMinutes * warningPercentage);
            return false;
        }

        public static TicketSlaInfo GetSlaInfo(ParameterizedWebObject set)
        {
            var info = new TicketSlaInfo();
            info.LowSla = DataUtil.GetInt32(set.GetParameterValue(TicketSLA.LOW_SLA_KEY), TicketSLA.LOW_SLA_DURATION);
            info.NormalSla = DataUtil.GetInt32(set.GetParameterValue(TicketSLA.NORMAL_SLA_KEY), TicketSLA.NORMAL_SLA_DURATION);
            info.HighSla = DataUtil.GetInt32(set.GetParameterValue(TicketSLA.NORMAL_SLA_KEY), TicketSLA.NORMAL_SLA_DURATION);
            info.WarningPercentage = DataUtil.GetDouble(set.GetParameterValue(TicketSLA.SLA_WARNING_PERCENTAGE_KEY), TicketSLA.SLA_WARNING_PERCENTAGE);

            return info;
        }

        public static string DisplayRunningTime(IncidentTicket item, TicketSlaInfo sla)
        {
            var dateCompletedUtc = item.DateClosed.Year != WConstants.DateTimeMinValue.Year ? item.DateClosed.ToUniversalTime() : item.DateClosed;
            var dateCreatedUtc = item.DateCreated.ToUniversalTime();
            var runningTimeString = "0 mins";
            var utcNow = DateTime.UtcNow;
            var type = IncidentType.Provider.Get(item.TypeId);
            TimeSpan runningTime;

            if (item.Status != TicketStatus.Closed && item.Status != TicketStatus.Completed && item.Status != TicketStatus.OnHold)
            {
                // Ticket still running

                runningTime = utcNow - dateCreatedUtc;

                if (runningTime.Ticks > 0 && runningTime.TotalMinutes >= 1)
                    runningTimeString = TimeUtil.TimeSpanToString(runningTime, false, true);

                if (type == null || type.UseSLA)
                {
                    var slaDuration = sla.GetSlaDuration(item);
                    var warningLevelMinutes = sla.GetWarningLevelMinutes(slaDuration);

                    if (utcNow >= dateCreatedUtc.AddHours(slaDuration))
                        return string.Format(SLA_RUNNING_TIME_FS, "#F44365", runningTime.Ticks > 0 ? runningTimeString : "N/A", "");
                    else if (utcNow >= dateCreatedUtc.AddMinutes(warningLevelMinutes))
                        return string.Format(SLA_RUNNING_TIME_FS, "#ffff00", runningTime.Ticks > 0 ? runningTimeString : "N/A", "");
                }
                else if (item.ETA > WConstants.DateTimeMinValue)
                {
                    var etaUtc = item.ETA.ToUniversalTime();

                    if (utcNow >= etaUtc)
                        return string.Format(SLA_RUNNING_TIME_FS, "#F44365", runningTime.Ticks > 0 ? runningTimeString : "N/A", "");
                    else if (IsInWarningLevel(utcNow, dateCreatedUtc, etaUtc, sla.WarningPercentage))
                        return string.Format(SLA_RUNNING_TIME_FS, "#ffff00", runningTime.Ticks > 0 ? runningTimeString : "N/A", "");
                }

                return string.Format(SLA_RUNNING_TIME_FS, "#25EF2B", runningTime.Ticks > 0 ? runningTimeString : "N/A", "");
            }
            else
            {
                if (item.Status == TicketStatus.OnHold)
                    runningTime = utcNow - dateCreatedUtc;
                else
                    runningTime = dateCreatedUtc < dateCompletedUtc ? dateCompletedUtc - dateCreatedUtc : new TimeSpan();

                if (runningTime.Ticks > 0 && runningTime.TotalMinutes >= 1)
                    runningTimeString = TimeUtil.TimeSpanToString(runningTime, false, true);

                return string.Format(SLA_RUNNING_TIME_FS, "#777", runningTime.Ticks > 0 ? runningTimeString : "N/A", string.Format(" ({0})", TicketStatus.GetText(item.Status)));
            }
        }

        public static int GetSlaStatus(IncidentTicket item, TicketSlaInfo sla)
        {
            var type = IncidentType.Provider.Get(item.TypeId);

            if (item.Status != TicketStatus.Closed && item.Status != TicketStatus.Completed)
            {
                var utcNow = DateTime.UtcNow;
                var dateCreatedUtc = item.DateCreated.ToUniversalTime();

                if (type == null || type.UseSLA)
                {
                    int slaDuration = sla.GetSlaDuration(item);
                    var warningLevelMinutes = sla.GetWarningLevelMinutes(slaDuration);

                    if (utcNow >= dateCreatedUtc.AddHours(slaDuration))
                        return TicketSLA.SLA_STATUS_CRITICAL;
                    else if (utcNow >= dateCreatedUtc.AddMinutes(warningLevelMinutes))
                        return TicketSLA.SLA_STATUS_WARNING;
                    else
                        return TicketSLA.SLA_STATUS_ONTRACK;
                }
                else if (item.ETA > WConstants.DateTimeMinValue)
                {
                    var etaUtc = item.ETA.ToUniversalTime();

                    if (utcNow >= etaUtc)
                        return TicketSLA.SLA_STATUS_CRITICAL;
                    else if (IsInWarningLevel(utcNow, dateCreatedUtc, etaUtc, sla.WarningPercentage))
                        return TicketSLA.SLA_STATUS_WARNING;
                    else
                        return TicketSLA.SLA_STATUS_ONTRACK;
                }
            }

            return TicketSLA.SLA_STATUS_NA;
        }

        public static string GetUserDisplayName(int userId)
        {
            var user = WebUser.Get(userId);
            return user != null ? AccountHelper.GetPrefixedName(user, NamePrefixes.Brotherhood) : string.Empty;
        }

        /*
        public static string FormatUserDisplay(WebUser user, string formatString, bool firstNameOnly = true, bool showPreviewOnly = false)
        {
            if (user != null && !string.IsNullOrEmpty(formatString))
            {
                var displayName = AccountHelper.GetPrefixedName(user, NamePrefixes.Brotherhood, firstNameOnly);
                return FormatUserDisplay(user.Id, showPreviewOnly ? DataHelper.GetStringPreview(displayName, 18) : displayName, formatString);
            }

            return string.Empty;
        }

        public static string FormatUserDisplay(int userId, string displayName, string formatString)
        {
            if (!string.IsNullOrEmpty(formatString) && userId > 0 && !string.IsNullOrEmpty(displayName))
            {
                NamedValueProvider provider = new NamedValueProvider();
                provider.Add("UserId", userId);
                provider.Add("Name", displayName);

                return Substituter.Substitute(formatString, provider);
            }

            return displayName;
        }*/

        public static WebGroup GetBaseSupportGroup(WContext context)
        {
            return GetBaseSupportGroup(context.Element.GetParameterSet());
        }

        public static WebGroup GetBaseSupportGroup(ParameterizedWebObject set)
        {
            if (set != null)
            {
                string supportGroupPath = set.GetParameterValue("SupportGroupPath");
                if (!string.IsNullOrEmpty(supportGroupPath))
                {
                    var supportGroup = WebGroup.SelectNode(supportGroupPath);
                    if (supportGroup != null)
                        return supportGroup;
                }
            }

            return null;
        }

        public static WebGroup GetSupportGroup(WContext context, WebUser user)
        {
            if (user != null)
            {
                var baseGroup = GetBaseSupportGroup(context);
                if (baseGroup != null)
                {
                    var userGroups = user.Groups;
                    var supportGroups = baseGroup.Children;
                    foreach (var supportGroup in supportGroups)
                    {
                        var userGroup = userGroups.FirstOrDefault(i => i.Id == supportGroup.Id);
                        if (userGroup != null)
                            return userGroup;
                    }
                }
            }

            return null;
        }

        public static string GenerateTicketGuid(string ticketPrefix = "IN")
        {
            var now = DateTime.Now;
            return string.Format("{0}{1:yyMMdd}{2:00000}", ticketPrefix, now, IncidentTicket.Provider.GetMaxTicketCount(now));
        }
    }
}
