using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using OCRSudokuSolver.NeuralNetworkNamespace;

namespace OCRSudokuSolver
{
    public partial class MainWindow : Form
    {

        public MainWindow()
        {
            InitializeComponent();
            lblIntro.Text = lblIntro.Text.Replace("{version}", Application.ProductVersion);
        }

        private void butNext_Click(object sender, EventArgs e)
        {
            //pictureBox1_Click_test(sender, e);
            //return;
            if (progressBar1.Visible) // Something is in progress
            {
                return;
            }

            if (wizardHost.SelectedIndex + 1 < wizardHost.TabCount)
            {
                wizardHost.SelectTab(wizardHost.SelectedIndex + 1);
            }
            else if (wizardHost.SelectedIndex + 1 == wizardHost.TabCount)
            {
                wizardHost.SelectTab(0);
            }
        }

        private void butBack_Click(object sender, EventArgs e)
        {
            if (progressBar1.Visible) // Something is in progress
            {
                return;
            }

            if (wizardHost.SelectedIndex > 0)
            {
                wizardHost.SelectTab(wizardHost.SelectedIndex - 1);
            }
        }

        private void wizardHost_Selected(object sender, TabControlEventArgs e)
        {
            switch (e.TabPageIndex)
            {
                case 0: // Welcome page
                    butNext.Text = "Next";
                    butBack.Enabled = false;
                    lblCheckOCR.Hide();
                    break;
                case 1: // Open&Edit
                    butNext.Text = "Solve";
                    butBack.Enabled = true;
                    break;
                case 2: // Solve
                    if (Solve())
                    {
                        butNext.Text = "Start over";
                        butBack.Enabled = true;
                    }
                    else
                    {
                        wizardHost.SelectTab(1);
                    }
                    break;
            }
        }

        private static int[,] DataTableToIntArray(DataGridView table, out bool empty)
        {
            int[,] array = new int[9, 9];
            empty = true;
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Application.DoEvents();
                    String cellContent = table.Rows[i].Cells[j].Value == null ? "" : table.Rows[i].Cells[j].Value.ToString();
                    Regex regex = new Regex("^[123456789]$|^$");
                    if (!regex.IsMatch(cellContent))
                    {
                        throw new ArgumentException("Cells must contain a digit 1-9 or nothing. \n" +
                                                    String.Format("Error occured at coordintates {0},{1}", i+1, j+1));
                        return null;
                    }
                    if (cellContent == "")
                        array[i, j] = 0;
                    else
                    {
                        array[i, j] = int.Parse(cellContent);
                        empty = false;
                    }
                }
            }
            return array;
        }

        private static void IntArrayToDataTable(int[,] array, DataGridView table, int[,] highlightedCells = null)
        {
            Font regularFont = table.Font;
            Font boldFont = new Font(regularFont.FontFamily, regularFont.Size, FontStyle.Bold);
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    //Application.DoEvents();
                    if (!(array[i, j] >= 0 && array[i, j] <= 9))
                    {
                        throw new ArgumentException("Cells must contain a digit 1-9 or nothing. \n" +
                                                    String.Format("Error occured at coordintates {0},{1}", i+1, j+1));
                    }
                    if (array[i, j] == 0)
                    {
                        table.Rows[i].Cells[j].Value = "";
                    }
                    else
                    {
                        table.Rows[i].Cells[j].Value = array[i, j];
                    }
                    if (highlightedCells != null && highlightedCells[i, j] != 0)
                    {
                        table.Rows[i].Cells[j].Style.Font = boldFont;
                    }
                    else
                    {
                        table.Rows[i].Cells[j].Style.Font = regularFont;
                    }
                }
            }
        }

        /// <summary>
        /// Function that checks the input data and calls the SudokuSolver
        /// Returns true if solving suceeded
        /// </summary>
        /// <returns></returns>
        private bool Solve()
        {
            progressBar1.Show();
            Application.DoEvents();
            try
            {
                bool empty;
                if (dataTable1.RowCount == 0)
                {
                MessageBox.Show(
                            "Please enter sudoku first or load it from file.",
                            "No data to solve", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    progressBar1.Hide();
                    return false;
                }

                // Load data from table
                int[,] table = DataTableToIntArray(dataTable1, out empty);
                if (empty)
                {
                MessageBox.Show(
                        "Please enter sudoku first or load it from file.",
                        "No data to solve", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    progressBar1.Hide();
                    return false;
                }
                // Solve the sudoku problem
                SudokuSolver solver = new SudokuSolver(table);
                bool solveable = solver.SolveSudoku(true);
                if (solveable)
                {
                    // Show the result in a table
                    InitDataTable(dataGridView1);
                    IntArrayToDataTable(solver.Sudoku, dataGridView1, table);
                    progressBar1.Hide();
                    return true;
                }
                else
                {
                    MessageBox.Show(
                        "Unfortunatelly, the sudoku you have entered cannot be solved. \nPlease make sure that the table sudoku you entered is correct.",
                        "Unsolvable sudoku", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    progressBar1.Hide();
                    return false;
                }
            }
            catch (FormatException fe)
            {
                MessageBox.Show(fe.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                progressBar1.Hide();
                return false;
            }
            catch (ArgumentException fe)
            {
                MessageBox.Show(fe.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                progressBar1.Hide();
                return false;
            }
        }

        private static void InitDataTable(DataGridView dataTable)
        {
            dataTable.Rows.Clear();
            dataTable.Columns.Clear();
            dataTable.Text = "Sudoku";
            for (int i = 0; i < 9; i++)
            {
                dataTable.Columns.Add(i.ToString(), (i+1).ToString());
            }
            dataTable.Rows.Add(9);

            for (int i = 0; i < 9; i++)
            {
                dataTable.Rows[i].HeaderCell.Value = (i + 1).ToString();
                dataTable.Columns[i].Width = (dataTable.ClientSize.Width - 60) / 9;
                for (int j = 0; j < 9; j++)
                {
                    dataTable.Rows[i].Cells[j].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                    if (((j / 3 == 0 || j / 3 == 2) && (i / 3 == 1)) || ((i / 3 == 0 || i / 3 == 2) && (j / 3 == 1)))
                    {
                        dataTable.Rows[i].Cells[j].Style.BackColor = Color.Aquamarine;
                    }
                }
            }
        }

        private void butBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog
            {
                CheckFileExists = true,
                Multiselect = false,
                InitialDirectory = Environment.CurrentDirectory,
                Filter = "Image files (*.png, *.jpg, *.bmp, *.jpeg)|*.png;*.jpg;*.bmp;*.jpeg"
            };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    progressBar1.Show();
                    pictBox.Image = Image.FromFile(ofd.FileName);
                    pictBox.Show();
                    Application.DoEvents();
                    int[,] table = OcrReader.ParsePicture(ofd.FileName, pictBox);
                    InitDataTable(dataTable1);
                    dataTable1.Show();
                    IntArrayToDataTable(table, dataTable1);
                    progressBar1.Hide();
                    lblCheckOCR.Show();
                }
                catch (FileNotFoundException)
                {
                    progressBar1.Hide();
                    dataTable1.Hide();
                    pictBox.Hide();
                    MessageBox.Show(
                        "The file with neural network does not exist.",
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch
                {
                    progressBar1.Hide();
                    dataTable1.Hide();
                    pictBox.Hide();
                    String msg = "An error while parsing image occured. \nPlease make sure that the image is correct. \nCropping image might also help.";
                    if (Properties.Settings.Default.Method == 1 && Properties.Settings.Default.NeuralNetPath != "Resources")
                    {
                        msg = "An error while parsing image occured. \nPlease make sure that the image and the neural network file is correct. \nCropping image might also help.";
                    }
                    MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void rbFrom_CheckedChanged(object sender, EventArgs e)
        {
            if (rbFromFile.Checked)
            {
                butBrowse.Enabled = true;
                InitDataTable(dataTable1);
                dataTable1.Hide();
                lblFromTable.Hide();
                lblCheckOCR.Hide();
                pictBox.Hide();
            }
            else
            {
                butBrowse.Enabled = false;
                pictBox.Hide();
                lblFromTable.Show();
                InitDataTable(dataTable1);
                lblCheckOCR.Hide();
                dataTable1.Show();
                dataTable1.Select();
            }
        }

        private void butSettings_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings();
            settings.ShowDialog();
        }

    }
}
