using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FlippedTicTacToe;

namespace WindowsFormsFlippedTicTacToe
{
    public partial class FormGame : Form
    {
        public event Func<Cell, eSymbols> ButtonClicked;
        public event Action<Cell> ComputerMoved;

        private const int ButtonSize = 50; // Assuming each button is 50x50 pixels
        private const int Margin = 10; // Assuming 10 pixels margin between buttons
        private Label lblPlayer1, lblPlayer2;

        public FormGame()
        {
            InitializeComponent();

            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            // Initialize labels for player names and scores
            lblPlayer1 = new Label { Font = new Font(FontFamily.GenericSansSerif, 12.0F, FontStyle.Regular), AutoSize = true };
            lblPlayer2 = new Label { Font = new Font(FontFamily.GenericSansSerif, 12.0F, FontStyle.Regular), AutoSize = true };

            // Positioning labels at the bottom of the form
            lblPlayer1.Location = new Point(10, this.ClientSize.Height - lblPlayer1.Height - 10);
            lblPlayer2.Location = new Point(10 + lblPlayer1.Width + 10, this.ClientSize.Height - lblPlayer2.Height - 10);

            this.Controls.Add(lblPlayer1);
            this.Controls.Add(lblPlayer2);
        }

        public void CreateGameBoard(int boardSize)
        {
            // Center the buttons on the form
            this.Size = new Size(85 * boardSize, 85 * boardSize);

            int startX = (this.ClientSize.Width - boardSize * (ButtonSize + Margin)) / 2;
            int startY = (this.ClientSize.Height - boardSize * (ButtonSize + Margin)) / 2;

            // Add new buttons
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    Button button = new Button
                    {
                        Size = new Size(ButtonSize, ButtonSize),
                        Location = new Point(startX + j * (ButtonSize + Margin), startY + i * (ButtonSize + Margin)),
                        Tag = new Cell((uint)i, (uint)j)
                    };
                    button.Click += Button_Click;
                    this.Controls.Add(button);
                }
            }

            // Adjust label locations after form size changed
            lblPlayer1.Location = new Point(10, this.ClientSize.Height - lblPlayer1.Height - 10);
            lblPlayer2.Location = new Point(10 + lblPlayer1.Width + 10, this.ClientSize.Height - lblPlayer2.Height - 10);
        }

        public void UpdatePlayerNamesAndScores(string player1Name, uint player1Score, string player2Name, uint player2Score, bool isPlayer1Turn)
        {
            lblPlayer1.Text = $"{player1Name}: {player1Score}";
            lblPlayer1.Font = new Font(lblPlayer1.Font, isPlayer1Turn ? FontStyle.Bold : FontStyle.Regular);

            lblPlayer2.Text = $"{player2Name}: {player2Score}";
            lblPlayer2.Font = new Font(lblPlayer2.Font, isPlayer1Turn ? FontStyle.Regular : FontStyle.Bold);

            // Adjust label locations after text changed
            lblPlayer2.Location = new Point(10 + lblPlayer1.Width + 10, lblPlayer2.Location.Y);
        }

        private void Button_Click(object sender, EventArgs e)
        {
            if (ButtonClicked != null && sender is Button button && button.Tag is Cell cell)
            {
                eSymbols symbol = ButtonClicked.Invoke(cell);
            }
        }

        public void UpdateCell(Cell cell, eSymbols symbol)
        {
            foreach (Control control in this.Controls)
            {
                if (control is Button button && button.Tag is Cell buttonCell && buttonCell.Equals(cell))
                {
                    button.Text = symbol.ToString();
                    button.Enabled = false;
                    break;
                }
            }
        }

        public void GenerateFinishMsg(eGameStatus i_GameStatus, Player i_Winner)
        {
            
        }
    }
}
