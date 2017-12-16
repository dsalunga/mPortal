using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WCMS.WebSystem.WebParts.RemoteIndexer.Common;

namespace WCMS.WebSystem.WebParts.RemoteIndexer
{
    class Program
    {
        static void Main(string[] args)
        {
            //string argInput = Console.ReadLine();

            //if (string.IsNullOrWhiteSpace(argInput))
            //{
            //    Console.WriteLine("\n Usage FTPListDirParser <uriString>");
            //    return;
            //}

            try
            {
                FtpIndexer indexer = new FtpIndexer("ftp://someorg.org/", "xupload", "xupload");
                //indexer.Start("ftp://someorg.org/", "ftpuser", "password");

                var itemList = indexer.GetItemList("FOLDER_PATH", 3); //this.GetFileList(responseString);
                Console.WriteLine("------------After Parsing-----------");

                foreach (FileStruct fileStruct in itemList)
                {
                    if (fileStruct.IsDirectory)
                        Console.WriteLine("<DIR> " + fileStruct.Name + "," + fileStruct.Owner + "," + fileStruct.Flags + "," + fileStruct.DateModified);
                    else
                        Console.WriteLine(fileStruct.Name + "," + fileStruct.Owner + "," + fileStruct.Flags + "," + fileStruct.DateModified);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            Console.ReadLine();
        }
    }
}
