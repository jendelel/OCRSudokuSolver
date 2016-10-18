using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace OCRSudokuSolver
{
    public static partial class OcrReader
    {

        public class MyLine
        {
            public MyLine(Point startPixel, Point endPixel)
            {
                StartPixel = startPixel;
                EndPixel = endPixel;
                Thickness = 1;
            }

            public int Thickness { get; set; }
            public Point StartPixel { get; set; }
            public Point EndPixel { get; set; }
            public int LineLength { get { return (int)Math.Sqrt((EndPixel.X - StartPixel.X) ^ 2 + (EndPixel.Y - StartPixel.Y) ^ 2); } }
            public override string ToString()
            {
                return String.Format("{0}-{1}, Length: {2}, Thickness: {3}", StartPixel, EndPixel, LineLength, Thickness);
            }
        }

        public class CellCoordinates
        {
            public CellCoordinates(Point topLeft, Point bottomRight)
            {
                TopLeft = topLeft;
                BottomRight = bottomRight;
            }
            public Point TopLeft { get; set; }
            public Point BottomRight { get; set; }

            public Rectangle ToRectangle()
            {
                return new Rectangle(TopLeft, new Size(BottomRight.X - TopLeft.X, BottomRight.Y - TopLeft.Y));
            }
        }

        //Private static members
        private static LearnWindow m_learnWindow  = null;

        private static void DisconnectPictureBox()
        {
            m_learnWindow.DisconnectPictureBox();
        }

        public static int[,] ParsePicture(string path, PictureBox pictBox = null)
        {
            int[,] ret = null;
            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }
            Image img = Image.FromFile(path);

            Bitmap bitmap = img as Bitmap;
            if (bitmap != null)
            {
                List<MyLine> hLines = new List<MyLine>();
                List<MyLine> vLines = new List<MyLine>();
                LoadPicture(ref bitmap, hLines, vLines);
                if (m_learnWindow != null)
                {
                    DisconnectPictureBox();
                }
                m_learnWindow = new LearnWindow(bitmap, hLines, vLines, pictBox);
                //m_learnWindow.ShowDialog();
                ret = m_learnWindow.RecognizeDigits();

                bitmap.Dispose();
            }
            return ret;
        }

        public static CellCoordinates GetCellCoordinates(List<MyLine> hLines, List<MyLine> vLines, int x, int y)
        {
            Debug.Assert(x >= 0 && x <= 8 && y >= 0 && y <= 8);
            return new CellCoordinates(new Point(vLines[x].StartPixel.X + vLines[x].Thickness, hLines[y].StartPixel.Y + hLines[y].Thickness), new Point(vLines[x + 1].StartPixel.X - 1, hLines[y + 1].StartPixel.Y - 1));

        }

        private static void LoadPicture(ref Bitmap bitmap, List<MyLine> hLines, List<MyLine> vLines)
        {

            Bitmap pic = bitmap;
            MakeBitmapBlackAndWhite(pic);
            // find the outline

            // Horizontal Lines
            GetHorizontalLines(pic, hLines);
            GetVerticalLines(pic, vLines);
            FilterHorizontalLines(hLines);
            FilterVerticalLines(vLines);
            Debug.Assert(vLines.Count == 10);
            Debug.Assert(hLines.Count == 10);

            Point topLeft = hLines[0].StartPixel;
            Point temp = hLines[hLines.Count - 1].EndPixel;
            Point bottomRight = new Point(temp.X, temp.Y + hLines[hLines.Count - 1].Thickness - 1);
            var croppedBitmap = new Bitmap(bottomRight.X - topLeft.X, bottomRight.Y - topLeft.Y);
            for (int x = 0; x < croppedBitmap.Width; x++)
            {
                Application.DoEvents();
                for (int y = 0; y < croppedBitmap.Height; y++)
                {
                    croppedBitmap.SetPixel(x, y, bitmap.GetPixel(topLeft.X + x, topLeft.Y + y));
                }
            }
            bitmap.Dispose();
            bitmap = croppedBitmap;

            // Adjust the lines' coordinates to the cropped picture
            for (int i = 0; i < hLines.Count; i++)
            {
                Application.DoEvents();
                var newStart = new Point(hLines[i].StartPixel.X - topLeft.X, hLines[i].StartPixel.Y - topLeft.Y);
                var newEnd = new Point(hLines[i].EndPixel.X - topLeft.X, hLines[i].EndPixel.Y - topLeft.Y);
                hLines[i].StartPixel = newStart;
                hLines[i].EndPixel = newEnd;
                newStart = new Point(vLines[i].StartPixel.X - topLeft.X, vLines[i].StartPixel.Y - topLeft.Y);
                newEnd = new Point(vLines[i].EndPixel.X - topLeft.X, vLines[i].EndPixel.Y - topLeft.Y);
                vLines[i].StartPixel = newStart;
                vLines[i].EndPixel = newEnd;
            }
        }

        private static void MakeBitmapBlackAndWhite(Bitmap pic)
        {
            for (int y = 0; y < pic.Height; y++)
            {
                for (int x = 0; x < pic.Width; x++)
                {
                    if (pic.GetPixel(x, y).GetBrightness() > (float)Properties.Settings.Default.White)
                        pic.SetPixel(x,y, Color.White);
                    else
                        pic.SetPixel(x, y, Color.Black);
                }
            }
        }


        /// <summary>
        /// Filters reduntant lines for the list and sets the correct thinkness
        /// </summary>
        /// <param name="hLines"></param>
        private static void FilterHorizontalLines(List<MyLine> hLines)
        {
            Point lastStartPixel = new Point(), lastEndPixel = new Point();
            for (int i = hLines.Count - 1; i >= 0; i--)
            {
                Application.DoEvents();
                Debug.Assert(hLines[i].StartPixel.Y == hLines[i].EndPixel.Y, "Not a horizontal line");
                if (i == hLines.Count - 1)
                {
                    lastStartPixel = hLines[i].StartPixel;
                    lastEndPixel = hLines[i].EndPixel;
                }
                else
                {
                    if (lastStartPixel.X == hLines[i].StartPixel.X && lastEndPixel.X == hLines[i].EndPixel.X && lastStartPixel.Y == hLines[i].StartPixel.Y + 1)
                    {
                        hLines[i].Thickness = hLines[i + 1].Thickness + 1;
                        hLines.RemoveAt(i + 1);
                    }
                    lastStartPixel = hLines[i].StartPixel;
                    lastEndPixel = hLines[i].EndPixel;
                }
            }
        }

        /// <summary>
        /// Filters reduntant lines for the list and sets the correct thinkness
        /// </summary>
        /// <param name="vLines"></param>
        private static void FilterVerticalLines(List<MyLine> vLines)
        {
            Point lastStartPixel = new Point(), lastEndPixel = new Point();
            for (int i = vLines.Count - 1; i >= 0; i--)
            {
                Application.DoEvents();
                Debug.Assert(vLines[i].StartPixel.X == vLines[i].EndPixel.X, "Not a vertical line");
                if (i == vLines.Count - 1)
                {
                    lastStartPixel = vLines[i].StartPixel;
                    lastEndPixel = vLines[i].EndPixel;
                }
                else
                {
                    if (lastStartPixel.Y == vLines[i].StartPixel.Y && lastEndPixel.Y == vLines[i].EndPixel.Y && lastStartPixel.X == vLines[i].StartPixel.X + 1)
                    {
                        vLines[i].Thickness = vLines[i + 1].Thickness + 1;
                        vLines.RemoveAt(i + 1);
                    }
                    lastStartPixel = vLines[i].StartPixel;
                    lastEndPixel = vLines[i].EndPixel;
                }
            }
        }

        private static void GetHorizontalLines(Bitmap pic, List<MyLine> hLines)
        {
            for (int y = 0; y < pic.Height; y++)
            {
                Color lastColor = Color.White;
                int lineLength = 0;
                int startPos = -1;
                int maxStart = -1, maxEnd = -1, maxLength = -1;
                for (int x = 0; x < pic.Width; x++)
                {
                    Application.DoEvents();
                    Color pixColor = pic.GetPixel(x, y);
                    if (pixColor == lastColor)
                    {
                        lineLength++;
                    }
                    if (!(lastColor.R == pixColor.R && lastColor.B == pixColor.B && lastColor.G == pixColor.G) || x == pic.Width - 1)
                    {
                        if (!(lastColor.Equals(Color.White)) && lineLength > maxLength)
                        {
                            maxStart = startPos;
                            maxEnd = x - 1;
                            maxLength = maxEnd - maxStart;
                        }
                        lastColor = pixColor;
                        startPos = x;
                        lineLength = 1;
                    }
                }
                if (maxStart >= 0 && maxEnd > 0 && maxLength > NUM_OF_PERCENT_OF_WIDTH_TO_BE_LINE * pic.Width)
                {
                    hLines.Add(new MyLine(new Point(maxStart, y), new Point(maxEnd, y)));
                }
            }
        }

        private static void GetVerticalLines(Bitmap pic, List<MyLine> vLines)
        {
            for (int x = 0; x < pic.Width; x++)
            {
                Color lastColor = Color.White;
                int lineLength = 0;
                int startPos = -1;
                int maxStart = -1, maxEnd = -1, maxLength = -1;
                for (int y = 0; y < pic.Height; y++)
                {
                    Application.DoEvents();
                    Color pixColor = pic.GetPixel(x, y);
                    if (pixColor == lastColor)
                    {
                        lineLength++;
                    }
                    if (!(lastColor.R == pixColor.R && lastColor.B == pixColor.B && lastColor.G == pixColor.G) || y == pic.Height - 1)
                    {
                        if (!(lastColor.Equals(Color.White)) && lineLength > maxLength)
                        {
                            maxStart = startPos;
                            maxEnd = y - 1;
                            maxLength = maxEnd - maxStart;
                        }
                        lastColor = pixColor;
                        startPos = y;
                        lineLength = 1;
                    }
                }
                if (maxStart >= 0 && maxEnd > 0 && maxLength > NUM_OF_PERCENT_OF_WIDTH_TO_BE_LINE * pic.Height)
                {
                    vLines.Add(new MyLine(new Point(x, maxStart), new Point(x, maxEnd)));
                }
            }
        }

    }


}
