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
            ignoreFiles.Add("!*.cs");
            ignoreFiles.Add("*.*");
            ignoreList.Add("ignoredirs", ignoreDirs);
            ignoreList.Add("ignorefiles", ignoreFiles);
            DirItem currTree = TreeRoutines.BuildTree(startPath, ".", ignoreList);
            File.WriteAllText($"{startPath}\\.vvinfo", currTree.ToString(JsonFormat.Indent));
            Console.Write("Done...");
            Console.ReadLine();
        }
    }
}
