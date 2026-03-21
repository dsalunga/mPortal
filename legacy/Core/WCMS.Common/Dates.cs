using System;
using System.Collections.Generic;
using System.Globalization;
using System.Web.UI.WebControls;

namespace des
{
    public abstract class Dates
    {
        #region Drop Down Items

        public static ListItem[] Days()
        {
            ListItemCollection LIC = new ListItemCollection();
            for (int x = 1; x < 32; x++)
            {
                LIC.Add(new ListItem(x.ToString(), x.ToString()));
            }
            ListItem[] LI = new ListItem[LIC.Count];
            LIC.CopyTo(LI, 0);
            return LI;
        }

        public static ListItem[] Months()
        {
            ListItemCollection LIC = new ListItemCollection();
            DateTimeFormatInfo DTF = new DateTimeFormatInfo();
            for (int x = 1; x < 13; x++)
            {
                LIC.Add(new ListItem(DTF.GetMonthName(x), x.ToString()));
            }
            ListItem[] LI = new ListItem[LIC.Count];
            LIC.CopyTo(LI, 0);
            return LI;
        }

        public static ListItem[] Years(int Start, int End)
        {
            ListItemCollection LIC = new ListItemCollection();
            for (int x = Start; x < End + 1; x++)
            {
                LIC.Add(new ListItem(x.ToString(), x.ToString()));
            }
            ListItem[] LI = new ListItem[LIC.Count];
            LIC.CopyTo(LI, 0);
            return LI;
        }

        #endregion

        #region Validators

        public static bool IsValid(string Day, string Month, string Year)
        {
            try
            {
                DateTime DT = new DateTime(int.Parse(Year), int.Parse(Month), int.Parse(Day));
                return true;
            }
            catch
            {
                return false;
            }

        }

        public static bool IsValid(int Day, int Month, int Year)
        {
            try
            {
                DateTime DT = new DateTime(Year, Month, Day);
                return true;
            }
            catch
            {
                return false;
            }

        }

        #endregion
    }
}
