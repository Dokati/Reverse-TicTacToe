namespace ReverseTicTacToe
{
    public class Board
    {
        private readonly int r_BoardSize;
        private readonly eIconType[,] r_Board;
        private int m_Capacity;

        public Board(int i_BoardSize)
        {
            r_BoardSize = i_BoardSize;
            r_Board = new eIconType[r_BoardSize, r_BoardSize];
            InitializeBoard();
        }

        public void InitializeBoard()
        {
            for (int i = 0; i < r_BoardSize; i++)
            {
                for (int j = 0; j < r_BoardSize; j++)
                {
                    r_Board[i, j] = eIconType.Empty;
                }
            }

            m_Capacity = r_BoardSize * r_BoardSize;
        }

        public int Capacity
        {
            get
            {
                return m_Capacity;
            }

            set
            {
                m_Capacity = value;
            }
        }

        public int Size
        {
            get
            {
                return r_BoardSize;
            }
        }

        public eIconType[,] GameBoard
        {
            get
            {
                return r_Board;
            }
        }
    }
}