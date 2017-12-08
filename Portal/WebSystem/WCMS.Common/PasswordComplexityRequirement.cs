using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using WCMS.Common.Utilities;

namespace WCMS.Common
{
    public class PasswordComplexityRequirement
    {
        public PasswordComplexityRequirement()
        {
            MinChars = 0;
        }

        public int MinChars { get; set; }
        public bool RequireLetter { get; set; }
        public bool RequireNumber { get; set; }

        /// <summary>
        /// Returns true if the Password passed the check.
        /// </summary>
        /// <param name="password"></param>
        /// <param name="minChars"></param>
        /// <param name="requireLetter"></param>
        /// <param name="requireNumber"></param>
        /// <returns></returns>
        public bool CheckComplexity(string password)
        {
            int charLen = password.Length;
            bool hasLetter = false;
            bool hasNumber = false;

            if(MinChars > charLen)
                return false;
            else if(charLen == 0)
                return true;
            
            var chars = password.ToArray();
            for(int i=0; i<charLen; i++)
            {
                var c = chars[i];
                
                if(RequireLetter && !hasLetter && char.IsLetter(c))
                {
                    if(!RequireNumber || hasNumber)
                        return true;

                    hasLetter = true;
                }

                if(RequireNumber && !hasNumber && char.IsNumber(c))
                {
                    if(!RequireLetter || hasLetter)
                        return true;

                    hasNumber = true;
                }
            }

            return (!RequireLetter || (RequireLetter && hasLetter)) && (!RequireNumber || (RequireNumber && hasNumber));
        }

        public static PasswordComplexityRequirement Parse(string requirementXml)
        {
            if (!string.IsNullOrEmpty(requirementXml))
            {
                XmlDocument xdoc = new XmlDocument();
                xdoc.LoadXml(requirementXml);

                PasswordComplexityRequirement requirement = new PasswordComplexityRequirement();

                var parentNode = xdoc.FirstChild;
                requirement.MinChars = DataHelper.GetInt32(XmlUtil.GetAttributeValue(parentNode, "MinChars", "0"));
                requirement.RequireLetter = DataHelper.GetBool(XmlUtil.GetAttributeValue(parentNode, "RequireLetter", "false"));
                requirement.RequireNumber = DataHelper.GetBool(XmlUtil.GetAttributeValue(parentNode, "RequireNumber", "false"));

                return requirement;
            }

            return null;
        }
    }
}
