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
        public DateTime StartBackupTimestamp { get; set; }// pote 8a einai h prwth fora pou 8a kanei backup
        public DateTime RepeatEvery { get; set; }// ka8e poso interval 8a kanei backup
        public string MySqlBinFolderPath { get; set; }

        private string
            dbName,// ama exei dialeksei full auto backup, tote auto den prepei na einai null
            dbFilePath = null;// ama exei dialeksei manual backup tote auto den prepei na einai null
            
        private bool 
            backupNow = true,// ama exei dialeksei na kanei backup twra / alliws exei dialeksei meta
            backupOnce = true,// ama exei dialeksei na kanei backup mia fora/ alliws einai me interval
            fullAutomatic = true,// ama exei dialeksei full auto / alliws einai manual
            withCompress = true;// ama exei dialeksei me compress / alliws einai xwris compress

        private Ftp ftpServer;// krata tis plhrofories gia ton ftp server.

        //END NEW FIELDS

        public Schedule(DateTime timestamp, string dbName, Ftp ftpServer)
        {
            this.dbName = dbName ?? throw new ArgumentNullException(nameof(dbName));
            this.ftpServer = ftpServer ?? throw new ArgumentNullException(nameof(ftpServer));
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
                if (fullAutomatic)
                {
                    withCompress = value;
                }
                else
                {
                    throw new Exception("In order to set, with compress, you must first choose full automatic.");
                }
            }
        }

        public Timer Timer
        {
            get { return timer; }
            set { timer = value; }
        }
    }
}
