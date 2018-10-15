using System;
using System.IO;

namespace VV
{
    public static partial class TreeRoutines
    {
        public static DirItem BuildTree(string startPath, string currDir, JObject ignoreList)
        {
            // todo build the tree
            string baseDirName = currDir.Substring(currDir.LastIndexOf("\\") + 1);
            DirItem result = new DirItem(baseDirName);
            string currDirFull;
            if (currDir != ".")
            {
                currDirFull = $"{startPath}\\{currDir}";
            }
            else
            {
                currDirFull = startPath;
            }
            foreach (string currFile in Directory.GetFiles(currDirFull))
            {
                string baseFileName = currFile.Substring(currFile.LastIndexOf("\\") + 1);
                if (!IgnoreFile(baseFileName, ignoreList))
                {
                    result.FileList.Add(new FileItem(baseFileName));
                }
            }
            foreach (string subDir in Directory.GetDirectories(currDirFull))
            {
                string baseSubDirName = subDir.Substring(subDir.LastIndexOf("\\") + 1);
                if (!IgnoreDir(baseSubDirName, ignoreList))
                {
                    result.DirList.Add(BuildTree(currDirFull, baseSubDirName, ignoreList));
                }
            }
            // done
            return result;
        }

        private static bool IgnoreFile(string baseFileName, JObject ignoreList)
        {
            return false;
        }

        private static bool IgnoreDir(string baseSubDirName, JObject ignoreList)
        {
            foreach (string ignoreItem in (JArray)ignoreList.GetValueOrNull("ignoredirs"))
            {
                if (baseSubDirName.Equals(ignoreItem, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
