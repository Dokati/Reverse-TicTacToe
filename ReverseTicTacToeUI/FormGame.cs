using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Drawing;
using ReverseTicTacToe;

namespace ReverseTicTacToeUI
{
    public class FormGame : Form
    {
        private const int k_PictureBoxCellSize = 60;
        private PictureBoxCell[,] m_PictureBoxMatrix;
        private FormGameSettings m_FormGameSettings;
        private EventGameDetailsArgs m_GameDetailsArgs;
        private Label m_LabelPlayerX;
        private Label m_LabelPlayerO;

        public event Action<Position> NewStep;

        public event Action<EventGameDetailsArgs> InitializeGame;

        public event Action ResetGameSession;

        public FormGame()
        {
            m_FormGameSettings = new FormGameSettings();
            m_FormGameSettings.ShowDialog();
            if(m_FormGameSettings.IsFormClosedByStartButton)
            { 
                initializeForm(); 
            }
        }

        private void initializeForm()
        {
            initializeGameDetailsArgs();
            initializeFormBorad();
            initializePictureBoxBoard();
        }

        private void initializeFormBorad()
        {
            string fullFilePath = Path.Combine(Resources.ResourcesFolderPath, Resources.BoardBackground);
            this.Size = new Size(m_GameDetailsArgs.BoardSize * 100, m_GameDetailsArgs.BoardSize * 100);
            this.BackgroundImage = Image.FromFile(fullFilePath);
            fullFilePath = Path.Combine(Resources.ResourcesFolderPath, Resources.TitleIcon);
            this.Icon = new Icon(fullFilePath);
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Text = "TicTacToeMisere";
            m_LabelPlayerX = new Label();
            m_LabelPlayerO = new Label();
            m_LabelPlayerX.BackColor = Color.LightBlue;
            m_LabelPlayerO.BackColor = Color.LightBlue;
            m_LabelPlayerX.Text = string.Format("{0}: 0", m_GameDetailsArgs.PlayerXName);
            m_LabelPlayerO.Text = string.Format("{0}: 0", m_GameDetailsArgs.PlayerOName);
            m_LabelPlayerX.Font = new Font(m_LabelPlayerX.Font.FontFamily,12, FontStyle.Bold);
            m_LabelPlayerO.Font = new Font(m_LabelPlayerX.Font.FontFamily,12, m_LabelPlayerO.Font.Style);
            this.Controls.Add(m_LabelPlayerX);  
            this.Controls.Add(m_LabelPlayerO);
        }

        private void initializeGameDetailsArgs()
        {
            string oPlayerName = m_FormGameSettings.PlayerOName == m_FormGameSettings.ComputerLabel ? Enum.GetName(typeof(ePlayerType), ePlayerType.Computer) : m_FormGameSettings.PlayerOName;

            m_GameDetailsArgs = new EventGameDetailsArgs(m_FormGameSettings.PlayerXName, oPlayerName, m_FormGameSettings.BoardSize, m_FormGameSettings.PlayerOType);
            OnInitializeGame(m_GameDetailsArgs);
        }

        private void initializePictureBoxBoard()
        {
            int centerX = (this.ClientSize.Width - (m_GameDetailsArgs.BoardSize * k_PictureBoxCellSize)) / 2;
            int centerY = (this.ClientSize.Height - (m_GameDetailsArgs.BoardSize * k_PictureBoxCellSize)) / 2;
            int xLocation = 0;
            int yLocation = 0;

            m_PictureBoxMatrix = new PictureBoxCell[m_GameDetailsArgs.BoardSize, m_GameDetailsArgs.BoardSize];
            for (int row = 0; row < m_GameDetailsArgs.BoardSize; row++)
            {
                for (int col = 0; col < m_GameDetailsArgs.BoardSize; col++)
                {
                    xLocation = (col * k_PictureBoxCellSize) + centerX;
                    yLocation = (row * k_PictureBoxCellSize) + centerY;
                    m_PictureBoxMatrix[row, col] = new PictureBoxCell(row, col);
                    m_PictureBoxMatrix[row, col].initializePictureBoxCell(k_PictureBoxCellSize, xLocation, yLocation);
                    m_PictureBoxMatrix[row, col].Click += new EventHandler(OnPictureBoxCell_Click);
                    m_PictureBoxMatrix[row, col].MouseEnter += new EventHandler(OnPictureBoxCell_Enter);
                    m_PictureBoxMatrix[row, col].MouseLeave += new EventHandler(OnPictureBoxCell_Leave);
                    this.Controls.Add(m_PictureBoxMatrix[row, col]);
                }
            }

            m_LabelPlayerX.AutoSize = true;
            m_LabelPlayerX.Location = new Point(((ClientSize.Width - m_LabelPlayerX.Width) / 2 ) - 43, yLocation + k_PictureBoxCellSize);
            m_LabelPlayerO.AutoSize = true;
            m_LabelPlayerO.Location = new Point(m_LabelPlayerX.Right + 27, m_LabelPlayerX.Location.Y);
        }

        private void OnPictureBoxCell_Leave(object sender, EventArgs e)
        {
            PictureBoxCell PictureBoxCell = sender as PictureBoxCell;
            if(PictureBoxCell.Image.Height == 207)
            {
                String fullFilePath = Path.Combine(Resources.ResourcesFolderPath, Resources.CellBackground);
                PictureBoxCell.Image = Image.FromFile(fullFilePath);
                PictureBoxCell.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void OnPictureBoxCell_Enter(object sender, EventArgs e)
        {
            PictureBoxCell PictureBoxCell = sender as PictureBoxCell;
            String fullFilePath = Path.Combine(Resources.ResourcesFolderPath, Resources.CellBackgroundHovering);
            if (PictureBoxCell.Image.Height == 207)
            {
                PictureBoxCell.Image = Image.FromFile(fullFilePath);
                PictureBoxCell.SizeMode = PictureBoxSizeMode.StretchImage;
            }
        }

        private void OnInitializeGame(EventGameDetailsArgs i_GameDetailsArgs)
        {
            if(InitializeGame != null)
            {
                InitializeGame.Invoke(i_GameDetailsArgs);
            }
        }

        private void OnResetGameSession()
        {
            if (ResetGameSession != null)
            {
                ResetGameSession.Invoke();
            }
        }

        internal void HandleVictory(Player i_WinningPlayer)
        {
            setLabelPlayerText(i_WinningPlayer);
            DialogResult result = MessageBox.Show(string.Format("The winner is {0}!\nWould you like to play another round?", i_WinningPlayer.PlayerName), "A win!", MessageBoxButtons.YesNo);

            continueOrExitGame(result);
        }

        private void continueOrExitGame(DialogResult i_UserChoise)
        {
            if (i_UserChoise == DialogResult.Yes)
            {
                cleanFormBoardAndEnableButtons();
                OnResetGameSession();
                HandleChangePlayer(eIconType.X);
            }
            else 
            {
                Application.Exit();
            }
        }

        internal void HandleTie()
        {
            DialogResult result = MessageBox.Show("Tie!\nWould you like to play another round?", "A Tie!", MessageBoxButtons.YesNo);

            continueOrExitGame(result);
        }

        private void setLabelPlayerText(Player i_Player)
        {
            if(i_Player.PlayerIcon == eIconType.X)
            {
                m_LabelPlayerX.Text = string.Format("{0}: {1}", i_Player.PlayerName, i_Player.PlayerScore);
            }
            else
            {
                m_LabelPlayerO.Text = string.Format("{0}: {1}", i_Player.PlayerName, i_Player.PlayerScore);
            }
        }

        private void cleanFormBoardAndEnableButtons()
        {
            string fullFilePath = Path.Combine(Resources.ResourcesFolderPath, Resources.CellBackground);
            for (int row = 0; row < m_GameDetailsArgs.BoardSize; row++)
            {
                for (int col = 0; col < m_GameDetailsArgs.BoardSize; col++)
                {
                    m_PictureBoxMatrix[row, col].Image = Image.FromFile(fullFilePath);
                }
            }
        }

        private void OnPictureBoxCell_Click(object sender, EventArgs e)
        {
            PictureBoxCell PictureBoxCell = sender as PictureBoxCell;

            OnNewStep(PictureBoxCell.PositionOnBoard);
        }

        internal void UpdateFormBoard(Position i_PositionOfNewStep, eIconType i_CurrentPlayer)
        {
            string fullFilePath;

            if (i_CurrentPlayer == eIconType.X)
            {
                fullFilePath = Path.Combine(Resources.ResourcesFolderPath, Resources.XIcon);
            }
            else
            {
                fullFilePath = Path.Combine(Resources.ResourcesFolderPath, Resources.OIcon);
            }

            m_PictureBoxMatrix[i_PositionOfNewStep.Row, i_PositionOfNewStep.Col].Image = Image.FromFile(fullFilePath);
            m_PictureBoxMatrix[i_PositionOfNewStep.Row, i_PositionOfNewStep.Col].SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void OnNewStep(Position i_PositionOnBoard)
        {
            if (NewStep != null)
            {
                NewStep.Invoke(i_PositionOnBoard);
            }
        }

        internal void HandleChangePlayer(eIconType i_NewCurrenPlayerIcon)
        {
            if (i_NewCurrenPlayerIcon == eIconType.X)
            {
                m_LabelPlayerO.Font = new Font(m_LabelPlayerX.Font, FontStyle.Regular);
                m_LabelPlayerX.Font = new Font(m_LabelPlayerX.Font, FontStyle.Bold);
            }
            else
            {
                m_LabelPlayerX.Font = new Font(m_LabelPlayerX.Font, FontStyle.Regular);
                m_LabelPlayerO.Font = new Font(m_LabelPlayerO.Font, FontStyle.Bold);
            }
        }

        public bool IsFormClosedByStartButton
        {
            get
            {
                return m_FormGameSettings.IsFormClosedByStartButton;
            }
        }

        public EventGameDetailsArgs GameDetailsArgs 
        {
            get
            {
                return m_GameDetailsArgs;
            }
        }
    }
}
