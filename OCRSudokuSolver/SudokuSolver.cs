using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace OCRSudokuSolver
{

    public class SudokuSolver
    {
        private class CellFilling : IComparable<CellFilling>
        {
            private MyBitArray m_options;
            private int m_optionsCount;
            public CellFilling(Point coords)
            {
                Coords = coords;
                m_options = new MyBitArray(9);
                m_optionsCount = 9;
            }

                public CellFilling(Point coords, MyBitArray options)
                {
                    Coords = coords;
                    m_options = options;
                    m_optionsCount = options.GetFalseIndexes().Count;
                }

            public Point Coords { get; set; }

            public MyBitArray Options
            {
                get
                {
                    return m_options;
                }
                set
                {
                    m_options = value;
                    m_optionsCount = m_options.GetFalseIndexes().Count;
                }
            }

            public int OptionsCount { get { return m_optionsCount; } }


            public int CompareTo(CellFilling other)
            {
                return other.OptionsCount.CompareTo(OptionsCount); // Descending order
            }
        }


        private int[,] m_sudokuTable;
        private MyBitArray[] m_rowRules, m_columnRules, m_blockRules;
        private List<CellFilling> m_emptyCells;
        public SudokuSolver(int[,] array)
        {
            // since arrays are passed by reference
            m_sudokuTable = new int[9,9];
            Array.Copy(array, m_sudokuTable, array.Length);
            Debug.Assert(m_sudokuTable.GetLength(0) == 9);
            Debug.Assert(m_sudokuTable.GetLength(1) == 9);
            m_rowRules = new MyBitArray[9];
            m_columnRules = new MyBitArray[9];
            m_blockRules = new MyBitArray[9];
            for (int i = 0; i < 9; i++)
            {
                m_rowRules[i] = new MyBitArray(9);
                m_columnRules[i] = new MyBitArray(9);
                m_blockRules[i] = new MyBitArray(9);
            }
            m_emptyCells = new List<CellFilling>();
            LoadSudoku();
        }

        private void LoadSudoku()
        {
            for (int i = 0; i < m_sudokuTable.GetLength(0); i++)
            {
                for (int j = 0; j < m_sudokuTable.GetLength(1); j++)
                {
                    int cellValue = m_sudokuTable[i, j];
                    if (cellValue == 0)
                    {
                        m_emptyCells.Add(new CellFilling(new Point(i, j)));
                    }
                    else
                    {
                        Debug.Assert(cellValue > 0 && cellValue <= 9);
                        if (m_rowRules[i][cellValue - 1] || m_columnRules[j][cellValue - 1] ||
                            m_blockRules[GetBlockIndex(i, j)][cellValue - 1])
                        {
                            throw new FormatException(String.Format("Invalid sudoku entry in cell {0},{1}.", i+1, j+1));
                        }
                        m_rowRules[i][cellValue - 1] = true;
                        m_columnRules[j][cellValue - 1] = true;
                        m_blockRules[GetBlockIndex(i, j)][cellValue - 1] = true;
                    }
                }
            }
        }


        private int GetBlockIndex(Point coords) { return GetBlockIndex(coords.X, coords.Y); }

        private int GetBlockIndex(int row, int column)
        {
            int row3Index = row / 3;
            int column3Index = column / 3;
            return row3Index * 3 + column3Index;
        }

        public bool SolveSudoku(bool doEvents = false)
        {
            if (m_emptyCells.Count == 0)
            {
                return true;
            }
            foreach (CellFilling cell in m_emptyCells)
            {
                cell.Options = m_rowRules[cell.Coords.X] | m_columnRules[cell.Coords.Y] | m_blockRules[GetBlockIndex(cell.Coords)];
            }
            m_emptyCells.Sort(); // Descending order
            int last = m_emptyCells.Count - 1;
            var cellCandidate = m_emptyCells[last];
            var options = cellCandidate.Options.GetFalseIndexes();
            if (options.Count == 0)
                return false;
            for (int j = 0; j < options.Count; j++)
            {
                if (doEvents)
                    Application.DoEvents();
                m_emptyCells.RemoveAt(last);
                // fill the cell and update the rules
                m_sudokuTable[cellCandidate.Coords.X, cellCandidate.Coords.Y] = options[j] + 1;
                m_rowRules[cellCandidate.Coords.X][options[j]] = true;
                m_columnRules[cellCandidate.Coords.Y][options[j]] = true;
                m_blockRules[GetBlockIndex(cellCandidate.Coords)][options[j]] = true;
                if (SolveSudoku(doEvents))
                {
                    return true;
                }
                else
                {
                    // revert the changes
                    m_sudokuTable[cellCandidate.Coords.X, cellCandidate.Coords.Y] = 0;
                    m_rowRules[cellCandidate.Coords.X][options[j]] = false;
                    m_columnRules[cellCandidate.Coords.Y][options[j]] = false;
                    m_blockRules[GetBlockIndex(cellCandidate.Coords)][options[j]] = false;
                }
                m_emptyCells.Insert(last, cellCandidate);
            }
            return false;
        }

        public int[,] Sudoku { get { return m_sudokuTable; } }

    }

}
