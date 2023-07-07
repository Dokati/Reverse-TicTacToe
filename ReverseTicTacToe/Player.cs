using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReverseTicTacToe
{
    public class Player
    {
        private readonly string r_PlayerName;
        private readonly ePlayerType r_PlayerType;
        private readonly eIconType r_PlayerIcon;
        private int m_PlayerScore;

        public Player(ePlayerType i_PlayerType, string i_PlayerName, eIconType i_PlayerIcon)
        {
            r_PlayerType = i_PlayerType;
            r_PlayerName = i_PlayerName;
            r_PlayerIcon = i_PlayerIcon;
            m_PlayerScore = 0;
        }

        public eIconType PlayerIcon 
        { 
            get
            { 
                return r_PlayerIcon; 
            }
        }

        public int PlayerScore
        {
            get
            {
                return m_PlayerScore;
            }

            set
            {
                 m_PlayerScore = value;
            }
        }

        public string PlayerName
        {
            get
            {
                return r_PlayerName;
            }
        }

        public ePlayerType PlayerType
        {
            get
            {
                return r_PlayerType;
            }
        }

        public static bool IsValidName(string i_PlayerName)
        {
            bool validName = true;
            char charToCheck;

            if(i_PlayerName == string.Empty)
            {
                validName = false;
            }

            for (int i = 0; i < i_PlayerName.Length && validName; i++)
            {
                charToCheck = i_PlayerName[i];
                if (charToCheck == ' ')
                {
                    validName = false;
                }
            }

            return validName;
        }
    }
}
