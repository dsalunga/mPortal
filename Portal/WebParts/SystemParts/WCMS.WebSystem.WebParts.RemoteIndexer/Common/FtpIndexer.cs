using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Net;

using WCMS.Common.Utilities;
using WCMS.Framework;

namespace WCMS.WebSystem.WebParts.RemoteIndexer.Common
{
    public class FtpIndexer : IRemoteIndexer
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string BaseAddress { get; set; }

        public char PathSeparator { get { return IndexerConstants.WebSeparator; } }

        private IWebProxy _proxy = null;

        public FtpIndexer()
        {
            _proxy = WebRequest.GetSystemWebProxy();
        }

        public FtpIndexer(string baseAddress, string userName, string password)
            : this()
        {
            BaseAddress = baseAddress;
            UserName = userName;
            Password = password;
        }

        public bool SaveToCache(string itemAddress, string target, int maxRetries)
        {
            return true;
        }

        public void DeleteCache(string cachePath)
        {
        }

        public List<FileStruct> GetItemList(string relativeOrPartialAddress, int maxRetries)
        {
            int remRetries = maxRetries > 0 ? maxRetries : 0;
            var itemList = new List<FileStruct>();
            string absPath = relativeOrPartialAddress.StartsWith(BaseAddress) ? relativeOrPartialAddress : FileHelper.Combine(BaseAddress, relativeOrPartialAddress, PathSeparator);
            Exception exception = null;

            do
            {
                try
                {
                    var path = FtpHelper.UrlEncode(absPath);
                    var ftpClient = (FtpWebRequest)WebRequest.Create(path);
                    ftpClient.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                    ftpClient.Proxy = _proxy;
                    //ftpClient.UsePassive = false; // Newly added
                    ftpClient.Credentials = new NetworkCredential(UserName, Password);

                    using (var response = ftpClient.GetResponse() as FtpWebResponse)
                    {
                        var reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.ASCII);
                        string responseString = reader.ReadToEnd();

                        reader.Close();
                        response.Close();

                        itemList = this.GetFileList(responseString);
                    }

                    exception = null;
                }
                catch (Exception ex)
                {
                    exception = ex;
                }

                remRetries--;
            }
            while (remRetries > 0 && exception != null);

            if (exception != null)
                throw exception;

            return itemList;
        }

        private List<FileStruct> GetFileList(string responseString)
        {
            var items = new List<FileStruct>();
            string[] lines = responseString.Split('\n');
            var listStyle = GuessFileListStyle(lines);

            foreach (string line in lines)
            {
                if (listStyle != FileListStyle.Unknown && line != "")
                {
                    var file = new FileStruct();
                    file.Name = "..";

                    switch (listStyle)
                    {
                        case FileListStyle.UnixStyle:
                            file = ParseUnixStyleRecord(line);
                            break;
                        case FileListStyle.WindowsStyle:
                            file = ParseWindowsStyleRecord(line);
                            break;
                    }

                    if (!(file.Name == "." || file.Name == ".."))
                        items.Add(file);
                }
            }

            return items;
        }

        private string GetHttpResponse(string url)
        {
            // used to build entire input
            var sb = new StringBuilder();

            // used on each read operation
            byte[] buffer = new byte[8192];

            // prepare the web page we will be asking for
            var request = (HttpWebRequest)WebRequest.Create(url); //"http://www.mayosoftware.com"

            // execute the request
            var response = (HttpWebResponse)request.GetResponse();

            // we will read data via the response stream
            using (Stream resStream = response.GetResponseStream())
            {
                string tempString = null;
                int count = 0;

                do
                {
                    // fill the buffer with data
                    count = resStream.Read(buffer, 0, buffer.Length);

                    // make sure we read some data
                    if (count != 0)
                    {
                        // translate from bytes to ASCII text
                        tempString = Encoding.ASCII.GetString(buffer, 0, count);

                        // continue building the string
                        sb.Append(tempString);
                    }
                }
                while (count > 0); // any more data to read?
            }

            // return page source
            return sb.ToString();
        }

        private FileStruct ParseWindowsStyleRecord(string record)
        {
            ///Assuming the record style as 
            /// 06-19-10  10:26AM               264258 2nd QTR _ITG Day 3 - 008.JPG
            /// 05-29-10  02:22AM       <DIR>          ams

            var fileStruct = new FileStruct();

            string line = record.Trim();
            string dateTimeString = line.Substring(0, 17).Trim();
            string dirOrSize = line.Substring(17, 21).Trim();

            fileStruct.Name = line.Substring(38).Trim();
            fileStruct.DateModified = DateTime.ParseExact(dateTimeString, "MM-dd-yy  hh:mmtt", CultureInfo.InvariantCulture);

            if (dirOrSize == "<DIR>")
            {
                // Record is directory
                fileStruct.IsDirectory = true;
            }
            else
            {
                // Record is file
                fileStruct.Size = long.Parse(dirOrSize);
            }

            return fileStruct;
        }


        private FileListStyle GuessFileListStyle(string[] recordList)
        {
            foreach (string s in recordList)
            {
                if (s.Length > 10 && Regex.IsMatch(s.Substring(0, 10), "(-|d)(-|r)(-|w)(-|x)(-|r)(-|w)(-|x)(-|r)(-|w)(-|x)"))
                    return FileListStyle.UnixStyle;
                else if (s.Length > 8 && Regex.IsMatch(s.Substring(0, 8), "[0-9][0-9]-[0-9][0-9]-[0-9][0-9]"))
                    return FileListStyle.WindowsStyle;
            }

            return FileListStyle.Unknown;
        }

        private FileStruct ParseUnixStyleRecord(string record)
        {
            /// Assuming record style as
            /// dr-xr-xr-x   1 owner    group               0 Nov 25  2002 bussys
            /// -rw-r--r-- 1 ftp ftp         120019 Aug 22  2012 292330_276680035774898_30890058_n.jpg
            /// -rw-r--r-- 1 ftp ftp            653 Nov 14  2012 HEALTH GUIDE.txt
            /// drwxr-xr-x 1 ftp ftp              0 Mar 29 10:12 New folder

            string tempString = record.Trim();

            var reader = new StringReader(tempString);

            var fileStruct = new FileStruct();
            fileStruct.Flags = DataHelper.ReadNextWord(reader); //tempString.Substring(0, 9);
            fileStruct.IsDirectory = (fileStruct.Flags[0] == 'd');

            //fileStruct.Owner = _cutSubstringFromStringWithTrim(ref tempString, ' ', 0);
            //fileStruct.Group = _cutSubstringFromStringWithTrim(ref tempString, ' ', 0);
            DataHelper.ReadNextWord(reader);

            fileStruct.Owner = DataHelper.ReadNextWord(reader);
            fileStruct.Group = DataHelper.ReadNextWord(reader);

            //string size = tempString.Substring(28, 14);
            fileStruct.Size = long.Parse(DataHelper.ReadNextWord(reader)); //size);

            // Skip Date
            var date = DataHelper.ReadNextWord(reader);
            date += " " + DataHelper.ReadNextWord(reader);
            date += " " + DataHelper.ReadNextWord(reader);

            //string dateString = tempString.Substring(43, 12); //DateTime.Parse(_cutSubstringFromStringWithTrim(ref tempString, ' ', 8)
            fileStruct.DateModified = DataHelper.GetDateTime(date); //DateTime.Now; //DateTime.Parse(dateString);
            fileStruct.Name = reader.ReadToEnd().Trim(); //tempString.Substring(56).Trim();   //Rest of the part is name

            return fileStruct;
        }
    }
}
