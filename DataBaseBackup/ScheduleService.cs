using DataBaseBackup.Class;
using DataBaseBackup.Server;
using System.Threading;
using System.Timers;

using System.ServiceProcess;
using System.Collections.Generic;
using System;

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
            
            if (schedule.BackupNow)// 8elei na ginei backup twra
            {
                new Thread(() =>
                {
                    Backup(schedule);
                }).Start();

                if (!schedule.BackupOnce)// kai einai repeat
                {
                    timer.Interval = (schedule.RepeatEvery - DateTime.Now).Milliseconds;//to pote apo twra 8a ginei to backup
                    timer.Elapsed += Timer_Elapsed;// ti 8elw na ginei otan perasei ena interval
                    timer.Start();
                }
            }
            else// einai backup meta
            {
                if (schedule.BackupOnce)
                {
                    timer.AutoReset = false;
                }

                timer.Interval = (schedule.RepeatEvery - DateTime.Now).Milliseconds;//to pote apo twra 8a ginei to backup
                timer.Elapsed += Timer_Elapsed;// ti 8elw na ginei otan perasei ena interval
                timer.Start();
            }
        }

        private void OnRemoveSchedule(Schedule schedule)
        {

        }

        private void Backup(Schedule schedule)
        {
            //edw 8a ginete to backup

            if (schedule.FullAutomatic)//einai full automatic
            {
                //gia na ektelestei h entolh sto shell, h entolh gia na kanei export thn DB
                int exitCode = ExportDB.Export(
                    schedule.MySqlBinFolderPath, 
                    schedule.FtpServer.getUsername(), 
                    schedule.FtpServer.Password, 
                    schedule.DBName,
                    out string exportedFilePath
                );

                if (exitCode == 0)//ok
                {
                    string finalFile;// einai to teliko arxeio, gia na ginei upload ston ftp

                    if (schedule.WithCompress)// tote prepei na ginei compress prin anebei ston ftp
                    {
                        //1. ginete to compress
                        finalFile = CompressFile(exportedFilePath);
                    }
                    else
                    {
                        finalFile = exportedFilePath;
                    }

                    UploadFile(finalFile, schedule);
                }
                else// NOT ok
                {
                    //TODO: na mpainei log gia oti kati phge straba sto export.
                }
            }
        }

        private void UploadFile(string filePath, Schedule schedule)
        {
            //1 connection me ton ftp
            //TODO: ama kati paei la8os prepei na mpei log, ama pane ola ok pali log
        }

        private string CompressFile(string filePath)
        {
            //TODO: compress to filePath
            return "";//prepei na kanei return to path tou COMPRESSED arxeiou
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            System.Timers.Timer timer = sender as System.Timers.Timer;
            if (timer.Enabled)//gia asfaleia blepw ama einai enable o timer
            {
                if (!timer.AutoReset)// ama den einai autoreset, tote einai mono gia mia fora, ara ton stamatw
                {
                    timer.Close();// apodesmeuw tous porous tou timer
                }

                //na ginei to backup
            }
        }
    }
}
