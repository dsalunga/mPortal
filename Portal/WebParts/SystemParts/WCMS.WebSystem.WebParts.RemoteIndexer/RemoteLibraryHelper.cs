using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;
using WCMS.Common.Net;
using WCMS.Common.Utilities;

namespace WCMS.WebSystem.WebParts.RemoteIndexer
{
    public class RemoteLibraryHelper
    {
        public static void InvokeDownload(int id, bool force = true, int timeOut = 0)
        {
            var item = RemoteItem.Provider.Get(id);
            if (item != null)
            {
                var library = item.Library;
                switch (library.SourceTypeId)
                {
                    case RemoteSourceTypes.WindowShare:
                        item.SetPathSeparator(IndexerConstants.FileSeparator);
                        InvokeWindowsFileDownload(library, item, force);
                        break;

                    case RemoteSourceTypes.Ftp:
                        InvokeFtpDownload(library, item, force, timeOut);
                        break;
                }
            }
            WCMS.Common.Utilities.HttpContextHelper.Current?.Response.CompleteAsync().Wait();
        }

        public static void InvokeWindowsFileDownload(RemoteItem item, bool force)
        {
            InvokeWindowsFileDownload(item.Library, item, force);
        }

        private static void InvokeWindowsFileDownload(RemoteLibrary library, RemoteItem item, bool force)
        {
            if (library.SourceTypeId == RemoteSourceTypes.WindowShare && !string.IsNullOrEmpty(library.UserName) && !string.IsNullOrEmpty(library.Password))
            {
                var networkCredential = new NetworkCredential(library.UserName, library.Password);
                new NetworkConnection(library.BaseAddress, networkCredential);
                WebUtil.DownloadFile(item.FullPath, item.Name, true, force);
            }
            else
            {
                WebUtil.DownloadFile(item.FullPath, item.Name, true, force);
            }
        }

        //private static string GetMd5Hash(string input)
        //{
        //    // Create a new Stringbuilder to collect the bytes 
        //    // and create a string.
        //    StringBuilder sBuilder = new StringBuilder();

        //    using (MD5 md5Hash = MD5.Create())
        //    {
        //        // Convert the input string to a byte array and compute the hash. 
        //        byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

        //        // Loop through each byte of the hashed data  
        //        // and format each one as a hexadecimal string. 
        //        for (int i = 0; i < data.Length; i++)
        //            sBuilder.Append(data[i].ToString("x2"));

        //        // Return the hexadecimal string. 
        //    }

        //    return sBuilder.ToString();
        //}

        public static void InvokeFtpDownload(RemoteLibrary library, RemoteItem item, bool force, int timeOut = 0)
        {
            var requestPath = FtpHelper.UrlEncode(item.BuildDisplayPath(library));
            var ftpClient = (FtpWebRequest)WebRequest.Create(requestPath);
            ftpClient.Method = WebRequestMethods.Ftp.DownloadFile;
            ftpClient.Proxy = WebRequest.GetSystemWebProxy();
            ftpClient.UseBinary = true;
            ftpClient.Timeout = timeOut > 0 ? timeOut : 3600000;
            ftpClient.KeepAlive = false;
            //ftpClient.UsePassive = false; // Newly added
            ftpClient.Credentials = new NetworkCredential(library.UserName, library.Password);

            //ftpClient.BeginGetResponse
            var httpContext = HttpContextHelper.Current;
            var response = httpContext.Response;
            var request = httpContext.Request;

            string lastUpdateTiemStr = item.DateModified.ToUniversalTime().ToString("r");
            string eTag = System.Net.WebUtility.UrlEncode(item.Name) + " " + lastUpdateTiemStr;
            if (!string.IsNullOrEmpty(request.Headers["If-Range"].ToString()))
            {
                if (request.Headers["If-Range"].ToString().Replace("\"", "") != eTag)
                {
                    response.StatusCode = (int)HttpStatusCode.PreconditionFailed;
                    return;
                }
            }

            try
            {
                using (var ftpResponse = ftpClient.GetResponse() as FtpWebResponse) // #2
                {
                    using (var ftpStream = ftpResponse.GetResponseStream())
                    {
                        long fileLength = ftpResponse.ContentLength;
                        long startBytes = 0;

                        if(fileLength < 0)
                            fileLength = item.Size;

                        response.Headers.Clear();
                        // BufferOutput not available in ASP.NET Core (responses are unbuffered by default)
                        //httpResponse.AddHeader("Content-MD5", GetMd5Hash(myFile));
                        response.Headers.Append("Accept-Ranges", "bytes");
                        response.Headers.Append("ETag", "\"" + eTag + "\"");
                        response.Headers.Append("Last-Modified", lastUpdateTiemStr);

                        if (force)
                        {
                            response.Headers.Append("Content-Disposition", string.Format("attachment; filename=\"{0}\"", item.Name));
                        }
                        else
                        {
                            response.Headers.Append("Content-Disposition", string.Format("filename=\"{0}\"", item.Name));
                            response.ContentType = MIMEHelper.GetMIMEType(item.Name);
                        }
                        response.Headers.Append("Content-Length", fileLength.ToString());
                        response.Headers.Append("Connection", "Keep-Alive");


                        if (!string.IsNullOrEmpty(request.Headers["Range"].ToString()))
                        {
                            response.StatusCode = (int)HttpStatusCode.PartialContent;
                            string[] range = request.Headers["Range"].ToString().Split(new char[] { '=', '-' });
                            startBytes = Convert.ToInt64(range[1]);
                            if (startBytes < 0 || startBytes >= fileLength)
                            {
                                return;
                            }
                                
                        }

                        if (startBytes > 0)
                            response.Headers.Append("Content-Range", string.Format(" bytes {0}-{1}/{2}", startBytes, fileLength - 1, fileLength));

                        int packSize = 1024 * 10; //read in block，every block 10K bytes //65536; //2048 // 10240;
                        int readCount;
                        byte[] buffer = new byte[packSize];

                        try
                        {
                            var responseStream = response.Body;

                            if (startBytes > 0)
                            {
                                byte[] dummyBuffer = new byte[startBytes];
                                ftpStream.Read(dummyBuffer, 0, (int)startBytes);
                            }

                            do
                            {
                                readCount = ftpStream.Read(buffer, 0, packSize);
                                responseStream.Write(buffer, 0, readCount);
                                response.Body.FlushAsync().GetAwaiter().GetResult();
                            }
                            while (readCount > 0 && !httpContext.RequestAborted.IsCancellationRequested);

                            //response.StatusCode = (int)HttpStatusCode.OK;
                            responseStream.Close();

                            // #1 should resolve:
                            // The remote server returned an error: (421) Service not available, closing control connection.
                        }
                        catch (WebException ex)
                        {
                            LogHelper.WriteLog(ex);
                        }
                        catch (System.Net.Http.HttpRequestException ex)
                        {
                            LogHelper.WriteLog(ex);
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                // Should resolve #2:
                // Unable to connect to the remote server

                LogHelper.WriteLog(ex);
            }
        }
    }
}
