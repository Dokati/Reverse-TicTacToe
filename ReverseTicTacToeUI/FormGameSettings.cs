using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using ReverseTicTacToe;

namespace ReverseTicTacToeUI
{
    public class FormGameSettings : Form
    {
        private const string k_ComputerLabel = "[Computer]";
        private Button m_ButtonStart;
        private CheckBox m_CheckSinglePlayer;
        private TextBox m_TextBoxPlayerX;
        private TextBox m_TextBoxPlayerO;
        private NumericUpDown m_NumericUpDownRows;
        private NumericUpDown m_NumericUpDownCols;
        private Label m_LabelPlayers;
        private Label m_LabelPlayer1;
        private Label m_LabelPlayer2;
        private Label m_LabelBoardSize;
        private Label m_LabelRows;
        private Label m_LabelCols;
        private bool m_FormClosedByStartButton = false;

        public FormGameSettings()
        {
            initializeComponents();
        }

        private void initializeComponents()
        {
            string fullFilePath = Path.Combine(Resources.ResourcesFolderPath, Resources.TitleIcon);
            this.Text = "TicTacToe Misere Settings";
            this.Icon = new Icon(fullFilePath);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            m_LabelPlayers = new Label();
            m_LabelPlayers.Text = "Players:";
            m_LabelPlayers.Left = 10;
            m_LabelPlayers.Top = 15;
            this.Controls.Add(m_LabelPlayers);

            m_LabelPlayer1 = new Label();
            m_LabelPlayer1.Text = "Player1:";
            m_LabelPlayer1.Left = 35;
            m_LabelPlayer1.Top = 40;
            m_LabelPlayer1.AutoSize = true;
            this.Controls.Add(m_LabelPlayer1);

            m_LabelPlayer2 = new Label();
            m_LabelPlayer2.Text = "Player2:";
            m_LabelPlayer2.Left = 50;
            m_LabelPlayer2.Top = 70;
            m_LabelPlayer2.AutoSize = true;
            this.Controls.Add(m_LabelPlayer2);

            m_CheckSinglePlayer = new CheckBox();
            m_CheckSinglePlayer.Left = 35;
            m_CheckSinglePlayer.Top = 65;
            m_CheckSinglePlayer.CheckStateChanged += new EventHandler(playerTypeCheckBoxButton_CheckedChanged);
            this.Controls.Add(m_CheckSinglePlayer);

            m_TextBoxPlayerX = new TextBox();
            m_TextBoxPlayerX.Left = 145;
            m_TextBoxPlayerX.Top = 40;
            this.Controls.Add(m_TextBoxPlayerX);

            m_TextBoxPlayerO = new TextBox();
            m_TextBoxPlayerO.Left = 145;
            m_TextBoxPlayerO.Top = 70;
            m_TextBoxPlayerO.Enabled = false;
            m_TextBoxPlayerO.Text = k_ComputerLabel;
            this.Controls.Add(m_TextBoxPlayerO);

            m_LabelBoardSize = new Label();
            m_LabelBoardSize.Text = "Board Size:";
            m_LabelBoardSize.Left = 10;
            m_LabelBoardSize.Top = 100;
            this.Controls.Add(m_LabelBoardSize);

            m_LabelRows = new Label();
            m_LabelRows.Text = "Rows:";
            m_LabelRows.Left = 35;
            m_LabelRows.Top = 125;
            m_LabelRows.AutoSize = true;
            this.Controls.Add(m_LabelRows);

            m_LabelCols = new Label();
            m_LabelCols.Text = "Cols:";
            m_LabelCols.Left = 155;
            m_LabelCols.Top = 125;
            m_LabelCols.AutoSize = true;
            this.Controls.Add(m_LabelCols);

            m_NumericUpDownRows = new NumericUpDown();
            m_NumericUpDownRows.Left = 80;
            m_NumericUpDownRows.Top = 125;
            m_NumericUpDownRows.Width = 50;
            m_NumericUpDownRows.Maximum = 10;
            m_NumericUpDownRows.Minimum = 4;
            m_NumericUpDownRows.ValueChanged += new EventHandler(numericUpDownCols_Click);
            this.Controls.Add(m_NumericUpDownRows);

            m_NumericUpDownCols = new NumericUpDown();
            m_NumericUpDownCols.Left = 200;
            m_NumericUpDownCols.Top = 125;
            m_NumericUpDownCols.Width = 50;
            m_NumericUpDownCols.Maximum = 10;
            m_NumericUpDownCols.Minimum = 4;
            m_NumericUpDownCols.ValueChanged += new EventHandler(numericUpDownRows_Click);
            this.Controls.Add(m_NumericUpDownCols);

            m_ButtonStart = new Button();
            m_ButtonStart.Text = "Start!";
            m_ButtonStart.Left = 20;
            m_ButtonStart.Top = 180;
            m_ButtonStart.Width = 250;
            m_ButtonStart.Click += new EventHandler(buttonStart_Click);
            this.Controls.Add(m_ButtonStart);
        }

        private void playerTypeCheckBoxButton_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox singlePlayerCheckBox = sender as CheckBox;

            if (singlePlayerCheckBox.Checked)
            {
                m_TextBoxPlayerO.Enabled = true;
                m_TextBoxPlayerO.Text = string.Empty;
            }
            else
            {
                m_TextBoxPlayerO.Enabled = false;
                m_TextBoxPlayerO.Text = k_ComputerLabel;
            }
        }

        public string PlayerXName
        {
            get
            {
                return m_TextBoxPlayerX.Text;
            }
        }

        public string PlayerOName
        {
            get
            {
                return m_TextBoxPlayerO.Text;
            }
        }

        public int BoardSize
        {
            get
            {
                return Convert.ToInt32(m_NumericUpDownRows.Value);
            }
        }

        public ePlayerType PlayerOType
        {
            get
            {
                return m_CheckSinglePlayer.Checked == true ? ePlayerType.Human : ePlayerType.Computer;
            }
        }

        public string ComputerLabel
        {
            get
            {
                return k_ComputerLabel;
            }
        }

        private void numericUpDownCols_Click(object sender, EventArgs e)
        {
            NumericUpDown numericUpDown = sender as NumericUpDown;

            m_NumericUpDownCols.Value = numericUpDown.Value;
        }

        private void numericUpDownRows_Click(object sender, EventArgs e)
        {
            NumericUpDown numericUpDown = sender as NumericUpDown;

            m_NumericUpDownRows.Value = numericUpDown.Value;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            string errorPlayerNameMessage = string.Empty;

            if (!Player.IsValidName(m_TextBoxPlayerX.Text) || !Player.IsValidName(m_TextBoxPlayerO.Text))
            {
                MessageBox.Show("The name entered is invalid!");
            }
            else
            {
                m_FormClosedByStartButton = true;
                this.Close();
            }
        }

        public bool IsFormClosedByStartButton
        {
            get
            {
                return m_FormClosedByStartButton;
            }
        }
    }
}