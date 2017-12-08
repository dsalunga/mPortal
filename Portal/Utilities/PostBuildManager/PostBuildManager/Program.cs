using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

using WCMS.Common.Utilities;

namespace PostBuildManager
{
    class Program
    {
        private const string PART = "/part:";
        private static string[] BIN_IGNORE = { "WCMS.WebSystem.", "WCMS.Framework.", "WCMS.Common.", "FredCK.", "AjaxControlToolkit." };
        private const string WEB_SYSTEM_IN_DLL = "WCMS.WebSystem.WebParts.";
        private static string[] GLOBAL_IGNORE_FOLDERS = { ".svn" };

        static void Main(string[] args)
        {
            var currentDir = AppDomain.CurrentDomain.BaseDirectory;
            var systemBaseDir = ConfigHelper.Get("SystemBaseDir");

            Console.WriteLine("CurrentDirectory: " + currentDir);
            Console.WriteLine("args: {0}", args);
            Console.WriteLine();
            // Console.WriteLine("PRESS ANY KEY TO CONTINUE...");
            // Console.Read();

            if (!string.IsNullOrEmpty(systemBaseDir))
            {
                Console.WriteLine("SystemBaseDir: " + systemBaseDir);

                systemBaseDir = Path.GetFullPath(systemBaseDir);

                Console.WriteLine("SystemBaseDir Full: " + systemBaseDir);

                if (Directory.Exists(systemBaseDir))
                {
                    foreach (var arg in args)
                    {
                        if (!string.IsNullOrEmpty(arg)) //arg.StartsWith("/part:"))
                        {
                            Console.WriteLine("PartBaseDir: " + arg);

                            var partBaseDir = Path.GetFullPath(arg);

                            Console.WriteLine("PartBaseDir Full: " + partBaseDir);

                            if (Directory.Exists(partBaseDir))
                            {
                                var projectPartDir = Path.Combine(currentDir, string.Format(@"{0}\Content\Parts", partBaseDir));
                                var projectMergeXml = Path.Combine(currentDir, string.Format(@"{0}\Merge.xml", partBaseDir));
                                var projectBinDir = Path.Combine(currentDir, string.Format(@"{0}\bin", partBaseDir));
                                var systemBinDir = Path.Combine(currentDir, string.Format(@"{0}\bin\", systemBaseDir));
                                var systemPartsDir = Path.Combine(currentDir, string.Format(@"{0}\Content\Parts", systemBaseDir));

                                #region Local Methods

                                Func<string, bool> IsIgnoreDir = (checkDir) =>
                                {
                                    bool ignore = false;
                                    foreach (var globalIgnoreDir in GLOBAL_IGNORE_FOLDERS)
                                    {
                                        var subDirName = Path.GetFileName(checkDir);
                                        if (globalIgnoreDir.Equals(subDirName, StringComparison.InvariantCultureIgnoreCase))
                                        {
                                            ignore = true;
                                            break;
                                        }
                                    }

                                    return ignore;
                                };

                                Action<string, string> CopyWithCheck = (source, dest) =>
                                {
                                    bool continueCopy = true;
                                    if (File.Exists(dest))
                                        if (File.GetLastWriteTimeUtc(source).Ticks == File.GetLastWriteTimeUtc(dest).Ticks)
                                            continueCopy = false;

                                    if (continueCopy)
                                        File.Copy(source, dest, true);
                                };

                                Action<string, string, string> CopyPartFilesRecursive = null;
                                CopyPartFilesRecursive = (currSysPartDir, srcSubDir, filter) =>
                                {
                                    if (Directory.Exists(srcSubDir))
                                    {
                                        var srcSubDirName = Path.GetFileName(srcSubDir);
                                        var systemPartDest = Path.Combine(currSysPartDir, srcSubDirName);
                                        if (!Directory.Exists(systemPartDest))
                                            Directory.CreateDirectory(systemPartDest);

                                        var partFiles = Directory.EnumerateFiles(srcSubDir);
                                        foreach (var partFile in partFiles)
                                        {
                                            if (filter == null || !partFile.EndsWith(filter))
                                            {
                                                var systemPartFileDest = Path.Combine(systemPartDest, Path.GetFileName(partFile));
                                                CopyWithCheck(partFile, systemPartFileDest);
                                            }
                                        }

                                        var partSubDirs = Directory.EnumerateDirectories(srcSubDir);
                                        foreach (var partSubDir in partSubDirs)
                                            if (!IsIgnoreDir(partSubDir))
                                                CopyPartFilesRecursive(systemPartDest, partSubDir, filter);
                                    }
                                };


                                #endregion

                                #region Copy all Web files

                                // Copy all Parts files
                                if (false) //Directory.Exists(projectPartDir))
                                {
                                    var subDirs = Directory.EnumerateDirectories(projectPartDir);
                                    if (subDirs.Count() > 0)
                                        foreach (var subDir in subDirs)
                                            if (!IsIgnoreDir(subDir))
                                                CopyPartFilesRecursive(systemPartsDir, subDir, ".cs");
                                }

                                #endregion

                                #region Copy Bin files

                                if (false) //Directory.Exists(projectBinDir))
                                {
                                    bool hasCopyDirXml = false;
                                    bool hasCopyFileXml = false;
                                    XmlNodeList copyDirNodes = null;
                                    XmlNodeList copyFileNodes = null;
                                    if (File.Exists(projectMergeXml))
                                    {
                                        XmlDocument xdoc = new XmlDocument();
                                        xdoc.Load(projectMergeXml);

                                        copyDirNodes = xdoc.SelectNodes("//CopyBin/Directories/Directory");
                                        copyFileNodes = xdoc.SelectNodes("//CopyBin/Files/File");

                                        hasCopyDirXml = copyDirNodes.Count > 0;
                                        hasCopyFileXml = copyFileNodes.Count > 0;
                                    }

                                    if (hasCopyFileXml)
                                    {
                                        // Copy dirs using Setup.xml
                                        foreach (XmlNode node in copyFileNodes)
                                        {
                                            string name = XmlUtil.GetAttributeValue(node, "Name");

                                            if (!string.IsNullOrEmpty(name))
                                            {
                                                if (!name.Contains('*'))
                                                {
                                                    var binFile = Path.Combine(projectBinDir, name);
                                                    var targetBinFile = Path.Combine(systemBinDir, name);
                                                    CopyWithCheck(binFile, targetBinFile);
                                                }
                                                else
                                                {
                                                    // Has Search Pattern
                                                    var binFiles = Directory.EnumerateFiles(projectBinDir, name);
                                                    foreach (var binFile in binFiles)
                                                    {
                                                        var targetBinFile = Path.Combine(systemBinDir, Path.GetFileName(binFile));
                                                        CopyWithCheck(binFile, targetBinFile);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // Copy bin files using default behaviors
                                        var binFiles = Directory.EnumerateFiles(projectBinDir);
                                        foreach (var binFile in binFiles)
                                        {
                                            var binFileName = Path.GetFileName(binFile);

                                            bool ignore = false;

                                            if (!binFileName.StartsWith(WEB_SYSTEM_IN_DLL, StringComparison.InvariantCultureIgnoreCase))
                                            {
                                                foreach (var binIgnore in BIN_IGNORE)
                                                {
                                                    if (binFileName.StartsWith(binIgnore, StringComparison.InvariantCultureIgnoreCase))
                                                    {
                                                        ignore = true;
                                                        break;
                                                    }
                                                }
                                            }

                                            if (!ignore)
                                            {
                                                var targetBinFile = Path.Combine(systemBinDir, binFileName);
                                                CopyWithCheck(binFile, targetBinFile);
                                            }
                                        }
                                    }

                                    if (hasCopyDirXml)
                                    {
                                        // Get file list from Setup.xml
                                        foreach (XmlNode node in copyDirNodes)
                                        {
                                            string name = XmlUtil.GetAttributeValue(node, "Name");

                                            if (!string.IsNullOrEmpty(name))
                                            {
                                                if (!name.Contains('*'))
                                                {
                                                    var binDir = Path.Combine(projectBinDir, name);
                                                    CopyPartFilesRecursive(systemBinDir, binDir, null);
                                                }
                                                else
                                                {
                                                    // Has SearchPattern
                                                    var binDirs = Directory.EnumerateDirectories(projectBinDir, name);
                                                    foreach (var binDir in binDirs)
                                                        if (!IsIgnoreDir(binDir))
                                                            CopyPartFilesRecursive(systemBinDir, binDir, null);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        // Copy bin dirs using default behavior
                                        var binDirs = Directory.EnumerateDirectories(projectBinDir);
                                        foreach (var binDir in binDirs)
                                            if (!IsIgnoreDir(binDir))
                                                CopyPartFilesRecursive(systemBinDir, binDir, null);
                                    }
                                }

                                #endregion
                            }
                            else
                            {
                                Console.WriteLine("PartBaseDir: Does not exist.");
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("SystemBaseDir: Does not exist.");
                }
            }
        }
    }
}
