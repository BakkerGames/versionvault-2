// FileItem.cs - 12/26/2019

using Common.JSON;
using System;
using System.IO;

namespace VV
{
    public class FileItem
    {
        private const string _dateTimeFormat = "O";

        public string Name;
        public readonly int? Type = 1;
        public long? Size = 0;
        public DateTime? UTCDate;
        public string MD5;
        public bool? Changed = false;

        public FileItem()
        {
        }

        public FileItem(string name)
        {
            Name = name;
        }

        public FileItem(string name, string path)
        {
            Name = name;
            string fullpath = $"{path}\\{name}";
            if (File.Exists(fullpath))
            {
                FileInfo fi = new FileInfo(fullpath);
                Size = fi.Length;
                UTCDate = fi.LastWriteTimeUtc;
                MD5 = MD5Utilities.CalcMD5FromFile(fullpath);
            }
        }

        public override string ToString()
        {
            return ToJson().ToString();
        }

        public string ToString(JsonFormat format)
        {
            return ToJson().ToString(format);
        }

        public JObject ToJson()
        {
            JObject result = new JObject();
            result.Add("name", Name);
            result.Add("type", Type);
            if (Size > 0)
            {
                result.Add("size", Size);
            }
            if (UTCDate.HasValue)
            {
                result.Add("utcdate", UTCDate.Value.ToString(_dateTimeFormat));
            }
            if (!string.IsNullOrEmpty(MD5))
            {
                result.Add("md5", MD5);
            }
            result.Add("changed", Changed);
            return result;
        }
    }
}
