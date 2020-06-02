using System;
using System.Collections.Generic;
using System.IO;

namespace Cas31.Libraries
{
    class FileManagement
    {
        private static string fileName = @"D:\Kurs\test.log";

        public static void WriteLine(string logMessage)
        {
            using (StreamWriter fileHandle = new StreamWriter(fileName, true))
            {
                fileHandle.WriteLine("{0}", logMessage);
            }
        }

        public static void Write(string logMessage)
        {
            using (StreamWriter fileHandle = new StreamWriter(fileName, true))
            {
                fileHandle.Write("{0}", logMessage);
            }
        }

    }
}
