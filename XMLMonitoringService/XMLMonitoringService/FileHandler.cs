using System;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace XMLMonitoringService
{
    static class FileHandler
    {
        private static readonly StringBuilder _sb = new StringBuilder();

        public static void Log(String message)
        {
            _sb.Append(message);
        }

        public static bool CanAccessFile(string FileName)
        {
            try
            {
                var fileToRead = new FileInfo(FileName);
                FileStream f = fileToRead.Open(FileMode.Open, FileAccess.Write, FileShare.None);
                /*
                     * Since the file is opened now close it and we can access it
                     */
                f.Close();
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Cannot open " + FileName + " Exception raised - " + ex.Message);
            }

            return false;
        }

        public static bool ValidateInputPath()
        {
            return Directory.Exists(Config.InputSource);          
        }

        public static void Write()
        {
            if (CanAccessFile(Config.OutputSource))
            {
                FileHandler.Log("\n");
                File.AppendAllText(Config.OutputSource, _sb.ToString());
                Console.WriteLine("XML data logged!");
                _sb.Clear();
            }
            
        }
    }
}