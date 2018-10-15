using System;

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

        public FileItem()
        {
        }

        public FileItem(string name)
        {
            Name = name;
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
            return result;
        }
    }
}
