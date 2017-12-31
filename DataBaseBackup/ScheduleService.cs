using DataBaseBackup.Class;
using DataBaseBackup.Server;
using System.Threading;
using System.Timers;

using System.ServiceProcess;
using System.Collections.Generic;

namespace DataBaseBackup
{
    partial class ScheduleService : ServiceBase
    {
        private ScheduleServer scheduleServer;

        public ScheduleService()
        {
            InitializeComponent();

            //initial variables
            scheduleServer = new ScheduleServer()
            {
                onAddSchedule = OnAddSchedule,
                onRemoveSchedule = OnRemoveSchedule
            };
            //end initial variables
        }

        protected override void OnStart(string[] args)
        {
            new Thread(() =>
            {
                scheduleServer.StartServer();

            }).Start();
        }

        protected override void OnStop()
        {
            scheduleServer.StopServer();// stamatw ton server

            //pernw olh thn lista, gia na stamathsw kai olous tous timers
            List<Schedule> schedules = scheduleServer.ScheduleList;
            foreach (Schedule schedule in schedules)
            {
                schedule.Timer.Close();
            }
            //end stop timers
        }

        private void OnAddSchedule(Schedule schedule)
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Elapsed += Timer_Elapsed;// ti 8elw na ginei otan perasei ena interval

        }

        private void OnRemoveSchedule(Schedule schedule)
        {

        }

        private void Backup(Schedule schedule)
        {
            //edw 8a ginete to backup

            if (schedule.FullAutomatic)//einai full automatic
            {
                //gia na ektelestei entolh sto shell, h entolh gia na kanei export thn DB
                
            }
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if ((sender as System.Timers.Timer).Enabled)//gia asfaleia blepw ama einai enable o timer
            {

            }
        }
    }
}
