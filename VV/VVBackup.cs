using System;
using System.IO;

namespace VV
{
    public static class VVBackup
    {
        public static long BackupTree(string startpath, string vvpath, DirItem currTree, ref long vaultCount)
        {
            long count = 0;
            foreach (FileItem fi in currTree.FileList)
            {
                vaultCount++;
                Console.Write("\r");
                Console.Write(vaultCount);
                string targetPath = $"{vvpath}\\{fi.Name}";
                if (!Directory.Exists(targetPath))
                {
                    Directory.CreateDirectory(targetPath);
                }
                // get the last period position in case there are more than one
                int periodPos = fi.Name.LastIndexOf(".");
                string targetFilename;
                if (periodPos < 0)
                {
                    targetFilename = $"{fi.MD5}";
                }
                else
                {
                    targetFilename = $"{fi.MD5}{fi.Name.Substring(periodPos)}";
                }
                string target = $"{targetPath}\\{targetFilename}";
                if (!File.Exists(target))
                {
                    File.Copy($"{startpath}\\{fi.Name}", target);
                    File.SetAttributes(target, FileAttributes.ReadOnly);
                    count++;
                    fi.Changed = true;
                }
            }
            foreach (DirItem di in currTree.DirList)
            {
                count += BackupTree($"{startpath}\\{di.Name}", $"{vvpath}\\{di.Name}", di, ref vaultCount);
            }
            return count;
        }
    }
}
