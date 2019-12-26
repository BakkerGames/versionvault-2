// VVSnapshot.cs - 12/26/2019

using Common.JSON;
using System;

namespace VV
{
    public class VVSnapshot
    {
        private const string _dateTimeFormat = "O";

        public string AppName = "vv";
        public decimal? Version = 2;
        public DateTime? UTCDate;
        public string DataMD5;
        public JObject Data;

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
            result.Add("appname", AppName);
            result.Add("version", Version);
            result.Add("utcdate", UTCDate.Value.ToString(_dateTimeFormat));
            result.Add("datamd5", DataMD5);
            result.Add("data", Data);
            return result;
        }
    }
}
