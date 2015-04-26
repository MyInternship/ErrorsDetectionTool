namespace Errors_detection_tool
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
            this.brwsbtn = new System.Windows.Forms.Button();
            this.filepathtb = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.header_groupBox = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ptag_cb = new System.Windows.Forms.ComboBox();
            this.generatebtn = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.IOCheckBox = new System.Windows.Forms.CheckBox();
            this.BoldItalicCheckBox = new System.Windows.Forms.CheckBox();
            this.conditionalCB = new System.Windows.Forms.CheckBox();
            this.HealCheckBox = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ascbBrowse = new System.Windows.Forms.Button();
            this.healDbtb = new System.Windows.Forms.TextBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.Menu_Strip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CRRAAnalysisReportGeneratorHelpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2.SuspendLayout();
            this.header_groupBox.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.Menu_Strip.SuspendLayout();
            this.SuspendLayout();
            // 
            // brwsbtn
            // 
            this.brwsbtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.brwsbtn.Location = new System.Drawing.Point(542, 23);
            this.brwsbtn.Margin = new System.Windows.Forms.Padding(4);
            this.brwsbtn.Name = "brwsbtn";
            this.brwsbtn.Size = new System.Drawing.Size(149, 21);
            this.brwsbtn.TabIndex = 0;
            this.brwsbtn.Text = "Browse Req Docs";
            this.brwsbtn.UseVisualStyleBackColor = true;
            this.brwsbtn.MouseLeave += new System.EventHandler(this.brwsbtn_MouseLeave_1);
            this.brwsbtn.Click += new System.EventHandler(this.brwsbtn_Click);
            this.brwsbtn.MouseEnter += new System.EventHandler(this.brwsbtn_MouseEnter);
            // 
            // filepathtb
            // 
            this.filepathtb.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filepathtb.Location = new System.Drawing.Point(47, 23);
            this.filepathtb.Margin = new System.Windows.Forms.Padding(4);
            this.filepathtb.Multiline = true;
            this.filepathtb.Name = "filepathtb";
            this.filepathtb.Size = new System.Drawing.Size(465, 21);
            this.filepathtb.TabIndex = 1;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.brwsbtn);
            this.groupBox2.Controls.Add(this.filepathtb);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(13, 271);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(707, 65);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Browse an SSRD";
            // 
            // header_groupBox
            // 
            this.header_groupBox.Controls.Add(this.label4);
            this.header_groupBox.Controls.Add(this.label3);
            this.header_groupBox.Location = new System.Drawing.Point(13, 28);
            this.header_groupBox.Margin = new System.Windows.Forms.Padding(4);
            this.header_groupBox.Name = "header_groupBox";
            this.header_groupBox.Padding = new System.Windows.Forms.Padding(4);
            this.header_groupBox.Size = new System.Drawing.Size(707, 70);
            this.header_groupBox.TabIndex = 92;
            this.header_groupBox.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.SystemColors.Control;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.label4.Location = new System.Drawing.Point(27, 24);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(421, 22);
            this.label4.TabIndex = 10;
            this.label4.Text = "Auto detection of Errors in Requirements Document";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Red;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(505, 24);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(159, 33);
            this.label3.TabIndex = 7;
            this.label3.Text = "Honeywell";
            // 
            // ptag_cb
            // 
            this.ptag_cb.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.ptag_cb.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.FileSystemDirectories;
            this.ptag_cb.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ptag_cb.FormattingEnabled = true;
            this.ptag_cb.Location = new System.Drawing.Point(48, 23);
            this.ptag_cb.Margin = new System.Windows.Forms.Padding(4);
            this.ptag_cb.Name = "ptag_cb";
            this.ptag_cb.Size = new System.Drawing.Size(464, 24);
            this.ptag_cb.TabIndex = 2;
            this.ptag_cb.MouseEnter += new System.EventHandler(this.ptag_cb_MouseEnter_1);
            this.ptag_cb.MouseLeave += new System.EventHandler(this.ptag_cb_MouseLeave);
            // 
            // generatebtn
            // 
            this.generatebtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.generatebtn.Location = new System.Drawing.Point(542, 23);
            this.generatebtn.Margin = new System.Windows.Forms.Padding(4);
            this.generatebtn.Name = "generatebtn";
            this.generatebtn.Size = new System.Drawing.Size(149, 22);
            this.generatebtn.TabIndex = 4;
            this.generatebtn.Text = "Click For Errors";
            this.generatebtn.UseVisualStyleBackColor = true;
            this.generatebtn.MouseLeave += new System.EventHandler(this.generatebtn_MouseLeave_1);
            this.generatebtn.Click += new System.EventHandler(this.generatebtn_Click);
            this.generatebtn.MouseEnter += new System.EventHandler(this.generatebtn_MouseEnter_1);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.generatebtn);
            this.groupBox1.Controls.Add(this.ptag_cb);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(13, 344);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(707, 67);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Select a P-Tag";
            // 
            // IOCheckBox
            // 
            this.IOCheckBox.AutoSize = true;
            this.IOCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IOCheckBox.Location = new System.Drawing.Point(382, 50);
            this.IOCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.IOCheckBox.Name = "IOCheckBox";
            this.IOCheckBox.Size = new System.Drawing.Size(309, 20);
            this.IOCheckBox.TabIndex = 9;
            this.IOCheckBox.Text = "missing parameters in input and Output sections";
            this.IOCheckBox.UseVisualStyleBackColor = true;
            // 
            // BoldItalicCheckBox
            // 
            this.BoldItalicCheckBox.AutoSize = true;
            this.BoldItalicCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BoldItalicCheckBox.Location = new System.Drawing.Point(382, 16);
            this.BoldItalicCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.BoldItalicCheckBox.Name = "BoldItalicCheckBox";
            this.BoldItalicCheckBox.Size = new System.Drawing.Size(219, 20);
            this.BoldItalicCheckBox.TabIndex = 8;
            this.BoldItalicCheckBox.Text = "Check Bold and Italics Mismatch";
            this.BoldItalicCheckBox.UseVisualStyleBackColor = true;
            // 
            // conditionalCB
            // 
            this.conditionalCB.AutoSize = true;
            this.conditionalCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.conditionalCB.Location = new System.Drawing.Point(47, 50);
            this.conditionalCB.Margin = new System.Windows.Forms.Padding(4);
            this.conditionalCB.Name = "conditionalCB";
            this.conditionalCB.Size = new System.Drawing.Size(340, 20);
            this.conditionalCB.TabIndex = 7;
            this.conditionalCB.Text = "CHeck conditionAl erroRs (Also mismatch iN braces)";
            this.conditionalCB.UseVisualStyleBackColor = true;
            // 
            // HealCheckBox
            // 
            this.HealCheckBox.AutoSize = true;
            this.HealCheckBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.HealCheckBox.Location = new System.Drawing.Point(47, 22);
            this.HealCheckBox.Margin = new System.Windows.Forms.Padding(4);
            this.HealCheckBox.Name = "HealCheckBox";
            this.HealCheckBox.Size = new System.Drawing.Size(186, 20);
            this.HealCheckBox.TabIndex = 6;
            this.HealCheckBox.Text = "Check with Heal DataBase";
            this.HealCheckBox.UseVisualStyleBackColor = true;
            this.HealCheckBox.CheckedChanged += new System.EventHandler(this.HealCheckBox_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ascbBrowse);
            this.groupBox3.Controls.Add(this.healDbtb);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(13, 200);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(707, 63);
            this.groupBox3.TabIndex = 93;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Browse an Heal DataBase";
            this.groupBox3.EnabledChanged += new System.EventHandler(this.groupBox3_EnabledChanged);
            // 
            // ascbBrowse
            // 
            this.ascbBrowse.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ascbBrowse.Location = new System.Drawing.Point(542, 23);
            this.ascbBrowse.Margin = new System.Windows.Forms.Padding(4);
            this.ascbBrowse.Name = "ascbBrowse";
            this.ascbBrowse.Size = new System.Drawing.Size(149, 21);
            this.ascbBrowse.TabIndex = 1;
            this.ascbBrowse.Text = "Browse Heal Databse";
            this.ascbBrowse.UseVisualStyleBackColor = true;
            this.ascbBrowse.MouseLeave += new System.EventHandler(this.ascbBrowse_MouseLeave_1);
            this.ascbBrowse.Click += new System.EventHandler(this.ascbBrowse_Click);
            this.ascbBrowse.MouseEnter += new System.EventHandler(this.ascbBrowse_MouseEnter);
            // 
            // healDbtb
            // 
            this.healDbtb.Location = new System.Drawing.Point(48, 23);
            this.healDbtb.Margin = new System.Windows.Forms.Padding(4);
            this.healDbtb.Multiline = true;
            this.healDbtb.Name = "healDbtb";
            this.healDbtb.Size = new System.Drawing.Size(464, 21);
            this.healDbtb.TabIndex = 0;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.HealCheckBox);
            this.groupBox4.Controls.Add(this.IOCheckBox);
            this.groupBox4.Controls.Add(this.conditionalCB);
            this.groupBox4.Controls.Add(this.BoldItalicCheckBox);
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.Location = new System.Drawing.Point(13, 105);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(707, 88);
            this.groupBox4.TabIndex = 10;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Check";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatus,
            this.toolStripProgressBar1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 418);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(728, 22);
            this.statusStrip1.TabIndex = 97;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatus
            // 
            this.toolStripStatus.Name = "toolStripStatus";
            this.toolStripStatus.Size = new System.Drawing.Size(118, 17);
            this.toolStripStatus.Text = "toolStripStatusLabel1";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            // 
            // Menu_Strip
            // 
            this.Menu_Strip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.Menu_Strip.Location = new System.Drawing.Point(0, 0);
            this.Menu_Strip.Name = "Menu_Strip";
            this.Menu_Strip.Size = new System.Drawing.Size(728, 24);
            this.Menu_Strip.TabIndex = 98;
            this.Menu_Strip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveLogToolStripMenuItem,
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // saveLogToolStripMenuItem
            // 
            this.saveLogToolStripMenuItem.Name = "saveLogToolStripMenuItem";
            this.saveLogToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.saveLogToolStripMenuItem.Text = "&Save Log";
            this.saveLogToolStripMenuItem.Click += new System.EventHandler(this.saveLogToolStripMenuItem_Click);
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.quitToolStripMenuItem.Text = "&Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CRRAAnalysisReportGeneratorHelpToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // CRRAAnalysisReportGeneratorHelpToolStripMenuItem
            // 
            this.CRRAAnalysisReportGeneratorHelpToolStripMenuItem.Name = "CRRAAnalysisReportGeneratorHelpToolStripMenuItem";
            this.CRRAAnalysisReportGeneratorHelpToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.CRRAAnalysisReportGeneratorHelpToolStripMenuItem.Text = "&Error Detection Tool Help";
            this.CRRAAnalysisReportGeneratorHelpToolStripMenuItem.Click += new System.EventHandler(this.CRRAAnalysisReportGeneratorHelpToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(728, 440);
            this.Controls.Add(this.Menu_Strip);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.header_groupBox);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Errors Detection Tool";
            this.Load += new System.EventHandler(this.Form1_Load_1);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.header_groupBox.ResumeLayout(false);
            this.header_groupBox.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.Menu_Strip.ResumeLayout(false);
            this.Menu_Strip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button brwsbtn;
        private System.Windows.Forms.TextBox filepathtb;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox header_groupBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox ptag_cb;
        private System.Windows.Forms.Button generatebtn;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button ascbBrowse;
        private System.Windows.Forms.TextBox healDbtb;
        private System.Windows.Forms.CheckBox HealCheckBox;
        private System.Windows.Forms.CheckBox conditionalCB;
        private System.Windows.Forms.CheckBox IOCheckBox;
        private System.Windows.Forms.CheckBox BoldItalicCheckBox;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatus;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.MenuStrip Menu_Strip;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CRRAAnalysisReportGeneratorHelpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
    }
}

