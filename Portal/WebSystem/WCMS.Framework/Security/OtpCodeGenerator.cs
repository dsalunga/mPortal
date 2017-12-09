using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;

//using WCMS.Server.Sms.Remoting.Adapter;
//using WCMS.Sms.Shared.Entities;

using WCMS.Common;
using WCMS.Common.Net;
using WCMS.Common.Utilities;
using WCMS.Framework.Core;
using WCMS.Framework.Utilities;

namespace WCMS.Framework
{
    public class OtpCodeGenerator
    {
        private NamedValueProvider _valueProvider;
        public NamedValueProvider ValueProvider
        {
            get { return _valueProvider; }
        }

        private string _otpCode;
        public string OtpCode
        {
            get { return _otpCode; }
        }

        public int UserId { get; set; }

        private OtpCodeGenerator() { }

        public OtpCodeGenerator(int userId = -1)
        {
            var numericGuid = DateTime.Now.Ticks.ToString();

            _otpCode = numericGuid.Substring(numericGuid.Length - 8);
            _valueProvider = new NamedValueProvider();

            UserId = userId;
        }

        public bool SendToEmail()
        {
            string template = WebRegistry.SelectNodeValue("/System/Security/OtpEmailTemplate");
            if (!string.IsNullOrEmpty(template) && UserId > 0)
            {
                var user = WebUser.Get(UserId);
                if (user != null)
                {
                    _valueProvider.Add("PIN", _otpCode);
                    _valueProvider.Add("NAME", user.FirstAndLastName);
                    _valueProvider.Add("EXPIRY", DateTimeHelper.TimeIntervalToString(GetExpiry(), TimeInterval.Minute));

                    var email = new WebMailMessage();
                    email.Body = Substituter.Substitute(template, _valueProvider);
                    email.SubjectAutoPrefix = "One-time PIN (Do not reply)";
                    email.To.Add(new MailAddress(user.Email, user.FirstAndLastName));
                    email.Send();

                    return true;
                }
            }

            return false;
        }

        public bool SendToMobile()
        {
            string template = WebRegistry.SelectNodeValue("/System/Security/OtpSmsTemplate");
            if (!string.IsNullOrEmpty(template) && UserId > 0)
            {
                var user = WebUser.Get(UserId);
                if (user != null && !string.IsNullOrEmpty(user.MobileNumber))
                {
                    _valueProvider.Add("PIN", _otpCode);
                    _valueProvider.Add("NAME", user.FirstAndLastName);
                    _valueProvider.Add("EXPIRY", DateTimeHelper.TimeIntervalToString(GetExpiry(), TimeInterval.Minute));

                    try
                    {
                        //bool usingInternetProxy = false;
                        //if (usingInternetProxy)
                        //{
                        //    IWebProxy proxy = WebRequest.GetSystemWebProxy();
                        //    string userName = ""; //provide internet proxy user name
                        //    string password = ""; //provide internet proxy password
                        //    proxy.Credentials = new NetworkCredential(userName, password);
                        //    WebRequest.DefaultWebProxy = proxy;
                        //}

                        return SmsHelper.SendMessage(WConfig.HttpSmsUrl, user.MobileNumber, Substituter.Substitute(template, _valueProvider));

                        //MessageQueueService msgService = new MessageQueueService();
                        //SmsMessage msg = new SmsMessage();
                        //msg.ApplicationId = "mPortal";
                        //msg.CreationUserId = user.Id;
                        //msg.Message = Substituter.Substitute(template, _valueProvider);
                        //msg.MobileNumber.Add(user.MobileNumber);
                        //msg.ModificationUserId = user.Id;
                        
                        //bool success = msgService.CreateSmsMessage(msg);
                        //return success;

                        //if (success)
                        //    Label1.Text = "Success!";
                        //else
                        //    Label1.Text = "Failed";
                    }
                    catch (Exception ex)
                    {
                        LogHelper.WriteLog(ex);
                    }
                }
            }

            return false;
        }

        public static int GetExpiry()
        {
            return DataHelper.GetInt32(WebRegistry.SelectNodeValue("/System/Security/OtpExpiry", "5"));
        }

        public static string Generate()
        {
            return (new OtpCodeGenerator(-1)).OtpCode;
        }
    }
}
