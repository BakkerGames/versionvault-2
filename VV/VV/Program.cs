// Program.cs - 10/15/2018

namespace VV
{
    class Program
    {
        static void Main(string[] args)
        {
            // todo check parameters
            string startPath = ".";
            JObject vvTree = TreeRoutines.BuildTree(startPath);
            // todo check tree vs dir/files
        }
    }
}
