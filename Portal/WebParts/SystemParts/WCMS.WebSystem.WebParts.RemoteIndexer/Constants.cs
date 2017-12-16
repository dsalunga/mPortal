using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WCMS.WebSystem.WebParts.RemoteIndexer
{
    public struct FileStruct
    {
        public string Flags;
        public string Owner;
        public string Group;
        public bool IsDirectory;
        public DateTime DateModified;
        public string Name;
        public long Size;
    }

    public enum FileListStyle
    {
        UnixStyle,
        WindowsStyle,
        Unknown
    }

    public class IndexerConstants
    {
        public const string LibraryId = "LibraryId";
        public const string DisplayUserNamePassword = "DisplayUserNamePassword";

        public const string PathKey = "Path";
        public const string DefaultRoot = "~";

        public const char FileSeparator = '\\';
        public const char WebSeparator = '/';

        public const int RemoteLibrary_OID = 99;
        public const int RemoteItem_OID = 98;
    }

    public struct RemoteItemTypes
    {
        public const int Folder = 0;
        public const int File = 1;
    }

    public struct RemoteSourceTypes
    {
        public const int Ftp = 0;
        public const int Http = 1;
        public const int WindowShare = 2;
        public const int UnixShare = 3;

        private static Dictionary<int, string> _values = new Dictionary<int, string>
        {
            {0, "Ftp"},
            {1, "Http"},
            {2, "Windows Share"},
            {3, "UNIX Share"}
        };

        public static Dictionary<int, string> Values { get { return _values; } }

        public static string ToString(int value)
        {
            if (_values.ContainsKey(value))
                return _values[value];

            return string.Empty;
        }
    }
}
