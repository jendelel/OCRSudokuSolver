using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using OCRSudokuSolver.NeuralNetworkNamespace;

namespace OCRSudokuSolver
{
    public partial class Settings : Form
    {
        private object m_cancelLock = new object();
        private bool m_cancel;

        public Settings()
        {
            InitializeComponent();
        }

        private void Settings_Load(object sender, EventArgs e)
        {
            numBrightness.Value = Properties.Settings.Default.White;
            if (Properties.Settings.Default.Method == 0)
            {
                rbDigitPatterns.Checked = true;
            }
            else
            {
                rbNeuralNet.Checked = true;
                if (Properties.Settings.Default.NeuralNetPath == "Resources")
                {
                    cbNeuralNet.SelectedIndex = 0;
                }
                else
                {
                    cbNeuralNet.SelectedIndex = 1;
                    txtNeuralNetPath.Text = Properties.Settings.Default.NeuralNetPath;
                }
            }
        }

        private void butInputFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Select folder with images";
            fbd.ShowNewFolderButton = false;
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                txtInputFolder.Text = fbd.SelectedPath;
            }
        }

        private void butOutputFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            fbd.Description = "Select output folder";
            fbd.ShowNewFolderButton = true;
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                txtOutputFolder.Text = fbd.SelectedPath;
            }
        }

        private void rb_CheckedChanged(object sender, EventArgs e)
        {
            if (rbNeuralNet.Checked)
            {
                cbNeuralNet.Enabled = true;
                if (cbNeuralNet.SelectedIndex == -1)
                    cbNeuralNet.SelectedIndex = 0;
                cbNeuralNet_SelectedIndexChanged(this, new EventArgs());
            }
            else
            {
                cbNeuralNet.Enabled = false;
                butNeuralNet.Enabled = false;
                txtNeuralNetPath.Enabled = false;
            }
        }

        private void cbNeuralNet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbNeuralNet.SelectedIndex == 0)
            {
                butNeuralNet.Enabled = false;
                txtNeuralNetPath.Enabled = false;
            }
            else
            {
                txtNeuralNetPath.Enabled = true;
                butNeuralNet.Enabled = true;
            }
        }

        private void butNeuralNet_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                Multiselect = false,
                Filter = "Neural network file *.xml|*.xml",
                InitialDirectory = Environment.CurrentDirectory,
                CheckFileExists = true
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtNeuralNetPath.Text = ofd.FileName;
            }
        }

        private void butRestore_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.Reset();
            Settings_Load(this, new EventArgs());
            txtInputFolder.Text = "";
            txtOutputFolder.Text = "";
            numBatchSize.Value = 20;
            numHidNeurons.Value = 30;
            numLambda.Value = 2.0m;
            numLearningRate.Value = 0.30m;
            numOfEpochs.Value = 30;
            txtLog.Text = "";
        }

        private void PrintLine(string value)
        {
            txtLog.Text = value + Environment.NewLine + txtLog.Text;
            txtLog.ScrollToCaret();
        }

        private void butTeach_Click(object sender, EventArgs e)
        {
            if (txtInputFolder.Text == "" || txtOutputFolder.Text == "" || numLearningRate.Value == 0)
            {
                MessageBox.Show("Incorrect parameter(s)", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int[] sizes = new int[] { LearnWindow.DIGIT_PATTERN_WIDTH * LearnWindow.DIGIT_PATTERN_HEIGHT, (int)numHidNeurons.Value, 10 };
            lock (m_cancelLock)
            {
                m_cancel = false;
            }
            PrintLine("Loading images...");
            progressBar1.Show();
            butTeach.Enabled = false;
            butOk.Enabled = false;
            butCancel.Enabled = false;
            butStop.Enabled = true;
            progressBar1.Minimum = 0;
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.Value = 0;
            NeuralNetwork net = new NeuralNetwork(sizes);
            var data = new List<Tuple<MyVector, MyVector>>();
            DirectoryInfo dir = new DirectoryInfo(txtInputFolder.Text);
            //DirectoryInfo dir = new DirectoryInfo(@"c:\Users\Luky\Downloads\t10k-images");
            List<FileInfo> files = new List<FileInfo>(dir.GetFiles("*.png", SearchOption.AllDirectories));
            files.AddRange(dir.GetFiles("*.jpg", SearchOption.AllDirectories));
            files.AddRange(dir.GetFiles("*.bmp", SearchOption.AllDirectories));
            files.AddRange(dir.GetFiles("*.jpeg", SearchOption.AllDirectories));
            progressBar1.Maximum = files.Count;
            foreach (var file in files)
            {
                using (Image temp = Image.FromFile(file.FullName))
                {
                    var bitmap = temp as Bitmap;
                    if (bitmap != null)
                    {
                        if (IsCancelled())
                            return;
                        if (bitmap.Width != LearnWindow.DIGIT_PATTERN_WIDTH ||
                            bitmap.Height != LearnWindow.DIGIT_PATTERN_HEIGHT)
                        {
                            continue;
                        }
                        MyVector input = LearnWindow.BitmapToInputVector(bitmap);
                        int output = int.Parse(file.Name.Substring(0, 1));
                        data.Add(new Tuple<MyVector, MyVector>(input, MyVector.UnitVector(10, output)));
                        Debug.Print("Picture num: {0}/{1} loaded.", data.Count, files.Count);
                        progressBar1.Value++;
                        //PrintLine(String.Format("Picture num: {0}/{1} loaded.", data.Count, files.Count)); // Very slow!!! TODO: Better loging console
                    }
                }
            }
            PrintLine("Network learning started...");
            progressBar1.Style = ProgressBarStyle.Marquee;
            net.StochasticGradientDescent(data.ToArray(), (int)numOfEpochs.Value, (int)numBatchSize.Value, (double)numLearningRate.Value, IsCancelled, PrintLine, txtOutputFolder.Text, (double)numLambda.Value, null, false, false, false, true);
            progressBar1.Hide();
            butTeach.Enabled = true;
            butStop.Enabled = false;
            butOk.Enabled = true;
            butCancel.Enabled = true;
        }

        private bool IsCancelled()
        {
            Application.DoEvents();
            bool cancelCopy;
            lock (m_cancelLock)
            {
                cancelCopy = m_cancel;
            }
            return cancelCopy;
        }

        private void butStop_Click(object sender, EventArgs e)
        {
            lock (m_cancelLock)
            {
                m_cancel = true;
            }
            PrintLine("Learning canceled.");
            butTeach.Enabled = true;
            butStop.Enabled = false;
            butOk.Enabled = true;
            butCancel.Enabled = true;
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void butOk_Click(object sender, EventArgs e)
        {
            if (rbNeuralNet.Checked)
            {
                Properties.Settings.Default.Method = 1;
                if (cbNeuralNet.SelectedIndex == 0)
                {
                    Properties.Settings.Default.NeuralNetPath = "Resources";
                }
                else
                {
                    Properties.Settings.Default.NeuralNetPath = txtNeuralNetPath.Text;
                }
            }
            else
            {
                Properties.Settings.Default.Method = 0;
            }
            Properties.Settings.Default.White = numBrightness.Value;
            Properties.Settings.Default.Save();
            Close();
        }

    }
}
