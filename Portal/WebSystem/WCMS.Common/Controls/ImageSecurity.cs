using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Web.Caching;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WCMS.Common.Controls
{
    [DefaultProperty("Text")]
    [ToolboxData("<{0}:ImageSecurity runat=server></{0}:ImageSecurity>")]
    public class ImageSecurity : WebControl, IHttpHandler
    {
        private const int CharsLen = 5;
        private const int ImageHeight = 40;
        private const int CharWidthPx = 21;
        private const int CacheExpiryInMin = 20;

        private const int CharY = (int)((double)ImageHeight / 2.5);
        private const int ImageWidth = CharsLen * CharWidthPx;
        public string uniqueId = string.Empty;

        public ImageSecurity() : base(HtmlTextWriterTag.Img) { }

        // Key used for storing the diplayed text
        private string UniqueId
        {
            get
            {
                if (string.IsNullOrEmpty(uniqueId))
                    uniqueId = Guid.NewGuid().ToString();

                return uniqueId;
            }
        }

        // Chars to display
        private string GenerateCode()
        {
            return Guid.NewGuid().ToString()
                .Replace("-", "").Substring(0, CharsLen);
        }

        public string Text
        {
            get
            {
                return string.Format("{0}", HttpContext.Current.Cache[this.UniqueId]);
            }
        }

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Page.RegisterRequiresControlState(this);
        }

        protected override void LoadControlState(object savedState)
        {
            Pair pair = savedState as Pair;
            if (pair != null)
            {
                this.uniqueId = pair.Second as string;
            }
        }

        protected override object SaveControlState()
        {
            Object baseState = base.SaveControlState();
            Pair pairState = new Pair(baseState, this.UniqueId);
            return pairState;
        }

        protected override void Render(HtmlTextWriter output)
        {
            output.AddAttribute(HtmlTextWriterAttribute.Src, "ImageSecurity.axd?uid=" + this.UniqueId);
            base.Render(output);
        }

        public void ProcessRequest(HttpContext context)
        {
            Bitmap bmp = new Bitmap(ImageWidth, ImageHeight);
            Graphics g = Graphics.FromImage(bmp);

            string randString = GenerateCode();
            string myUID = context.Request["uid"].ToString();

            if (context.Cache[myUID] == null)
            {
                context.Cache.Add(myUID, randString, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(CacheExpiryInMin), System.Web.Caching.CacheItemPriority.Normal, null);
            }
            else
            {
                context.Cache[myUID] = randString;
            }

            g.FillRectangle(Brushes.Aquamarine, 0, 0, ImageWidth, ImageHeight);
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            // Draw the chars
            Random rand = new Random();
            for (int i = 0; i < randString.Length; i++)
            {
                Font drawFont = new Font("Tahoma", rand.Next(10, 20), (rand.Next() % 2 == 0 ? FontStyle.Bold : FontStyle.Regular));
                g.DrawString(randString.Substring(i, 1), drawFont, Brushes.Black, i * 20, rand.Next(0, CharY));
            }

            // Draw ellipses
            Point[] pt = new Point[15];
            for (int i = 0; i < 15; i++)
            {
                pt[i] = new Point(rand.Next() % ImageWidth, rand.Next() % ImageHeight);
                g.DrawEllipse(Pens.LightSteelBlue, pt[i].X, pt[i].Y, rand.Next() % 30 + 1, rand.Next() % 30 + 1);
            }

            context.Response.Clear();
            context.Response.ClearHeaders();
            context.Response.ContentType = "image/jpeg";

            bmp.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);

            context.Response.End();
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }
}
