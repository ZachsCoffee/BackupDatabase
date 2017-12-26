using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseBackup.Class
{
    public class Schedule
    {
        private DateTime timeStamp, repeatEvery;
        private string dbName, ftpServerDomainName;
        private bool repeat = false;

        public Schedule(DateTime timestamp, string dbName, string ftpServerDomainName)
        {
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
