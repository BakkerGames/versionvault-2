// TreeRoutines.cs - 05/06/2019

using Common.JSON;
using System;
using System.IO;
using System.Text.RegularExpressions;

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
                    result.FileList.Add(new FileItem(baseFileName, currDirFull));
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

        private static bool IgnoreFile(string name, JObject ignoreList)
        {
            // check for "!" don't ignore first
            foreach (string ignoreItem in (JArray)ignoreList.GetValueOrNull("ignorefiles"))
            {
                if (ignoreItem.StartsWith("!"))
                {
                    if (RegexMatches(name, ignoreItem.Substring(1)))
                    {
                        return false;
                    }
                }
            }
            // check for ignore matches next
            foreach (string ignoreItem in (JArray)ignoreList.GetValueOrNull("ignorefiles"))
            {
                if (name.Equals(ignoreItem, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                else if (ignoreItem.Contains("*") || ignoreItem.Contains("?"))
                {
                    if (RegexMatches(name, ignoreItem))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static bool IgnoreDir(string name, JObject ignoreList)
        {
            // check for "!" don't ignore first
            foreach (string ignoreItem in (JArray)ignoreList.GetValueOrNull("ignoredirs"))
            {
                if (ignoreItem.StartsWith("!"))
                {
                    if (RegexMatches(name, ignoreItem.Substring(1)))
                    {
                        return false;
                    }
                }
            }
            // check for ignore matches next
            foreach (string ignoreItem in (JArray)ignoreList.GetValueOrNull("ignoredirs"))
            {
                if (name.Equals(ignoreItem, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
                else if (ignoreItem.Contains("*") || ignoreItem.Contains("?"))
                {
                    if (RegexMatches(name, ignoreItem))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private static bool RegexMatches(string name, string ignoreItem)
        {
            // change ? and * to proper regular expressions
            Regex rx = new Regex($"^{ignoreItem.Replace(".", "\\.").Replace("?", ".").Replace("*", ".*")}$"
                , RegexOptions.Compiled | RegexOptions.IgnoreCase);
            if (rx.IsMatch(name))
            {
                return true;
            }
            return false;
        }
    }
}
