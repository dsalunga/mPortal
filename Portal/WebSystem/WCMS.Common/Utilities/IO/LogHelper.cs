using System;
using System.Data;
using System.Configuration;
using System.Text;
using System.IO;
using System.Linq;
using System.Net.Mail;

using System.Web;

namespace WCMS.Common.Utilities
{
    /// <summary>
    /// Summary description for LogHelper
    /// </summary>
    public static class LogHelper
    {
        private static string _logPath;
        private static readonly object _lock = new object();

        static LogHelper()
        {
            _logPath = ConfigHelper.Get("LogPath");
            if (string.IsNullOrEmpty(_logPath))
            {
                CreateLogDirectory();
            }
            else if (!_logPath.Contains(":") && !_logPath.Contains(@"\\") && HttpContext.Current != null)
            {
                _logPath = HttpContext.Current.Server.MapPath(_logPath);
                if (!Directory.Exists(_logPath))
                    Directory.CreateDirectory(_logPath);
            }
            else
            {
                // LogPath in config should have \ at the end
                _logPath = FileHelper.EvalPath(_logPath);
                if (!Directory.Exists(_logPath))
                {
                    try
                    {
                        Directory.CreateDirectory(_logPath);
                    }
                    catch // (Exception ex)
                    {
                        CreateLogDirectory();
                    }
                }
            }
        }

        private static void CreateLogDirectory()
        {
            var context = HttpContext.Current;
            if (context != null)
            {
                _logPath = context.Server.MapPath("~/App_Data/Logs/");
                if (!Directory.Exists(_logPath))
                    Directory.CreateDirectory(_logPath);
            }
            else
            {
                _logPath = Directory.GetCurrentDirectory();
            }
        }

        public static string CurrentLogFile
        {
            get
            {
                return Path.Combine(_logPath,
                    string.Format("Log_{0}.txt", DateTime.Now.ToString("yyyyMMdd")));
            }
        }

        public static void WriteLog(bool writeConsole, bool shout, string format, params object[] arg)
        {
            format = string.Format(format, arg);
            string errorLine = DateTime.Now + "\t" + format;

            try
            {
                lock (_lock)
                {
                    using (TextWriter w = new StreamWriter(CurrentLogFile, true))
                    {
                        w.WriteLine(errorLine);
                        w.Close();
                    }
                }
            }
            catch { }

            if (writeConsole)
                Console.WriteLine(errorLine);
        }

        public static void WriteLog(string filePath, string format, params object[] args)
        {
            try
            {
                lock (_lock)
                {
                    using (var w = new StreamWriter(filePath, true))
                    {
                        w.WriteLine(format, args);
                        w.Close();
                    }
                }
            }
            catch { }
        }

        public static void WriteLog(bool writeConsole, string error, params object[] arg)
        {
            WriteLog(writeConsole, false, error, arg);
        }

        public static void WriteLog(string format, params object[] arg)
        {
            WriteLog(false, format, arg);
        }

        public static void WriteLog(bool writeConsole, Exception ex)
        {
            string err = string.Format("ERROR: {0}Stack Trace: {1}",
                ex.Message + Environment.NewLine,
                ex.StackTrace + Environment.NewLine
            );
            WriteLog(writeConsole, err);
        }

        public static void WriteLog(Exception ex)
        {
            WriteLog(false, ex);
        }

        public static void WriteLog(HttpContext sender, Exception err)
        {
            string msg = string.Format(
                "Error in: {0}Error Message: {1}Stack Trace: {2}",
                sender.Request.Url + Environment.NewLine,
                err.Message + Environment.NewLine,
                err.StackTrace + Environment.NewLine + Environment.NewLine
            );

            WriteLog(false, false, msg);
        }

        public static string StringArrayToString(string[] s)
        {
            string arrayString = null;
            if (s != null)
            {
                if (s.Length > 0)
                {
                    var sb = new StringBuilder();
                    sb.Append(s.First());
                    if (s.Length > 1)
                    {
                        for (int i = 1; i < s.Length; i++)
                        {
                            sb.Append("," + s[i]);
                        }
                    }
                    arrayString = sb.ToString();
                }
            }
            return arrayString;
        }

        #region Shout Email

        public static void SendShoutEmail()
        {
            SendShoutEmail(null);
        }

        public static void SendShoutEmail(string subject)
        {
            SendShoutEmail(string.Empty, subject, false);
        }

        public static void SendShoutEmail(string body, string _subject, bool appendSubject)
        {
            string shoutEnabled = ConfigHelper.Get("ShoutEnabled");
            string shoutSubject = ConfigHelper.Get("ShoutSubject");
            string shoutTo = ConfigHelper.Get("ShoutTo");
            string shoutFrom = ConfigHelper.Get("ShoutFrom");
            string shoutFromName = ConfigHelper.Get("ShoutFromName");

            if (shoutEnabled != "1")
            {
                LogHelper.WriteLog(false, "Email notification disabled.");
                return;
            }

            string sSubject;
            MailMessage email = new MailMessage();

            if (appendSubject && !string.IsNullOrEmpty(_subject))
            {
                sSubject = shoutSubject + " - " + _subject;
            }
            else if (!appendSubject && !string.IsNullOrEmpty(_subject))
            {
                sSubject = _subject;
            }
            else
            {
                sSubject = shoutSubject;
            }

            email.Body = body;
            email.Subject = sSubject;
            email.To.Add(shoutTo);

            if (File.Exists(CurrentLogFile))
                email.Attachments.Add(new Attachment(LogHelper.CurrentLogFile));

            email.Priority = MailPriority.High;
            email.From = new MailAddress(shoutFrom, shoutFromName);

            SmtpClient smtpClient = new SmtpClient();
            try
            {
                //send email
                smtpClient.Send(email);
                email.Dispose();

                WriteLog(false, "Email notification sent.");
            }
            catch (Exception ex)
            {
                //Log the failure message
                WriteLog(false, ex);
            }
            finally
            {
                if (email != null)
                    email.Dispose();
            }
        }

        #endregion
    }
}