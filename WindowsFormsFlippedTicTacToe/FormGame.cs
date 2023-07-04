using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FlippedTicTacToe;

namespace WindowsFormsFlippedTicTacToe
{
    public partial class FormGame : Form
    {
        private const int k_MarginBetweenButtons = 3;
        private Label m_LabelPlayer1;
        private Label m_LabelPlayer2;
        private ButtonBoard[,] m_BoardButtons;
        private Font m_BoldFont;
        private Font m_NormalFont;

        public event Func<Cell, eSymbols> ButtonClicked;

        public FormGame()
        {
            InitializeComponent();
        }

        public void CreateGameBoard(int i_BoardSize)
        {
            this.setFormSize(i_BoardSize);
            this.fillBoardButtonMatrix(i_BoardSize);
            this.initializeLabelsForPlayersNameAndScore();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.PerformLayout();
        }

        private void fillBoardButtonMatrix(int i_BoardSize)
        {
            int startX = 10;
            int startY = 10;

            m_BoardButtons = new ButtonBoard[i_BoardSize, i_BoardSize];
            for (int i = 0; i < i_BoardSize; i++)
            {
                for (int j = 0; j < i_BoardSize; j++)
                {
                    Point buttonLocation = new Point(startX + j * (ButtonBoard.k_ButtonSize + k_MarginBetweenButtons), startY + i * (ButtonBoard.k_ButtonSize + k_MarginBetweenButtons));

                    ButtonBoard button = new ButtonBoard(i, j, buttonLocation);
                    button.Click += Button_Click;
                    this.Controls.Add(button);
                    m_BoardButtons[i, j] = button;
                }
            }
        }

        private void setFormSize(int i_BoardSize)
        {
            int totalWidth = (ButtonBoard.k_ButtonSize + k_MarginBetweenButtons) * i_BoardSize + 30;
            this.Size = new Size(totalWidth, totalWidth + 50);
        }

        private void initializeLabelsForPlayersNameAndScore()
        {
            m_LabelPlayer1 = new Label();
            m_LabelPlayer2 = new Label();

            m_BoldFont = new Font(m_LabelPlayer1.Font, FontStyle.Bold);
            m_NormalFont = new Font(m_LabelPlayer1.Font, FontStyle.Regular);

            m_LabelPlayer1.Text = "Player1: 0";
            m_LabelPlayer2.Text = "Player2: 0";

            m_LabelPlayer1.AutoSize = true;
            m_LabelPlayer2.AutoSize = true;

            this.Controls.Add(m_LabelPlayer1);
            this.Controls.Add(m_LabelPlayer2);

            int yPosition = m_BoardButtons[m_BoardButtons.GetLength(0) - 1, 0].Bottom + 10; // position of labels will be just below last row of buttons
            int totalWidth = m_LabelPlayer1.Width + m_LabelPlayer2.Width + 3;
            int player1LabelX = (this.ClientSize.Width - totalWidth) / 2;
            int player2LabelX = player1LabelX + m_LabelPlayer1.Width + 3;

            m_LabelPlayer1.Location = new Point(player1LabelX, yPosition);
            m_LabelPlayer2.Location = new Point(player2LabelX, yPosition);
        }



    public void UpdatePlayerNamesAndScores(string i_Player1Name, uint i_Player1Score, string i_Player2Name, uint i_Player2Score)
        {
            m_LabelPlayer1.Text = $"{i_Player1Name}: {i_Player1Score}";
            m_LabelPlayer2.Text = $"{i_Player2Name}: {i_Player2Score}";
        }

        public void MakeCurrentPlayerLabelBold(bool i_IsPlayer1CurrentPlayer)
        {
            if(i_IsPlayer1CurrentPlayer)
            {
                m_LabelPlayer1.Font = m_BoldFont;
                m_LabelPlayer2.Font = m_NormalFont;
            }
            else
            {
                m_LabelPlayer1.Font = m_NormalFont;
                m_LabelPlayer2.Font = m_BoldFont;
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (ButtonClicked != null)
            {
                ButtonBoard buttonBoard = sender as ButtonBoard;

                if(buttonBoard != null)
                {
                    Cell cell = (Cell)buttonBoard.Tag;
                    ButtonClicked.Invoke(cell);
                }
            }
        }

        public void UpdateCell(Cell i_Cell, eSymbols i_Symbol)
        {
            if (i_Cell.Row >= m_BoardButtons.GetLength(0) || i_Cell.Column >= m_BoardButtons.GetLength(1))
            {
                throw new ArgumentException("The provided cell is out of the game board bounds.");
            }
            ButtonBoard button = m_BoardButtons[i_Cell.Row, i_Cell.Column];
            button.Text = i_Symbol.ToString();
            button.Enabled = false;
        }
        
        public void ResetBoardButtons()
        {
            for (int i = 0; i < m_BoardButtons.GetLength(0); i++)
            {
                for (int j = 0; j < m_BoardButtons.GetLength(1); j++)
                {
                    m_BoardButtons[i, j].Text = string.Empty;
                    m_BoardButtons[i, j].Enabled = true;
                }
            }
        }

        private void FormGame_Load(object sender, EventArgs e)
        {

        }
    }
}
