using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using DataBaseBackup.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseBackup.Server
{
    public class ScheduleServer
    {
        public enum MessageCode
        {
            GetInfo, Delete, Add
        }

        public const int DEFAULT_PORT = 51100;
        private bool isStoped = true;
        private List<Schedule> schedules;
        private TcpListener server;

        public ScheduleServer()
        {
            schedules = new List<Schedule>();
        }

        /// <summary>
        /// Starts a new server. Can't run a new server, if you didn't first close the old one.
        /// </summary>
        public void StartServer()
        {
            if (!isStoped)
            {
                throw new Exception("You can't run again a new server, if you didn't first close the old one.");
            }

            try
            {
                server = new TcpListener(IPAddress.Loopback, DEFAULT_PORT);
                server.Start();
                isStoped = false;

                while (!isStoped)
                {
                    TcpClient income = server.AcceptTcpClient();
                    NetworkStream stream = income.GetStream();

                    switch (stream.ReadByte())
                    {
                        case (int)MessageCode.GetInfo:
                            SendInfo(stream);
                            break;
                        case (int)MessageCode.Add:
                            AddSchedule(stream);
                            break;
                        case (int)MessageCode.Delete:
                            RemoveSchedule(stream);
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                isStoped = true;
            }
            
        }

        /// <summary>
        /// Stops the server, release the sockets.
        /// </summary>
        public void StopServer()
        {
            isStoped = true;
            server.Stop();
        }

        private void AddSchedule(NetworkStream stream)
        {
            Schedule schedule = (Schedule) new BinaryFormatter().Deserialize(stream);//diabazw apo to stream to object
            schedule.ID = schedules.Count;//bazw to ID tou
            schedules.Add(schedule);//to bazw sthn list
        }

        private void RemoveSchedule(NetworkStream stream)
        {
            byte[] buffer = new byte[4];
            int ok = stream.Read(buffer, 0, 4);//diabazw to id 

            if (ok != -1)
            {
                int id = BitConverter.ToInt32(buffer, 0);
                for (int i=0; i<schedules.Count; i++)
                {
                    if (schedules[i].ID == id)
                    {
                        schedules.RemoveAt(i);
                        break;
                    }
                }
            }
        }

        private void SendInfo(NetworkStream stream)
        {
            new BinaryFormatter().Serialize(stream, schedules);
        }

        public bool IsStoped
        {
            get { return isStoped; }
        }

        /// <summary>
        /// Gets the list with schedules.
        /// </summary>
        public List<Schedule> ScheduleList
        {
            get
            {
                return schedules;
            }
        }
    }
}
