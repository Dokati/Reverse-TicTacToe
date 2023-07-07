using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ReverseTicTacToe;

namespace ReverseTicTacToeUI
{
    public class GameManager
    {
        private readonly Game r_Game;
        private readonly FormGame r_FormGame;

        public GameManager()
        {
            r_FormGame = new FormGame();
            r_Game = new Game();
        }

        public void RunGame()
        {
            if (r_FormGame.IsFormClosedByStartButton)
            {
                initializeGameHandler(r_FormGame.GameDetailsArgs);
                addDelegatesToWinUIEvents();
                addDelegatesToLogicEvents();
                r_FormGame.ShowDialog();
            }
        }

        private void addDelegatesToWinUIEvents()
        {
            r_FormGame.NewStep += transferNewStepHandler;
            r_FormGame.InitializeGame += initializeGameHandler;
            r_FormGame.ResetGameSession += resetGameSessionHandler;
        }

        private void addDelegatesToLogicEvents()
        {
            r_Game.BoardUpdate += boardUpdateHandler;
            r_Game.WinAnnouncement += winAnnouncementHandler;
            r_Game.TieAnnouncement += tieAnnouncementHandler;
            r_Game.ChangePlayer += r_FormGame.HandleChangePlayer;
        }

        private void tieAnnouncementHandler()
        {
            r_FormGame.HandleTie();
        }

        private void resetGameSessionHandler()
        {
            r_Game.ResetGameSession();
        }

        private void winAnnouncementHandler(Player i_Player)
        {
            r_FormGame.HandleVictory(i_Player);
        }

        private void boardUpdateHandler(Position i_position)
        {
            r_FormGame.UpdateFormBoard(i_position, r_Game.CurrentPlayer.PlayerIcon);
        }

        private void initializeGameHandler(EventGameDetailsArgs i_Args)
        {
            r_Game.InitializeGameDetails(i_Args.PlayerXName, i_Args.PlayerOName, i_Args.PlayerOType, i_Args.BoardSize);
            r_Game.ResetGameSession();
        }

        private void transferNewStepHandler(Position i_Position)
        {
            r_Game.StepHandler(i_Position);
        }
    }
}