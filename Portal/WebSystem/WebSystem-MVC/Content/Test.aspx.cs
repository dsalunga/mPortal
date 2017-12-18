using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Reflection;
using System.Net.Mail;
using System.Linq;
using System.Diagnostics;
using System.Text;

using WCMS.Framework;
using WCMS.Framework.Core.Manager;

using WCMS.Common.Utilities;
using System.Net;
using System.IO;
using WCMS.Framework.Core.SqlProvider;
using WCMS.Framework.Core;

namespace WCMS.WebSystem
{
    public partial class Test : Page
    {
        public class Test01
        {
            private int _id;

            public int Id
            {
                get { return _id; }
                set { _id = value; }
            }

            private string _name;

            public string Name
            {
                get { return _name; }
                set { _name = value; }
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                WebUser user = WebUser.Get("dsalunga");
                txtEncrypt.Text = user.Password;
                //user.Password = WebCryptography.EncryptString(
                //KeyPair kp = WebCryptography.GenerateKeyPair();
                //txtEncrypt.Text = kp.PrivateKey;
                //txtDecrypt.Text = kp.PublicKey;

                //WebRegistry item = WebRegistry.SelectNode("/System/Security/PrivateKey");
                //item.Value = kp.PrivateKey;
                //item.Update();

                //item = WebRegistry.SelectNode("/System/Security/PublicKey");
                //item.Value = kp.PublicKey;
                //item.Update();

                System.Web.HttpBrowserCapabilities browser = Request.Browser;
                string s = "Browser Capabilities\n"
                    + "Type = " + browser.Type + "\n"
                    + "Name = " + browser.Browser + "\n"
                    + "Version = " + browser.Version + "\n"
                    + "Major Version = " + browser.MajorVersion + "\n"
                    + "Minor Version = " + browser.MinorVersion + "\n"
                    + "Platform = " + browser.Platform + "\n"
                    + "Is Beta = " + browser.Beta + "\n"
                    + "Is Crawler = " + browser.Crawler + "\n"
                    + "Is AOL = " + browser.AOL + "\n"
                    + "Is Win16 = " + browser.Win16 + "\n"
                    + "Is Win32 = " + browser.Win32 + "\n"
                    + "Supports Frames = " + browser.Frames + "\n"
                    + "Supports Tables = " + browser.Tables + "\n"
                    + "Supports Cookies = " + browser.Cookies + "\n"
                    + "Supports VBScript = " + browser.VBScript + "\n"
                    + "Supports JavaScript = " +
                        browser.EcmaScriptVersion.ToString() + "\n"
                    + "Supports Java Applets = " + browser.JavaApplets + "\n"
                    + "Supports ActiveX Controls = " + browser.ActiveXControls
                          + "\n"
                    + "Supports JavaScript Version = " +
                        browser["JavaScriptVersion"] + "\n";

                txtEditorText.Text = s;
            }
        }

        protected void cmdTest_Click(object sender, EventArgs e)
        {
            //List<CalendarEvent> events = CalendarEvent.Get();
            //foreach (var ev in events)
            //{
            //    ev.LastReminderSent = (DateTime)System.Data.SqlTypes.SqlDateTime.MinValue;
            //    ev.Update();
            //}

            //// Initialize an instance (works only inside a UserControl/Control)
            //WContext context = new WContext(this);

            //var member = c.GetProfile("USER_ID", new DateTime(2010, 10, 2));
            //if (member != null)
            //    Response.Write(member.FullName);

            //var p = c.GetPhoto(1);
            //imgThumb.Src = p[0].PhotoFileName;
            var sites = WSite.GetList();
            var sitesDic = sites.ToDictionary(i => i.Id, i => i);

            Stopwatch watch = new Stopwatch();
            watch.Start();
            for (int i = 0; i < 100; i++)
            {
                var ds = DataHelper.ToDataSet(sites);

                //foreach (var item in sites)
                //{
                //    var y = sites.Find(z => z.Id == item.Id);
                //    if (y != null)
                //        if (y.Id > 0)
                //            x++;
                //}
            }
            watch.Stop();
            txtDecrypt.Text = "List: " + watch.Elapsed + Environment.NewLine;

            watch.Reset();
            watch.Start();
            for (int i = 0; i < 100; i++)
            {
                var ds = DataHelper.ToDataSet(from s in sites
                                              select new
                                              {
                                                  s.Id,
                                                  s.Name,
                                                  s.ParentId,
                                              });

                //foreach (var item in sitesDic)
                //{
                //    var y = sitesDic[item.Key];
                //    if (y != null)
                //        if (y.Id > 0)
                //            x++;
                //}
            }
            watch.Stop();
            txtDecrypt.Text += "Dictionary: " + watch.Elapsed;
        }

        protected void cmdEncrypt_Click(object sender, EventArgs e)
        {
            txtDecrypt.Text = WCryptography.EncryptString(txtEncrypt.Text);
        }

        protected void cmdDecrypt_Click(object sender, EventArgs e)
        {
            txtEncrypt.Text = WCryptography.DecryptString(txtDecrypt.Text);
        }

        protected void cmdSerialize_Click(object sender, EventArgs e)
        {
            WebUser user = new WebUser();
            user.Email = "email@live.com";
            user.FirstName = "Daniel";
            user.LastName = "Salunga";
            user.UserName = "dsalunga";

            SerializationUtil.Serialize<WebUser>(@"C:\user.dat", user);
        }

        protected void cmdDeserialize_Click(object sender, EventArgs e)
        {
            WebUser user = SerializationUtil.Deserialize<WebUser>(@"C:\user.dat");

            Response.Write(string.Format("Username: {0}, FirstName: {1}, LastName: {2}, Email: {3}",
                user.UserName, user.FirstName, user.LastName, user.Email));
        }

        protected void cmdGetEditorText_Click(object sender, EventArgs e)
        {
        }

        protected void cmdPerf01_Click(object sender, EventArgs e)
        {
            var manager = WPage.Provider as WebPageManager;
            if (manager != null)
            {
                var cache = manager.Cache.ObjectCache;

                Stopwatch sw = new Stopwatch();

                // where + to list
                sw.Start();
                for (int i = 0; i < 50000; i++)
                {
                    var list = cache.Values.Where(item => !item.Name.Equals("xxx!!!!")).ToList();
                }

                sw.Stop();

                txtDecrypt.Text += string.Format("Where + ToList: {0}{1}", sw.Elapsed, Environment.NewLine);

                sw = new Stopwatch();
                sw.Start();

                for (int i = 0; i < 50000; i++)
                {
                    //List<WebPage> items = new List<WebPage>();

                    //foreach(var item in cache.Values)
                    //{

                    //    if (!item.Name.Equals("xxx!!!!"))
                    //        items.Add(item);
                    //}
                    var list = cache.Values.Where(item => !item.Name.Equals("xxx!!!!"));
                }

                sw.Stop();

                txtDecrypt.Text += string.Format("Where: {0}{1}", sw.Elapsed, Environment.NewLine);
            }
        }

        protected void cmdPerf02_Click(object sender, EventArgs e)
        {
            List<int> items02 = new List<int>();
            for (int i = 1000; i < 2000; i++)
                items02.Add(i);





            Stopwatch sw = new Stopwatch();
            sw.Start();

            for (int i = 0; i < 5000; i++)
            {
                List<int> list = new List<int>();
                for (int x = 0; x < 1000; x++)
                    list.Add(x);

                list.AddRange(items02);
                //foreach (var item in items02)
                //    list.Add(item);
            }

            sw.Stop();

            txtDecrypt.Text += string.Format("List: {0}{1}", sw.Elapsed, Environment.NewLine);



            var enu2 = items02.AsEnumerable();

            sw = new Stopwatch();
            sw.Start();

            for (int i = 0; i < 5000; i++)
            {
                List<int> list = new List<int>();
                for (int x = 0; x < 1000; x++)
                    list.Add(x);

                var res = list.Concat(enu2);
            }

            sw.Stop();

            txtDecrypt.Text += string.Format("IEnunerable: {0}{1}", sw.Elapsed, Environment.NewLine);
        }

        protected void cmdXmlObject_Click(object sender, EventArgs e)
        {
            var sites = WSite.GetList();

            var sitesXml = DataHelper.ToXml(sites, "Sites");

            var sites2 = DataHelper.FromXml<WSite>(sitesXml);
        }

        protected void cmdFtpDownload_Click(object sender, EventArgs e)
        {
            FtpWebRequest ftpClient = (FtpWebRequest)WebRequest.Create("ftp://localhost/Through%20The%20Storm_voice.mp3");
            ftpClient.Method = WebRequestMethods.Ftp.DownloadFile;
            //ftpClient.Proxy = _proxy;
            ftpClient.UseBinary = true;
            ftpClient.Timeout = 300000;
            ftpClient.KeepAlive = false;
            ftpClient.Credentials = new NetworkCredential(@"CORP\username", "password");

            FtpWebResponse ftpResponse = ftpClient.GetResponse() as FtpWebResponse;
            //StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.UTF8);

            Stream ftpStream = ftpResponse.GetResponseStream();

            long len = ftpResponse.ContentLength;
            int bufferSize = 2048;
            int readCount;
            byte[] buffer = new byte[bufferSize];

            Response.AppendHeader("content-disposition", string.Format("attachment; filename=\"{0}\"", "download.mp3"));

            Stream responseStream = Response.OutputStream;
            do
            {
                readCount = ftpStream.Read(buffer, 0, bufferSize);
                responseStream.Write(buffer, 0, readCount);
            }
            while (readCount > 0);


            ftpResponse.Close();
            responseStream.Close();

            Response.End();

            //string responseString = reader.ReadToEnd();

            //reader.Close();
            //response.Close();

        }

        protected void cmdWCFCacheTest_Click(object sender, EventArgs e)
        {
            WebServices.DataSyncClient client = new WebServices.DataSyncClient();
            WebSiteIdentityProvider provider = new WebSiteIdentityProvider();
            var manager = WebSiteIdentity.Provider;

            Stopwatch sw = new Stopwatch();

            sw.Start();
            // Start WCF Test
            for (var i = 0; i < 10000; i++)
            {
                var bindings = client.GetBindings();
                bindings.Count();
            }

            sw.Stop();

            txtEditorText.Text += "\n\nWCF Access Elapsed: " + sw.Elapsed;


            // Start Direct DB Access Test
            sw.Restart();
            for (var i = 0; i < 10000; i++)
            {
                List<WebSiteIdentity> bindings = new List<WebSiteIdentity>();
                bindings.AddRange(provider.GetList());

                bindings.Count();
            }

            sw.Stop();
            
            txtEditorText.Text += "\nDirect DB Access Elapsed: " + sw.Elapsed;


            // Start Direct Cache Access Test
            sw.Restart();
            for (var i = 0; i < 10000; i++)
            {
                List<WebSiteIdentity> bindings = new List<WebSiteIdentity>();
                bindings.AddRange(manager.GetList());

                bindings.Count();
            }

            sw.Stop();

            txtEditorText.Text += "\nDirect Cache Access Elapsed: " + sw.Elapsed;
        }

        protected void cmdError_Click(object sender, EventArgs e)
        {
            var a = 0;
            var y = 5 / a;
        }
    }
}