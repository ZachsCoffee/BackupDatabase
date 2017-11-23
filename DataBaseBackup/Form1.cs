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


     
        
        private void testConnectionSFTP()
        {
            using (sftpclient = new SftpClient(domainName.Text, (int)port.Value,username.Text,password.Text)) // dimiourgia antikeimenou gia sindeso sftp
            {
                
                try
                {
                    sftpclient.Connect();
                }
                catch (Renci.SshNet.Common.SshAuthenticationException AuthEx)
                {
                    MessageBox.Show(AuthEx.Message, "Authentication Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch(System.Net.Sockets.SocketException sochEx)
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

 
        private void button2_Click_1(object sender, EventArgs e)
        {
            
            string startupPath = System.IO.Path.GetFullPath(@"..\..\LogFiles");

            //Add to list all files from LogFiles folder
            foreach (string s in Directory.GetFiles(startupPath, "*.txt").Select(Path.GetFileName))
                listBox1.Items.Add(s);

            
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LogFile log1 = new LogFile();
            ArrayList list = log1.print();

            foreach (Object obj in list)
                label19.Text = obj.ToString();
            Console.WriteLine();
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

        }

        private void deleteFtpServer_Click(object sender, EventArgs e)
        {
            int index = serversListBox.SelectedIndex;
            stream.DeleteLines(index);
            serversListBox.Items.RemoveAt(index);

        }



        //END EVENT METHODS
    }

      
    }
