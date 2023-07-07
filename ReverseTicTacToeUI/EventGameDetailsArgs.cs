using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReverseTicTacToe;

namespace ReverseTicTacToeUI
{
    public class EventGameDetailsArgs
    {
        private readonly string r_PlayerXName;
        private readonly string r_PlayerOName;
        private readonly int r_BoardSize;
        private readonly ePlayerType r_PlayerOType;

        public EventGameDetailsArgs(string i_PlayerXName, string i_PlayerOName, int i_BoardSize, ePlayerType i_PlayerOType)
        {
            r_PlayerXName = i_PlayerXName;
            r_PlayerOName = i_PlayerOName;
            r_BoardSize = i_BoardSize;
            r_PlayerOType = i_PlayerOType;
        }

        public int BoardSize
        {
            get
            {
                return r_BoardSize;
            }
        }

        public string PlayerOName
        {
            get
            {
                return r_PlayerOName;
            }
        }

        public string PlayerXName
        {
            get
            {
                return r_PlayerXName;
            }
        }

        public ePlayerType PlayerOType
        {
            get
            {
                return r_PlayerOType;
            }
        }
    }
}
