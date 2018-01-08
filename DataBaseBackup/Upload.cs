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
                MessageBox.Show("Please fill all fields", "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            password = textBox1.Text.ToString();
            
        }
    }
}
