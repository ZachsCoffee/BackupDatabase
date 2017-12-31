﻿using DataBaseBackup.Server;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Renci.SshNet;
using System.IO;
using DataBaseBackup.Class;
using System.Collections;
using System.Text.RegularExpressions;
using System.Net;
using System.Threading;

namespace DataBaseBackup
{
    public partial class Form1 : Form
    {
        enum ConnectionStatus//einai 3 katastaseis gia to label connectionStatusLabel, (method SetConnectionStatus)
        {
            NotTested, OK, Failed, Testing
        }

        private Panel[] panels;
        private Panel currentPanel;
        SftpClient sftpclient;
        Sftp sftp;
        Ftp ftp;
        ObjectStream stream = new ObjectStream("saveServer");
        public string fileName; //onoma arxeiou
        public string fullFileName; // fullpath name

        public Form1()
        {
            InitializeComponent();
            stream.ClearFile();
            
            //ola ta nea panels prepei na mpoun se auton ton pinaka, kai meta sthn switch (method MenuClick)
            panels = new Panel[] {databasePanel, serversPanel, backupPanel, logPanel} ;//krata ola ta panel gia na mporeis na ta allazeis 
            currentPanel = databasePanel;//einai to arxiko

            //declare gia ta panels
            for (int i=1; i<panels.Length; i++)
            {
                panels[i].Visible = false;
            }
            currentPanel.Visible = true;
            //end panels

            dateTimeWhen.CustomFormat = "dd/MM/yyyy hh:mm:ss";
            dateTimeWhen.MinDate = DateTime.Now;
            serverType.SelectedIndex = 0;

            /*new Thread(() =>
            {
                //server
                ScheduleServer server = new ScheduleServer();
                server.StartServer();

            }).Start();

            new Thread(() =>
            {
                //Thread.Sleep(1000);
                ScheduleClient.AddSchedule(new Schedule(new DateTime(), "a", "b"));
                ScheduleClient.AddSchedule(new Schedule(new DateTime(), "a1", "b1"));
                ScheduleClient.AddSchedule(new Schedule(new DateTime(), "a2", "b2"));

                List<Schedule> list = ScheduleClient.GetInfo();
                Debug.Print(list[0].ToString());

                ScheduleClient.DeleteSchedule(0);
                ScheduleClient.DeleteSchedule(2);
                list = ScheduleClient.GetInfo();
                Debug.Print(list.Count+"");
                Debug.Print(list[0].ToString());
            }).Start();*/
        }

        //CUSTOM METHODS
        private void MenuClick(object sender, EventArgs e)
        {
            switch ((sender as Button).Name)
            {
                case "databaseButton":
                    SwitchPanels(0, "Database");
                    break;
                case "serversButton":
                    SwitchPanels(1, "Servers");
                    break;
                case "backupButton":
                    SwitchPanels(2, "Backup");
                    break;
                case "logButton":
                    SwitchPanels(3, "Log");
                    break;
            }
        }

        private void SwitchPanels(int index, string title)
        {
            currentPanel.Visible = false;
            panels[index].Visible = true;

            currentPanel = panels[index];

            pageTitle.Text = title;
        }

        private void ResetServerActionValues()
        {
            serverType.SelectedIndex = 0;
            domainName.Clear();
            port.ResetText();
            username.Clear();
            SetConnectionStatus(ConnectionStatus.NotTested);
        }

        private void SetConnectionStatus(ConnectionStatus status)
        {
            switch (status)
            {
                case ConnectionStatus.NotTested:
                    connectionStatusLabel.ForeColor = System.Drawing.Color.Black;
                    connectionStatusLabel.Text = "Not tested";
                    break;
                case ConnectionStatus.OK:
                    connectionStatusLabel.ForeColor = System.Drawing.Color.Green;
                    connectionStatusLabel.Text = "OK";
                    break;
                case ConnectionStatus.Failed:
                    connectionStatusLabel.ForeColor = System.Drawing.Color.Red;
                    connectionStatusLabel.Text = "FAILED";
                    break;
                case ConnectionStatus.Testing:
                    connectionStatusLabel.ForeColor = System.Drawing.Color.Black;
                    connectionStatusLabel.Text = "Testing...";
                    break;
            }
        }


        /*
        private void downloadWithSFTP()
        {
       
            using (var sftp = new SftpClient(host, port, username, pass))
            {
                sftp.Connect();
                var files = sftp.ListDirectory(remoteDirectory);

                foreach (var file in files)
                {
                    comboBox2.Items.Add(file);
                    if (file.Name.Equals(textBox1.Text))
                    {
                        using (Stream file1 = File.OpenWrite(path))
                        {
                            sftp.DownloadFile(remoteDirectory + textBox1.Text, file1);
                        }
                    }

                }
                
            }
        }
        */

        private void downloadWithFTP()
        {
            string host = "ftp://127.0.0.1:21";
            
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(string.Format("{0}/{1}", host, "test.txt")));
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential("Test", "12345");
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                using (var file = File.Create("./" + "test"))
                {
                    responseStream.CopyTo(file);
                }
            }
            catch (WebException e)
            {
                String status = ((FtpWebResponse)e.Response).StatusDescription;
                MessageBox.Show(status.ToString());
            }

        }




        private void uploadWithSFTP()
        {
            string line =ftpServers.SelectedItem.ToString();
            string[] parts = line.Split(',');

            try
            {
                string host = parts[1];
                string username = parts[3];
                string pass = password.Text;
                int port =Int32.Parse(parts[2]);

                using (var stream = new FileStream(fullFileName, FileMode.Open))
                {

                    using (var client = new SftpClient(host, port, username, pass))
                    {
                        client.Connect();
                        //client.UploadFile(stream, fname + ".zip");
                        client.UploadFile(stream, fileName);
                        client.Disconnect();
                        client.Dispose();
                    }
                }

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            
            


        }

        private void uploadWithFTP()
        {
            string line = ftpServers.SelectedItem.ToString();
            string[] parts = line.Split(',');
            string host = "ftp://"+parts[1]+":"+parts[2];
            string username = parts[3];
            string pass = password.Text;
            

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(string.Format("{0}/{1}", host, fileName)));
            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(username, pass);
            Stream FtpStream = request.GetRequestStream();
            FileStream fs = File.OpenRead(fullFileName);

            byte[] buffer = new byte[1024];
            double total = (double)fs.Length;
            int byteRead = 0;
            double read = 0;
           
            do
            {
                byteRead = fs.Read(buffer, 0, 2048);
                FtpStream.Write(buffer, 0, byteRead);
                read += (double)byteRead;
            }
            while (byteRead != 0);
            fs.Close();
            FtpStream.Close();
            
        }

     



        private void testConnectionSFTP()
        {
            using (sftpclient = new SftpClient(domainName.Text, (int)port.Value, username.Text, password.Text)) // dimiourgia antikeimenou gia sindeso sftp
            {

                try
                {
                    sftpclient.Connect();
                }
                catch (Renci.SshNet.Common.SshAuthenticationException AuthEx)
                {
                    MessageBox.Show(AuthEx.Message, "Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (System.Net.Sockets.SocketException sochEx)
                {
                    MessageBox.Show(sochEx.Message, "Port Number Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


                if (sftpclient.IsConnected) //elenxos gia to connection
                {
                    SetConnectionStatus(ConnectionStatus.OK);
                    saveServer.Enabled = true;
                }
                else
                {
                    SetConnectionStatus(ConnectionStatus.Failed);
                }
                sftpclient.Disconnect();
                sftpclient.Dispose();


            }
        }

        private void testConnectionFTP()
        {
            //an mporw na kanw listing tote exw sinthethi kanonika :P
            string uri = "ftp://" + domainName.Text + ":" + port.Value.ToString();
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(uri));
            request.UsePassive = false;
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            request.Credentials = new NetworkCredential(username.Text,password.Text);
            request.KeepAlive = true;

            try
            {
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                SetConnectionStatus(ConnectionStatus.OK);
                saveServer.Enabled = true;
                

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SetConnectionStatus(ConnectionStatus.Failed);
            }
           


        }
        //END CUSTOM METHODS

        //EVENT METHODS
        private void newFtpServer_Click(object sender, EventArgs e)
        {
            actionTitle.Text = "New server";

            configServersPanel.Visible = true;
        }

        private void editFtpServer_Click(object sender, EventArgs e)
        {
            actionTitle.Text = "Edit server";
            configServersPanel.Visible = true;

        }

        private void makeAction_Click(object sender, EventArgs e)
        {
            //ResetServerActionValues();
            if (serverType.Text.Equals("SFTP"))
            {
                testConnectionSFTP();
            }
            else
            {
                testConnectionFTP();
            }
            
        }

        private void cancelAction_Click(object sender, EventArgs e)
        {
            configServersPanel.Visible = false;
            ResetServerActionValues();
        }

        
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.InitialDirectory = "C:\\";
            openFileDialog1.Filter = "Text|*.txt|All|*.*"; ;
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (DialogResult.OK == openFileDialog1.ShowDialog())
            {
            

            }
        }

        private void serverType_SelectedIndexChanged(object sender, EventArgs e)//me to pou dialegei protocolo sftp i ftp ginetai automata allagei port
        {
            if (serverType.SelectedItem.ToString().Equals("SFTP"))
            {
                port.Value = 22;
            }
            else
            {
                port.Value = 21;
            }
        }

        private void saveServer_Click(object sender, EventArgs e)
        {
            if (actionTitle.Text.Equals("Edit server"))
            {
                int index = serversListBox.SelectedIndex;
                if (index < 0)
                {
                    MessageBox.Show("The Listbox is empty", "!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (serverType.Text.Equals("SFTP"))
                {
                    sftp = new Sftp("", "", "", "");
                    sftp.setServerType(serverType.Text);
                    sftp.setDomainName(domainName.Text);
                    sftp.setPort(port.Value.ToString());
                    sftp.setUsername(username.Text);
                    stream.DeleteLines(index);
                    serversListBox.Items.RemoveAt(index);
                    stream.WriteLines(sftp.ToString());
                }
                else
                {
                    ftp = new Ftp("", "", "", "");
                    ftp.setServerType(serverType.Text);
                    ftp.setDomainName(domainName.Text);
                    ftp.setPort(port.Value.ToString());   
                    ftp.setUsername(username.Text);
                    stream.DeleteLines(index);
                    serversListBox.Items.RemoveAt(index);
                    stream.WriteLines(ftp.ToString());

                }
                
                serversListBox.Items.Clear();
                
                ArrayList list = stream.ReadLines();
                foreach (Object obj in list)
                    serversListBox.Items.Add(obj);

            }
            else
            {
                if (serverType.Text.Equals("SFTP"))
                {
                    sftp = new Sftp(serverType.Text, domainName.Text, port.Value.ToString(), username.Text);
                    //MessageBox.Show(sftp.ToString(), "Test", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    stream.WriteLines(sftp.ToString());
                    serversListBox.Items.Add(sftp.ToString());
                }
                else
                {
                    ftp = new Ftp(serverType.Text, domainName.Text, port.Value.ToString(), username.Text);
                    stream.WriteLines(ftp.ToString());
                    serversListBox.Items.Add(ftp.ToString());
                }
                
            }

            ArrayList mylist = stream.ReadLines();
            ftpServers.Items.Clear();
            foreach (Object obj in mylist)
                ftpServers.Items.Add(obj);
            

        }

        private void deleteFtpServer_Click(object sender, EventArgs e)
        {
            int index = serversListBox.SelectedIndex;
            stream.DeleteLines(index);
            serversListBox.Items.RemoveAt(index);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            
        }

        private void Full_Automatic_Click(object sender, EventArgs e)
        {

            databaseFilePath.Enabled = false;
            browseDatabase.Enabled = false;
            autoDatabaseName.Enabled = true;
            compressCheckBox.Enabled = true;

            errorProvider1.SetError(databaseFilePath, "");//na bgalw to warning apo to textbox
        }

        private void Manual_Click(object sender, EventArgs e)
        {
            databaseFilePath.Enabled = true;
            browseDatabase.Enabled = true;
            compressCheckBox.Enabled = false;
            autoDatabaseName.Enabled = false;
        }

        private void Now_Click(object sender, EventArgs e)
        {
            dateTimeWhen.Enabled = false;
            repeatPanel.Enabled = false;
        }

        private void Later_Click(object sender, EventArgs e)
        {
            dateTimeWhen.Enabled = true;
        }

        LogFile log1 = new LogFile();
        String email = "";
        bool errorLogs = true;
        bool successLogs = false;
        bool infoLogs = false;
        //Test button
        private void button2_Click_1(object sender, EventArgs e)
        {
            
            log1.UpdateLogFile("01", "error", DateTime.Now, "desc",dataGridView1, errorLogs, successLogs, infoLogs,email);

            //string startupPath = System.IO.Path.GetFullPath(@"..\..\LogFiles");
        }


        //Email Validation Method
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            Regex regEmail = new Regex(@"^(([^<>()[\]\\.,;:\s@\""]+"
                            + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@"
                            + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"
                            + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"
                            + @"[a-zA-Z]{2,}))$",
                            RegexOptions.Compiled);

            if (!regEmail.IsMatch(textBox2.Text))
            {
                errorProvider1.SetError(textBox2, "Please enter a Valid Email Address.");
            }
            else
            {
                errorProvider1.SetError(textBox2, "");
            }
        }

        
        //Apply email changes
        private void button4_Click(object sender, EventArgs e)
        {
            email = textBox2.Text;
            if (checkBox1.Checked) errorLogs = true; else errorLogs = false;
            if (checkBox3.Checked) successLogs = true; else successLogs = false;
            if (checkBox2.Checked) infoLogs = true; else infoLogs = false;
        }

        private void Show_Schedules(object sender, EventArgs e)
        {
            new BackupSchedules().Show();
        }

        private void Once_Click(object sender, EventArgs e)
        {
            repeatPanel.Enabled = false;
            if (laterRadio.Checked)
            {
                dateTimeWhen.Enabled = true;
            }
            
        }

        private void Repeat_Click(object sender, EventArgs e)
        {
            repeatPanel.Enabled = true;
            dateTimeWhen.Enabled = false;
        }

        private void ApplySchedule(object sender, EventArgs e)
        {
            //validation
            if (this.ValidateChildren())//ama petuxan ola ta validation tote kanonika ginete to schedule
            {

            }
            else//DEN prepei na ginei to shedule
            {

            }
            //end validation
        }

        private void ValidateDayPicker(object sender, CancelEventArgs e)
        {
            if (dateTimeWhen.Enabled)
            {
                if (dateTimeWhen.Value.CompareTo(DateTime.Now) < 0)//ama h wra pou ebale einai poio palia apo twra
                {
                    errorProvider1.SetError(dateTimeWhen, "The date/time can't be before the current time.");
                }
                else
                {
                    errorProvider1.SetError(dateTimeWhen, "");
                }
            }
        }

        private void ValidateDatabaseFile(object sender, CancelEventArgs e)
        {
            if (manualRadio.Checked)//ama einai sto manual
            {
                
                if (!File.Exists(databaseFilePath.Text))//ama DEN uparxei to arxeio pou exei dialeksei
                {
                    errorProvider1.SetError(databaseFilePath, "The file not exist");
                }
            }
        }

        private void ValidateFTPServer(object sender, CancelEventArgs e)
        {
            if (ftpServers.SelectedIndex < 0)
            {
                errorProvider1.SetError(ftpServers, "You must select a SFTP/FTP server.");
            }
        }

        private void browseDatabase_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Multiselect = false, ValidateNames = true, Filter = "All files|*.*" })
            {

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    FileInfo fi = new FileInfo(ofd.FileName);
                  
                    fileName = fi.Name;
                    fullFileName = fi.FullName;
                }
                databaseFilePath.Text = fullFileName;
            }

        }




        //END EVENT METHODS
    }      
}
