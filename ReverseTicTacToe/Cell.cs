using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReverseTicTacToe
{
    public class Cell
    {
        public enum eIconType
        {
            X = 'X',
            O = 'O',
            Empty = ' ',
        }

        Cell.eIconType cellState =  Cell.eIconType.Empty;

        public Cell()
        {
            cellState =  Cell.eIconType.Empty;
        }
        public Cell.eIconType CellState
        {
            get 
            {
                return cellState; 
            }
            set
            {
                if(isEmpty())
                {
                    cellState = value;
                }
            }
        }
        public bool isEmpty()
        {
            if (cellState !=  Cell.eIconType.Empty)
                return false;
            return true;
        }

        //public void markField(Player player)
        //{
        //    if (isEmpty())
        //    {
        //        if (player.getSign() == 'X')
        //            cellState =  Cell.eIconType.FLD_X;
        //        else
        //            cellState =  Cell.eIconType.FLD_O;
        //    }
        //}

    }
}


