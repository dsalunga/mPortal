using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using System.Collections.Generic;
using System.IO;
using System.Diagnostics;
using System.Text;

namespace WCMS.Common.Utilities
{
    public abstract class Compression
    {
        private static string binPath;
        public const string SupportedExtensions = ".7z,.zip,.gz,.bz2";

        private static readonly Dictionary<string, string> SupportedArchives = new Dictionary<string, string>
        {
            {".zip", "zip"},
            {".7z", "7z"},
            {".gz", "gzip"},
            {".bz2", "bzip2"}
        };

        public static bool ExecuteExtractor(List<string> archives)
        {
            if (archives.Count > 0)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var archive in archives)
                    sb.AppendFormat(" \"{0}\"", archive);

                var processPath = WebHelper.MapPath(ConfigHelper.Get("Extractor.Path"));

                Process m = new Process();
                m.StartInfo.Arguments = sb.ToString();
                m.StartInfo.FileName = processPath;
                m.StartInfo.WorkingDirectory = FileHelper.GetFolder(processPath);
                m.Start();
            }

            return true;
        }

        public static bool IsSupportedArchive(string filename)
        {
            var ext = Path.GetExtension(filename).ToLower();

            return SupportedArchives.ContainsKey(ext);
        }

        static Compression()
        {
            var path = ConfigHelper.Get("Compressor.Path");
            if (string.IsNullOrEmpty(path))
                path = "~/Content/Plugins/7za/7za.exe";

            binPath = path;
        }

        private static string GetArchiveType(string file)
        {
            string ext = Path.GetExtension(file).ToLower();

            switch (ext)
            {
                case ".7z":
                    return "7z";
                case ".zip":
                    return "zip";
                case ".gz":
                    return "gzip";
                case ".bz2":
                    return "bzip2";
                case ".rar":
                    return "rar";
                default:
                    return string.Empty;
            }
        }

        public static int Add(string zipFile, string sourcePath, bool waitForExit, string password = "")
        {
            string cmdLineFormat = "a -t{0} \"{1}\" \"{2}\"";
            if (!string.IsNullOrWhiteSpace(password))
                cmdLineFormat += string.Format(@" -p""{0}""", password);

            string s7z = Context.Request.MapPath(binPath);
            string sArgs = string.Format(cmdLineFormat, GetArchiveType(zipFile), zipFile, sourcePath);

            return ExecuteProcess(waitForExit, s7z, sArgs);
        }

        public static int Add(string zipFile, string sourcePath, string password = "")
        {
            return Add(zipFile, sourcePath, false, password);
        }


        private static HttpContext Context { get { return HttpContext.Current; } }

        public static int Download2(string zipFile, string sourcePath, string password = "")
        {
            string cmdLineFormat = "a -t{0} \"{1}\" \"{2}\"";
            if(!string.IsNullOrWhiteSpace(password))
                cmdLineFormat += string.Format(@" -p""{0}""", password);

            string sFile = Path.GetFileName(zipFile);
            string s7z = Context.Request.MapPath(binPath);
            string sArgs = string.Format(cmdLineFormat, GetArchiveType(zipFile), zipFile, sourcePath);

            var exitCode = ExecuteProcess(true, s7z, sArgs);

            Context.Response.Clear();
            Context.Response.AppendHeader("content-disposition", "attachment; filename=" + sFile);
            Context.Response.WriteFile(zipFile);
            Context.Response.End();

            return exitCode;
        }

        public static int Download(string sourcePath, string baseFilename = "", string password = "")
        {
            string downloadFilename = string.IsNullOrWhiteSpace(baseFilename) ? "Download" : baseFilename;

            return Download2(Context.Request.MapPath(string.Format(WebHelper.TEMP_DATA_PATH + "{0}_{1}.zip", downloadFilename, Convert.ToString(DateTime.Now.Ticks, 16))), sourcePath, password);
        }

        public static int Extract(string sourcePath, string destPath, bool replaceAll, bool waitForExit)
        {
            // Extract with full paths (x), Replace all files (-y) SKIP: -aos, REPLACE: -aoa
            string s7z = WebHelper.MapPath(binPath);
            string sReplace = (replaceAll) ? "-aoa" : "-aos";
            string sDest = (destPath == string.Empty) ? string.Empty : string.Format("-o\"{0}\" ", destPath);
            string sArgs = string.Format("x \"{0}\" {1}-r {2}", sourcePath, sDest, sReplace);

            return ExecuteProcess(waitForExit, s7z, sArgs);
        }

        private static int ExecuteProcess(bool waitForExit, string filename, string sArgs)
        {
            Process m = new Process();
            m.StartInfo.Arguments = sArgs;
            m.StartInfo.FileName = filename;
            m.StartInfo.CreateNoWindow = true;
            m.StartInfo.UseShellExecute = false;
            m.Start();
            m.PriorityClass = ProcessPriorityClass.BelowNormal;
            if (waitForExit)
            {
                m.WaitForExit();
                return m.ExitCode;
            }

            return -1;
        }

        public static int Extract(string sSource, string sDestination, bool replaceAll)
        {
            return Extract(sSource, sDestination, replaceAll, false);
        }

        public static int Extract(string sSource, string sDestination)
        {
            return Extract(sSource, sDestination, false, false);
        }

        public static int Extract(string sSource)
        {
            return Extract(sSource, string.Empty, false, false);
        }
    }
}
