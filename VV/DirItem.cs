// DirItem.cs - 05/06/2019

using Common.JSON;
using System.Collections.Generic;

namespace VV
{
    public class DirItem
    {
        public string Name;
        public readonly int? Type = 0;
        public List<FileItem> FileList = new List<FileItem>();
        public List<DirItem> DirList = new List<DirItem>();

        public DirItem()
        {
        }

        public DirItem(string name)
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
            if (FileList.Count > 0)
            {
                JArray files = new JArray();
                foreach (FileItem file in FileList)
                {
                    files.Add(file.ToJson());
                }
                result.Add("files", files);
            }
            if (DirList.Count > 0)
            {
                JArray dirs = new JArray();
                foreach (DirItem dir in DirList)
                {
                    dirs.Add(dir.ToJson());
                }
                result.Add("dirs", dirs);
            }
            return result;
        }
    }
}
