namespace DataBaseBackup
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
            this.configServersPanel = new System.Windows.Forms.Panel();
            this.cancelAction = new System.Windows.Forms.Button();
            this.makeAction = new System.Windows.Forms.Button();
            this.serverType = new System.Windows.Forms.ComboBox();
            this.port = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.username = new System.Windows.Forms.TextBox();
            this.domainName = new System.Windows.Forms.TextBox();
            this.actionTitle = new System.Windows.Forms.Label();
            this.deleteFtpServer = new System.Windows.Forms.Button();
            this.editFtpServer = new System.Windows.Forms.Button();
            this.newFtpServer = new System.Windows.Forms.Button();
            this.serversListBox = new System.Windows.Forms.ListBox();
            this.label6 = new System.Windows.Forms.Label();
            this.logPanel = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.databasePanel.SuspendLayout();
            this.serversPanel.SuspendLayout();
            this.configServersPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.port)).BeginInit();
            this.logPanel.SuspendLayout();
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
            this.pageTitle.Font = new System.Drawing.Font("Century Gothic", 15.75F);
            this.pageTitle.Location = new System.Drawing.Point(347, 45);
            this.pageTitle.Name = "pageTitle";
            this.pageTitle.Size = new System.Drawing.Size(111, 24);
            this.pageTitle.TabIndex = 1;
            this.pageTitle.Text = "Database";
            this.pageTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            this.databaseButton.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.databaseName.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.databaseName.FormattingEnabled = true;
            this.databaseName.Items.AddRange(new object[] {
            "MySQL"});
            this.databaseName.Location = new System.Drawing.Point(14, 79);
            this.databaseName.Name = "databaseName";
            this.databaseName.Size = new System.Drawing.Size(121, 24);
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
            this.serversPanel.Controls.Add(this.configServersPanel);
            this.serversPanel.Controls.Add(this.deleteFtpServer);
            this.serversPanel.Controls.Add(this.editFtpServer);
            this.serversPanel.Controls.Add(this.newFtpServer);
            this.serversPanel.Controls.Add(this.label6);
            this.serversPanel.Controls.Add(this.serversListBox);
            this.serversPanel.Location = new System.Drawing.Point(249, 103);
            this.serversPanel.Name = "serversPanel";
            this.serversPanel.Size = new System.Drawing.Size(526, 418);
            this.serversPanel.TabIndex = 3;
            // 
            // configServersPanel
            // 
            this.configServersPanel.BackColor = System.Drawing.Color.White;
            this.configServersPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.configServersPanel.Controls.Add(this.cancelAction);
            this.configServersPanel.Controls.Add(this.makeAction);
            this.configServersPanel.Controls.Add(this.serverType);
            this.configServersPanel.Controls.Add(this.port);
            this.configServersPanel.Controls.Add(this.label8);
            this.configServersPanel.Controls.Add(this.label7);
            this.configServersPanel.Controls.Add(this.label11);
            this.configServersPanel.Controls.Add(this.label10);
            this.configServersPanel.Controls.Add(this.username);
            this.configServersPanel.Controls.Add(this.domainName);
            this.configServersPanel.Controls.Add(this.actionTitle);
            this.configServersPanel.Location = new System.Drawing.Point(274, 41);
            this.configServersPanel.Name = "configServersPanel";
            this.configServersPanel.Size = new System.Drawing.Size(247, 340);
            this.configServersPanel.TabIndex = 8;
            this.configServersPanel.Visible = false;
            // 
            // cancelAction
            // 
            this.cancelAction.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.cancelAction.Location = new System.Drawing.Point(14, 304);
            this.cancelAction.Name = "cancelAction";
            this.cancelAction.Size = new System.Drawing.Size(75, 23);
            this.cancelAction.TabIndex = 10;
            this.cancelAction.Text = "Cancel";
            this.cancelAction.UseVisualStyleBackColor = true;
            this.cancelAction.Click += new System.EventHandler(this.cancelAction_Click);
            // 
            // makeAction
            // 
            this.makeAction.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.makeAction.Location = new System.Drawing.Point(162, 304);
            this.makeAction.Name = "makeAction";
            this.makeAction.Size = new System.Drawing.Size(75, 23);
            this.makeAction.TabIndex = 9;
            this.makeAction.Text = "Action";
            this.makeAction.UseVisualStyleBackColor = true;
            this.makeAction.Click += new System.EventHandler(this.makeAction_Click);
            // 
            // serverType
            // 
            this.serverType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.serverType.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.serverType.FormattingEnabled = true;
            this.serverType.Items.AddRange(new object[] {
            "SFTP",
            "FTP"});
            this.serverType.Location = new System.Drawing.Point(14, 73);
            this.serverType.Name = "serverType";
            this.serverType.Size = new System.Drawing.Size(106, 24);
            this.serverType.TabIndex = 8;
            // 
            // port
            // 
            this.port.Font = new System.Drawing.Font("Century Gothic", 10.25F);
            this.port.Location = new System.Drawing.Point(14, 207);
            this.port.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.port.Name = "port";
            this.port.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.port.Size = new System.Drawing.Size(75, 24);
            this.port.TabIndex = 7;
            this.port.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Century Gothic", 10.25F);
            this.label8.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label8.Location = new System.Drawing.Point(10, 46);
            this.label8.Margin = new System.Windows.Forms.Padding(10, 10, 10, 5);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(50, 19);
            this.label8.TabIndex = 6;
            this.label8.Text = "Server";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Century Gothic", 10.25F);
            this.label7.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label7.Location = new System.Drawing.Point(10, 107);
            this.label7.Margin = new System.Windows.Forms.Padding(10, 10, 10, 5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(108, 19);
            this.label7.TabIndex = 6;
            this.label7.Text = "Domain name";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Century Gothic", 10.25F);
            this.label11.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label11.Location = new System.Drawing.Point(10, 239);
            this.label11.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(77, 19);
            this.label11.TabIndex = 6;
            this.label11.Text = "Username";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Century Gothic", 10.25F);
            this.label10.ForeColor = System.Drawing.SystemColors.GrayText;
            this.label10.Location = new System.Drawing.Point(10, 180);
            this.label10.Margin = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(35, 19);
            this.label10.TabIndex = 6;
            this.label10.Text = "Port";
            // 
            // username
            // 
            this.username.Font = new System.Drawing.Font("Century Gothic", 10.25F);
            this.username.Location = new System.Drawing.Point(14, 273);
            this.username.Margin = new System.Windows.Forms.Padding(10);
            this.username.Name = "username";
            this.username.Size = new System.Drawing.Size(223, 24);
            this.username.TabIndex = 5;
            // 
            // domainName
            // 
            this.domainName.Font = new System.Drawing.Font("Century Gothic", 10.25F);
            this.domainName.Location = new System.Drawing.Point(14, 141);
            this.domainName.Margin = new System.Windows.Forms.Padding(10);
            this.domainName.Name = "domainName";
            this.domainName.Size = new System.Drawing.Size(223, 24);
            this.domainName.TabIndex = 5;
            // 
            // actionTitle
            // 
            this.actionTitle.AutoSize = true;
            this.actionTitle.Font = new System.Drawing.Font("Century Gothic", 12.25F);
            this.actionTitle.Location = new System.Drawing.Point(10, 10);
            this.actionTitle.Margin = new System.Windows.Forms.Padding(10, 10, 10, 5);
            this.actionTitle.Name = "actionTitle";
            this.actionTitle.Size = new System.Drawing.Size(65, 21);
            this.actionTitle.TabIndex = 4;
            this.actionTitle.Text = "Action";
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
            // editFtpServer
            // 
            this.editFtpServer.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.editFtpServer.Location = new System.Drawing.Point(111, 273);
            this.editFtpServer.Name = "editFtpServer";
            this.editFtpServer.Size = new System.Drawing.Size(75, 23);
            this.editFtpServer.TabIndex = 6;
            this.editFtpServer.Text = "Edit";
            this.editFtpServer.UseVisualStyleBackColor = true;
            this.editFtpServer.Click += new System.EventHandler(this.editFtpServer_Click);
            // 
            // newFtpServer
            // 
            this.newFtpServer.Font = new System.Drawing.Font("Century Gothic", 9.25F);
            this.newFtpServer.Location = new System.Drawing.Point(192, 273);
            this.newFtpServer.Name = "newFtpServer";
            this.newFtpServer.Size = new System.Drawing.Size(75, 23);
            this.newFtpServer.TabIndex = 5;
            this.newFtpServer.Text = "New";
            this.newFtpServer.UseVisualStyleBackColor = true;
            this.newFtpServer.Click += new System.EventHandler(this.newFtpServer_Click);
            // 
            // serversListBox
            // 
            this.serversListBox.Font = new System.Drawing.Font("Century Gothic", 10.25F);
            this.serversListBox.FormattingEnabled = true;
            this.serversListBox.ItemHeight = 19;
            this.serversListBox.Location = new System.Drawing.Point(16, 41);
            this.serversListBox.Name = "serversListBox";
            this.serversListBox.Size = new System.Drawing.Size(251, 194);
            this.serversListBox.TabIndex = 4;
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
            // logPanel
            // 
            this.logPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.logPanel.Controls.Add(this.button2);
            this.logPanel.Controls.Add(this.textBox1);
            this.logPanel.Controls.Add(this.label9);
            this.logPanel.Controls.Add(this.label12);
            this.logPanel.Location = new System.Drawing.Point(249, 103);
            this.logPanel.Name = "logPanel";
            this.logPanel.Size = new System.Drawing.Size(526, 418);
            this.logPanel.TabIndex = 10;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.button2.Location = new System.Drawing.Point(404, 87);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Browse";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(16, 88);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(370, 20);
            this.textBox1.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Century Gothic", 8.25F);
            this.label9.Location = new System.Drawing.Point(13, 59);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(84, 16);
            this.label9.TabIndex = 1;
            this.label9.Text = "Open Log File:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Century Gothic", 12.25F);
            this.label12.Location = new System.Drawing.Point(10, 10);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(77, 21);
            this.label12.TabIndex = 0;
            this.label12.Text = "Log Files";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
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
            this.Controls.Add(this.logPanel);
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
            this.configServersPanel.ResumeLayout(false);
            this.configServersPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.port)).EndInit();
            this.logPanel.ResumeLayout(false);
            this.logPanel.PerformLayout();
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
        private System.Windows.Forms.Panel configServersPanel;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox domainName;
        private System.Windows.Forms.Label actionTitle;
        private System.Windows.Forms.NumericUpDown port;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox serverType;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button cancelAction;
        private System.Windows.Forms.Button makeAction;
        private System.Windows.Forms.TextBox username;
        private System.Windows.Forms.Panel logPanel;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

