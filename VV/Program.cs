// Program.cs - 12/26/2019

using Common.JSON;
using System;
using System.IO;

namespace VV
{
    class Program
    {
        static int Main(string[] args)
        {
            try
            {
                string startPath = args[0];
                if (!File.Exists($"{startPath}\\.vvconfig"))
                {
                    Console.WriteLine(".vvconfig file not found");
                    return 1;
                }
                JObject vvconfig = JObject.Parse(File.ReadAllText($"{startPath}\\.vvconfig"));
                string vvPath = (string)vvconfig.GetValue("VVPath");

                Console.WriteLine($"Vaulting files from \"{startPath}\" to \"{vvPath}\"");

                // build ignoreList
                JObject ignoreList = new JObject();
                JArray ignoreDirs = new JArray();
                ignoreDirs.Add("bin");
                ignoreDirs.Add("obj");
                ignoreDirs.Add(".*");
                JArray ignoreFiles = new JArray();
                ignoreFiles.Add(".*");
                ignoreFiles.Add("!.gitignore");
                ignoreList.Add("ignoredirs", ignoreDirs);
                ignoreList.Add("ignorefiles", ignoreFiles);

                // build list of files in a tree
                DirItem currTree = TreeRoutines.BuildTree(startPath, ".", ignoreList);

                // update base path to new VV2 directory
                vvPath = vvPath.Replace("\\VersionVault\\", "\\VV2\\");

                // backup the entire list of files
                long changeCount = VVBackup.BackupTree(startPath, vvPath, currTree);
                if (changeCount == 0)
                {
                    Console.WriteLine("No changes found");
                    return 0;
                }

                // build snapshot file
                if (!Directory.Exists($"{vvPath}\\.vvsnapshot"))
                {
                    Directory.CreateDirectory($"{vvPath}\\.vvsnapshot");
                }
                VVSnapshot vvs = new VVSnapshot();
                vvs.UTCDate = DateTime.UtcNow;
                vvs.Changes = changeCount;
                vvs.Data = currTree.ToJson();
                vvs.DataMD5 = MD5Utilities.CalcMD5FromString(currTree.ToString());
                File.WriteAllText(
                    $"{vvPath}\\.vvsnapshot\\{vvs.UTCDate?.ToString("yyyyMMddHHmmss")}.json",
                    vvs.ToString(JsonFormat.Indent));

                // done no errors
                Console.WriteLine($"Changes found: {changeCount}");
                return 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error encountered:\n{ex.Message}");
                return 1;
            }
            finally
            {
#if DEBUG
                Console.Write("Press enter to continue...");
                Console.ReadLine();
#endif
            }
        }
    }
}
