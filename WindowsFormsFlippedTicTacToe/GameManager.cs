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

        private void createGameBoard()
        {
            int x = 5;
        }
    }
}
