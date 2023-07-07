using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace ReverseTicTacToe
{
    public class Game
    {
        private Player m_PlayerX;
        private Player m_PlayerO;
        private Player m_CurrentPlayer;
        private Board m_Board;
        private Random m_RandomNumber;

        public event Action<Player> WinAnnouncement;

        public event Action TieAnnouncement;

        public event Action<eIconType> ChangePlayer;

        public event Action<Position> BoardUpdate;

        public Game()
        {
            m_PlayerX = null;
            m_PlayerO = null;
            m_CurrentPlayer = null;
            m_Board = null;
            m_RandomNumber = null;
        }

        public Board Board
        {
            get
            {
                return m_Board;
            }
        }

        public Player CurrentPlayer
        {
            get
            {
                return m_CurrentPlayer;
            }
        }

        public Player PlayerX
        {
            get
            {
                return m_PlayerX;
            }
        }

        public Player PlayerO
        {
            get
            {
                return m_PlayerO;
            }
        }

        public void InitializeGameDetails(string i_PlayerXName, string i_PlayerOName, ePlayerType i_PlayerOType, int i_BoardSize)
        {
            m_PlayerX = new Player(ePlayerType.Human, i_PlayerXName, eIconType.X);
            m_PlayerO = new Player(i_PlayerOType, i_PlayerOName, eIconType.O);
            m_Board = new Board(i_BoardSize);
            m_RandomNumber = new Random();
        }

        public void ResetGameSession()
        {
            m_Board.InitializeBoard();
            m_CurrentPlayer = m_PlayerX;
        }

        private bool checkIfRowSequenceExist(Position i_Position)
        {
            bool result = true;

            for (int i = 0; i < m_Board.Size; i++)
            {
                if (m_Board.GameBoard[i_Position.Row, i] != m_Board.GameBoard[i_Position.Row, i_Position.Col])
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        private bool checkIfColumnSequenceExist(Position i_Position)
        {
            bool result = true;

            for (int i = 0; i < m_Board.Size; i++)
            {
                if (m_Board.GameBoard[i, i_Position.Col] != m_Board.GameBoard[i_Position.Row, i_Position.Col])
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        private bool checkIfDiagonalSequenceExist(Position i_Position)
        {
            bool result = true;

            for (int i = 0; i < m_Board.Size; i++)
            {
                if (m_Board.GameBoard[i, i] != m_Board.GameBoard[i_Position.Row, i_Position.Col])
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        private bool checkIfAntiDiagonalSequenceExist(Position i_Position)
        {
            bool result = true;

            for (int i = 0; i < m_Board.Size; i++)
            {
                if (m_Board.GameBoard[i, m_Board.Size - i - 1] != m_Board.GameBoard[i_Position.Row, i_Position.Col])
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        private bool checkIfPlayerHasLostTheGame(Position i_Position)
        {
            bool result = false;

            result = result || checkIfRowSequenceExist(i_Position);
            result = result || checkIfColumnSequenceExist(i_Position);

            // if neccesary to check the diagonal
            if (i_Position.Row == i_Position.Col) 
            {
                result = result || checkIfDiagonalSequenceExist(i_Position);
            }

            // if neccesary to check the anti-diagonal
            if (i_Position.Row + i_Position.Col == m_Board.Size - 1)
            {
                result = result || checkIfAntiDiagonalSequenceExist(i_Position);
            }

            return result;
        }

        private bool checkIfTheCellIsEmpty(Position i_Position)
        {
            return m_Board.GameBoard[i_Position.Row, i_Position.Col] == eIconType.Empty;
        }

        private void changeCurrentPlayer()
        {
            if (m_CurrentPlayer.PlayerIcon == eIconType.X)
            {
                m_CurrentPlayer = m_PlayerO;
            }
            else
            {
                m_CurrentPlayer = m_PlayerX;
            }

            OnChangePlayer(m_CurrentPlayer.PlayerIcon);
        }

        private void OnChangePlayer(eIconType i_PlayerIcon)
        {
            ChangePlayer?.Invoke(i_PlayerIcon);
        }

        private void incrementWinningPlayerScore()
        {
            if (m_CurrentPlayer.PlayerIcon == eIconType.O)
            {
                m_PlayerX.PlayerScore++;
            }
            else
            {
                m_PlayerO.PlayerScore++;
            }
        }

        private void fillCellWithCurrentPlayerIcon(Position i_Position)
        {
            m_Board.GameBoard[i_Position.Row, i_Position.Col] = m_CurrentPlayer.PlayerIcon;
        }

        private bool boardIsFull()
        {
            return m_Board.Capacity == 0;
        }

        private eGameStatuss executeStep(Position i_Position)
        {
            eGameStatuss result;

            if (checkIfTheCellIsEmpty(i_Position))
            {
                fillCellWithCurrentPlayerIcon(i_Position);
                OnBoardUpdated(i_Position);
                if (checkIfPlayerHasLostTheGame(i_Position))
                {
                    incrementWinningPlayerScore();
                    result = eGameStatuss.Loss;
                    changeCurrentPlayer();
                    OnWinAnnouncement(m_CurrentPlayer);
                }
                else
                {
                    changeCurrentPlayer();
                    result = eGameStatuss.SuccesfullStep;
                    m_Board.Capacity--;
                    if (boardIsFull())
                    {
                        result = eGameStatuss.Tie;
                        OnTieAnnouncement();
                    }
                }
            }
            else
            {
                result = eGameStatuss.InvalidStep;
            }

            return result;
        }

        private void OnTieAnnouncement()
        {
            if (TieAnnouncement != null)
            {
                TieAnnouncement.Invoke();
            }
        }

        private void OnWinAnnouncement(Player i_CurrentPlayer)
        {
            if(WinAnnouncement != null) 
            {
                WinAnnouncement.Invoke(i_CurrentPlayer);
            }
        }

        private void OnBoardUpdated(Position i_Position)
        {
            if (BoardUpdate != null)
            {
                BoardUpdate.Invoke(i_Position);
            }
        }

        public eGameStatuss StepHandler(Position i_Position)
        {
            eGameStatuss result;

            result = executeStep(i_Position);
            if (m_CurrentPlayer.PlayerType == ePlayerType.Computer && result == eGameStatuss.SuccesfullStep)
            {
                result = m_Board.Capacity < 10 ? aiComputerStepExecuter() : computerStepExecuter();
            }

            return result;
        }

        private eGameStatuss computerStepExecuter()
        {
            Position computerStep = new Position();
            bool keepRandomUntilValidStep = true;
            eGameStatuss result = eGameStatuss.SuccesfullStep;

            while (keepRandomUntilValidStep)
            {
                computerStep.Row = m_RandomNumber.Next(0, m_Board.Size);
                computerStep.Col = m_RandomNumber.Next(0, m_Board.Size);
                result = executeStep(computerStep);
                if (eGameStatuss.InvalidStep != result)
                {
                    keepRandomUntilValidStep = false;
                }
            }

            return result;
        }

        private eGameStatuss aiComputerStepExecuter()
        {
            Position computerStep = getBestMove();
            eGameStatuss result = eGameStatuss.SuccesfullStep;
            result = executeStep(computerStep);

            return result;
        }

        private Position getBestMove()
        {
            int bestScore = int.MinValue;
            Position bestMove = new Position();
            Position tempStep = new Position();

            // Loop through all available moves and evaluate them using the Minimax algorithm
            for (int row = 0; row < m_Board.Size; row++)
            {
                for (int col = 0; col < m_Board.Size; col++)
                {
                    tempStep.SetPosition(row, col);
                    if (m_Board.GameBoard[row, col] == eIconType.Empty)
                    {
                        m_Board.GameBoard[row, col] = eIconType.O;
                        int score = miniMax(eIconType.X, eIconType.O, false, tempStep);
                        m_Board.GameBoard[row, col] = eIconType.Empty;
                        if (score > bestScore)
                        {
                            bestScore = score;
                            bestMove.SetPosition(row, col);
                        }
                    }
                }
            }

            return bestMove;
        }

        private int miniMax(eIconType i_PlayerSymbol, eIconType i_ComputerSymbol, bool i_IsMaximizing, Position i_TempStep)
        {
            int returnValue;

            if (!i_IsMaximizing && checkIfPlayerHasLostTheGame(i_TempStep))
            {
                returnValue = -1;
            }
            else if (i_IsMaximizing && checkIfPlayerHasLostTheGame(i_TempStep))
            {
                returnValue = 1;
            }
            else if (boardIsFull())
            {
                returnValue = 0;
            }
            else if(i_IsMaximizing)
            {
                int bestScore = int.MinValue;

                for (int row = 0; row < m_Board.Size; row++)
                {
                    for (int col = 0; col < m_Board.Size; col++)
                    {
                        i_TempStep.SetPosition(row, col);
                        if (m_Board.GameBoard[row, col] == eIconType.Empty)
                        {
                            m_Board.GameBoard[row, col] = i_ComputerSymbol;
                            int score = miniMax(i_PlayerSymbol, i_ComputerSymbol, false, i_TempStep);
                            m_Board.GameBoard[row, col] = eIconType.Empty;
                            bestScore = Math.Max(bestScore, score);
                        }
                    }
                }

                returnValue = bestScore;
            }
            else
            {
                int bestScore = int.MaxValue;

                for (int row = 0; row < m_Board.Size; row++)
                {
                    for (int col = 0; col < m_Board.Size; col++)
                    {
                        if (m_Board.GameBoard[row, col] == eIconType.Empty)
                        {
                            i_TempStep.SetPosition(row, col);
                            m_Board.GameBoard[row, col] = i_PlayerSymbol;
                            int score = miniMax(i_PlayerSymbol, i_ComputerSymbol, true, i_TempStep);
                            m_Board.GameBoard[row, col] = eIconType.Empty;
                            bestScore = Math.Min(bestScore, score);
                        }
                    }
                }

                returnValue = bestScore;
            }

            return returnValue;
        }
    }
}
