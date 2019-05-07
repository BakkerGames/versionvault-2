using System.IO;

namespace VV
{
    public static class VVBackup
    {
        public static void BackupTree(string startpath, string vvpath, DirItem currTree)
        {
            foreach (FileItem fi in currTree.FileList)
            {
                string targetPath = $"{vvpath}\\{fi.Name}";
                if (!Directory.Exists(targetPath))
                {
                    Directory.CreateDirectory(targetPath);
                }
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
                if (!File.Exists($"{targetPath}\\{targetFilename}"))
                {
                    File.Copy($"{startpath}\\{fi.Name}", $"{targetPath}\\{targetFilename}");
                }
            }
            foreach (DirItem di in currTree.DirList)
            {
                BackupTree($"{startpath}\\{di.Name}", $"{vvpath}\\{di.Name}", di);
            }
        }
    }
}
