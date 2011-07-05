namespace WindowsFormsApplication1
{
    partial class Form12
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose (bool disposing)
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
            this.lvwProcesses = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.txtMachine = new System.Windows.Forms.TextBox();
            this.cmdRefersh = new System.Windows.Forms.Button();
            this.txtUserDomainName = new System.Windows.Forms.TextBox();
            this.txtUserInteractive = new System.Windows.Forms.TextBox();
            this.txtUserName = new System.Windows.Forms.TextBox();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.txtWorkingSet = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCurrentDirectory = new System.Windows.Forms.TextBox();
            this.txtExitCode = new System.Windows.Forms.TextBox();
            this.txtHasShutdownStarted = new System.Windows.Forms.TextBox();
            this.txtOSVersion = new System.Windows.Forms.TextBox();
            this.txtProcessorCount = new System.Windows.Forms.TextBox();
            this.txtSystemDirectory = new System.Windows.Forms.TextBox();
            this.txtTickCount = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.txtStackTrace = new System.Windows.Forms.RichTextBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label_a2 = new System.Windows.Forms.Label();
            this.label_a1 = new System.Windows.Forms.Label();
            this.label_b2 = new System.Windows.Forms.Label();
            this.label_h1 = new System.Windows.Forms.Label();
            this.label_f1 = new System.Windows.Forms.Label();
            this.label_e1 = new System.Windows.Forms.Label();
            this.label_d1 = new System.Windows.Forms.Label();
            this.label_c1 = new System.Windows.Forms.Label();
            this.label_b1 = new System.Windows.Forms.Label();
            this.label_i1 = new System.Windows.Forms.Label();
            this.label_c2 = new System.Windows.Forms.Label();
            this.label_d2 = new System.Windows.Forms.Label();
            this.label_e2 = new System.Windows.Forms.Label();
            this.label_f2 = new System.Windows.Forms.Label();
            this.label_h2 = new System.Windows.Forms.Label();
            this.label_i2 = new System.Windows.Forms.Label();
            this.label_g2 = new System.Windows.Forms.Label();
            this.label_g1 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lvwProcesses
            // 
            this.lvwProcesses.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lvwProcesses.CheckBoxes = true;
            this.lvwProcesses.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
            this.lvwProcesses.FullRowSelect = true;
            this.lvwProcesses.GridLines = true;
            this.lvwProcesses.HoverSelection = true;
            this.lvwProcesses.Location = new System.Drawing.Point(3, 3);
            this.lvwProcesses.MultiSelect = false;
            this.lvwProcesses.Name = "lvwProcesses";
            this.lvwProcesses.ShowItemToolTips = true;
            this.lvwProcesses.Size = new System.Drawing.Size(674, 497);
            this.lvwProcesses.TabIndex = 0;
            this.lvwProcesses.UseCompatibleStateImageBehavior = false;
            this.lvwProcesses.View = System.Windows.Forms.View.Details;
            this.lvwProcesses.Click += new System.EventHandler(this.lvwProcesses_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Process name";
            this.columnHeader1.Width = 233;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Window Header";
            this.columnHeader2.Width = 276;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Process Status";
            this.columnHeader3.Width = 93;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Memory";
            // 
            // txtMachine
            // 
            this.txtMachine.Location = new System.Drawing.Point(811, 33);
            this.txtMachine.Name = "txtMachine";
            this.txtMachine.Size = new System.Drawing.Size(250, 20);
            this.txtMachine.TabIndex = 1;
            // 
            // cmdRefersh
            // 
            this.cmdRefersh.Location = new System.Drawing.Point(939, 515);
            this.cmdRefersh.Name = "cmdRefersh";
            this.cmdRefersh.Size = new System.Drawing.Size(121, 23);
            this.cmdRefersh.TabIndex = 2;
            this.cmdRefersh.Text = "Refersh";
            this.cmdRefersh.UseVisualStyleBackColor = true;
            this.cmdRefersh.Click += new System.EventHandler(this.cmdRefersh_Click);
            // 
            // txtUserDomainName
            // 
            this.txtUserDomainName.Location = new System.Drawing.Point(811, 59);
            this.txtUserDomainName.Name = "txtUserDomainName";
            this.txtUserDomainName.Size = new System.Drawing.Size(250, 20);
            this.txtUserDomainName.TabIndex = 3;
            // 
            // txtUserInteractive
            // 
            this.txtUserInteractive.Location = new System.Drawing.Point(811, 85);
            this.txtUserInteractive.Name = "txtUserInteractive";
            this.txtUserInteractive.Size = new System.Drawing.Size(250, 20);
            this.txtUserInteractive.TabIndex = 4;
            // 
            // txtUserName
            // 
            this.txtUserName.Location = new System.Drawing.Point(811, 111);
            this.txtUserName.Name = "txtUserName";
            this.txtUserName.Size = new System.Drawing.Size(250, 20);
            this.txtUserName.TabIndex = 5;
            // 
            // txtVersion
            // 
            this.txtVersion.Location = new System.Drawing.Point(811, 137);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Size = new System.Drawing.Size(250, 20);
            this.txtVersion.TabIndex = 6;
            // 
            // txtWorkingSet
            // 
            this.txtWorkingSet.Location = new System.Drawing.Point(811, 163);
            this.txtWorkingSet.Name = "txtWorkingSet";
            this.txtWorkingSet.Size = new System.Drawing.Size(250, 20);
            this.txtWorkingSet.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(683, 36);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 8;
            this.label1.Text = "Machine";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(683, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "UserDomainName";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(683, 88);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "UserInteractive";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(683, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(57, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "UserName";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(683, 140);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 13);
            this.label5.TabIndex = 12;
            this.label5.Text = "Version";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(683, 166);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 13);
            this.label6.TabIndex = 13;
            this.label6.Text = "WorkingSet";
            // 
            // txtCurrentDirectory
            // 
            this.txtCurrentDirectory.Location = new System.Drawing.Point(811, 189);
            this.txtCurrentDirectory.Name = "txtCurrentDirectory";
            this.txtCurrentDirectory.Size = new System.Drawing.Size(250, 20);
            this.txtCurrentDirectory.TabIndex = 14;
            // 
            // txtExitCode
            // 
            this.txtExitCode.Location = new System.Drawing.Point(811, 215);
            this.txtExitCode.Name = "txtExitCode";
            this.txtExitCode.Size = new System.Drawing.Size(250, 20);
            this.txtExitCode.TabIndex = 15;
            // 
            // txtHasShutdownStarted
            // 
            this.txtHasShutdownStarted.Location = new System.Drawing.Point(811, 241);
            this.txtHasShutdownStarted.Name = "txtHasShutdownStarted";
            this.txtHasShutdownStarted.Size = new System.Drawing.Size(250, 20);
            this.txtHasShutdownStarted.TabIndex = 16;
            // 
            // txtOSVersion
            // 
            this.txtOSVersion.Location = new System.Drawing.Point(811, 267);
            this.txtOSVersion.Name = "txtOSVersion";
            this.txtOSVersion.Size = new System.Drawing.Size(250, 20);
            this.txtOSVersion.TabIndex = 17;
            // 
            // txtProcessorCount
            // 
            this.txtProcessorCount.Location = new System.Drawing.Point(811, 293);
            this.txtProcessorCount.Name = "txtProcessorCount";
            this.txtProcessorCount.Size = new System.Drawing.Size(250, 20);
            this.txtProcessorCount.TabIndex = 18;
            // 
            // txtSystemDirectory
            // 
            this.txtSystemDirectory.Location = new System.Drawing.Point(811, 444);
            this.txtSystemDirectory.Name = "txtSystemDirectory";
            this.txtSystemDirectory.Size = new System.Drawing.Size(250, 20);
            this.txtSystemDirectory.TabIndex = 20;
            // 
            // txtTickCount
            // 
            this.txtTickCount.Location = new System.Drawing.Point(811, 470);
            this.txtTickCount.Name = "txtTickCount";
            this.txtTickCount.Size = new System.Drawing.Size(250, 20);
            this.txtTickCount.TabIndex = 21;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(683, 192);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "CurrentDirectory";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(683, 218);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(49, 13);
            this.label8.TabIndex = 23;
            this.label8.Text = "ExitCode";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(683, 244);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(108, 13);
            this.label9.TabIndex = 24;
            this.label9.Text = "HasShutdownStarted";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(683, 270);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(57, 13);
            this.label10.TabIndex = 25;
            this.label10.Text = "OSVersion";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(683, 296);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(82, 13);
            this.label11.TabIndex = 26;
            this.label11.Text = "ProcessorCount";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(683, 322);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(63, 13);
            this.label12.TabIndex = 27;
            this.label12.Text = "StackTrace";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(683, 447);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(83, 13);
            this.label13.TabIndex = 28;
            this.label13.Text = "SystemDirectory";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(683, 473);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(56, 13);
            this.label14.TabIndex = 29;
            this.label14.Text = "TickCount";
            // 
            // txtStackTrace
            // 
            this.txtStackTrace.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.txtStackTrace.Location = new System.Drawing.Point(811, 319);
            this.txtStackTrace.Name = "txtStackTrace";
            this.txtStackTrace.ReadOnly = true;
            this.txtStackTrace.Size = new System.Drawing.Size(250, 119);
            this.txtStackTrace.TabIndex = 30;
            this.txtStackTrace.Text = "";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(3, -1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.ShowToolTips = true;
            this.tabControl1.Size = new System.Drawing.Size(1087, 571);
            this.tabControl1.TabIndex = 31;
            
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.MenuBar;
            this.tabPage1.Controls.Add(this.lvwProcesses);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.label11);
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.label13);
            this.tabPage1.Controls.Add(this.label14);
            this.tabPage1.Controls.Add(this.txtCurrentDirectory);
            this.tabPage1.Controls.Add(this.txtExitCode);
            this.tabPage1.Controls.Add(this.txtHasShutdownStarted);
            this.tabPage1.Controls.Add(this.txtMachine);
            this.tabPage1.Controls.Add(this.txtOSVersion);
            this.tabPage1.Controls.Add(this.txtProcessorCount);
            this.tabPage1.Controls.Add(this.txtStackTrace);
            this.tabPage1.Controls.Add(this.txtSystemDirectory);
            this.tabPage1.Controls.Add(this.txtTickCount);
            this.tabPage1.Controls.Add(this.txtUserDomainName);
            this.tabPage1.Controls.Add(this.txtUserInteractive);
            this.tabPage1.Controls.Add(this.txtUserName);
            this.tabPage1.Controls.Add(this.txtVersion);
            this.tabPage1.Controls.Add(this.txtWorkingSet);
            this.tabPage1.Controls.Add(this.cmdRefersh);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1079, 545);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Running processes";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.MenuBar;
            this.tabPage2.Controls.Add(this.label_g2);
            this.tabPage2.Controls.Add(this.label_g1);
            this.tabPage2.Controls.Add(this.label_i2);
            this.tabPage2.Controls.Add(this.label_h2);
            this.tabPage2.Controls.Add(this.label_f2);
            this.tabPage2.Controls.Add(this.label_e2);
            this.tabPage2.Controls.Add(this.label_d2);
            this.tabPage2.Controls.Add(this.label_c2);
            this.tabPage2.Controls.Add(this.label_i1);
            this.tabPage2.Controls.Add(this.label_a2);
            this.tabPage2.Controls.Add(this.label_a1);
            this.tabPage2.Controls.Add(this.label_b2);
            this.tabPage2.Controls.Add(this.label_h1);
            this.tabPage2.Controls.Add(this.label_f1);
            this.tabPage2.Controls.Add(this.label_e1);
            this.tabPage2.Controls.Add(this.label_d1);
            this.tabPage2.Controls.Add(this.label_c1);
            this.tabPage2.Controls.Add(this.label_b1);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1079, 545);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Selected process details";
            
            // 
            // label_a2
            // 
            this.label_a2.AutoSize = true;
            this.label_a2.Location = new System.Drawing.Point(200, 13);
            this.label_a2.Name = "label_a2";
            this.label_a2.Size = new System.Drawing.Size(45, 13);
            this.label_a2.TabIndex = 8;
            this.label_a2.Tag = "";
            this.label_a2.Text = "Process";
            // 
            // label_a1
            // 
            this.label_a1.AutoSize = true;
            this.label_a1.Location = new System.Drawing.Point(17, 13);
            this.label_a1.Name = "label_a1";
            this.label_a1.Size = new System.Drawing.Size(45, 13);
            this.label_a1.TabIndex = 7;
            this.label_a1.Tag = "";
            this.label_a1.Text = "Process";
            // 
            // label_b2
            // 
            this.label_b2.AutoSize = true;
            this.label_b2.Location = new System.Drawing.Point(200, 36);
            this.label_b2.Name = "label_b2";
            this.label_b2.Size = new System.Drawing.Size(97, 13);
            this.label_b2.TabIndex = 6;
            this.label_b2.Tag = "";
            this.label_b2.Text = "PrivateMemorySize";
            // 
            // label_h1
            // 
            this.label_h1.AutoSize = true;
            this.label_h1.Location = new System.Drawing.Point(17, 169);
            this.label_h1.Name = "label_h1";
            this.label_h1.Size = new System.Drawing.Size(148, 13);
            this.label_h1.TabIndex = 5;
            this.label_h1.Text = "NonpagedSystemMemorySize";
            // 
            // label_f1
            // 
            this.label_f1.AutoSize = true;
            this.label_f1.Location = new System.Drawing.Point(17, 124);
            this.label_f1.Name = "label_f1";
            this.label_f1.Size = new System.Drawing.Size(95, 13);
            this.label_f1.TabIndex = 4;
            this.label_f1.Text = "PagedMemorySize";
            // 
            // label_e1
            // 
            this.label_e1.AutoSize = true;
            this.label_e1.Location = new System.Drawing.Point(17, 102);
            this.label_e1.Name = "label_e1";
            this.label_e1.Size = new System.Drawing.Size(129, 13);
            this.label_e1.TabIndex = 3;
            this.label_e1.Text = "PagedSystemMemorySize";
            // 
            // label_d1
            // 
            this.label_d1.AutoSize = true;
            this.label_d1.Location = new System.Drawing.Point(17, 80);
            this.label_d1.Name = "label_d1";
            this.label_d1.Size = new System.Drawing.Size(120, 13);
            this.label_d1.TabIndex = 2;
            this.label_d1.Text = "PeakPagedMemorySize";
            // 
            // label_c1
            // 
            this.label_c1.AutoSize = true;
            this.label_c1.Location = new System.Drawing.Point(17, 58);
            this.label_c1.Name = "label_c1";
            this.label_c1.Size = new System.Drawing.Size(118, 13);
            this.label_c1.TabIndex = 1;
            this.label_c1.Text = "PeakVirtualMemorySize";
            // 
            // label_b1
            // 
            this.label_b1.AutoSize = true;
            this.label_b1.Location = new System.Drawing.Point(17, 36);
            this.label_b1.Name = "label_b1";
            this.label_b1.Size = new System.Drawing.Size(97, 13);
            this.label_b1.TabIndex = 0;
            this.label_b1.Tag = "";
            this.label_b1.Text = "PrivateMemorySize";
            // 
            // label_i1
            // 
            this.label_i1.AutoSize = true;
            this.label_i1.Location = new System.Drawing.Point(17, 192);
            this.label_i1.Name = "label_i1";
            this.label_i1.Size = new System.Drawing.Size(148, 13);
            this.label_i1.TabIndex = 9;
            this.label_i1.Text = "NonpagedSystemMemorySize";
            // 
            // label_c2
            // 
            this.label_c2.AutoSize = true;
            this.label_c2.Location = new System.Drawing.Point(200, 58);
            this.label_c2.Name = "label_c2";
            this.label_c2.Size = new System.Drawing.Size(13, 13);
            this.label_c2.TabIndex = 10;
            this.label_c2.Tag = "";
            this.label_c2.Text = "L";
            // 
            // label_d2
            // 
            this.label_d2.AutoSize = true;
            this.label_d2.Location = new System.Drawing.Point(200, 80);
            this.label_d2.Name = "label_d2";
            this.label_d2.Size = new System.Drawing.Size(13, 13);
            this.label_d2.TabIndex = 11;
            this.label_d2.Tag = "";
            this.label_d2.Text = "L";
            // 
            // label_e2
            // 
            this.label_e2.AutoSize = true;
            this.label_e2.Location = new System.Drawing.Point(200, 102);
            this.label_e2.Name = "label_e2";
            this.label_e2.Size = new System.Drawing.Size(13, 13);
            this.label_e2.TabIndex = 12;
            this.label_e2.Tag = "";
            this.label_e2.Text = "L";
            // 
            // label_f2
            // 
            this.label_f2.AutoSize = true;
            this.label_f2.Location = new System.Drawing.Point(200, 124);
            this.label_f2.Name = "label_f2";
            this.label_f2.Size = new System.Drawing.Size(13, 13);
            this.label_f2.TabIndex = 13;
            this.label_f2.Tag = "";
            this.label_f2.Text = "L";
            // 
            // label_h2
            // 
            this.label_h2.AutoSize = true;
            this.label_h2.Location = new System.Drawing.Point(200, 169);
            this.label_h2.Name = "label_h2";
            this.label_h2.Size = new System.Drawing.Size(13, 13);
            this.label_h2.TabIndex = 14;
            this.label_h2.Tag = "";
            this.label_h2.Text = "L";
            // 
            // label_i2
            // 
            this.label_i2.AutoSize = true;
            this.label_i2.Location = new System.Drawing.Point(200, 192);
            this.label_i2.Name = "label_i2";
            this.label_i2.Size = new System.Drawing.Size(13, 13);
            this.label_i2.TabIndex = 15;
            this.label_i2.Tag = "";
            this.label_i2.Text = "L";
            // 
            // label_g2
            // 
            this.label_g2.AutoSize = true;
            this.label_g2.Location = new System.Drawing.Point(200, 147);
            this.label_g2.Name = "label_g2";
            this.label_g2.Size = new System.Drawing.Size(13, 13);
            this.label_g2.TabIndex = 17;
            this.label_g2.Tag = "";
            this.label_g2.Text = "L";
            // 
            // label_g1
            // 
            this.label_g1.AutoSize = true;
            this.label_g1.Location = new System.Drawing.Point(17, 147);
            this.label_g1.Name = "label_g1";
            this.label_g1.Size = new System.Drawing.Size(95, 13);
            this.label_g1.TabIndex = 16;
            this.label_g1.Text = "PagedMemorySize";
            // 
            // Form12
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1102, 572);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form12";
            this.Text = "Form12";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView lvwProcesses;
        private System.Windows.Forms.TextBox txtMachine;
        private System.Windows.Forms.Button cmdRefersh;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.TextBox txtUserDomainName;
        private System.Windows.Forms.TextBox txtUserInteractive;
        private System.Windows.Forms.TextBox txtUserName;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.TextBox txtWorkingSet;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtCurrentDirectory;
        private System.Windows.Forms.TextBox txtExitCode;
        private System.Windows.Forms.TextBox txtHasShutdownStarted;
        private System.Windows.Forms.TextBox txtOSVersion;
        private System.Windows.Forms.TextBox txtProcessorCount;
        private System.Windows.Forms.TextBox txtSystemDirectory;
        private System.Windows.Forms.TextBox txtTickCount;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.RichTextBox txtStackTrace;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label_f1;
        private System.Windows.Forms.Label label_e1;
        private System.Windows.Forms.Label label_d1;
        private System.Windows.Forms.Label label_c1;
        private System.Windows.Forms.Label label_b1;
        private System.Windows.Forms.Label label_b2;
        private System.Windows.Forms.Label label_h1;
        private System.Windows.Forms.Label label_a2;
        private System.Windows.Forms.Label label_a1;
        private System.Windows.Forms.Label label_i1;
        private System.Windows.Forms.Label label_h2;
        private System.Windows.Forms.Label label_f2;
        private System.Windows.Forms.Label label_e2;
        private System.Windows.Forms.Label label_d2;
        private System.Windows.Forms.Label label_c2;
        private System.Windows.Forms.Label label_i2;
        private System.Windows.Forms.Label label_g2;
        private System.Windows.Forms.Label label_g1;
    }
}