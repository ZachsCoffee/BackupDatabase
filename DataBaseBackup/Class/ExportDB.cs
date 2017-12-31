using System.IO;
using System;
namespace DataBaseBackup.Class
{
    public class ExportDB
    {
        public static string exportPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Exports\");

        public static int Export(string binPath, string userName, string password, string dbName)
        {
            Directory.CreateDirectory(exportPath);
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo
            {
                WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                FileName = "cmd.exe",
                Arguments = "/C mysqldump -u " + userName + " -p " + password + " " + dbName + " > " + exportPath + dbName + DateTime.Now.ToString(" dd-MM-yy H-mm-ss FFFFF")+".txt"
            };
            process.StartInfo = startInfo;
            process.Start();

            return process.ExitCode;
        }
    }
}
