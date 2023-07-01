using System;
using System.Windows.Forms;
using FlippedTicTacToe;

namespace WindowsFormsFlippedTicTacToe
{
    public sealed class GameManager
    {
        private readonly GameEngine r_GameEngine = new GameEngine();

        public void Run()
        {
            setGameSettings();
            createGameBoard();
        }

        private void setGameSettings()
        {
            FormSettings formSettings = new FormSettings();
            DialogResult result = formSettings.ShowDialog();

            if(result == DialogResult.OK)
            {
                bool isPlayer2Computer = formSettings.CheckBoxPlayer2Checked == false;

                r_GameEngine.SetFirstPlayer(formSettings.Player1Name);
                r_GameEngine.SetSecondPlayer(isPlayer2Computer, formSettings.Player2Name);
                r_GameEngine.SetGameBoardSize(formSettings.BoardWidth);
            }
        }

        private void buttonClicked(Button b, Cell cell)
        {
            //r_GameEngine.MakeMove(cell);
            //b.Text = r_GameEngine.CurrentPlayer.Symbol;
            //b.Enabled = false;
        }

        private void createGameBoard()
        {
            //Buttonmatrix

            //    for(int i = 0; i < UPPER; i++)
            //    {
            //        for(int j = 0; j < UPPER; j++)
            //        {
            //            Buttonmatrix[i, j] = new Button();
            //            Cell cell = new Cell(i, j);
            //            Buttonmatrix[i, j].Click = buttonClicked(Buttonmatrix[i, j], cell);
            //        }
            //    }
        }
    }
}
