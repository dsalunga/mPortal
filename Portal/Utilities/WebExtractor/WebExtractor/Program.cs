using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using WCMS.Common.Utilities;

namespace WebExtractor
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                var rootPath = FileHelper.EvalPath(ConfigHelper.Get("Extractor.RootPath"));

                Console.WriteLine("RootPath: {0}", rootPath);

                foreach (var arg in args)
                {
                    var filePath = FileHelper.IsAbsolutePath(arg) ? FileHelper.EvalPath(arg) : FileHelper.Combine(rootPath, arg);
                    if (File.Exists(filePath))
                    {
                        var fileFolder = FileHelper.GetFolder(filePath);

                        Console.WriteLine("Extracting: {0}", filePath);

                        Compression.Extract(filePath, fileFolder, true, true);

                        Console.WriteLine("Success! Deleting archive...");

                        File.Delete(filePath);
                    }
                    else
                    {
                        Console.WriteLine("Error, file does not exist: {0}", filePath);
                    }
                }

                Console.WriteLine("Extraction completed!");
            }
            else
            {
                Console.WriteLine("Missing arguments: app.exe \"archive_01\" \"archive_02\" ...");
            }
        }
    }
}
