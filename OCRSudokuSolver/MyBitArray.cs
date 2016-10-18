using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace OCRSudokuSolver
{
    class MyBitArray : IComparable<MyBitArray>
    {
        private readonly int m_size;
        private uint m_value;
        public MyBitArray(int size)
        {
            m_size = size;
            m_value = 0;
        }

        public MyBitArray(uint value, int size)
        {
            m_value = value;
            m_size = size;
        }

        public void SetAll(bool value)
        {
            m_value = value == false ? 0u : ~(0u);
        }

        public int Count { get { return m_size; } }

        public bool this[int index]
        {
            get { return ((m_value >> index) % 2 == 1); }
            set
            {
                uint operand = (uint)(1 << index);
                if (value == false)
                {
                    m_value = m_value & (~operand);
                }
                else
                {
                    m_value = m_value | (operand);
                }
            }
        }

        public void Xor(MyBitArray a)
        {
            m_value = m_value ^ a.m_value;
        }

        public void Or(MyBitArray a)
        {
            m_value = m_value | a.m_value;
        }

        public void And(MyBitArray a)
        {
            m_value = m_value & a.m_value;
        }

        public static MyBitArray Or(MyBitArray a1, MyBitArray a2)
        {
            Debug.Assert(a1.m_size == a2.m_size);
            return new MyBitArray(a1.m_value | a2.m_value, a1.m_size);
        }

        public static MyBitArray operator |(MyBitArray a1, MyBitArray a2)
        {
            return MyBitArray.Or(a1, a2);
        }

        public static MyBitArray Xor(MyBitArray a1, MyBitArray a2)
        {
            Debug.Assert(a1.m_size == a2.m_size);
            return new MyBitArray(a1.m_value ^ a2.m_value, a1.m_size);
        }

        public static MyBitArray operator ^(MyBitArray a1, MyBitArray a2)
        {
            return MyBitArray.Xor(a1, a2);
        }

        public static MyBitArray And(MyBitArray a1, MyBitArray a2)
        {
            Debug.Assert(a1.m_size == a2.m_size);
            return new MyBitArray(a1.m_value & a2.m_value, a1.m_size);
        }

        public static MyBitArray operator &(MyBitArray a1, MyBitArray a2)
        {
            return MyBitArray.And(a1, a2);
        }

        public List<int> GetTrueIndexes()
        {
            List<int> result = new List<int>(m_size);
            uint temp = m_value;
            int counter = 0;
            while (temp != 0 && counter < m_size)
            {
                if (temp % 2 == 1)
                {
                    result.Add(counter);
                }
                counter++;
                temp = temp >> 1;
            }
            return result;
        }

        public List<int> GetFalseIndexes()
        {
            List<int> result = new List<int>(m_size);
            uint temp = m_value;
            int counter = 0;
            while (counter < m_size)
            {
                if (temp % 2 == 0)
                {
                    result.Add(counter);
                }
                counter++;
                temp = temp >> 1;
            }
            return result;
        }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            for (int i = 0; i < m_size; i++)
            {
                if (i % 3 == 0)
                    s.Append(" ");
                s.Append(this[i] ? 1 : 0);
            }
            return String.Format("{0}:{1}", m_value, s.ToString());
        }

        public int CompareTo(MyBitArray other)
        {
            return m_value.CompareTo(other.m_value);
        }

        public override bool Equals(object obj)
        {
            if (obj is MyBitArray)
            {
                return m_value.Equals(((MyBitArray)obj).m_value);
            }
            return base.Equals(obj);
        }
    }
}
