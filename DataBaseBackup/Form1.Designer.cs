﻿namespace DataBaseBackup
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pageTitle = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.logButton = new System.Windows.Forms.Button();
            this.downloadDatabase = new System.Windows.Forms.Button();
            this.backupButton = new System.Windows.Forms.Button();
            this.serversButton = new System.Windows.Forms.Button();
            this.databaseButton = new System.Windows.Forms.Button();
            this.databasePanel = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.exportButton = new System.Windows.Forms.Button();
            this.databaseName = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.browseImportDatabase = new System.Windows.Forms.Button();
            this.browseBinFolder = new System.Windows.Forms.Button();
            this.dbNameExport = new System.Windows.Forms.TextBox();
            this.importDatabasePath = new System.Windows.Forms.TextBox();
            this.binFolderPath = new System.Windows.Forms.TextBox();
            this.serversPanel = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.serversListBox = new System.Windows.Forms.ListBox();
            this.newFtpServer = new System.Windows.Forms.Button();
            this.editFtpServer = new System.Windows.Forms.Button();
            this.deleteFtpServer = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.databasePanel.SuspendLayout();
            this.serversPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pageTitle);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(787, 100);
            this.panel1.TabIndex = 0;
            // 
            // pageTitle
            // 
            this.pageTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.pageTitle.AutoSize = true;
            this.pageTitle.Font = new System.Drawing.Font("Century Gothic", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pageTitle.Location = new System.Drawing.Point(347, 45);
            this.pageTitle.Name = "pageTitle";
            this.pageTitle.Size = new System.Drawing.Size(111, 24);
            this.pageTitle.TabIndex = 1;
            this.pageTitle.Text = "Database";
            this.pageTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DataBaseBackup.Properties.Resources.logo;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 85);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel2.Controls.Add(this.logButton);
            this.panel2.Controls.Add(this.downloadDatabase);
            this.panel2.Controls.Add(this.backupButton);
            this.panel2.Controls.Add(this.serversButton);
            this.panel2.Controls.Add(this.databaseButton);
            this.panel2.Location = new System.Drawing.Point(12, 103);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(231, 227);
            this.panel2.TabIndex = 1;
            // 
            // logButton
            // 
            this.logButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.logButton.Font = new System.Drawing.Font("Century Gothic", 12.25F);
            this.logButton.Image = global::DataBaseBackup.Properties.Resources.document_32xMD;
            this.logButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.logButton.Location = new System.Drawing.Point(0, 180);
            this.logButton.Name = "logButton";
            this.logButton.Size = new System.Drawing.Size(231, 45);
            this.logButton.TabIndex = 4;
            this.logButton.Text = "Log";
            this.logButton.UseVisualStyleBackColor = true;
            this.logButton.Click += new System.EventHandler(this.MenuClick);
            // 
            // downloadDatabase
            // 
            this.downloadDatabase.Dock = System.Windows.Forms.DockStyle.Top;
            this.downloadDatabase.Font = new System.Drawing.Font("Century Gothic", 12.25F);
            this.downloadDatabase.Image = global::DataBaseBackup.Properties.Resources.build_Selection_32xMD;
            this.downloadDatabase.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.downloadDatabase.Location = new System.Drawing.Point(0, 135);
            this.downloadDatabase.Name = "downloadDatabase";
            this.downloadDatabase.Size = new System.Drawing.Size(231, 45);
            this.downloadDatabase.TabIndex = 3;
            this.downloadDatabase.Text = "Download DB";
            this.downloadDatabase.UseVisualStyleBackColor = true;
            this.downloadDatabase.Click += new System.EventHandler(this.MenuClick);
            // 
            // backupButton
            // 
            this.backupButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.backupButton.Font = new System.Drawing.Font("Century Gothic", 12.25F);
            this.backupButton.Image = global::DataBaseBackup.Properties.Resources.package_32xMD;
            this.backupButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.backupButton.Location = new System.Drawing.Point(0, 90);
            this.backupButton.Name = "backupButton";
            this.backupButton.Size = new System.Drawing.Size(231, 45);
            this.backupButton.TabIndex = 2;
            this.backupButton.Text = "Backup";
            this.backupButton.UseVisualStyleBackColor = true;
            this.backupButton.Click += new System.EventHandler(this.MenuClick);
            // 
            // serversButton
            // 
            this.serversButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.serversButton.Font = new System.Drawing.Font("Century Gothic", 12.25F);
            this.serversButton.Image = global::DataBaseBackup.Properties.Resources.server_Remote_32xMD;
            this.serversButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.serversButton.Location = new System.Drawing.Point(0, 45);
            this.serversButton.Name = "serversButton";
            this.serversButton.Size = new System.Drawing.Size(231, 45);
            this.serversButton.TabIndex = 1;
            this.serversButton.Tag = "";
            this.serversButton.Text = "Servers";
            this.serversButton.UseVisualStyleBackColor = true;
            this.serversButton.Click += new System.EventHandler(this.MenuClick);
            // 
            // databaseButton
            // 
            this.databaseButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.databaseButton.Font = new System.Drawing.Font("Century Gothic", 12.25F);
            this.databaseButton.Image = global::DataBaseBackup.Properties.Resources.database_32xLG;
            this.databaseButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.databaseButton.Location = new System.Drawing.Point(0, 0);
            this.databaseButton.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.databaseButton.Name = "databaseButton";
            this.databaseButton.Size = new System.Drawing.Size(231, 45);
            this.databaseButton.TabIndex = 0;
            this.databaseButton.Tag = "";
            this.databaseButton.Text = "Database";
            this.databaseButton.UseVisualStyleBackColor = true;
            this.databaseButton.Click += new System.EventHandler(this.MenuClick);
            // 
            // databasePanel
            // 
            this.databasePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.databasePanel.Controls.Add(this.button1);
            this.databasePanel.Controls.Add(this.exportButton);
            this.databasePanel.Controls.Add(this.databaseName);
            this.databasePanel.Controls.Add(this.label2);
            this.databasePanel.Controls.Add(this.label4);
            this.databasePanel.Controls.Add(this.label5);
            this.databasePanel.Controls.Add(this.label3);
            this.databasePanel.Controls.Add(this.label1);
            this.databasePanel.Controls.Add(this.browseImportDatabase);
            this.databasePanel.Controls.Add(this.browseBinFolder);
            this.databasePanel.Controls.Add(this.dbNameExport);
            this.databasePanel.Controls.Add(this.importDatabasePath);
            this.databasePanel.Controls.Add(this.binFolderPath);
            this.databasePanel.Location = new System.Drawing.Point(249, 103);
            this.databasePanel.Name = "databasePanel";
            this.databasePanel.Size = new System.Drawing.Size(526, 418);
            this.databasePanel.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.button1.Location = new System.Drawing.Point(198, 347);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(150, 23);
            this.button1.TabIndex = 5;
            this.button1.Text = "Open file location";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // exportButton
            // 
            this.exportButton.BackColor = System.Drawing.Color.Gainsboro;
            this.exportButton.FlatAppearance.BorderSize = 0;
            this.exportButton.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.exportButton.Location = new System.Drawing.Point(354, 347);
            this.exportButton.Name = "exportButton";
            this.exportButton.Size = new System.Drawing.Size(75, 23);
            this.exportButton.TabIndex = 4;
            this.exportButton.Text = "Export";
            this.exportButton.UseVisualStyleBackColor = false;
            // 
            // databaseName
            // 
            this.databaseName.Enabled = false;
            this.databaseName.FormattingEnabled = true;
            this.databaseName.Items.AddRange(new object[] {
            "MySQL"});
            this.databaseName.Location = new System.Drawing.Point(14, 79);
            this.databaseName.Name = "databaseName";
            this.databaseName.Size = new System.Drawing.Size(121, 21);
            this.databaseName.TabIndex = 3;
            this.databaseName.Text = "MySQL";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Century Gothic", 10.25F);
            this.label2.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label2.Location = new System.Drawing.Point(10, 47);
            this.label2.Margin = new System.Windows.Forms.Padding(10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "Database";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Century Gothic", 12.25F);
            this.label4.Location = new System.Drawing.Point(10, 273);
            this.label4.Margin = new System.Windows.Forms.Padding(10, 10, 10, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(152, 21);
            this.label4.TabIndex = 2;
            this.label4.Text = "Export database";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Century Gothic", 12.25F);
            this.label5.Location = new System.Drawing.Point(10, 11);
            this.label5.Margin = new System.Windows.Forms.Padding(10, 10, 10, 5);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(107, 21);
            this.label5.TabIndex = 2;
            this.label5.Text = "Preferences";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Century Gothic", 12.25F);
            this.label3.Location = new System.Drawing.Point(10, 193);
            this.label3.Margin = new System.Windows.Forms.Padding(10, 10, 10, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(155, 21);
            this.label3.TabIndex = 2;
            this.label3.Text = "Import database";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 10.25F);
            this.label1.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label1.Location = new System.Drawing.Point(10, 115);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(149, 19);
            this.label1.TabIndex = 2;
            this.label1.Text = "Database, Bin folder";
            // 
            // browseImportDatabase
            // 
            this.browseImportDatabase.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.browseImportDatabase.Location = new System.Drawing.Point(446, 229);
            this.browseImportDatabase.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.browseImportDatabase.Name = "browseImportDatabase";
            this.browseImportDatabase.Size = new System.Drawing.Size(75, 24);
            this.browseImportDatabase.TabIndex = 1;
            this.browseImportDatabase.Text = "Browse";
            this.browseImportDatabase.UseVisualStyleBackColor = true;
            // 
            // browseBinFolder
            // 
            this.browseBinFolder.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.browseBinFolder.Location = new System.Drawing.Point(446, 144);
            this.browseBinFolder.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.browseBinFolder.Name = "browseBinFolder";
            this.browseBinFolder.Size = new System.Drawing.Size(75, 24);
            this.browseBinFolder.TabIndex = 1;
            this.browseBinFolder.Text = "Browse";
            this.browseBinFolder.UseVisualStyleBackColor = true;
            // 
            // dbNameExport
            // 
            this.dbNameExport.Font = new System.Drawing.Font("Century Gothic", 10.25F);
            this.dbNameExport.Location = new System.Drawing.Point(14, 309);
            this.dbNameExport.Margin = new System.Windows.Forms.Padding(10);
            this.dbNameExport.Name = "dbNameExport";
            this.dbNameExport.Size = new System.Drawing.Size(416, 24);
            this.dbNameExport.TabIndex = 0;
            this.dbNameExport.Text = "Database name.";
            // 
            // importDatabasePath
            // 
            this.importDatabasePath.Font = new System.Drawing.Font("Century Gothic", 10.25F);
            this.importDatabasePath.Location = new System.Drawing.Point(14, 229);
            this.importDatabasePath.Margin = new System.Windows.Forms.Padding(10);
            this.importDatabasePath.Name = "importDatabasePath";
            this.importDatabasePath.Size = new System.Drawing.Size(416, 24);
            this.importDatabasePath.TabIndex = 0;
            this.importDatabasePath.Text = "Path to database file.";
            // 
            // binFolderPath
            // 
            this.binFolderPath.Font = new System.Drawing.Font("Century Gothic", 10.25F);
            this.binFolderPath.Location = new System.Drawing.Point(14, 144);
            this.binFolderPath.Margin = new System.Windows.Forms.Padding(10);
            this.binFolderPath.Name = "binFolderPath";
            this.binFolderPath.Size = new System.Drawing.Size(416, 24);
            this.binFolderPath.TabIndex = 0;
            this.binFolderPath.Text = "C:\\Program Files\\MySQL\\MySQL Server x.x\\bin";
            // 
            // serversPanel
            // 
            this.serversPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.serversPanel.Controls.Add(this.deleteFtpServer);
            this.serversPanel.Controls.Add(this.editFtpServer);
            this.serversPanel.Controls.Add(this.newFtpServer);
            this.serversPanel.Controls.Add(this.serversListBox);
            this.serversPanel.Controls.Add(this.label6);
            this.serversPanel.Location = new System.Drawing.Point(249, 103);
            this.serversPanel.Name = "serversPanel";
            this.serversPanel.Size = new System.Drawing.Size(526, 418);
            this.serversPanel.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Century Gothic", 12.25F);
            this.label6.Location = new System.Drawing.Point(11, 10);
            this.label6.Margin = new System.Windows.Forms.Padding(10, 10, 10, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 21);
            this.label6.TabIndex = 3;
            this.label6.Text = "FTP servers";
            // 
            // serversListBox
            // 
            this.serversListBox.Font = new System.Drawing.Font("Century Gothic", 10.25F);
            this.serversListBox.FormattingEnabled = true;
            this.serversListBox.ItemHeight = 19;
            this.serversListBox.Location = new System.Drawing.Point(16, 41);
            this.serversListBox.Name = "serversListBox";
            this.serversListBox.Size = new System.Drawing.Size(333, 213);
            this.serversListBox.TabIndex = 4;
            // 
            // newFtpServer
            // 
            this.newFtpServer.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.newFtpServer.Location = new System.Drawing.Point(274, 272);
            this.newFtpServer.Name = "newFtpServer";
            this.newFtpServer.Size = new System.Drawing.Size(75, 23);
            this.newFtpServer.TabIndex = 5;
            this.newFtpServer.Text = "New";
            this.newFtpServer.UseVisualStyleBackColor = true;
            // 
            // editFtpServer
            // 
            this.editFtpServer.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.editFtpServer.Location = new System.Drawing.Point(193, 272);
            this.editFtpServer.Name = "editFtpServer";
            this.editFtpServer.Size = new System.Drawing.Size(75, 23);
            this.editFtpServer.TabIndex = 6;
            this.editFtpServer.Text = "Edit";
            this.editFtpServer.UseVisualStyleBackColor = true;
            // 
            // deleteFtpServer
            // 
            this.deleteFtpServer.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.deleteFtpServer.Location = new System.Drawing.Point(16, 272);
            this.deleteFtpServer.Name = "deleteFtpServer";
            this.deleteFtpServer.Size = new System.Drawing.Size(75, 23);
            this.deleteFtpServer.TabIndex = 7;
            this.deleteFtpServer.Text = "Delete";
            this.deleteFtpServer.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(787, 533);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.databasePanel);
            this.Controls.Add(this.serversPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Database Backup";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.databasePanel.ResumeLayout(false);
            this.databasePanel.PerformLayout();
            this.serversPanel.ResumeLayout(false);
            this.serversPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label pageTitle;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button databaseButton;
        private System.Windows.Forms.Button serversButton;
        private System.Windows.Forms.Button backupButton;
        private System.Windows.Forms.Button downloadDatabase;
        private System.Windows.Forms.Button logButton;
        private System.Windows.Forms.Panel databasePanel;
        private System.Windows.Forms.ComboBox databaseName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button browseBinFolder;
        private System.Windows.Forms.TextBox binFolderPath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button browseImportDatabase;
        private System.Windows.Forms.TextBox importDatabasePath;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button exportButton;
        private System.Windows.Forms.TextBox dbNameExport;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel serversPanel;
        private System.Windows.Forms.Button deleteFtpServer;
        private System.Windows.Forms.Button editFtpServer;
        private System.Windows.Forms.Button newFtpServer;
        private System.Windows.Forms.ListBox serversListBox;
        private System.Windows.Forms.Label label6;
    }
}

