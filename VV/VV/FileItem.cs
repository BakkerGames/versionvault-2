using System;

namespace VV
{
    public class FileItem
    {
        public string Name;
        public readonly int? Type = 1;
        public long? Size = 0;
        public DateTime UTCDate;
        public string MD5;

        public FileItem()
        {
        }

        public FileItem(string name)
        {
            Name = name;
        }
    }
}
