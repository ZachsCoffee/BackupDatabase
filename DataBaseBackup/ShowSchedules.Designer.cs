namespace DataBaseBackup
{
    partial class BackupSchedules
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BackupSchedules));
            this.schedulesListBox = new System.Windows.Forms.ListBox();
            this.label9 = new System.Windows.Forms.Label();
            this.deleteSchedules = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // schedulesListBox
            // 
            this.schedulesListBox.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.schedulesListBox.FormattingEnabled = true;
            this.schedulesListBox.ItemHeight = 17;
            this.schedulesListBox.Items.AddRange(new object[] {
            "[minutes:hours] [d/a/t/e] [databe name] [domain name of server]"});
            this.schedulesListBox.Location = new System.Drawing.Point(23, 55);
            this.schedulesListBox.Margin = new System.Windows.Forms.Padding(10);
            this.schedulesListBox.Name = "schedulesListBox";
            this.schedulesListBox.Size = new System.Drawing.Size(453, 344);
            this.schedulesListBox.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Century Gothic", 12.25F);
            this.label9.Location = new System.Drawing.Point(19, 19);
            this.label9.Margin = new System.Windows.Forms.Padding(10, 10, 10, 5);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(93, 21);
            this.label9.TabIndex = 7;
            this.label9.Text = "Schedules";
            // 
            // deleteSchedules
            // 
            this.deleteSchedules.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.deleteSchedules.Location = new System.Drawing.Point(489, 55);
            this.deleteSchedules.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.deleteSchedules.Name = "deleteSchedules";
            this.deleteSchedules.Size = new System.Drawing.Size(124, 24);
            this.deleteSchedules.TabIndex = 10;
            this.deleteSchedules.Text = "Delete";
            this.deleteSchedules.UseVisualStyleBackColor = true;
            // 
            // BackupSchedules
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(628, 418);
            this.Controls.Add(this.deleteSchedules);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.schedulesListBox);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "BackupSchedules";
            this.Text = "Backup schedules";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox schedulesListBox;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button deleteSchedules;
    }
}