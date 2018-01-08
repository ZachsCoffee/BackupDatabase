using System;
using System.ComponentModel;
using System.Windows.Forms;
using Renci.SshNet;
using System.IO;
using DataBaseBackup.Class;
using System.Collections;
using System.Text.RegularExpressions;
using System.Net;
using DataBaseBackup.Server;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Renci.SshNet.Sftp;
using System.Threading.Tasks;
using System.ServiceProcess;
using System.Diagnostics;

namespace DataBaseBackup
{
    public partial class Form1 : Form
    {
        public enum ConnectionStatus//einai 3 katastaseis gia to label connectionStatusLabel, (method SetConnectionStatus)
        {
            NotTested, OK, Failed, Testing
        }

        private Panel[] panels;
        private Panel currentPanel;
        static SftpClient sftpclient;
        Sftp sftp=null;
        ObjectStream stream;
        List<string> Allnames = new List<string>(); //lista gia na krataw ta arxeio pou thelei na katevasei
        string[] parts = new string[4];


        //Initiation of logFile variables

        private LogFile log1 = new LogFile();
        private VariableStorage logVariables;
        private VariableStorage generalVariables;

        public static string serverSettings; //mia static gia na krataw to server pou epelexse o xristis.
      

        public Form1()
        {
            InitializeComponent();

            //gia to logfiles folder
            Directory.CreateDirectory(Path.GetFullPath(@".\LogFiles"));
            Directory.CreateDirectory(Path.GetFullPath(@".\var"));
            Directory.CreateDirectory(@".\servers");
            logVariables = new VariableStorage(Path.GetFullPath(@".\LogFiles\logV"));
            generalVariables = new VariableStorage(Path.GetFullPath(@".\var\genV"));
            stream = new ObjectStream(@".\servers\saveServer");
            Fillinit();
            try
            {
                //gia to service
                ServiceController serviceController = new ServiceController("ScheduleService");
                switch (serviceController.Status)
                {
                    case ServiceControllerStatus.Paused:
                        serviceController.Start();
                        break;
                    case ServiceControllerStatus.Stopped:
                        serviceController.Start();
                        break;
                }
                serviceController.Refresh();
                //end service
            }
            catch (Exception ex)
            {

            }
            
            //stream.ClearFile();
            SetDownloadPanelNotVisble(); //kanw not visible ta download panel

            //ola ta nea panels prepei na mpoun se auton ton pinaka, kai meta sthn switch (method MenuClick)
            panels = new Panel[] {databasePanel, serversPanel, backupPanel, logPanel,downloadPanel} ;//krata ola ta panel gia na mporeis na ta allazeis 
            currentPanel = databasePanel;//einai to arxiko

            //declare gia ta panels
            for (int i=1; i<panels.Length; i++)
            {
                panels[i].Visible = false;
            }
            currentPanel.Visible = true;
            //end panels
            serverType.SelectedIndex = 0;

            //VariableStorage logVariables = new VariableStorage(System.IO.Path.GetFullPath(@"..\..\LogFiles\logV"));
            try
            {
                ScheduleClient.SetLogFile(new LogFile());//stelnw to log file sto service
            }
            catch (Exception ex)
            {

            }

            //giati ksekina me to database panel gemizw to textbox
            object dbBinFolder = generalVariables.GetVariable("dbBinFolder");
            if (dbBinFolder != null)
            {
                binFolderPath.TextBox.Text = dbBinFolder.ToString();
            }

            //TestService test = new TestService();
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
                    log1.updateGridView(dataGridView1);
                    textBox2.Text = logVariables.GetVariable("email").ToString();
                    if (logVariables.GetVariable("errorLogs").ToString() == "true") checkBox1.Checked = true; else checkBox1.Checked = false;
                    if (logVariables.GetVariable("successLogs").ToString() == "true") checkBox3.Checked = true; else checkBox3.Checked = false;
                    if (logVariables.GetVariable("infoLogs").ToString() == "true") checkBox2.Checked = true; else checkBox2.Checked = false;
                    break;
                case "downloadDatabase":
                    SwitchPanels(4, "Download DB");
                    break;
            }
        }


        public void Fillinit()
        {
            ArrayList list = stream.ReadLines();
            foreach (Object obj in list) {
                serversListBox.Items.Add(obj);
                ftpServers.Items.Add(obj);
                FtpDownload.Items.Add(obj);
            }
                
            
        }
       


        public static ConnectionStatus testConnectionFTP(string domainName, string port, string username, string password)
        {
            //an mporw na kanw listing tote exw sinthethi kanonika :P

            string uri = "ftp://" + domainName + ":" + port;
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(uri));
            request.UsePassive = false;
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            request.Credentials = new NetworkCredential(username, password);
            request.KeepAlive = true;

            try
            {
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                return ConnectionStatus.OK;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return ConnectionStatus.Failed;
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

        public static ConnectionStatus testConnectionSFTP(string domainName, string port, string username, string password)
        {
            using (sftpclient = new SftpClient(domainName, Convert.ToInt32(port), username, password)) // dimiourgia antikeimenou gia sindeso sftp
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
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


                if (sftpclient.IsConnected) //elenxos gia to connection
                {
                    return ConnectionStatus.OK;
                }
                else
                {
                    return ConnectionStatus.Failed;
                }
            }
        }


        private Schedule BuildSchedule()
        {
            string[] parts = serverSettings.Split(',');
            string serverType = parts[0];
            string host = parts[1];
            string port = parts[2];
            string username = parts[3];

            Ftp ftpServer = new Ftp(serverType,host,port,username);//edw bale ta xaraktitistika tou ftp server, wste meta na ta parei to scedule, ola ta pedia
            ftpServer.Password = Upload.password;
            Schedule schedule = new Schedule()
            {
                FullAutomatic = fullAutomaticRadio.Checked,
                BackupNow = nowRadio.Checked,
                BackupOnce = onceRadio.Checked,
                WithCompress = compressCheckBox.Checked,
                FtpServer = ftpServer,
                DBName = backupDatabaseName.Text,
                DBFilePath = databaseFilePath.Text,
                DBpassword = Upload.dbPassword,
                DBusername = Upload.dbUsername,
                MySqlBinFolderPath = generalVariables.GetVariable("dbBinFolder").ToString()
            };

            if (schedule.BackupOnce && !schedule.BackupNow)
            {
                schedule.BackupDateTime = onceDatetimePicker.Value;
            }
            else
            {
                DateTime tempDateTime = DateTime.Now;
                tempDateTime.AddDays(Convert.ToDouble(daysNumber.Value));
                tempDateTime.AddHours(Convert.ToDouble(hoursNumber.Value));
                tempDateTime.AddMinutes(Convert.ToDouble(minutesNumber.Value));
                schedule.BackupDateTime = tempDateTime;
            }
            return schedule;
        }
        //END CUSTOM METHODS

        //EVENT METHODS
        private void newFtpServer_Click(object sender, EventArgs e)
        {
            actionTitle.Text = "New server";
            configServersPanel.Visible = true;
            domainName.Text = "";
            username.Text = "";
            password.Text = "";
        }

        private void editFtpServer_Click(object sender, EventArgs e)
        {
            actionTitle.Text = "Edit server";
            configServersPanel.Visible = true;
            string[] line=serversListBox.SelectedItem.ToString().Split(',');
            serverType.SelectedItem = line[0];
            domainName.Text = line[1];
            port.Value = Convert.ToUInt32(line[2]);
            username.Text = line[3];
            password.Text = "";


        }

        private void makeAction_Click(object sender, EventArgs e)
        {
            //ResetServerActionValues();
            if(domainName.Text.ToString().Equals("") || username.Text.ToString().Equals("") || password.Text.ToString().Equals(""))
            {
                MessageBox.Show("Please fill all fields");
                
            }
            else
            {
                if (serverType.SelectedItem.ToString().Equals("SFTP"))
                {
                    ConnectionStatus connectionStatus = testConnectionSFTP(domainName.Text, port.Value.ToString(), username.Text, password.Text);
                    SetConnectionStatus(connectionStatus);
                    if (connectionStatus == ConnectionStatus.OK)
                    {

                        saveServer.Enabled = true;
                    }
                }
                else
                {
                    ConnectionStatus connectionStatus = testConnectionFTP(domainName.Text, port.Value.ToString(), username.Text, password.Text);
                    SetConnectionStatus(connectionStatus);
                    if (connectionStatus == ConnectionStatus.OK)
                    {
                        
                        saveServer.Enabled = true;
                    }
                }
            }
            
        }

        private void cancelAction_Click(object sender, EventArgs e)
        {
            configServersPanel.Visible = false;
            ResetServerActionValues();
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
                    return;
                }
                
                sftp.setServerType(serverType.Text);
                sftp.setDomainName(domainName.Text);
                sftp.setPort(port.Value.ToString());
                sftp.setUsername(username.Text);
                stream.DeleteLines(index);
                stream.WriteLines(sftp.ToString());
                serversListBox.Items.Clear();
                ArrayList list = stream.ReadLines();
                foreach (Object obj in list)
                    serversListBox.Items.Add(obj);
                


            }
            else
            {
                sftp = new Sftp(serverType.Text, domainName.Text, port.Value.ToString(), username.Text);
                //MessageBox.Show(sftp.ToString(), "Test", MessageBoxButtons.OK, MessageBoxIcon.Error);
                stream.WriteLines(sftp.ToString());
                serversListBox.Items.Add(sftp.ToString());
            }
            ArrayList servers = stream.ReadLines();
            ftpServers.Items.Clear();
            foreach (Object obj in servers)
                ftpServers.Items.Add(obj);
            FtpDownload.Items.Clear();
            foreach(Object obj in servers)
                FtpDownload.Items.Add(obj);
            saveServer.Enabled = false;
            SetConnectionStatus(ConnectionStatus.NotTested);


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
            backupDatabaseName.Enabled = true;
            compressCheckBox.Enabled = true;
            databaseFilePath.Enabled = false;
            browseDatabase.Enabled = false;

            errorProvider1.SetError(databaseFilePath, "");
        }

        private void Manual_Click(object sender, EventArgs e)
        {
            backupDatabaseName.Enabled = false;
            compressCheckBox.Enabled = false;
            databaseFilePath.Enabled = true;
            browseDatabase.Enabled = true;
        }

        private void Now_Click(object sender, EventArgs e)
        {
            repeatPanel.Enabled = false;
            onceDatetimePicker.Enabled = false;
        }

        public void button2_Click_1(object sender, EventArgs e)//Send email          
        {
            log1.updateMail(logVariables.GetVariable("email").ToString(), logVariables.GetVariable("errorLogs").ToString(), logVariables.GetVariable("successLogs").ToString(), logVariables.GetVariable("infoLogs").ToString());
            //log1.UpdateLogFile("01", "error", DateTime.Now, "desc");
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
        private void button4_Click_1(object sender, EventArgs e)
        {
            logVariables.PutVariable("email", textBox2.Text);
            if (checkBox1.Checked) logVariables.PutVariable("errorLogs", "true"); else logVariables.PutVariable("errorLogs", "false");
            if (checkBox3.Checked) logVariables.PutVariable("successLogs", "true"); else logVariables.PutVariable("successLogs", "false");
            if (checkBox2.Checked) logVariables.PutVariable("infoLogs", "true"); else logVariables.PutVariable("infoLogs", "false");
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
                onceDatetimePicker.Enabled = true;
            }
            else
            {
                onceDatetimePicker.Enabled = false;
            }
        }

        private void Repeat_Click(object sender, EventArgs e)
        {
            repeatPanel.Enabled = true;
            onceDatetimePicker.Enabled = false;
        }

        private void ApplySchedule(object sender, EventArgs e)
        {
            string selectFtp = ftpServers.GetItemText(this.ftpServers.SelectedItem);
            serverSettings = selectFtp;
            Upload up = new Upload();
            up.ShowDialog();
            if(ftpServers.SelectedIndex < 0)
            {
                MessageBox.Show("Please Select Server", "Select Server", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            //validation
            if (this.ValidateChildren())//ama petuxan ola ta validation tote kanonika ginete to schedule
            {
                if (fullAutomaticRadio.Checked)
                {
                    if (generalVariables.GetVariable("dbBinFolder") != null)
                    {
                        if (ScheduleClient.AddSchedule(BuildSchedule()))// ama ola phgan ok.
                        {
                            applyFeedback.Text = "Schedule, successful added";
                            applyFeedback.ForeColor = System.Drawing.Color.DodgerBlue;
                        }
                        else
                        {
                            applyFeedback.Text = "Failed to add schedule";
                            applyFeedback.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Can't continue, with full automatic backup. Please setup first, the path to mysql bin folder.", "Can't apply", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    if (File.Exists(databaseFilePath.Text))
                    {
                        if (ScheduleClient.AddSchedule(BuildSchedule()))// ama ola phgan ok.
                        {
                            applyFeedback.Text = "Schedule, successful added";
                            applyFeedback.ForeColor = System.Drawing.Color.DodgerBlue;
                        }
                        else
                        {
                            applyFeedback.Text = "Failed to add schedule";
                            applyFeedback.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    else
                    {
                        MessageBox.Show("The database, file not exist.", "Can't apply", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                
                // 2. ama den exei dialeksei automatic prepei na uparxei to arxeio pou exei dwsei

            }
            else//DEN prepei na ginei to schedule
            {

            }
            //end validation
        }

        private void ValidateDatabaseFile(object sender, CancelEventArgs e)
        {
            if (manualPanel.Enabled)//ama einai sto manual
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

        private void button6_Click(object sender, EventArgs e)//Clear log file button
        {
            
            DialogResult dialogResult = MessageBox.Show("This will clear the entire log file.\n Are you sure?", "Warning", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                System.IO.File.WriteAllText(@".\LogFiles\test1.txt", string.Empty);
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }

        private void Later_Click(object sender, EventArgs e)
        {
            onceDatetimePicker.Enabled = false;
            if (onceRadio.Checked)
            {
                onceDatetimePicker.Enabled = true;
            }
            else
            {
                onceDatetimePicker.Enabled = false;
            }
        }

        private void Save_Bin_Folder_Click(object sender, EventArgs e)
        {
            if (File.Exists(binFolderPath.TextBox.Text + @"\mysqldump.exe"))
            {
                generalVariables.PutVariable("dbBinFolder", binFolderPath.TextBox.Text);
            }
            else
            {
                MessageBox.Show("Wrong path, please set the corect full path, to mysql bin folder.", "Wrong path", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void browseDatabase_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Multiselect = false, ValidateNames = true, Filter = "All files|*.*" })
            {

                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    FileInfo fi = new FileInfo(ofd.FileName);
                    databaseFilePath.Text = fi.FullName;   
                }
                
                
            }


        }

        private void SelectFileB_Click(object sender, EventArgs e)
        {
            if(FtpDownload.SelectedItem == null)
            {
                MessageBox.Show("There is no saved server.", "No Server", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(FtpDownload.SelectedIndex == -1)
            {
                MessageBox.Show("Please select a server to connect.", "Select Server", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (PasswordTBDownload.Text.ToString().Equals(""))
            {
                MessageBox.Show("Please enter the password.", "Password", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string[] parts = FtpDownload.SelectedItem.ToString().Split(',');
            string serverType = parts[0];
            if (serverType.Equals("SFTP"))
            {
                listingWithSFTP();
            }
            else
            {
                listingWithFTP();
            }
        }


        // methodoi gia to download__Start
        private void SetConnectionDownloadStatus(ConnectionStatus status)
        {
            switch (status)
            {
                case ConnectionStatus.NotTested:
                    DownloadConnection.ForeColor = System.Drawing.Color.Black;
                    DownloadConnection.Text = "Not tested";
                    break;
                case ConnectionStatus.OK:
                    DownloadConnection.ForeColor = System.Drawing.Color.Green;
                    DownloadConnection.Text = "OK";
                    break;
                case ConnectionStatus.Failed:
                    DownloadConnection.ForeColor = System.Drawing.Color.Red;
                    DownloadConnection.Text = "FAILED";
                    break;
                case ConnectionStatus.Testing:
                    DownloadConnection.ForeColor = System.Drawing.Color.Black;
                    DownloadConnection.Text = "Testing...";
                    break;
            }
        }

        public static string getHomePath()
        {
            
            if (System.Environment.OSVersion.Platform == System.PlatformID.Unix)
                return System.Environment.GetEnvironmentVariable("HOME");

            return System.Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");
        }

        // methodoi gia na katevasw ston fakelo Downloads
        public static string getDownloadFolderPath()
        {
            if (System.Environment.OSVersion.Platform == System.PlatformID.Unix)
            {
                string pathDownload = System.IO.Path.Combine(getHomePath(), "Downloads");
                return pathDownload;
            }

            return System.Convert.ToString(
                Microsoft.Win32.Registry.GetValue(
                     @"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders"
                    , "{374DE290-123F-4565-9164-39C4925E467B}"
                    , String.Empty
                )
            );
        }
        private void downloadWithFTP(string filename)
        {
            
            string host = parts[1];
            string port = parts[2];
            string username = parts[3];

            string hostName = "ftp://"+host+":"+port;
            string url = string.Format("{0}/{1}", hostName, filename);
            try
            {
                WebRequest sizeRequest = WebRequest.Create(url);
                sizeRequest.Credentials = new NetworkCredential(username, PasswordTBDownload.Text.ToString());
                sizeRequest.Method = WebRequestMethods.Ftp.GetFileSize;
                int size = (int)sizeRequest.GetResponse().ContentLength;
                progressBar1.Invoke(
                (MethodInvoker)(() => progressBar1.Maximum = size));
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(url));
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential(username, PasswordTBDownload.Text.ToString());
                request.KeepAlive = true;
                request.UsePassive = true;
                //FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                //Stream responseStream = response.GetResponseStream();

                using (Stream ftpStream = request.GetResponse().GetResponseStream())
                using (var fileStream = File.Create(getDownloadFolderPath() + @"\" + filename))
                {
                    byte[] buffer = new byte[10240];
                    int read;
                    while ((read = ftpStream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        fileStream.Write(buffer, 0, read);
                        int position = (int)fileStream.Position;
                        int percent = (int)(((float)read / (float)fileStream.Length) * 100);
                        progressBar1.Invoke(
                        (MethodInvoker)(() => progressBar1.Value = position));
                        DownloadProgress.Invoke(
                        (MethodInvoker)(() => DownloadProgress.Text = percent+"%"));
                        
                    }
                    MessageBox.Show("Download Complete");
                    ftpStream.Close();


                }
            }
            catch (Exception e)
            { /*
                String status = ((FtpWebResponse)e.Response).StatusDescription;
                MessageBox.Show(status, "Αn Εrror Οccurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
                */
                MessageBox.Show(e.Message, "Αn Εrror Οccurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void downloadWithSFTP(string fileName)
        {
            
            string host = parts[1];
            string port = parts[2];
            string username = parts[3];
            
            try
            {
                using (Stream stream = new FileStream(getDownloadFolderPath() + @"\" + fileName,FileMode.Create))
                using (var sftp = new SftpClient(host, Convert.ToInt32(port), username, PasswordTBDownload.Text.ToString()))
                {
                    sftp.Connect();
                    SftpFileAttributes attributes = sftp.GetAttributes("./"+fileName);
                    var files = sftp.ListDirectory("./");
                    progressBar1.Invoke(
                            (MethodInvoker)delegate { progressBar1.Maximum = (int)attributes.Size; });
                        sftp.DownloadFile(fileName, stream,UpdateProgressBar);
                        MessageBox.Show("Download Complete");
                     
                    sftp.Disconnect();
                    sftp.Dispose();   
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "Αn Εrror Οccurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
         
           
        }

        private void listingWithFTP()
        {
            if (Allnames.Count > 0)
            {
                Allnames.Clear();
            }
            
            string host = parts[1];
            string port = parts[2];
            string username = parts[3];
            string hostName = "ftp://"+host+":"+port;

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(hostName));
            request.UsePassive = false;
            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            request.Credentials = new NetworkCredential(username,PasswordTBDownload.Text.ToString());
            request.KeepAlive = true;

            try
            {
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                List<string> entries = new List<string>();
                Allnames = new List<string>();
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    //Read the Response as String and split using New Line character.
                    entries = reader.ReadToEnd().Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
                    reader.Dispose();
                }

                response.Close();
                DataTable dtFiles = new DataTable();
                dtFiles.Columns.AddRange(new DataColumn[3] { new DataColumn("Name", typeof(string)),
                                                    new DataColumn("Size", typeof(decimal)),
                                                    new DataColumn("Date", typeof(string))});
                foreach (string entry in entries)
                {
                    string[] splits = entry.Split(new string[] { " ", }, StringSplitOptions.RemoveEmptyEntries);

                    //Determine whether entry is for File or Directory.
                    bool isFile = splits[0].Substring(0, 1) != "d";
                    bool isDirectory = splits[0].Substring(0, 1) == "d";

                    //If entry is for File, add details to DataTable.
                    if (isFile)
                    {
                        dtFiles.Rows.Add();
                        dtFiles.Rows[dtFiles.Rows.Count - 1]["Size"] = decimal.Parse(splits[4]) / 1024;
                        dtFiles.Rows[dtFiles.Rows.Count - 1]["Date"] = string.Join(" ", splits[5], splits[6], splits[7]);
                        string name = string.Empty;
                        for (int i = 8; i < splits.Length; i++)
                        {
                            name = string.Join(" ", name, splits[i]);
                        }
                        Allnames.Add(name.Trim());
                        dtFiles.Rows[dtFiles.Rows.Count - 1]["Name"] = name.Trim();
                    }
                }
                gvFiles.DataSource = dtFiles;
                gvFiles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                gvFiles.MultiSelect = false;
                SetConnectionDownloadStatus(ConnectionStatus.OK);
                SetDownloadPanelVisible();
                //gvFiles.Columns[0].Selected = true;

            }
            catch (WebException ex)
            {
                SetConnectionDownloadStatus(ConnectionStatus.Failed);
                MessageBox.Show(ex.Message, "Αn Εrror Οccurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void listingWithSFTP()
        {
            if (Allnames.Count > 0)
            {
                Allnames.Clear();
            }

            string host = parts[1];
            string port = parts[2];
            string username = parts[3];
            try
            {
                using (var sftp = new SftpClient(host, Convert.ToInt32(port), username, PasswordTBDownload.Text.ToString()))
                {
                    sftp.Connect();
                    var files = sftp.ListDirectory("./");
                    DataTable dtFiles = new DataTable();
                    dtFiles.Columns.AddRange(new DataColumn[3] { new DataColumn("Name", typeof(string)),
                                                    new DataColumn("Size", typeof(decimal)),
                                                    new DataColumn("Date", typeof(string))});
                    foreach (var file in files)
                    {
                        if (file.IsRegularFile)
                        {
                            dtFiles.Rows.Add();
                            dtFiles.Rows[dtFiles.Rows.Count - 1]["Size"] = decimal.Parse(file.Length.ToString());
                            dtFiles.Rows[dtFiles.Rows.Count - 1]["Date"] = file.LastAccessTime;
                            dtFiles.Rows[dtFiles.Rows.Count - 1]["Name"] = file.Name;
                            Allnames.Add(file.Name);
                        }
                    }
                    gvFiles.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    gvFiles.MultiSelect = false;
                    gvFiles.DataSource = dtFiles;
                    sftp.Disconnect();
                    sftp.Dispose();
                    SetConnectionDownloadStatus(ConnectionStatus.OK);
                    SetDownloadPanelVisible();


                }
            
            }
            catch(Exception e)
            {
                SetConnectionDownloadStatus(ConnectionStatus.Failed);
                MessageBox.Show(e.Message, "Αn Εrror Οccurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetDownloadPanelNotVisble()
        {
            SelectFileDownload.Visible = false;
            DonwloadAfterSFile.Visible = false;
        }
        private void SetDownloadPanelVisible()
        {
            SelectFileDownload.Visible = true;
            DonwloadAfterSFile.Visible = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if(gvFiles.SelectedRows.Count < 0)
            {
                MessageBox.Show("Please select a file to download", "Select A File", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string filename = Allnames.ElementAt(gvFiles.CurrentRow.Index);
            
            string serverType = parts[0];
            if (serverType.Equals("SFTP"))
            {
                Task.Run(() => downloadWithSFTP(filename));        
            }
            else
            {
                Task.Run( () => downloadWithFTP(filename));
            }
       
        }

        private void UpdateProgressBar(ulong downloaded)
        {
            progressBar1.Invoke(
                (MethodInvoker)delegate { progressBar1.Value = (int)downloaded;
                 int percent = (int)(((double)progressBar1.Value / (double)progressBar1.Maximum) * 100);
                 DownloadProgress.Text = percent + " %";
                });
        }

        private void FtpDownload_SelectedIndexChanged(object sender, EventArgs e)
        {
            parts = FtpDownload.SelectedItem.ToString().Split(',');
            SetDownloadPanelNotVisble();
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void browseBinFolder_Click(object sender, EventArgs e)
        {
  
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                binFolderPath.TextBox.Text = folderBrowserDialog1.SelectedPath;
            }
        }


        private void browseImportDatabase_Click(object sender, EventArgs e)
        {
            openFileDialog1.Title = "C# Corner Open File Dialog";
            openFileDialog1.InitialDirectory = @"c:\";
            openFileDialog1.Filter = "All files (*.*)|*.*|All files (*.*)|*.*";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //importDatabasePath.Text = openFileDialog1.FileName;
            }
        }

        private void button8_Click(object sender, EventArgs e)//refresh button gia to logpanel
        {
            dataGridView1.Rows.Clear();
            log1.updateGridView(dataGridView1);
        }


        string exportPath;
        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start(ExportDB.GetExportsFolder());
        }

        private void exportButton_Click(object sender, EventArgs e)
        {
            int code = ExportDB.Export(generalVariables.GetVariable("dbBinFolder").ToString(), userNameTextBox.Text,passwordTextBox.Text, dbNameExport.Text,out string exportFile);
            if (code == 0)
            {
                MessageBox.Show("Export complete successful.", "Export complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please make sure, username, passwrod, database name, and bin folder path is correct.", "Export failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            log1.UpdateLogFile(log1.getId().ToString(),"info",DateTime.Now,"test");
        }





        // methodoi gia to download__Finish

        //END EVENT METHODS
    }
}
