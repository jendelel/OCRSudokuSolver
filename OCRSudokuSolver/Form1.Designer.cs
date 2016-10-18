namespace OCRSudokuSolver
{
    partial class MainWindow
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.butNext = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.butBack = new System.Windows.Forms.Button();
            this.butSettings = new System.Windows.Forms.Button();
            this.wizardHost = new System.Windows.Forms.TabControl();
            this.welcomePage = new System.Windows.Forms.TabPage();
            this.lblIntro = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.lblFromTable = new System.Windows.Forms.Label();
            this.lblCheckOCR = new System.Windows.Forms.Label();
            this.pictBox = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.rbFromTable = new System.Windows.Forms.RadioButton();
            this.rbFromFile = new System.Windows.Forms.RadioButton();
            this.butBrowse = new System.Windows.Forms.Button();
            this.dataTable1 = new System.Windows.Forms.DataGridView();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.wizardHost.SuspendLayout();
            this.welcomePage.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).BeginInit();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // butNext
            // 
            this.butNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butNext.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.butNext.ImageKey = "icon_Next.png";
            this.butNext.ImageList = this.imageList1;
            this.butNext.Location = new System.Drawing.Point(521, 6);
            this.butNext.Name = "butNext";
            this.butNext.Size = new System.Drawing.Size(84, 26);
            this.butNext.TabIndex = 0;
            this.butNext.Text = "Next";
            this.butNext.UseVisualStyleBackColor = true;
            this.butNext.Click += new System.EventHandler(this.butNext_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "icon_Settings.png");
            this.imageList1.Images.SetKeyName(1, "icon_Next.png");
            this.imageList1.Images.SetKeyName(2, "icon_OpenFolder.png");
            this.imageList1.Images.SetKeyName(3, "icon_Open.png");
            this.imageList1.Images.SetKeyName(4, "icon_Home.png");
            this.imageList1.Images.SetKeyName(5, "icon_Back.png");
            this.imageList1.Images.SetKeyName(6, "icon_Edit.png");
            this.imageList1.Images.SetKeyName(7, "icon_Cancel.png");
            this.imageList1.Images.SetKeyName(8, "icon_Teach.png");
            this.imageList1.Images.SetKeyName(9, "icon_Sudoku.png");
            // 
            // butBack
            // 
            this.butBack.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butBack.Enabled = false;
            this.butBack.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butBack.ImageKey = "icon_Back.png";
            this.butBack.ImageList = this.imageList1;
            this.butBack.Location = new System.Drawing.Point(431, 6);
            this.butBack.Name = "butBack";
            this.butBack.Size = new System.Drawing.Size(84, 26);
            this.butBack.TabIndex = 2;
            this.butBack.Text = "Back";
            this.butBack.UseVisualStyleBackColor = true;
            this.butBack.Click += new System.EventHandler(this.butBack_Click);
            // 
            // butSettings
            // 
            this.butSettings.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butSettings.ImageKey = "icon_Settings.png";
            this.butSettings.ImageList = this.imageList1;
            this.butSettings.Location = new System.Drawing.Point(4, 6);
            this.butSettings.Name = "butSettings";
            this.butSettings.Size = new System.Drawing.Size(84, 26);
            this.butSettings.TabIndex = 3;
            this.butSettings.Text = "Settings";
            this.butSettings.UseVisualStyleBackColor = true;
            this.butSettings.Click += new System.EventHandler(this.butSettings_Click);
            // 
            // wizardHost
            // 
            this.wizardHost.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wizardHost.Controls.Add(this.welcomePage);
            this.wizardHost.Controls.Add(this.tabPage2);
            this.wizardHost.Controls.Add(this.tabPage1);
            this.wizardHost.ImageList = this.imageList1;
            this.wizardHost.Location = new System.Drawing.Point(0, 1);
            this.wizardHost.Name = "wizardHost";
            this.wizardHost.SelectedIndex = 0;
            this.wizardHost.Size = new System.Drawing.Size(609, 367);
            this.wizardHost.TabIndex = 4;
            this.wizardHost.Selected += new System.Windows.Forms.TabControlEventHandler(this.wizardHost_Selected);
            // 
            // welcomePage
            // 
            this.welcomePage.Controls.Add(this.lblIntro);
            this.welcomePage.ImageKey = "icon_Home.png";
            this.welcomePage.Location = new System.Drawing.Point(4, 23);
            this.welcomePage.Name = "welcomePage";
            this.welcomePage.Padding = new System.Windows.Forms.Padding(3);
            this.welcomePage.Size = new System.Drawing.Size(601, 340);
            this.welcomePage.TabIndex = 0;
            this.welcomePage.Text = "Welcome page";
            this.welcomePage.UseVisualStyleBackColor = true;
            // 
            // lblIntro
            // 
            this.lblIntro.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblIntro.AutoSize = true;
            this.lblIntro.Location = new System.Drawing.Point(28, 37);
            this.lblIntro.Name = "lblIntro";
            this.lblIntro.Size = new System.Drawing.Size(548, 130);
            this.lblIntro.TabIndex = 0;
            this.lblIntro.Text = resources.GetString("lblIntro.Text");
            this.lblIntro.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.lblFromTable);
            this.tabPage2.Controls.Add(this.lblCheckOCR);
            this.tabPage2.Controls.Add(this.pictBox);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.rbFromTable);
            this.tabPage2.Controls.Add(this.rbFromFile);
            this.tabPage2.Controls.Add(this.butBrowse);
            this.tabPage2.Controls.Add(this.dataTable1);
            this.tabPage2.ImageKey = "icon_Edit.png";
            this.tabPage2.Location = new System.Drawing.Point(4, 23);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(601, 340);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Open&&Edit";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // lblFromTable
            // 
            this.lblFromTable.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFromTable.AutoSize = true;
            this.lblFromTable.Location = new System.Drawing.Point(9, 74);
            this.lblFromTable.Name = "lblFromTable";
            this.lblFromTable.Size = new System.Drawing.Size(172, 13);
            this.lblFromTable.TabIndex = 7;
            this.lblFromTable.Text = "Please copy digits into proper cells.";
            this.lblFromTable.Visible = false;
            // 
            // lblCheckOCR
            // 
            this.lblCheckOCR.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCheckOCR.AutoSize = true;
            this.lblCheckOCR.Location = new System.Drawing.Point(8, 74);
            this.lblCheckOCR.Name = "lblCheckOCR";
            this.lblCheckOCR.Size = new System.Drawing.Size(281, 26);
            this.lblCheckOCR.TabIndex = 6;
            this.lblCheckOCR.Text = "Please check that all digits has been recognized correctly.\r\nIf you notice an err" +
    "or, please correct it.";
            this.lblCheckOCR.Visible = false;
            // 
            // pictBox
            // 
            this.pictBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictBox.Location = new System.Drawing.Point(321, 111);
            this.pictBox.Name = "pictBox";
            this.pictBox.Size = new System.Drawing.Size(280, 223);
            this.pictBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictBox.TabIndex = 5;
            this.pictBox.TabStop = false;
            this.pictBox.Visible = false;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(251, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Please select a method of entering sudoku problem.";
            // 
            // rbFromTable
            // 
            this.rbFromTable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbFromTable.AutoSize = true;
            this.rbFromTable.Location = new System.Drawing.Point(497, 10);
            this.rbFromTable.Name = "rbFromTable";
            this.rbFromTable.Size = new System.Drawing.Size(77, 17);
            this.rbFromTable.TabIndex = 3;
            this.rbFromTable.Text = "From table:";
            this.rbFromTable.UseVisualStyleBackColor = true;
            this.rbFromTable.CheckedChanged += new System.EventHandler(this.rbFrom_CheckedChanged);
            // 
            // rbFromFile
            // 
            this.rbFromFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.rbFromFile.AutoSize = true;
            this.rbFromFile.Checked = true;
            this.rbFromFile.Location = new System.Drawing.Point(306, 10);
            this.rbFromFile.Name = "rbFromFile";
            this.rbFromFile.Size = new System.Drawing.Size(98, 17);
            this.rbFromFile.TabIndex = 2;
            this.rbFromFile.TabStop = true;
            this.rbFromFile.Text = "From image file:";
            this.rbFromFile.UseVisualStyleBackColor = true;
            // 
            // butBrowse
            // 
            this.butBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.butBrowse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butBrowse.ImageKey = "icon_Open.png";
            this.butBrowse.ImageList = this.imageList1;
            this.butBrowse.Location = new System.Drawing.Point(407, 6);
            this.butBrowse.Name = "butBrowse";
            this.butBrowse.Size = new System.Drawing.Size(84, 26);
            this.butBrowse.TabIndex = 1;
            this.butBrowse.Text = "Browse";
            this.butBrowse.UseVisualStyleBackColor = true;
            this.butBrowse.Click += new System.EventHandler(this.butBrowse_Click);
            // 
            // dataTable1
            // 
            this.dataTable1.AllowUserToAddRows = false;
            this.dataTable1.AllowUserToDeleteRows = false;
            this.dataTable1.AllowUserToResizeColumns = false;
            this.dataTable1.AllowUserToResizeRows = false;
            this.dataTable1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.dataTable1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataTable1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dataTable1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataTable1.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dataTable1.Location = new System.Drawing.Point(6, 111);
            this.dataTable1.MultiSelect = false;
            this.dataTable1.Name = "dataTable1";
            this.dataTable1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Sunken;
            this.dataTable1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataTable1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataTable1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataTable1.Size = new System.Drawing.Size(284, 223);
            this.dataTable1.TabIndex = 0;
            this.dataTable1.Visible = false;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.ImageKey = "icon_Teach.png";
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(601, 340);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Solve";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(137, 40);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(342, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Sudoku has been successfully solved. You can see the solution below.";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeColumns = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(165, 88);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dataGridView1.Size = new System.Drawing.Size(284, 217);
            this.dataGridView1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.butSettings);
            this.panel1.Controls.Add(this.butBack);
            this.panel1.Controls.Add(this.butNext);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 368);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(609, 38);
            this.panel1.TabIndex = 5;
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(94, 8);
            this.progressBar1.MarqueeAnimationSpeed = 30;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(331, 24);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 4;
            this.progressBar1.Visible = false;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(609, 406);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.wizardHost);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(700, 500);
            this.MinimumSize = new System.Drawing.Size(625, 444);
            this.Name = "MainWindow";
            this.Text = "OCRSudokuSolver - Jendele";
            this.wizardHost.ResumeLayout(false);
            this.welcomePage.ResumeLayout(false);
            this.welcomePage.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataTable1)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button butNext;
        private System.Windows.Forms.Button butBack;
        private System.Windows.Forms.Button butSettings;
        private System.Windows.Forms.TabControl wizardHost;
        private System.Windows.Forms.TabPage welcomePage;
        private System.Windows.Forms.Label lblIntro;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbFromTable;
        private System.Windows.Forms.RadioButton rbFromFile;
        private System.Windows.Forms.Button butBrowse;
        private System.Windows.Forms.DataGridView dataTable1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictBox;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblCheckOCR;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label lblFromTable;

    }
}

