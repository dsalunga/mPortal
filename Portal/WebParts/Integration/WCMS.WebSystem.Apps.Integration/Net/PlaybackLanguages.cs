using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.WebSystem.Apps.Integration.Net
{
    public struct PlaybackLanguages
    {
        public const string Tagalog = "TL"; // Tagalog (ISO 639-1)
        public const string English = "EN";
        public const string Neutral = "";

        private static Dictionary<string, string> _values;

        static PlaybackLanguages()
        {
            _values = new Dictionary<string, string>()
            {
                {"TL", "Tagalog"},
                {"EN", "English"},
                {"", "Neutral"}
            };
        }

        public static string GetText(string langCode)
        {
            if (_values.ContainsKey(langCode))
                return _values[langCode];

            return string.Empty;
        }

        public static Dictionary<string, string> Values
        {
            get { return _values; }
        }
    }
}
