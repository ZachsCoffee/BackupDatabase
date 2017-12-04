using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBaseBackup.Class
{
    class LogFile
    {
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

        public void sendMail(String body,String to)
        {
            var fromAddress = new MailAddress("databasebackupmail@gmail.com", "Database Backup");
            var toAddress = new MailAddress(to, "To Name");
            const string fromPassword = "temp9409";
            const string subject = "Log File";

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


        public void UpdateLogFile(String Id,  String type, DateTime time, String desc)
        {
            ObjStr.WriteLines(Id);
            ObjStr.WriteLines(type);
            ObjStr.WriteLines(time);
            ObjStr.WriteLines(desc);
        }

        public void updateMail(String email)
        {
            /*
            String body = "";
            if (string.Equals(type, "error") && errorLogs)
            {
                body += Id + " " + type + " " + time + " " + desc;
                //sendMail(body, email);
            }
            */
        }

        public void updateGridView(DataGridView d)
        {
            ArrayList list = ObjStr.ReadLines();
            String Id="";
            String type = "";
            String time = "";
            String desc = "";
            var i = 0;
            foreach(var item in list)
            {
                if (i == 0) Id = item.ToString();
                else if (i == 1) type = item.ToString();
                else if (i == 2) time = item.ToString();
                else if (i == 3)
                {
                    desc = item.ToString();
                    d.Rows.Add(Id, type, time, desc);//Fill datagridview with the current log row  
                    i = 0;
                    continue;
                }
                i++;
            }
        }

        public ArrayList returnSpecificLogs(String typeOfLogs)
        {
            ArrayList allErrorLogs = new ArrayList(); 
            String Id = "";
            String type = "";
            String time = "";
            String desc = "";
            ArrayList list = ObjStr.ReadLines();
            var i = 0;
            foreach (var item in list)
            {
                if (i == 0) Id = item.ToString();
                else if (i == 1) type = item.ToString();
                else if (i == 2) time = item.ToString();
                else if (i == 3)
                {
                    desc = item.ToString();
                    if (type == typeOfLogs)
                    {
                        allErrorLogs.Add(Id);
                        allErrorLogs.Add(type);
                        allErrorLogs.Add(time);
                        allErrorLogs.Add(desc);
                    }
                    i = 0;
                    continue;
                }
                i++; 
            }
            return allErrorLogs;
        }
    }
}
