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
        private int serverSettingsid;


        public Form1()
        {
            InitializeComponent();

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

            onLoadValue();

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
            testConnectionSFTP();
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


     
        // nani
        private void testConnectionSFTP()
        {
            
            
            
            
            using (SftpClient sftp = new SftpClient(domainName.Text, (int)port.Value,username.Text,password.Text)) // dimiourgia antikeimenou gia sindeso sftp
            {
                
                try
                {
                    sftp.Connect();
                }
                catch (Renci.SshNet.Common.SshAuthenticationException AuthEx)
                {
                    MessageBox.Show(AuthEx.Message, "Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch(System.Net.Sockets.SocketException sochEx)
                {
                    MessageBox.Show(sochEx.Message, "Port Number Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


                if (sftp.IsConnected) //elenxos gia to connection
                {
                    SetConnectionStatus(ConnectionStatus.OK);
                    saveServer.Enabled = true;
                }
                else
                {
                    SetConnectionStatus(ConnectionStatus.Failed);
                }
                sftp.Disconnect();
                sftp.Dispose();

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
            if (actionTitle.Text == "Edit server")
            {
                editServer();

                serversListBox.Items.Clear();

            }
            else
            {
                writeServer(serverSettingsid.ToString(), serverType.Text, domainName.Text, port.Value.ToString(), username.Text);
                readSavedServer();

            }
        }


        private void readSavedServer()
        {
            string line;
            using (StreamReader readServerSettings = new StreamReader("saveServerSettings.txt"))
            {
                while ((line = readServerSettings.ReadLine()) != null)
                {
                    string id = line;
                    string serverType = readServerSettings.ReadLine();
                    string domainName = readServerSettings.ReadLine();
                    string port = readServerSettings.ReadLine();
                    string username = readServerSettings.ReadLine();

                }
                readServerSettings.Dispose();

            }



        }







        private void writeServer(string id, string serType, string domainNam, string port, string user)
        {

            //
            string savedLine = serType.ToLower() + ":" + domainNam + ":" + port + ":" + user;
            using (FileStream fs = new FileStream("saveServerSettings.txt", FileMode.Append))
            {
                fs.Seek(0, SeekOrigin.End);
                //save ta stoixe serverType ,domain name,porn number,username kai id

                using (StreamWriter saveServerSettings = new StreamWriter(fs))
                {
                    saveServerSettings.WriteLine(id);
                    saveServerSettings.WriteLine(serType);
                    saveServerSettings.WriteLine(domainNam);
                    saveServerSettings.WriteLine(port);
                    saveServerSettings.WriteLine(user);
                    saveServerSettings.Flush();
                    saveServerSettings.Dispose();

                }



            }

            serversListBox.Items.Add(savedLine);
            serverSettingsid++;
            changeValue();


        }


        private void deleteServer()
        {
            string line = null;
            string id = serversListBox.SelectedIndex.ToString(); //-------------------edw s


            using (FileStream fs = new FileStream("saveServerSettings.txt", FileMode.Open, FileAccess.ReadWrite))
            {
                fs.Seek(0, SeekOrigin.Begin);
                using (StreamReader reader = new StreamReader(fs))
                {
                    using (StreamWriter writer = new StreamWriter(fs))
                    {
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line.Equals(id))
                            {
                                for (int i = 0; i <= 4; i++)
                                {
                                    writer.WriteLine("");
                                }
                            }
                            writer.WriteLine(line);
                        }
                    }


                }


                fs.Dispose();
            }




        }




        private void editServer()
        {
            string id = serversListBox.SelectedIndex.ToString();


        }

        private void deleteFtpServer_Click(object sender, EventArgs e)
        {
            string id = serversListBox.SelectedIndex.ToString();
            if (id != null)
            {
                serversListBox.Items.RemoveAt(Int32.Parse(id));
            }
            serverSettingsid--;
            changeValue();
            deleteServer();



        }


        private void onLoadValue()
        {
            string line;
            using (StreamReader readValue = new StreamReader("currentValue.txt"))
            {

                line = readValue.ReadLine();

                readValue.Dispose();
            }
            serverSettingsid = Int32.Parse(line);

        }

        private void changeValue()
        {
            string id = File.ReadAllText("currentValue.txt");
            id = id.Replace(id, serverSettingsid.ToString());
            File.WriteAllText("currentValue.txt", id);
        }

   

























        //END EVENT METHODS
    }

      
    }
