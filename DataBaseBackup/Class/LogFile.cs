using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;

namespace DataBaseBackup.Class
{
    class LogFile
    {
        private DateTime time;
        private String type="";
        private String desc="";

        private string startupPath = System.IO.Path.GetFullPath(@"..\..\LogFiles\test1.txt");
        private ObjectStream ObjStr;

        public LogFile()
        {
            ObjStr = new ObjectStream(startupPath);         
        }


        public ArrayList print()
        {
            ArrayList list = ObjStr.ReadLines();
            return list;
        }

        public void sendMail()
        {
            var fromAddress = new MailAddress("databasebackupmail@gmail.com", "Database Backup");
            var toAddress = new MailAddress("panos9409@gmail.com", "To Name");
            const string fromPassword = "temp9409";
            const string subject = "Log File";
            const string body = "Body";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body
            })
            {
                smtp.Send(message);
            }
        }

    }




}
