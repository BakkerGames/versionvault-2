// Program.cs - 05/06/2019

using Common.JSON;
using System;
using System.IO;

namespace VV
{
    class Program
    {
        static void Main(string[] args)
        {
            // todo check parameters
            string startPath = "D:\\Projects\\Common.JSON";
            if (!File.Exists($"{startPath}\\.vvconfig"))
            {
                Console.WriteLine(".vvconfig file not found");
                return;
            }
            JObject vvconfig = JObject.Parse(File.ReadAllText($"{startPath}\\.vvconfig"));
            Console.WriteLine(vvconfig.ToString(JsonFormat.Indent));
            Console.WriteLine(vvconfig.GetValue("VVPath"));
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
            DirItem currTree = TreeRoutines.BuildTree(startPath, ".", ignoreList);
            string vvPath = (string)vvconfig.GetValue("VVPath");

            vvPath = "D:\\VV2\\Common.JSON"; // ### for testing

            //VVBackup.BackupTree(startPath, vvPath, currTree);
            VVBackup.BackupTree(startPath, vvPath, currTree);
            if (!Directory.Exists($"{vvPath}\\.vvsnapshot"))
            {
                Directory.CreateDirectory($"{vvPath}\\.vvsnapshot");
            }
            File.WriteAllText($"{vvPath}\\.vvsnapshot\\{DateTime.UtcNow.ToString("yyyyMMddHHmmss")}.json", 
                currTree.ToString(JsonFormat.Indent));
            Console.Write("Done...");
            Console.ReadLine();
        }
    }
}
