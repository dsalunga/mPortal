using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.WebSystem.WebParts.Incident
{
    public sealed class IncidentConstants
    {
        public const string UserDisplayFormatString = "UserDisplayFormatString";

        public const int INCIDENT_CATEGORY_ID = 112;
        public const int INCIDENT_TICKET_ID = 113;
        public const int INCIDENT_TICKET_HISTORY_ID = 114;
        public const int INCIDENT_TYPE_ID = 117;
    }

    public sealed class TicketStatus
    {
        private static Dictionary<int, string> _values = new Dictionary<int, string>
        {
            {0, "Submitted"},
            {1, "Assigned"},
            {2, "In Progress"},
            {5, "On Hold"},
            {3, "Completed"},
            {4, "Closed"}
        };

        public static Dictionary<int, string> Values { get { return _values; } }

        public static string GetText(int key)
        {
            if (_values.ContainsKey(key))
                return _values[key];

            return string.Empty;
        }

        public const int Submitted = 0;
        public const int Assigned = 1;
        public const int InProgress = 2;
        public const int Completed = 3;
        public const int Closed = 4;
        public const int OnHold = 5;
        public const int Rejected = 6;

        public const int AllClosed = -3;
        public const int AllOpen = -2;
    }

    public sealed class TicketUrgency
    {
        private static Dictionary<int, string> _values = new Dictionary<int, string>
        {
            {0, "Low"},
            {1, "Normal"},
            {2, "High"}
        };

        public static Dictionary<int, string> Values { get { return _values; } }

        public static string GetText(int key)
        {
            if (_values.ContainsKey(key))
                return _values[key];

            return string.Empty;
        }

        public const int Low = 0;
        public const int Normal = 1;
        public const int High = 2;
    }

    public sealed class TicketHistoryType
    {
        public const int History = 0;
        public const int Note = 1;
    }

    public sealed class TicketSLA
    {
        public const string HIGH_SLA_KEY = "SLA-High";
        public const string NORMAL_SLA_KEY = "SLA-Normal";
        public const string LOW_SLA_KEY = "SLA-Low";

        public const string SLA_WARNING_PERCENTAGE_KEY = "SLA-Warning-Percentage";

        public const int HIGH_SLA_DURATION = 24;
        public const int NORMAL_SLA_DURATION = 72;
        public const int LOW_SLA_DURATION = 168;

        public const double SLA_WARNING_PERCENTAGE = 0.8;

        public const int SLA_STATUS_ONTRACK = 1;
        public const int SLA_STATUS_WARNING = 2;
        public const int SLA_STATUS_CRITICAL = 3;
        public const int SLA_STATUS_NA = 4;
    }

    public class TicketSlaInfo
    {
        public TicketSlaInfo() { }

        public TicketSlaInfo(int lowSla, int normalSla, int highSla, double warningPercentage)
        {
            this.LowSla = lowSla;
            this.NormalSla = normalSla;
            this.HighSla = highSla;
            this.WarningPercentage = warningPercentage;
        }

        public int LowSla { get; set; }
        public int NormalSla { get; set; }
        public int HighSla { get; set; }
        public double WarningPercentage { get; set; }

        public int GetSlaDuration(IncidentTicket item)
        {
            switch (item.Urgency)
            {
                case TicketUrgency.High:
                    return this.HighSla;

                case TicketUrgency.Low:
                    return this.LowSla;

                default:
                    return this.NormalSla;
            }
        }

        public int GetWarningLevelMinutes(int slaDuration)
        {
            return (int)((slaDuration * 60) * this.WarningPercentage);
        }
    }
}
