using DataBaseBackup.Class;
using DataBaseBackup.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBaseBackup
{
    public partial class BackupSchedules : Form
    {
        public BackupSchedules()
        {
            InitializeComponent();

            List<Schedule> schedules = ScheduleClient.GetInfo();

            foreach (Schedule schedule in schedules)
            {
                schedulesListBox.Items.Add(schedule);
                Debug.Print("aaa");
            }
        }
    }
}
