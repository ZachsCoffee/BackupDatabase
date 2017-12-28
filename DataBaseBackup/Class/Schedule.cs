using System;
using System.Timers;

namespace DataBaseBackup.Class
{
    [Serializable]
    public class Schedule
    {

        public int ID { get; set; }

        [NonSerialized]
        private Timer timer;//me ton timer 8a ginete to schedule.

        //START NEW FIELDS
        private DateTime timeStamp, //pote 8a einai h prwth fora pou 8a kanei backup
            repeatEvery;//ka8e poso interval 8a kanei backup
        private string dbName, //onoma bashs
            ftpServerDomainName;//domain name to FTP server pou dialekse na kanei to backup, H ip tou server
        private bool repeat = false;//ama exei dialeksei na uparxei interval sto backup
        //END NEW FIELDS

        public Schedule(DateTime timestamp, string dbName, string ftpServerDomainName)
        {
            //timer = new Timer(11000);
            this.timeStamp = timestamp;
            this.dbName = dbName ?? throw new ArgumentNullException(nameof(dbName));
            this.ftpServerDomainName = ftpServerDomainName ?? throw new ArgumentNullException(nameof(ftpServerDomainName));
        }

        public override string ToString()
        {
            return "Timestamp: "+timeStamp.ToShortTimeString()+"\t"+timeStamp.ToShortDateString()+"\t" +
                "Database name: "+dbName+"\tServer name: "+ftpServerDomainName;
        }

        public void Repeat(DateTime repeatEvery)
        {
            if (repeatEvery == null)
            {
                throw new ArgumentNullException(nameof(repeatEvery));
            }

            this.repeatEvery = repeatEvery;
        }

        public bool IsRepeat
        {
            get
            {
                return repeat;
            }
        }
    }
}
