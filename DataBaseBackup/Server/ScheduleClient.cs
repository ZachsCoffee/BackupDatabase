using System.Net;
using System.Net.Sockets;
using DataBaseBackup.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;

namespace DataBaseBackup.Server
{
    public class ScheduleClient
    {
        public enum MessageCode
        {
            GetInfo, Delete, Add
        }

        /// <summary>
        /// Adds a schedule to the server.
        /// </summary>
        /// <param name="schedule">The new schedule to be add.</param>
        public static void AddSchedule(Schedule schedule)
        {
            NetworkStream stream = Connect(out TcpClient client);//kanw sundesh

            stream.WriteByte((byte)MessageCode.Add);//stelnw to code
            new BinaryFormatter().Serialize(stream, schedule);//stelnw to schedule

            //stream.Close();
            //client.Close();
        }

        /// <summary>
        /// Gets from the server all the information, for the stored schedules.
        /// </summary>
        /// <returns>A List, filled with the schedules.</returns>
        public static List<Schedule> GetInfo()
        {
            NetworkStream stream = Connect(out TcpClient client);

            stream.WriteByte((byte)MessageCode.GetInfo);

            List<Schedule> list = (List<Schedule>)new BinaryFormatter().Deserialize(stream);
            //stream.Close();
            //client.Close();

            return list;
        }

        /// <summary>
        /// Deletes a schedule from the server.
        /// </summary>
        /// <param name="scheduleID">The ID of the schedule to delete.</param>
        public static void DeleteSchedule(int scheduleID)
        {
            if (scheduleID < 0)
            {
                throw new ArgumentException("Schedule ID can't be < 0");
            }

            NetworkStream stream = Connect(out TcpClient client);

            stream.WriteByte((byte)MessageCode.Delete);

            byte[] buffer = BitConverter.GetBytes(scheduleID);
            stream.Write(buffer, 0, buffer.Length);

            //stream.Close();
            //client.Close();
        }

        private static NetworkStream Connect(out TcpClient tcpClient)
        {
            TcpClient client = new TcpClient();
            tcpClient = client;
            client.Connect(new IPEndPoint(IPAddress.Loopback, ScheduleServer.DEFAULT_PORT));

            return client.GetStream();
        }
    }
}
