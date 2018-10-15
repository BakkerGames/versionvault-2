using System.Collections.Generic;

namespace VV
{
    public class DirItem
    {
        public string Name;
        public readonly int? Type = 0;
        public List<FileItem> Files = new List<FileItem>();
        public List<DirItem> Dirs = new List<DirItem>();

        public DirItem()
        {
        }

        public DirItem(string name)
        {
            Name = name;
        }
    }
}
