﻿using DataBaseBackup.Class;
using DataBaseBackup.Server;
using System.Threading;
using System.Timers;
using System.ServiceProcess;
using System.Collections.Generic;
using System;
using System.IO.Compression;
using System.IO;
using System.Net;
using Renci.SshNet;
using System.Windows.Forms;

namespace DataBaseBackup
{
    
    partial class ScheduleService : ServiceBase
    {
        private ScheduleServer scheduleServer;
        private LogFile log1;
        private VariableStorage logVariables;

        public ScheduleService()
        {
            InitializeComponent(); 
        }
        
        protected override void OnStart(string[] args)
        {
            log1 = new LogFile();//Logfile initiation
            logVariables = new VariableStorage(Path.GetFullPath(@".\LogFiles\logV"));//initial variables
            scheduleServer = new ScheduleServer()
            {
                onAddSchedule = OnAddSchedule,
                onRemoveSchedule = OnRemoveSchedule,
                onSetLog = OnSetLog
            };
            //end initial variables

            new Thread(() =>
            {
                scheduleServer.StartServer();

            }).Start();
        }
        
        protected override void OnStop()
        {
            try
            {
                scheduleServer.StopServer();// stamatw ton server
                //pernw olh thn lista, gia na stamathsw kai olous tous timers
                List<Schedule> schedules = scheduleServer.ScheduleList;
                foreach (Schedule schedule in schedules)
                {
                    if (schedule.Timer != null)
                    {
                        schedule.Timer.Close();
                    }
                    
                }
                //end stop timers
            }
            catch (Exception ex)
            {

            }
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
                    timer.Interval = (schedule.BackupDateTime - DateTime.Now).Milliseconds;//to pote apo twra 8a ginei to backup
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

                timer.Interval = (schedule.BackupDateTime - DateTime.Now).Milliseconds;//to pote apo twra 8a ginei to backup
                timer.Elapsed += Timer_Elapsed;// ti 8elw na ginei otan perasei ena interval
                timer.Start();
            }
            schedule.Timer = timer;
        }

        private void OnSetLog(LogFile logFile)
        {
            log1 = new LogFile(logFile.StartupPath, logFile.LogVariablePath);
        }

        private void OnRemoveSchedule(Schedule schedule)
        {
            schedule.Timer.Close();
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
                    if (schedule.FtpServer.getServerType().ToString().Equals("SFTP"))
                    {
                        UploadFileWithSFTP(finalFile, schedule);
                    }
                    else
                    {
                        UploadFileWithFTP(finalFile, schedule);
                    }
                    //UploadFile(finalFile, schedule);
                }
                else// NOT ok
                {
                    //TODO: na mpainei log gia oti kati phge straba sto export.
                    log1.UpdateLogFile(log1.getId().ToString(),"error",DateTime.Now,"export fail");//Id,Type,Datetime,Description
                }
            }
        }

        private void UploadFile(string filePath, Schedule schedule)
        {
            //1 connection me ton ftp
            //TODO: ama kati paei la8os prepei na mpei log, ama pane ola ok pali log

            //log otan ola ok
            //log otan kati paei lathos
        }

        private void UploadFileWithFTP(string filePath, Schedule schedule)
        {
            try
            {
                string uri = "ftp://" + schedule.FtpServer.getDomainName() + ":" + schedule.FtpServer.getPort();
                string username = schedule.FtpServer.getUsername();
                FileInfo fi = new FileInfo(filePath);
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(string.Format("{0}/{1}", uri, fi.Name)));
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(schedule.FtpServer.getUsername(), Upload.password);
                Stream FtpStream = request.GetRequestStream();
                FileStream fs = File.OpenRead(filePath);

                byte[] buffer = new byte[2048];
                double total = (double)fs.Length;
                int byteRead = 0;
                do
                {
                    byteRead = fs.Read(buffer, 0, buffer.Length);
                    FtpStream.Write(buffer, 0, byteRead);
                }
                while (byteRead != 0);
                fs.Close();
                FtpStream.Close();
                log1.UpdateLogFile(log1.getId().ToString(), "success", DateTime.Now, "upload completed");

            }
            catch (WebException e)
            {
                log1.UpdateLogFile(log1.getId().ToString(), "error", DateTime.Now, "upload failed");
            }
         

        }

        private void UploadFileWithSFTP(string filePath, Schedule schedule)
        {
            FileInfo fi = new FileInfo(filePath);
            string host = schedule.FtpServer.getDomainName();
            string port = schedule.FtpServer.getPort();
            string username = schedule.FtpServer.getUsername();
            string password = Upload.password;

            try
            {
                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    using (var client = new SftpClient(host, Convert.ToInt32(port), username, password))
                    {
                        client.Connect();
                        client.UploadFile(stream, fi.Name);
                        client.Disconnect();
                        client.Dispose();
                        log1.UpdateLogFile(log1.getId().ToString(), "success", DateTime.Now, "upload completed");
                    }

                }

            }
            catch (Exception e)
            {
                string logType = "error";
                string desc = "upload failed";
                log1.UpdateLogFile(log1.getId().ToString(), logType, DateTime.Now, desc);
                if (logVariables.GetVariable("errorLogs").ToString() == "true")
                {
                    string body = "The following log have been created to your database backup:\n";
                    body += "Id= " + log1.getId().ToString() + " type= "+ logType+" at " + DateTime.Now + " with the following reason : "+ desc;
                    log1.sendMail(body, logVariables.GetVariable("email").ToString());
                }

            }


        }


        private string CompressFile(string filePath)
        {
            string zipFilePath = "";

            using (Ionic.Zip.ZipFile zip = new Ionic.Zip.ZipFile())
            {
                FileInfo fi = new FileInfo(filePath);
                zip.AddFile(filePath);
                zip.CompressionLevel = Ionic.Zlib.CompressionLevel.Default; //epilogi tou compression
                zipFilePath = fi.Name + ".zip";
                zip.Save(zipFilePath);
            }
            File.Delete(zipFilePath);
            return zipFilePath;

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
