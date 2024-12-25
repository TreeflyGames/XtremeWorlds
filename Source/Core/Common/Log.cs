using System;
using System.IO;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.CompilerServices;

namespace Core
{

    public static class Log
    {
        public static string GetFileContents(string fullPath)
        {
            string strContents = "";
            StreamReader objReader;

            try
            {
                if (!File.Exists(fullPath))
                    File.Create(fullPath);
                objReader = new StreamReader(fullPath);
                strContents = objReader.ReadToEnd();
                objReader.Close();
            }
            catch
            {
            }
            return strContents;
        }

        public static bool Add(string strData, string FN)
        {
            string fullpath;
            string contents;
            int bAns = 0;
            StreamWriter objReader;

            // Check if the directory exists
            if (!Directory.Exists(Path.Logs))
            {
                // Create the directory
                Directory.CreateDirectory(Path.Logs);
            }

            fullpath = System.IO.Path.Combine(Path.Logs, FN);
            contents = GetFileContents(fullpath);
            contents = contents + Environment.NewLine + strData;

            try
            {
                objReader = new StreamWriter(fullpath);
                objReader.Write(contents);
                objReader.Close();
                bAns = 1;
            }
            catch
            {
            }
            return Conversions.ToBoolean(bAns);
        }

        public static bool AddTextToFile(string strData, string fn)
        {
            string fullpath;
            string contents;
            int bAns = 0;
            StreamWriter objReader;

            fullpath = System.IO.Path.Combine(Path.Database, fn);
            contents = GetFileContents(fullpath);
            contents = contents + Environment.NewLine + strData;

            try
            {
                objReader = new StreamWriter(fullpath);
                objReader.Write(contents);
                objReader.Close();
                bAns = 1;
            }
            catch
            {
            }
            return Conversions.ToBoolean(bAns);
        }
    }
}