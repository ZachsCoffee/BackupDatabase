using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace DataBaseBackup
{
    public partial class Upload : Form
    {
        public enum ConnectionStatus//einai 3 katastaseis gia to label connectionStatusLabel, (method SetConnectionStatus)
        {
            NotTested, OK, Failed, Testing
        }

        public static string password;
        public Upload()
        {
            InitializeComponent();
            InitListBox();
        }
     


        public void InitListBox()
        {
            listBox1.Items.Clear();
            listBox1.Items.Add(Form1.serverSettings);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.ToString().Equals(""))
            {
                MessageBox.Show("Please fill the field", "Empty Field", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            string[] parts=Form1.serverSettings.Split(',');
            if (parts[0].Equals("SFTP"))
            {
                if(Form1.testConnectionSFTP(parts[1],parts[2],parts[3],textBox1.Text.ToString()) == Form1.ConnectionStatus.OK)
                {
                    label2.Text = "OK";
                    label2.ForeColor= System.Drawing.Color.Green;
                    password = textBox1.Text.ToString();
                }
                else{
                    label2.Text = "Failed";
                    label2.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                if(Form1.testConnectionFTP(parts[1], parts[2], parts[3], textBox1.Text.ToString()) == Form1.ConnectionStatus.OK)
                {
                    label2.Text = "OK";
                    label2.ForeColor = System.Drawing.Color.Green;
                    password = textBox1.Text.ToString();
                }
                else
                {
                    label2.Text = "Failed";
                    label2.ForeColor = System.Drawing.Color.Red;
                }
            }
            
            
        }
    }
}
