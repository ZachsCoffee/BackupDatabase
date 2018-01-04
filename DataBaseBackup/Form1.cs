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
        Sftp sftp=null;
        ObjectStream stream = new ObjectStream("saveServer");

        //Initiation of logFile variables
        
        private LogFile log1 = new LogFile();
        private VariableStorage logVariables = new VariableStorage(Path.GetFullPath(@"..\..\LogFiles\logV"));
        private VariableStorage generalVariables = new VariableStorage(Path.GetFullPath(@"..\..\var\genV"));

        public static string serverSettings; //mia static gia na krataw to server pou epelexse o xristis.
        
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
            serverType.SelectedIndex = 0;

            //VariableStorage logVariables = new VariableStorage(System.IO.Path.GetFullPath(@"..\..\LogFiles\logV"));
     
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
            }
        }

        private void testConnectionFTP()
        {
            //an mporw na kanw listing tote exw sinthethi kanonika :P
            string uri = "ftp://" + domainName.Text + ":" + port.Value.ToString();
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(uri));
            request.UsePassive = false;
            request.Method = WebRequestMethods.Ftp.ListDirectory;
            request.Credentials = new NetworkCredential(username.Text, password.Text);
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

        private Schedule BuildSchedule()
        {
            string[] parts = serverSettings.Split(',');
            string serverType = parts[0];
            string host = parts[1];
            string port = parts[2];
            string username = parts[3];

            Ftp ftpServer = new Ftp(serverType,host,port,username);//edw bale ta xaraktitistika tou ftp server, wste meta na ta parei to scedule, ola ta pedia

            Schedule schedule = new Schedule()
            {
                FullAutomatic = fullAutomaticRadio.Checked,
                BackupNow = nowRadio.Checked,
                BackupOnce = onceRadio.Checked,
                WithCompress = compressCheckBox.Checked,
                FtpServer = ftpServer,
                DBName = backupDatabaseName.Text,
                DBFilePath = databaseFilePath.Text
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
        }

        private void editFtpServer_Click(object sender, EventArgs e)
        {
            actionTitle.Text = "Edit server";
            configServersPanel.Visible = true;

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
                testConnectionSFTP();
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
                System.IO.File.WriteAllText(@"..\..\LogFiles\test1.txt", string.Empty);
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
                string selectFtp=ftpServers.GetItemText(this.ftpServers.SelectedItem);
                serverSettings = selectFtp;
                Upload up = new Upload();
                up.ShowDialog();
                
            }


        }


        //END EVENT METHODS
    }      
}
