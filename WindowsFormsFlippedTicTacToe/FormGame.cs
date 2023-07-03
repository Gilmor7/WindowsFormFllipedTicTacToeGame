using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FlippedTicTacToe;

namespace WindowsFormsFlippedTicTacToe
{
    public partial class FormGame : Form
    {
        private const int k_MarginBetweenButtons = 10;
        private Label m_LabelPlayer1;
        private Label m_LabelPlayer2;
        private ButtonBoard[,] m_BoardButtons;

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
        }

        private void fillBoardButtonMatrix(int i_BoardSize)
        {
            int startX = (this.ClientSize.Width - i_BoardSize * (ButtonBoard.k_ButtonSize + k_MarginBetweenButtons)) / 2;
            int startY = (this.ClientSize.Height - i_BoardSize * (ButtonBoard.k_ButtonSize + k_MarginBetweenButtons)) / 2;

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
            int width = i_BoardSize * (ButtonBoard.k_ButtonSize + 35);
            this.Size = new Size(width, width);
        }

        private void initializeLabelsForPlayersNameAndScore()
        {
            m_LabelPlayer1 = new Label();
            m_LabelPlayer2 = new Label();

            m_LabelPlayer1.Location = new Point(10, this.ClientSize.Height - m_LabelPlayer1.Height - 10);
            m_LabelPlayer2.Location = new Point(10 + m_LabelPlayer2.Width + 10, this.ClientSize.Height - m_LabelPlayer2.Height - 10);
            this.Controls.Add(m_LabelPlayer1);
            this.Controls.Add(m_LabelPlayer2);
        }

        public void UpdatePlayerNamesAndScores(string player1Name, uint player1Score, string player2Name, uint player2Score, bool isPlayer1Turn)
        {
            //TODO: make this method to be better, no need to render all of them again everytime.
            m_LabelPlayer1.Text = $"{player1Name}: {player1Score}";
            m_LabelPlayer1.Font = new Font(m_LabelPlayer1.Font, isPlayer1Turn ? FontStyle.Bold : FontStyle.Regular);

            m_LabelPlayer2.Text = $"{player2Name}: {player2Score}";
            m_LabelPlayer2.Font = new Font(m_LabelPlayer2.Font, isPlayer1Turn ? FontStyle.Regular : FontStyle.Bold);

            // Adjust label locations after text changed
            m_LabelPlayer2.Location = new Point(10 + m_LabelPlayer1.Width + 10, m_LabelPlayer2.Location.Y);
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
            //TODO: Should we add validation for Cell here?
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
    }
}
