using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCMS.Framework
{
    public class OtpCache
    {
        public OtpCache(int userId, string redirUrl, string otpCode, int otpType, string updatePwdUrl = null)
        {
            this.UserId = userId;
            this.RedirUrl = redirUrl;
            this.OtpCode = otpCode;
            this.OtpType = otpType;

            this.DateTime = DateTime.Now;

            this.UpdatePwdUrl = updatePwdUrl;
        }

        public int UserId { get; set; }
        public string RedirUrl { get; set; }
        public string UpdatePwdUrl { get; set; }
        public string OtpCode { get; set; }
        public DateTime DateTime { get; set; }
        public int OtpType { get; set; }
        public bool SentToEmailInstead { get; set; }
        public bool Impersonate { get; set; }
        public bool RememberLogin { get; set; }
        public string Password { get; set; }
    }
}
