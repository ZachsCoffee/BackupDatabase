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

namespace DataBaseBackup
{
    public partial class Form1 : Form
    {
        private Panel[] panels;
        private Panel currentPanel;
        public Form1()
        {
            InitializeComponent();

            //ola ta nea panels prepei na mpoun se auton ton pinaka, kai meta sthn switch (method MenuClick)
            panels = new Panel[] {databasePanel, serversPanel};//krata ola ta panel gia na mporeis na ta allazeis 
            currentPanel = databasePanel;//einai to arxiko
        }

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
            }
        }

        private void SwitchPanels(int index, string title)
        {
            currentPanel.SendToBack();
            panels[index].BringToFront();

            currentPanel = panels[index];

            pageTitle.Text = title;
        }
    }
}
