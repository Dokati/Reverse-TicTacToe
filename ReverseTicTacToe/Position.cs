using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReverseTicTacToe
{
    public class Position
    {
        private int m_Row;
        private int m_Col;

        public Position(int i_Row = -1, int i_Col = -1)
        {
            m_Row = i_Row;
            m_Col = i_Col;
        }

        public void SetPosition(int i_Row, int i_Col)
        {
            m_Row = i_Row;
            m_Col = i_Col;
        }

        public int Row
        {
            get
            {
                return m_Row;
            }

            set
            {
                m_Row = value;
            }
        }

        public int Col
        {
            get
            {
                return m_Col;
            }

            set
            {
                m_Col = value;
            }
        }
    }
}
