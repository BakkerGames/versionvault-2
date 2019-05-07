// MD5Utilities.cs - 05/06/2019

using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace VV
{
    public class MD5Utilities
    {
        public static string CalcMD5(string filepath)
        {
            MD5 md5Hasher = MD5.Create();
            FileStream fs = File.OpenRead(filepath);
            byte[] result = md5Hasher.ComputeHash(fs);
            fs.Close();
            StringBuilder hexResult = new StringBuilder();
            foreach (byte b in result)
            {
                hexResult.Append(b.ToString("x2"));
            }
            return hexResult.ToString();
        }
    }
}
