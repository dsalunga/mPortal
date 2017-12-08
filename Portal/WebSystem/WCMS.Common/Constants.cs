using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Collections.Specialized;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Globalization;

namespace WCMS.Common
{
    public enum DateType : int
    {
        Day = 1,
        Month = 2,
        Year = 3
    }

    public abstract class WebConstants
    {
        public const string EMPTY_HREF = "javascript:";

        /*
        public static ListItem[] GetDropDownDate(DateType dtpType)
        {
            ListItem[] dateList;
            switch (dtpType)
            {
                case DateType.Day:
                    dateList = new ListItem[31];
                    for (int i = 1; i < 32; i++)
                    {
                        ListItem item = new ListItem(i.ToString(), i.ToString());
                        if (DateTime.Now.Day == i)
                            item.Selected = true;

                        dateList[i - 1] = item;
                    }
                    break;

                case DateType.Month:
                    dateList = new ListItem[12];
                    for (int i = 1; i < 13; i++)
                    {
                        ListItem item = new ListItem(DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(i), i.ToString());
                        if (DateTime.Now.Month == i)
                            item.Selected = true;

                        dateList[i - 1] = item;
                    }
                    break;

                case DateType.Year:
                    dateList = new ListItem[(DateTime.Now.Year - 1990) + 2];
                    for (int i = 1990; i < DateTime.Now.Year + 2; i++)
                    {
                        ListItem item = new ListItem(i.ToString(), i.ToString());
                        if (DateTime.Now.Year == i)
                            item.Selected = true;

                        dateList[i - 1990] = item;
                    }
                    break;

                default:
                    dateList = new ListItem[1];
                    break;
            }
            return dateList;
        }
        */

        //public static string ToggleImage(bool Status)
        //{
        //    return (Status) ? "/Content/Assets/Images/Common/ico_check.gif" : "/Content/Assets/Images/Common/ico_x.gif";
        //}

        public const string ConnectionString = "ConnectionString";
    }
}