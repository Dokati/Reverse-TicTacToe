using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using ReverseTicTacToe;

namespace ReverseTicTacToeUI
{
    public class PictureBoxCell : PictureBox
    {
        private Position m_PositionOnBoard;

        public PictureBoxCell(int i_RowPosition, int i_ColPosition)
        { 
            m_PositionOnBoard = new Position(i_RowPosition, i_ColPosition);
        }

        public void initializePictureBoxCell(int i_PictureBoxCellSize, int i_XLocation, int i_YLocation)
        {
            string fullFilePath = Path.Combine(Resources.ResourcesFolderPath, Resources.CellBackground);
            this.AutoSize = false;
            this.Size = new Size(i_PictureBoxCellSize, i_PictureBoxCellSize);
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Image = Image.FromFile(fullFilePath);
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.Location = new Point(i_XLocation, i_YLocation);
        }

        public Position PositionOnBoard
        {
            get
            {
                return m_PositionOnBoard;
            }
        }

        public int Row
        {
            get
            {
                return m_PositionOnBoard.Row;
            }
        }

        public int Col
        {
            get
            {
                return m_PositionOnBoard.Col;
            }
        }
    }
}
