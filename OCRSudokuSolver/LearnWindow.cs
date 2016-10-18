using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using OCRSudokuSolver.Properties;
using OCRSudokuSolver.NeuralNetworkNamespace;

namespace OCRSudokuSolver
{
    public partial class LearnWindow : Form
    {

        private LearnWindow()
        {
            InitializeComponent();
        }


        private readonly static List<Bitmap> DigitPatterns = new List<Bitmap>(9);

        private readonly List<OcrReader.MyLine> m_hLines;
        private readonly List<OcrReader.MyLine> m_vLines;
        private readonly Bitmap m_picture;
        public int[,] Result; 

        public LearnWindow(Bitmap bitmap, List<OcrReader.MyLine> hLines, List<OcrReader.MyLine> vLines, PictureBox pictBox = null)
        {
            InitializeComponent();
            m_hLines = hLines;
            m_vLines = vLines;
            m_picture = new Bitmap(bitmap);
            Result = new int[9,9];
            pictureBox1 = pictBox;
            if (pictureBox1 != null)
            {
                pictureBox1.Image = new Bitmap(m_picture);
            }
            LoadDigitPatterns();
        }

        public void DisconnectPictureBox()
        {
            pictureBox1.MouseLeave -= pictureBox1_MouseLeave;
            pictureBox1.MouseMove -= pictureBox1_MouseMove;
        }

        /// <summary>
        /// Loads digit patterns from resources into a list
        /// </summary>
        private static void LoadDigitPatterns()
        {
            if (DigitPatterns.Count == 0)
            {
                DigitPatterns.Add(OCRSudokuSolver.DigitPatterns._1);
                DigitPatterns.Add(OCRSudokuSolver.DigitPatterns._2);
                DigitPatterns.Add(OCRSudokuSolver.DigitPatterns._3);
                DigitPatterns.Add(OCRSudokuSolver.DigitPatterns._4);
                DigitPatterns.Add(OCRSudokuSolver.DigitPatterns._5);
                DigitPatterns.Add(OCRSudokuSolver.DigitPatterns._6);
                DigitPatterns.Add(OCRSudokuSolver.DigitPatterns._7);
                DigitPatterns.Add(OCRSudokuSolver.DigitPatterns._8);
                DigitPatterns.Add(OCRSudokuSolver.DigitPatterns._9);
            }
        }

        private void LearnWindow_Load(object sender, EventArgs e)
        {
        }

        Point m_lastCellCoords = new Point();
        Rectangle m_lastCellRectangle = new Rectangle();

        private Point PicBoxCoordsToBitmapCoords(Point originalPoint)
        {
            float wScale = (float)m_picture.Width / (float)pictureBox1.ClientSize.Width;
            float hScale = (float)m_picture.Height / (float)pictureBox1.ClientSize.Height;
            return new Point((int)(originalPoint.X * wScale), (int)(originalPoint.Y * hScale));
        }

        private Point BitmapCoordsToCellCoords(Point pt)
        {
            // first get the 3x3 block, then the actual cell
            // finding the correct block
            int blockY = -1, blockX = -1;
            if (pt.Y < m_hLines[3].StartPixel.Y)
                blockY = 0;
            else if (pt.Y < m_hLines[6].StartPixel.Y)
                blockY = 1;
            else if (pt.Y >= m_hLines[6].StartPixel.Y)
                blockY = 2;
            if (pt.X < m_vLines[3].StartPixel.X)
                blockX = 0;
            else if (pt.X < m_vLines[6].StartPixel.X)
                blockX = 1;
            else if (pt.X >= m_vLines[6].StartPixel.X)
                blockX = 2;
            // Check for the bounds
            Debug.Assert(blockX >= 0 && blockX <= 8 && blockY >= 0 && blockY <= 8);
            int cellY = -1, cellX = -1;
            // finding the correct cell
            if (pt.Y < m_hLines[3 * blockY + 1].StartPixel.Y)
                cellY = blockY * 3;
            else if (pt.Y < m_hLines[3 * blockY + 2].StartPixel.Y)
                cellY = blockY * 3 + 1;
            else if (pt.Y >= m_hLines[3 * blockY + 2].StartPixel.Y)
                cellY = blockY * 3 + 2;
            if (pt.X < m_vLines[3 * blockX + 1].StartPixel.X)
                cellX = blockX * 3;
            else if (pt.X < m_vLines[3 * blockX + 2].StartPixel.X)
                cellX = blockX * 3 + 1;
            else if (pt.X >= m_vLines[3 * blockX + 2].StartPixel.X)
                cellX = blockX * 3 + 2;
            // Check for the bounds
            Debug.Assert(cellX >= 0 && cellX <= 8 && cellY >= 0 && cellY <= 8);
            return new Point(cellX, cellY);
        }

        /// <summary>
        /// Method for creating a nice red highlighting rectangle above the cell under cursor 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                Point cellCoords = BitmapCoordsToCellCoords(PicBoxCoordsToBitmapCoords(e.Location));
                if (cellCoords == m_lastCellCoords)
                    return;
                var cellRectangle =
                    OcrReader.GetCellCoordinates(m_hLines, m_vLines, cellCoords.X, cellCoords.Y).ToRectangle();
                if (!m_lastCellRectangle.IsEmpty)
                {
                    var b = pictureBox1.Image as Bitmap;
                    for (int y = m_lastCellRectangle.Top; y < m_lastCellRectangle.Bottom; y++)
                    {
                        for (int x = m_lastCellRectangle.Left; x < m_lastCellRectangle.Right; x++)
                        {
                            Debug.Assert(b != null, "b != null");
                            b.SetPixel(x, y, m_picture.GetPixel(x, y));
                        }
                    }
                }
                m_lastCellRectangle = cellRectangle;
                m_lastCellCoords = cellCoords;
                Graphics g = Graphics.FromImage(pictureBox1.Image);
                g.FillRectangle(new SolidBrush(Color.FromArgb(50, 255, 0, 0)), cellRectangle);
                pictureBox1.Refresh();
            } catch {}
        }

        /// <summary>
        /// Method for debugging purposes only
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            // Test the coords conversion
            /*Point newCoords = PicBoxCoordsToBitmapCoords(e.Location);
            MessageBox.Show(String.Format("X:{0}\nY:{1}", newCoords.X, newCoords.Y));*/

            // Test the line offset
            /*Graphics g = Graphics.FromImage(pictureBox1.Image);
            for (int i = 0; i < m_hLines.Count; i++)
            {
                g.DrawLine(Pens.Red, m_hLines[i].StartPixel, m_hLines[i].EndPixel);
                g.DrawLine(Pens.Green, m_vLines[i].StartPixel, m_vLines[i].EndPixel);
            }
            pictureBox1.Refresh();*/

            // Test the coords to cell conversion
            /*Point newCoords = BitmapCoordsToCellCoords(PicBoxCoordsToBitmapCoords(e.Location));
            MessageBox.Show(String.Format("X:{0}\nY:{1}", newCoords.X, newCoords.Y));*/
            m_picture.Save(@"c:\Users\Luky\Documents\Visual Studio 2013\Projects\OCRSudokuSolver\OCRSudokuSolver\test_pictures\sudokuOnline_out.png");
            //FindFilledCells();
            RecognizeDigits();

        }

        /// <summary>
        /// Method for clearing a nice red highlighting rectangle above the cell under cursor 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                if (!m_lastCellRectangle.IsEmpty)
                {
                    var b = pictureBox1.Image as Bitmap;
                    for (int y = m_lastCellRectangle.Top; y < m_lastCellRectangle.Bottom; y++)
                    {
                        for (int x = m_lastCellRectangle.Left; x < m_lastCellRectangle.Right; x++)
                        {
                            Debug.Assert(b != null, "b != null");
                            b.SetPixel(x, y, m_picture.GetPixel(x, y));
                        }
                    }
                    m_lastCellCoords = new Point();
                    pictureBox1.Refresh();
                }
            }
            catch {}
        }

        //private void EvaluatePoints(List<Rectangle> filledCells, SetOfPointsWrapper points)
        //{
        //    Dictionary<MyBitArray, bool> listOfResults = new Dictionary<MyBitArray, bool>();
        //    Color penColor = m_picture.GetPixel(0, 0);
        //    for (int i = 0; i < filledCells.Count; i++)
        //    {
        //        MyBitArray result = new MyBitArray(points.SetOfPoints.Count);
        //        result.SetAll(false);
        //        for (int j = 0; j < points.SetOfPoints.Count; j++)
        //        {
        //            result[j] = m_picture.GetPixel(filledCells[i].Left + points.SetOfPoints[j].Y, filledCells[i].Top + points.SetOfPoints[j].X).Equals(penColor);
        //        }
        //        if (!listOfResults.ContainsKey(result))
        //            listOfResults.Add(result, false);
        //    }
        //    points.Rank = listOfResults.Count;
        //}

        //private SetOfPointsWrapper GenerateSetOfPoints(Random r, int count, int width, int heigth)
        //{
        //    SetOfPointsWrapper result = new SetOfPointsWrapper(count);
        //    for (int i = 0; i < count; i++)
        //    {
        //        result.SetOfPoints.Add(new Point(r.Next(0, heigth + 1), r.Next(0, width + 1)));
        //    }
        //    return result;
        //}

        //private void RecognizeDigits()
        //{
        //    Random r = new Random(123456);
        //    List<SetOfPointsWrapper> listOfSetsOfPoints = new List<SetOfPointsWrapper>(NUM_OF_SETS_OF_POINTS_FOR_RECOGNITION);
        //    var filledCells = FindFilledCells();
        //    for (int i = 0; i < NUM_OF_SETS_OF_POINTS_FOR_RECOGNITION; i++)
        //    {
        //        listOfSetsOfPoints.Add(GenerateSetOfPoints(r, NUM_OF_POINTS_FOR_RECOGNITION, filledCells[0].Width, filledCells[0].Height));
        //        EvaluatePoints(filledCells, listOfSetsOfPoints[i]);
        //    }
        //    listOfSetsOfPoints.Sort();
        //    while (listOfSetsOfPoints[0].Rank < 9)
        //    {
        //        // Take the best two sets and create a new one and reaplace the worst one with it
        //        SetOfPointsWrapper childSet = new SetOfPointsWrapper(NUM_OF_POINTS_FOR_RECOGNITION);
        //        for (int i = 0; i < NUM_OF_POINTS_FOR_RECOGNITION; i++)
        //        {
        //            int random = r.Next(0, MUTATION);
        //            if (random == 0) // mutate new point
        //            {
        //                childSet.SetOfPoints.Add(new Point(r.Next(0, filledCells[0].Height + 1), r.Next(0, filledCells[0].Width + 1)));
        //            }
        //            else
        //            {
        //                random = r.Next(0, 2); // 50:50 chance
        //                if (random == 0)
        //                    childSet.SetOfPoints.Add(listOfSetsOfPoints[0].SetOfPoints[i]);
        //                else
        //                    childSet.SetOfPoints.Add(listOfSetsOfPoints[1].SetOfPoints[i]);
        //            }
        //        }
        //        EvaluatePoints(filledCells, childSet);
        //        listOfSetsOfPoints.RemoveAt(listOfSetsOfPoints.Count-1);
        //        listOfSetsOfPoints.Add(childSet);
        //        listOfSetsOfPoints.Sort();
        //    }
        //    Debug.Assert(listOfSetsOfPoints[0].Rank == 9);
        //}


        /// <summary>
        /// Using appropriate method recognizes the digits in the cells
        /// </summary>
        /// <returns></returns>
        public int[,] RecognizeDigits()
        {
            int[,] table = new int[9,9];
            NeuralNetwork net = null;
            if (Properties.Settings.Default.Method == 1)
            {
                String neuralNetPath = Properties.Settings.Default.NeuralNetPath;
                bool deleteTemp = false;
                if (neuralNetPath == "Resources")
                {
                    neuralNetPath = System.IO.Path.GetTempFileName();
                    System.IO.File.WriteAllText(neuralNetPath, Resources.netInEpoch10_10111);
                    deleteTemp = true;
                }
                net = NeuralNetwork.FromFile(neuralNetPath);
                if (deleteTemp)
                {
                    System.IO.File.Delete(neuralNetPath);
                }
            }
            var filledCells = FindFilledCells();
            for (int i = 0; i < filledCells.Count; i++)
            {
                Application.DoEvents();
                Rectangle digitRectangle = LocateDigit(filledCells[i].Rect); // Get the borders of the digit
                // Print it into a bitmap
                Rectangle destRectangle = new Rectangle(new Point(DIGIT_PATTERN_MARGIN_LEFT, DIGIT_PATTERN_MARGIN_TOP), new Size(DIGIT_PATTERN_WIDTH-DIGIT_PATTERN_MARGIN_LEFT-DIGIT_PATTERN_MARGIN_RIGHT, DIGIT_PATTERN_HEIGHT - DIGIT_PATTERN_MARGIN_TOP - DIGIT_PATTERN_MARGIN_BOTTOM));
                // Print scaled onto a bitmap with white margins
                Bitmap marginedBitmap = new Bitmap(DIGIT_PATTERN_WIDTH, DIGIT_PATTERN_HEIGHT);
                Graphics g = Graphics.FromImage(marginedBitmap);
                g.DrawImage(m_picture, destRectangle, digitRectangle, GraphicsUnit.Pixel);

                //marginedBitmap.Save(@"c:\Users\Luky\Documents\Visual Studio 2013\Projects\OCRSudokuSolver\OCRSudokuSolver\test_pictures\s.bmp");
                if (Properties.Settings.Default.Method == 1 && net != null)
                {
                    MyVector output = net.FeedForward(BitmapToInputVector(marginedBitmap));
                    int recognizedDigit = NeuralNetwork.MaxArg(output);
                    Debug.Print("Recognized {0} at pos {1},{2}", recognizedDigit, filledCells[i].Y, filledCells[i].X);
                    table[filledCells[i].Y, filledCells[i].X] = recognizedDigit;
                }
                else // Digit Pattern method
                {
                    List<Tuple<int, double>> resultList = new List<Tuple<int, double>>(8);
                    for (int j = 0; j < DigitPatterns.Count; j++)
                    {
                        Application.DoEvents();
                        double correctCounter = 0;
                        for (int x = 0; x < marginedBitmap.Height; x++)
                        {
                            for (int y = 0; y < marginedBitmap.Width; y++)
                            {
                                if (marginedBitmap.GetPixel(y, x).Equals(DigitPatterns[j].GetPixel(y, x)) &&
                                    !marginedBitmap.GetPixel(y, x).Equals(Color.White))
                                    correctCounter++;
                            }
                        }
                        resultList.Add(new Tuple<int, double>(j,
                            correctCounter/(DIGIT_PATTERN_HEIGHT*DIGIT_PATTERN_WIDTH)));
                    }
                    resultList.Sort(CompareTuples);
                    Debug.Print("Recognized {0} at pos {1},{2}", resultList[0].Item1 + 1, filledCells[i].Y, filledCells[i].X);
                    table[filledCells[i].Y, filledCells[i].X] = resultList[0].Item1 + 1;
                }
            }
            if (pictureBox1 != null)
            {
                pictureBox1.MouseMove += pictureBox1_MouseMove;
                pictureBox1.MouseLeave += pictureBox1_MouseLeave;
            }
            return table;
        }

        public static MyVector BitmapToInputVector(Bitmap bitmap)
        {
            MyVector input = new MyVector(DIGIT_PATTERN_WIDTH * DIGIT_PATTERN_HEIGHT);
            for (int i = 0; i < DIGIT_PATTERN_WIDTH; i++)
            {
                for (int j = 0; j < DIGIT_PATTERN_HEIGHT; j++)
                {
                    input[i * DIGIT_PATTERN_WIDTH + j] = (1 - GetBrightness(bitmap.GetPixel(i, j)));
                }
            }
            return input;
        }

        public static double GetBrightness(Color c)
        {
            return (0.2126 * c.R + 0.7152 * c.G + 0.0722 * c.B) / 255d;
        }

        /// <summary>
        /// Method for sorting
        /// </summary>
        /// <param name="item1"></param>
        /// <param name="item2"></param>
        /// <returns></returns>
        private int CompareTuples(Tuple<int, double> item1, Tuple<int, double> item2)
        {
            return item2.Item2.CompareTo(item1.Item2);
        }

        /// <summary>
        /// Returns the smallest possible rectangle that contains the whole digit
        /// </summary>
        /// <param name="cell"></param>
        /// <returns></returns>
        private Rectangle LocateDigit(Rectangle cell)
        {
            int left = cell.Right, right = cell.Left, top = cell.Bottom, bottom = cell.Top;
            Color penColor = m_picture.GetPixel(0, 0);
            for (int y = cell.Top; y < cell.Bottom; y++)
            {
                for (int x = cell.Left; x < cell.Right; x++)
                {
                    if (m_picture.GetPixel(x, y).Equals(penColor))
                    {
                        left = Math.Min(left, x);
                        right = Math.Max(right, x);
                        top = Math.Min(y, top);
                        bottom = Math.Max(y, bottom);
                    }
                }
            }
            return new Rectangle(left, top, right - left + 1, bottom - top + 1);
        }

        /// <summary>
        /// Returns a list of filled cells.
        /// Lists through every cell and if at least 1% is non-white, the cell is considered to be non-empty.
        /// </summary>
        /// <returns></returns>
        private List<MyRectangle> FindFilledCells()
        {
            Color penColor = m_picture.GetPixel(0, 0); // Get color of pen using which the numbers are written
            List<MyRectangle> result = new List<MyRectangle>();
            for (int x = 0; x < 9; x++)
            {
                for (int y = 0; y < 9; y++)
                {
                    Rectangle cell = OcrReader.GetCellCoordinates(m_hLines, m_vLines, x, y).ToRectangle();
                    int numOfPixelsFilled = 0;
                    for (int i = cell.Left; i < cell.Right; i++)
                    {
                        for (int j = cell.Top; j < cell.Bottom; j++)
                        {
                            if (m_picture.GetPixel(i, j).Equals(penColor))
                                numOfPixelsFilled++;
                        }
                    }
                    // If at least 1% is filled
                    if (numOfPixelsFilled > NUM_OF_PERCENT_FILLED * (cell.Width * cell.Height))
                    {
                        // Test of correctness 
                        /*Graphics g = Graphics.FromImage(pictureBox1.Image);
                        g.DrawRectangle(new Pen(Color.Red, 3), cell);
                        pictureBox1.Refresh();
                        Application.DoEvents();*/
                        result.Add(new MyRectangle(cell, x, y));
                    }
                }
            }
            //pictureBox1.Refresh();
            return result;
        }

    }

    public class MyRectangle
    {
        public MyRectangle(Rectangle rect, int x, int y)
        {
            Rect = rect;
            X = x;
            Y = y;
        }

        public Rectangle Rect
        {
            get;
            set;
        }

        public int X { get; set; }
        public int Y { get; set; }
    }
}
