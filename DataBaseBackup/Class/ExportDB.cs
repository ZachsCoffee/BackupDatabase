using System.IO;
using System;
using System.Diagnostics;

namespace DataBaseBackup.Class
{
    public class ExportDB
    {
        public static string exportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Exports\");

        public static int Export(string binPath, string userName, string password, string dbName, out string exportedFile)
        {

            exportedFile = exportPath + dbName + DateTime.Now.ToString(" dd-MM-yy H-mm-ss FFFFF") + ".txt";
            //Debug.Print("/C \"" + binPath + "\\mysqldump\" -u " + userName + " -p" + password + " " + dbName + " > \"" + exportedFile+"\"");
            Directory.CreateDirectory(exportPath);
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                WorkingDirectory = binPath,
                
                FileName = "cmd.exe",
                Arguments = "/C /K mysqldump -u " + userName + " -p" + password + " " + dbName + " > \"" + exportedFile + "\""
                //Arguments = "mysqldump"
            };

            process.StartInfo = startInfo;
            process.Start();
          
            process.WaitForExit();
            //Debug.Print(process.ExitCode + "");
            return process.ExitCode == 1 ? 0 : process.ExitCode;
        }
    }
}
