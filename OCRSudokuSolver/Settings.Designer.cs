namespace OCRSudokuSolver
{
    partial class Settings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numBrightness = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.txtNeuralNetPath = new System.Windows.Forms.TextBox();
            this.butNeuralNet = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.cbNeuralNet = new System.Windows.Forms.ComboBox();
            this.rbNeuralNet = new System.Windows.Forms.RadioButton();
            this.rbDigitPatterns = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.butStop = new System.Windows.Forms.Button();
            this.txtInputFolder = new System.Windows.Forms.TextBox();
            this.butInputFolder = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.numHidNeurons = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.numBatchSize = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.numOfEpochs = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.numLambda = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.numLearningRate = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.butTeach = new System.Windows.Forms.Button();
            this.txtOutputFolder = new System.Windows.Forms.TextBox();
            this.butOutputFolder = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.butCancel = new System.Windows.Forms.Button();
            this.butOk = new System.Windows.Forms.Button();
            this.butRestore = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBrightness)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHidNeurons)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBatchSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOfEpochs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLambda)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLearningRate)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numBrightness);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtNeuralNetPath);
            this.groupBox1.Controls.Add(this.butNeuralNet);
            this.groupBox1.Controls.Add(this.cbNeuralNet);
            this.groupBox1.Controls.Add(this.rbNeuralNet);
            this.groupBox1.Controls.Add(this.rbDigitPatterns);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(460, 106);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "OCR method:";
            // 
            // numBrightness
            // 
            this.numBrightness.DecimalPlaces = 2;
            this.numBrightness.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numBrightness.Location = new System.Drawing.Point(410, 42);
            this.numBrightness.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numBrightness.Name = "numBrightness";
            this.numBrightness.Size = new System.Drawing.Size(44, 20);
            this.numBrightness.TabIndex = 6;
            this.numBrightness.Value = new decimal(new int[] {
            6,
            0,
            0,
            65536});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(344, 16);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(94, 26);
            this.label8.TabIndex = 5;
            this.label8.Text = "Brightness to be \r\nregarded as white:";
            // 
            // txtNeuralNetPath
            // 
            this.txtNeuralNetPath.Location = new System.Drawing.Point(19, 70);
            this.txtNeuralNetPath.Name = "txtNeuralNetPath";
            this.txtNeuralNetPath.ReadOnly = true;
            this.txtNeuralNetPath.Size = new System.Drawing.Size(222, 20);
            this.txtNeuralNetPath.TabIndex = 3;
            // 
            // butNeuralNet
            // 
            this.butNeuralNet.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butNeuralNet.ImageKey = "icon_Open.png";
            this.butNeuralNet.ImageList = this.imageList1;
            this.butNeuralNet.Location = new System.Drawing.Point(247, 68);
            this.butNeuralNet.Name = "butNeuralNet";
            this.butNeuralNet.Size = new System.Drawing.Size(91, 23);
            this.butNeuralNet.TabIndex = 4;
            this.butNeuralNet.Text = "Browse";
            this.butNeuralNet.UseVisualStyleBackColor = true;
            this.butNeuralNet.Click += new System.EventHandler(this.butNeuralNet_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "icon_Back.png");
            this.imageList1.Images.SetKeyName(1, "icon_Cancel.png");
            this.imageList1.Images.SetKeyName(2, "icon_Edit.png");
            this.imageList1.Images.SetKeyName(3, "icon_Home.png");
            this.imageList1.Images.SetKeyName(4, "icon_Next.png");
            this.imageList1.Images.SetKeyName(5, "icon_Open.png");
            this.imageList1.Images.SetKeyName(6, "icon_OpenFolder.png");
            this.imageList1.Images.SetKeyName(7, "icon_Settings.png");
            this.imageList1.Images.SetKeyName(8, "icon_Teach.png");
            this.imageList1.Images.SetKeyName(9, "icon_OK.png");
            this.imageList1.Images.SetKeyName(10, "icon_Close.png");
            this.imageList1.Images.SetKeyName(11, "icon_Restore.png");
            // 
            // cbNeuralNet
            // 
            this.cbNeuralNet.FormattingEnabled = true;
            this.cbNeuralNet.Items.AddRange(new object[] {
            "Default neural network",
            "Neural network from file"});
            this.cbNeuralNet.Location = new System.Drawing.Point(127, 41);
            this.cbNeuralNet.Name = "cbNeuralNet";
            this.cbNeuralNet.Size = new System.Drawing.Size(211, 21);
            this.cbNeuralNet.TabIndex = 2;
            this.cbNeuralNet.SelectedIndexChanged += new System.EventHandler(this.cbNeuralNet_SelectedIndexChanged);
            // 
            // rbNeuralNet
            // 
            this.rbNeuralNet.AutoSize = true;
            this.rbNeuralNet.Checked = true;
            this.rbNeuralNet.Location = new System.Drawing.Point(19, 42);
            this.rbNeuralNet.Name = "rbNeuralNet";
            this.rbNeuralNet.Size = new System.Drawing.Size(102, 17);
            this.rbNeuralNet.TabIndex = 1;
            this.rbNeuralNet.TabStop = true;
            this.rbNeuralNet.Text = "Neural Network:";
            this.rbNeuralNet.UseVisualStyleBackColor = true;
            // 
            // rbDigitPatterns
            // 
            this.rbDigitPatterns.AutoSize = true;
            this.rbDigitPatterns.Location = new System.Drawing.Point(19, 19);
            this.rbDigitPatterns.Name = "rbDigitPatterns";
            this.rbDigitPatterns.Size = new System.Drawing.Size(91, 17);
            this.rbDigitPatterns.TabIndex = 0;
            this.rbDigitPatterns.Text = "Digit Patterns:";
            this.rbDigitPatterns.UseVisualStyleBackColor = true;
            this.rbDigitPatterns.CheckedChanged += new System.EventHandler(this.rb_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.butStop);
            this.groupBox2.Controls.Add(this.txtInputFolder);
            this.groupBox2.Controls.Add(this.butInputFolder);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.numHidNeurons);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.numBatchSize);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.numOfEpochs);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.numLambda);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.numLearningRate);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.butTeach);
            this.groupBox2.Controls.Add(this.txtOutputFolder);
            this.groupBox2.Controls.Add(this.butOutputFolder);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtLog);
            this.groupBox2.Location = new System.Drawing.Point(12, 133);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(460, 242);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Teach neural network:";
            // 
            // butStop
            // 
            this.butStop.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butStop.ImageKey = "icon_Cancel.png";
            this.butStop.ImageList = this.imageList1;
            this.butStop.Location = new System.Drawing.Point(363, 155);
            this.butStop.Name = "butStop";
            this.butStop.Size = new System.Drawing.Size(91, 23);
            this.butStop.TabIndex = 17;
            this.butStop.Text = "Stop";
            this.butStop.UseVisualStyleBackColor = true;
            this.butStop.Click += new System.EventHandler(this.butStop_Click);
            // 
            // txtInputFolder
            // 
            this.txtInputFolder.Location = new System.Drawing.Point(83, 19);
            this.txtInputFolder.Name = "txtInputFolder";
            this.txtInputFolder.ReadOnly = true;
            this.txtInputFolder.Size = new System.Drawing.Size(274, 20);
            this.txtInputFolder.TabIndex = 1;
            // 
            // butInputFolder
            // 
            this.butInputFolder.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butInputFolder.ImageKey = "icon_OpenFolder.png";
            this.butInputFolder.ImageList = this.imageList1;
            this.butInputFolder.Location = new System.Drawing.Point(363, 17);
            this.butInputFolder.Name = "butInputFolder";
            this.butInputFolder.Size = new System.Drawing.Size(91, 23);
            this.butInputFolder.TabIndex = 2;
            this.butInputFolder.Text = "Browse";
            this.butInputFolder.UseVisualStyleBackColor = true;
            this.butInputFolder.Click += new System.EventHandler(this.butInputFolder_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 22);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(63, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Input folder:";
            // 
            // numHidNeurons
            // 
            this.numHidNeurons.Location = new System.Drawing.Point(410, 73);
            this.numHidNeurons.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numHidNeurons.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numHidNeurons.Name = "numHidNeurons";
            this.numHidNeurons.Size = new System.Drawing.Size(44, 20);
            this.numHidNeurons.TabIndex = 11;
            this.numHidNeurons.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(297, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Num of hid. neurons:";
            // 
            // numBatchSize
            // 
            this.numBatchSize.Location = new System.Drawing.Point(109, 102);
            this.numBatchSize.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numBatchSize.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numBatchSize.Name = "numBatchSize";
            this.numBatchSize.Size = new System.Drawing.Size(44, 20);
            this.numBatchSize.TabIndex = 13;
            this.numBatchSize.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Mini batch size:";
            // 
            // numOfEpochs
            // 
            this.numOfEpochs.Location = new System.Drawing.Point(109, 73);
            this.numOfEpochs.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numOfEpochs.Name = "numOfEpochs";
            this.numOfEpochs.Size = new System.Drawing.Size(44, 20);
            this.numOfEpochs.TabIndex = 7;
            this.numOfEpochs.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 75);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "Number of epochs:";
            // 
            // numLambda
            // 
            this.numLambda.DecimalPlaces = 1;
            this.numLambda.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.numLambda.Location = new System.Drawing.Point(247, 102);
            this.numLambda.Name = "numLambda";
            this.numLambda.Size = new System.Drawing.Size(44, 20);
            this.numLambda.TabIndex = 15;
            this.numLambda.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(170, 104);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 14;
            this.label3.Text = "Lambda: ";
            // 
            // numLearningRate
            // 
            this.numLearningRate.DecimalPlaces = 2;
            this.numLearningRate.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.numLearningRate.Location = new System.Drawing.Point(247, 73);
            this.numLearningRate.Name = "numLearningRate";
            this.numLearningRate.Size = new System.Drawing.Size(44, 20);
            this.numLearningRate.TabIndex = 9;
            this.numLearningRate.Value = new decimal(new int[] {
            3,
            0,
            0,
            65536});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(169, 75);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Learning rate:";
            // 
            // butTeach
            // 
            this.butTeach.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butTeach.ImageKey = "icon_Teach.png";
            this.butTeach.ImageList = this.imageList1;
            this.butTeach.Location = new System.Drawing.Point(363, 126);
            this.butTeach.Name = "butTeach";
            this.butTeach.Size = new System.Drawing.Size(91, 23);
            this.butTeach.TabIndex = 16;
            this.butTeach.Text = "Start!";
            this.butTeach.UseVisualStyleBackColor = true;
            this.butTeach.Click += new System.EventHandler(this.butTeach_Click);
            // 
            // txtOutputFolder
            // 
            this.txtOutputFolder.Location = new System.Drawing.Point(83, 46);
            this.txtOutputFolder.Name = "txtOutputFolder";
            this.txtOutputFolder.ReadOnly = true;
            this.txtOutputFolder.Size = new System.Drawing.Size(274, 20);
            this.txtOutputFolder.TabIndex = 4;
            // 
            // butOutputFolder
            // 
            this.butOutputFolder.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butOutputFolder.ImageKey = "icon_OpenFolder.png";
            this.butOutputFolder.ImageList = this.imageList1;
            this.butOutputFolder.Location = new System.Drawing.Point(363, 44);
            this.butOutputFolder.Name = "butOutputFolder";
            this.butOutputFolder.Size = new System.Drawing.Size(91, 23);
            this.butOutputFolder.TabIndex = 5;
            this.butOutputFolder.Text = "Browse";
            this.butOutputFolder.UseVisualStyleBackColor = true;
            this.butOutputFolder.Click += new System.EventHandler(this.butOutputFolder_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 49);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Output folder:";
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(6, 128);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtLog.Size = new System.Drawing.Size(351, 107);
            this.txtLog.TabIndex = 0;
            // 
            // butCancel
            // 
            this.butCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butCancel.ImageKey = "icon_Close.png";
            this.butCancel.ImageList = this.imageList1;
            this.butCancel.Location = new System.Drawing.Point(375, 381);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(91, 23);
            this.butCancel.TabIndex = 3;
            this.butCancel.Text = "Cancel";
            this.butCancel.UseVisualStyleBackColor = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // butOk
            // 
            this.butOk.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butOk.ImageKey = "icon_OK.png";
            this.butOk.ImageList = this.imageList1;
            this.butOk.Location = new System.Drawing.Point(278, 381);
            this.butOk.Name = "butOk";
            this.butOk.Size = new System.Drawing.Size(91, 23);
            this.butOk.TabIndex = 2;
            this.butOk.Text = "OK";
            this.butOk.UseVisualStyleBackColor = true;
            this.butOk.Click += new System.EventHandler(this.butOk_Click);
            // 
            // butRestore
            // 
            this.butRestore.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.butRestore.ImageKey = "icon_Restore.png";
            this.butRestore.ImageList = this.imageList1;
            this.butRestore.Location = new System.Drawing.Point(12, 381);
            this.butRestore.Name = "butRestore";
            this.butRestore.Size = new System.Drawing.Size(121, 23);
            this.butRestore.TabIndex = 4;
            this.butRestore.Text = "Restore defaults";
            this.butRestore.UseVisualStyleBackColor = true;
            this.butRestore.Click += new System.EventHandler(this.butRestore_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(139, 381);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(133, 23);
            this.progressBar1.TabIndex = 5;
            this.progressBar1.Visible = false;
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 407);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.butRestore);
            this.Controls.Add(this.butOk);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(497, 445);
            this.MinimumSize = new System.Drawing.Size(497, 445);
            this.Name = "Settings";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.Settings_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numBrightness)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numHidNeurons)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numBatchSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numOfEpochs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLambda)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numLearningRate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button butNeuralNet;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ComboBox cbNeuralNet;
        private System.Windows.Forms.RadioButton rbNeuralNet;
        private System.Windows.Forms.RadioButton rbDigitPatterns;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.NumericUpDown numLearningRate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button butTeach;
        private System.Windows.Forms.TextBox txtOutputFolder;
        private System.Windows.Forms.Button butOutputFolder;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numLambda;
        private System.Windows.Forms.NumericUpDown numBatchSize;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numOfEpochs;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtInputFolder;
        private System.Windows.Forms.Button butInputFolder;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown numHidNeurons;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button butCancel;
        private System.Windows.Forms.Button butOk;
        private System.Windows.Forms.TextBox txtNeuralNetPath;
        private System.Windows.Forms.Button butStop;
        private System.Windows.Forms.NumericUpDown numBrightness;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button butRestore;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}