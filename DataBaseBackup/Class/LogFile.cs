using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBaseBackup.Class
{
    [Serializable]
    public class LogFile
    {
        private string startupPath;
        public string LogVariablePath { get; set; }
        [NonSerialized]
        private ObjectStream ObjStr;
        [NonSerialized]
        private VariableStorage logVariables;
        [NonSerialized]
        private int Id = 0;

        public LogFile()
        {
            Directory.CreateDirectory(@".\LogFiles\");
            this.startupPath = System.IO.Path.GetFullPath(@".\LogFiles\test1.txt");
            this.LogVariablePath = Path.GetFullPath(@".\LogFiles\logV");

            logVariables = new VariableStorage(LogVariablePath);
            ObjStr = new ObjectStream(startupPath);

            object id = logVariables.GetVariable("Id");
            if (id != null)
            {
                Id = Int32.Parse(id.ToString());
            }
            
        }

        public LogFile(string startupPath, string logVariablesPath)
        {
            Directory.CreateDirectory(@".\LogFiles\");
            this.startupPath = startupPath;
            this.LogVariablePath = logVariablesPath;

            logVariables = new VariableStorage(logVariablesPath);
            ObjStr = new ObjectStream(startupPath);
            object id = logVariables.GetVariable("Id");
            if (id != null)
            {
                Id = Int32.Parse(id.ToString());
            }
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
            const string subject = "DB Logs";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            {
                MailMessage message = new MailMessage();
                message.To.Add(to);
                message.From = fromAddress;
                message.Subject = subject;
                message.Body = body;
                smtp.SendAsync(message, "Test");

            }
        }

        public string StartupPath
        {
            get
            {
                return startupPath;
            }
            set
            {
                startupPath = value;
            }
        }

        public void UpdateLogFile(String Id,  String type, DateTime time, String desc)
        {
            ObjStr.WriteLines(Id);
            ObjStr.WriteLines(type);
            ObjStr.WriteLines(time);
            ObjStr.WriteLines(desc);
        }

        public void updateMail(String email,String errorLogs,String successLogs,String infoLogs)
        {
            string body = "The following logs have been created to your database backup:\n";
            if (errorLogs == "true")
            {
                ArrayList allErrorLogs = returnSpecificLogs("error");
                var i = 0;
                body += "Error Logs:\n";
                foreach (var item in allErrorLogs)
                {
                    body += item+" ";
                    if (i == 3)
                    {
                        body += "\n";
                        i = -1;
                    }
                    i++;
                }
            }
            if (successLogs == "true")
            {
                ArrayList allSuccessLogs = returnSpecificLogs("success");
                var i = 0;
                body += "Success Logs:\n";
                foreach (var item in allSuccessLogs)
                {
                    body += item + " ";
                    if (i == 3)
                    {
                        body += "\n";
                        i = -1;
                    }
                    i++;
                }
            }
            if (infoLogs == "true")
            {
                ArrayList allInfoLogs = returnSpecificLogs("info");
                var i = 0;
                body += "Info Logs:\n";
                foreach (var item in allInfoLogs)
                {
                    body += item + " ";
                    if (i == 3)
                    {
                        body += "\n";
                        i = -1;
                    }
                    i++;
                }
            }
            sendMail(body, email);
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
            ArrayList allLogs = new ArrayList(); 
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
                        allLogs.Add(Id);
                        allLogs.Add(type);
                        allLogs.Add(time);
                        allLogs.Add(desc);
                    }
                    i = 0;
                    continue;
                }
                i++; 
            }
            return allLogs;
        }
        
        public int getId()
        {
            object id = logVariables.GetVariable("Id");
            if (id != null)
            {
                Id = Int32.Parse(id.ToString());
                logVariables.PutVariable("Id", ++Id);
                return Id++;
            }
            return 0;
        }
    }
}
