using System;
using System.Timers;

namespace DataBaseBackup.Class
{
    [Serializable]
    public class Schedule
    {

        public int ID { get; set; }

        [NonSerialized]
        private Timer timer;// me ton timer 8a ginete to schedule.

        //START NEW FIELDS
        public DateTime RepeatEvery { get; set; }// ka8e poso interval 8a kanei backup
        public string MySqlBinFolderPath { get; set; }

        private string
            dbName,// ama exei dialeksei full auto backup, tote auto den prepei na einai null
            dbFilePath = null;// ama exei dialeksei manual backup tote auto den prepei na einai null

        public bool 
            BackupNow = true, // ama exei dialeksei na kanei backup twra / alliws exei dialeksei meta
            BackupOnce = true;// ama exei dialeksei na kanei backup mia fora/ alliws einai me interval

        private bool 
            fullAutomatic = true,// ama exei dialeksei full auto / alliws einai manual
            withCompress = true;// ama exei dialeksei me compress / alliws einai xwris compress

        public Ftp FtpServer { get; set; }// krata tis plhrofories gia ton ftp server.

        //END NEW FIELDS

        public Schedule(DateTime timestamp, string dbName, Ftp ftpServer)
        {
            this.dbName = dbName ?? throw new ArgumentNullException(nameof(dbName));
            FtpServer = ftpServer ?? throw new ArgumentNullException(nameof(ftpServer));
        }

        public bool FullAutomatic
        {
            get { return fullAutomatic; }

            set
            {
                withCompress = fullAutomatic = value;
            }
        }

        public bool WithCompress
        {
            get { return withCompress; }

            set
            {
                withCompress = value;
            }
        }

        public string DBName
        {
            get { return dbName; }
            set { dbName = value; }
        }

        public Timer Timer
        {
            get { return timer; }
            set { timer = value; }
        }
    }
}
