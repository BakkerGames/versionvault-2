// Program.cs - 10/15/2018

using System;

namespace VV
{
    class Program
    {
        static void Main(string[] args)
        {
            // todo check parameters
            string startPath = "..\\..";
            JObject ignoreList = new JObject();
            JArray ignoreDirs = new JArray();
            ignoreDirs.Add("bin");
            ignoreDirs.Add("obj");
            JArray ignoreFiles = new JArray();
            ignoreList.Add("ignoredirs", ignoreDirs);
            ignoreList.Add("ignorefiles", ignoreFiles);
            DirItem currTree = TreeRoutines.BuildTree(startPath, ".", ignoreList);
            // todo check tree vs dir/files
            Console.WriteLine(currTree.ToString(JsonFormat.Indent));
            Console.ReadLine();
        }
    }
}
