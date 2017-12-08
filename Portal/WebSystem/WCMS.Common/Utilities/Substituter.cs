using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;

namespace WCMS.Common.Utilities
{
    /// <summary>
    /// Performs substition on a string and replaces instances of $(x) values with values obtained by looking up dictionaries 
    /// where the key=x.  Formats can also be specified in by specifying the conversion type and format delimited by the pipe character.
    /// e.g. $(CurrentDate|System.DateTime|dd-MMM-yyyy)
    /// the type can be ommitted if the value is already in the expected type
    /// e.g. $(CurrentDate|dd-MMM-yyyy)
    /// </summary>    
    /// <code>
    /// 
    ///     Example using passed Dictionary object for lookup:
    ///            string result = Substitutor.Substitute("Hello my name is $(username) and today is $(today|System.DateTime|dd-MMM-yyyy)", myDictionary); 
    /// 
    ///     Example using lookupCallback delegate to lookup TaskAgent.TaskParametersCollection:
    ///            string result = Substitutor.Substitute("Hello my name is $(username) and today is $(today)", delegate(string name) 
    ///            {
    ///                if (taskParameters.Contains(name))
    ///                {
    ///                    return taskParameters[name].Value;
    ///                }
    ///                else
    ///                {
    ///                    return null;
    ///                }
    ///            } );                  
    /// </code>        
    public class Substituter
    {
        public readonly static string DefaultProviderKey = "Context";
        public readonly static string DefaultContentToken = "$(Content)";
        public readonly static string DefaultKey = "Content";

        public readonly static string DefaultLeftEnclose = "$(";
        public readonly static string DefaultRightEnclose = ")";

        const string RegexPattern = "{0}.+?{1}";
        //const string RegexPattern = @"{0}.*?\(?.*?\)?{1}"; // 

        private static Regex _cachedRegex;
        private static string _cachedRegexLeftEnclose;
        private static string _cachedRegexRightEnclose;

        private static readonly object _syncLock = new object();

        /// <summary>
        /// Substitutes $(x) values in stringValue by looking up the values dictionary where key=x
        /// </summary>
        /// <param name="stringValue">the string to replace $(x) values in</param>
        /// <param name="values">The dictionary to look up the replacement values</param>
        /// <returns>stringValue with the $(x) values replaced with substitutes when found</returns>
        public static string Substitute(string stringValue, IDictionary values)
        {
            return Substitute(stringValue, values, DefaultLeftEnclose, DefaultRightEnclose);
        }

        /// <summary>
        /// Substitutes $(x) values in stringValue, the delegate specified in lookupCallback is called for each match allowing
        /// custom lookup and formatting to be performed
        /// </summary>
        /// <param name="stringValue">the string to replace $(x) values in</param>
        /// <param name="lookupCallback">Delegate that receives the name of the value to lookup</param>
        /// <returns>stringValue with the $(x) values replaced with substitutes when found</returns>
        public static string Substitute(string stringValue, ReplacementLookup lookupCallback)
        {
            return Substitute(stringValue, lookupCallback, DefaultLeftEnclose, DefaultRightEnclose);
        }

        /// <summary>
        /// Substitutes $(x) values in stringValue, the delegate specified in lookupCallback is called for each match allowing
        /// custom lookup and formatting to be performed
        /// </summary>
        /// <param name="stringValue">the string to replace $(x) values in</param>
        /// <param name="lookupCallback">Delegate that receives the name of the value to lookup</param>
        /// <param name="leftEnclose">the left encloser (e.g. '$(')</param>
        /// <param name="rightEnclose">the right encloser (e.g. ')')</param>
        /// <returns>stringValue with the $(x) values replaced with substitutes when found</returns>
        public static string Substitute(string stringValue, ReplacementLookup lookupCallback, string leftEnclose, string rightEnclose)
        {
            CustomMatchEvaluator evaluator = new CustomMatchEvaluator(leftEnclose, rightEnclose, lookupCallback);
            return Substitute(stringValue, leftEnclose, rightEnclose, evaluator.EvaluateMatch);
        }

        /// <summary>
        /// Substitutes $(x) values in stringValue by looking up the values dictionary where key=x
        /// </summary>
        /// <param name="stringValue">the string to replace $(x) values in</param>
        /// <param name="values">The dictionary to look up the replacement values</param>
        /// <param name="leftEnclose">the left encloser (e.g. '$(')</param>
        /// <param name="rightEnclose">the right encloser (e.g. ')')</param>
        /// <returns>stringValue with the $(x) values replaced with substitutes when found</returns>
        public static string Substitute(string stringValue, IDictionary values, string leftEnclose, string rightEnclose)
        {
            var evaluator = new DictionaryMatchEvaluator(leftEnclose, rightEnclose, values);
            return Substitute(stringValue, leftEnclose, rightEnclose, evaluator.EvaluateMatch);
        }

        /// <summary>
        /// Main Substiture methods (others are overloaded versions)
        /// </summary>
        /// <param name="stringValue">the string to replace $(x) values in</param>
        /// <param name="leftEnclose">the left encloser (e.g. '$(')</param>
        /// <param name="rightEnclose">the right encloser (e.g. ')')</param>
        /// <param name="matchEvaluator">MatchEvaluator delegate to evaluate the match</param>
        /// <returns>stringValue with the $(x) values replaced with substitutes when found</returns>
        public static string Substitute(string stringValue, string leftEnclose, string rightEnclose, MatchEvaluator matchEvaluator)
        {
            const int recursionLimit = 10;
            int i = 0;

            string result = GetRegex(leftEnclose, rightEnclose).Replace(stringValue, matchEvaluator);

            while (i++ < recursionLimit && result != stringValue)
            {
                stringValue = result;
                result = GetRegex(leftEnclose, rightEnclose).Replace(stringValue, matchEvaluator);
            }
            return result;
        }

        private static Regex GetRegex(string leftEnclose, string rightEnclose)
        {
            lock (_syncLock)
            {
                if (_cachedRegex == null)
                {
                    _cachedRegex = new Regex(string.Format(RegexPattern, Regex.Escape(leftEnclose), Regex.Escape(rightEnclose)), (RegexOptions.Multiline | RegexOptions.IgnoreCase));
                    _cachedRegexLeftEnclose = leftEnclose;
                    _cachedRegexRightEnclose = rightEnclose;

                    return _cachedRegex;
                }
                else if (_cachedRegexLeftEnclose == leftEnclose && _cachedRegexRightEnclose == rightEnclose)
                {
                    return _cachedRegex;
                }
                else
                {
                    return new Regex(string.Format(RegexPattern, Regex.Escape(leftEnclose), Regex.Escape(rightEnclose)), (RegexOptions.Multiline | RegexOptions.IgnoreCase));
                }
            }
        }

        #region Objects Encapsulating Match Evaluation

        /// <summary>
        /// Base object for MatchEvaluators
        /// </summary>
        private abstract class BaseMatchEvaluator
        {
            protected int LeftSubLen
            {
                get { return _leftSubLen; }
                set { _leftSubLen = value; }
            } private int _leftSubLen;

            protected int TotalSubLen
            {
                get { return _totalSubLen; }
                set { _totalSubLen = value; }
            } private int _totalSubLen;

            public BaseMatchEvaluator(string leftEnclose, string rightEnclose)
            {
                _leftSubLen = leftEnclose.Length;
                _totalSubLen = rightEnclose.Length + _leftSubLen;
            }

            protected void extractElementsFromMatch(string match, out string name, out string typeName, out string format)
            {
                name = removeEnclosingChars(match);
                typeName = null;
                format = null;

                int formatStart = name.IndexOf("|");
                if (formatStart > -1)
                {
                    format = name.Substring(formatStart + 1);
                    name = name.Substring(0, formatStart);

                    formatStart = format.IndexOf("|");
                    if (formatStart > -1)
                    {
                        typeName = format.Substring(0, formatStart).Trim();
                        format = format.Substring(formatStart + 1);
                    }
                }
            }

            protected string removeEnclosingChars(string matchedValue)
            {
                return matchedValue.Substring(LeftSubLen, matchedValue.Length - TotalSubLen);
            }

            public string EvaluateMatch(Match match)
            {
                string name;
                string typeName;
                string format;

                extractElementsFromMatch(match.ToString(), out name, out typeName, out format);

                object replacementValue = LookupReplacement(name);

                if (replacementValue == null)
                {
                    return match.ToString();
                }
                else
                {
                    return formatReplacement(replacementValue, typeName, format);
                }
            }

            protected string formatReplacement(object replacementValue, string typeName, string format)
            {
                string returnValue;

                if (typeName != null)
                {
                    Type type = Type.GetType(typeName, false);

                    if (type != null)
                    {
                        if (replacementValue.GetType() != type)
                        {
                            replacementValue = Convert.ChangeType(replacementValue, type);
                        }
                    }
                }

                if (format != null)
                {
                    if (replacementValue is IFormattable)
                    {
                        returnValue = ((IFormattable)replacementValue).ToString(format, null);
                    }
                    else
                    {
                        returnValue = string.Format("{0:" + format + "}", replacementValue);
                    }
                }
                else
                {
                    returnValue = replacementValue.ToString();
                }
                return returnValue;
            }

            protected abstract object LookupReplacement(string name);

        }

        /// <summary>
        /// MatchEvaluator for looking up values in dictionary
        /// </summary>
        private class DictionaryMatchEvaluator : BaseMatchEvaluator
        {
            private IDictionary _values;

            public DictionaryMatchEvaluator(string leftEnclose, string rightEnclose, IDictionary values)
                : base(leftEnclose, rightEnclose)
            {
                _values = values;
            }

            protected override object LookupReplacement(string name)
            {
                if (_values.Contains(name))
                {
                    return _values[name];
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Custom Match Evaluator for custom lookups via the ReplacementLookup delegate
        /// </summary>
        private class CustomMatchEvaluator : BaseMatchEvaluator
        {
            private ReplacementLookup _replacementLookupDel;

            public CustomMatchEvaluator(string leftEnclose, string rightEnclose, ReplacementLookup replacementLookupDel)
                : base(leftEnclose, rightEnclose)
            {
                _replacementLookupDel = replacementLookupDel;
            }

            protected override object LookupReplacement(string name)
            {
                return _replacementLookupDel(name);
            }
        }

        /// <summary>
        /// Delegate for looking up external dictionaries, collections etc allowing formatting etc to be applied
        /// </summary>
        /// <param name="name">The name of the item to lookup (e.g. if '$(Book)' was the match then the value would be 'Book')</param>
        /// <returns>Should return the replacement value, if no match was found then return null to skip the replacement</returns>
        public delegate object ReplacementLookup(string name);

        #endregion

        ///// <summary>
        ///// Delegate to use for looking up replacement values in DIT.Common.Configuration.Settings
        ///// </summary>
        //public static ReplacementLookup LookupSettings = new ReplacementLookup(lookupSettings);

        //private static object lookupSettings(string lookupName)
        //{
        //    object value = DIT.Common.Configuration.Settings.GetSetting(lookupName);

        //    return value != null ? value.ToString() : null;

        //}

        public static string Substitute(string stringValue, Dictionary<string, INamedValueProvider> context)
        {
            return Substitute(stringValue, (string target) =>
            {
                return GetNamedValue(target, context);
            });
        }

        public static object Substitute(string stringValue, Dictionary<string, INamedObjectProvider> context)
        {
            return Substitute(stringValue, (string target) =>
            {
                return GetNamedValue(target, context);
            });
        }

        public static string Substitute(string stringValue, INamedValueProvider values)
        {
            var context = new Dictionary<string, INamedValueProvider>();
            context.Add(DefaultProviderKey, values);

            return Substitute(stringValue, (string target) =>
            {
                return GetNamedValue(target, context);
            });
        }

        public static object Substitute(string stringValue, INamedObjectProvider values)
        {
            var context = new Dictionary<string, INamedObjectProvider>();
            context.Add(DefaultProviderKey, values);

            return Substitute(stringValue, (string target) =>
            {
                return GetNamedValue(target, context);
            });
        }

        public static string Substitute(string templateString, string key, string value)
        {
            var values = new NamedValueProvider();
            values.Add(key, value);

            return Substitute(templateString, values);
        }

        public static string Substitute(string templateString, string defaultKeyValue)
        {
            var values = new NamedValueProvider();
            values.Add(DefaultKey, defaultKeyValue);

            return Substitute(templateString, values);
        }

        public static string GetNamedValue(string sourceName, Dictionary<string, INamedValueProvider> context)
        {
            string sourceValue = null;
            if (sourceName.Contains(":"))
            {
                string[] sourceParts = sourceName.Split(':');

                string keyBase = sourceParts.First(); //.ToLower();
                string key = sourceParts[1]; //.ToLower();
                if (sourceParts.Length > 1 && context.ContainsKey(keyBase))
                {
                    var dict = context[keyBase];

                    //if (dict.ContainsKey(key))
                    sourceValue = dict[key];
                    //else
                    //    throw new KeyNotFoundException(string.Format("The given key was not present in the dictionary: {0}.", sourceName));
                }
                else
                {
                    sourceValue = string.Format("$({0})", sourceName);
                }
            }
            else
            {
                if (context.ContainsKey(Substituter.DefaultProviderKey) && context[Substituter.DefaultProviderKey].ContainsKey(sourceName))
                    sourceValue = context[Substituter.DefaultProviderKey][sourceName];
                /*else
                    throw new KeyNotFoundException(string.Format("The given key was not present in the dictionary: {0}.", sourceName));
                 */
            }

            return sourceValue;
        }

        public static object GetNamedValue(string sourceName, Dictionary<string, INamedObjectProvider> context)
        {
            object sourceValue = null;
            if (sourceName.Contains(":"))
            {
                string[] sourceParts = sourceName.Split(':');

                string keyBase = sourceParts.First(); //.ToLower();
                string key = sourceParts[1]; //.ToLower();
                if (sourceParts.Length > 1 && context.ContainsKey(keyBase))
                {
                    var dict = context[keyBase];

                    if (dict.ContainsKey(key))
                        sourceValue = dict[key];
                    else
                        throw new KeyNotFoundException(string.Format("The given key was not present in the dictionary: {0}.", sourceName));
                }
                else
                {
                    sourceValue = string.Format("$({0})", sourceName);
                }
            }
            else
            {
                if (context.ContainsKey(Substituter.DefaultProviderKey) && context[Substituter.DefaultProviderKey].ContainsKey(sourceName))
                    sourceValue = context[Substituter.DefaultProviderKey][sourceName];
                /*else
                    throw new KeyNotFoundException(string.Format("The given key was not present in the dictionary: {0}.", sourceName));
                 */
            }

            return sourceValue;
        }
    }
}
