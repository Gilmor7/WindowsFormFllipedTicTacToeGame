using System;
using System.Windows.Forms;
using FlippedTicTacToe;

namespace WindowsFormsFlippedTicTacToe
{
    public sealed class GameManager
    {
        private readonly GameEngine r_GameEngine;
        private readonly FormGame r_FormGame;

        public GameManager()
        {
            r_GameEngine = new GameEngine();
            r_FormGame = new FormGame();
            r_FormGame.ButtonClicked += HandleButtonClicked;
            r_GameEngine.MoveMade += HandleMoveMade;
            r_GameEngine.GameFinished += HandleGameFinished;
        }

        public void Run()
        {
            setGameSettings();
            createGameBoard();
            r_FormGame.ShowDialog();
        }

        private void setGameSettings()
        {
            FormSettings formSettings = new FormSettings();
            DialogResult result = formSettings.ShowDialog();

            if (result == DialogResult.OK)
            {
                bool isPlayer2Computer = formSettings.CheckBoxPlayer2Checked == false;

                r_GameEngine.SetFirstPlayer(formSettings.Player1Name);
                r_GameEngine.SetSecondPlayer(isPlayer2Computer, formSettings.Player2Name);
                r_GameEngine.SetGameBoardSize(formSettings.BoardWidth);
            }
        }

        private void createGameBoard()
        {
            r_FormGame.CreateGameBoard((int)r_GameEngine.GameBoard.Size);
            updatePlayerNamesAndScores();
        }

        private eSymbols HandleButtonClicked(Cell i_Cell)
        {
            r_GameEngine.MakeMove(i_Cell);

            if (r_GameEngine.Player2.IsComputer && r_GameEngine.GameStatus == eGameStatus.InProgress)
            {
                r_GameEngine.MakeRandomMove();
            }

            r_FormGame.UpdatePlayerNamesAndScores(r_GameEngine.Player1.Name, r_GameEngine.Player1.Score, r_GameEngine.Player2.Name, r_GameEngine.Player2.Score, r_GameEngine.CurrentPlayer == r_GameEngine.Player1);

            return r_GameEngine.CurrentPlayer.Symbol;
        }

        private void HandleMoveMade(Cell i_Cell)
        {
            r_FormGame.UpdateCell(i_Cell, r_GameEngine.CurrentPlayer.Symbol);
        }

        private void updatePlayerNamesAndScores()
        {
            string player1Name = r_GameEngine.Player1.Name;
            uint player1Score = r_GameEngine.Player1.Score;
            string player2Name = r_GameEngine.Player2.Name;
            uint player2Score = r_GameEngine.Player2.Score;
            bool isPlayer1Turn = r_GameEngine.CurrentPlayer == r_GameEngine.Player1;

            r_FormGame.UpdatePlayerNamesAndScores(
                player1Name,
                player1Score,
                player2Name,
                player2Score,
                isPlayer1Turn);
        }

        private void HandleGameFinished(eGameStatus i_GameStatus, Player i_Winner)
        {
            string msg = this.generateFinishMsg(i_GameStatus, i_Winner);
            string caption = this.generateFinishCaption(i_GameStatus);

            updatePlayerNamesAndScores();
            DialogResult result = MessageBox.Show(msg, caption, MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                r_GameEngine.RestartGame();
                r_FormGame.ResetBoardButtons();
            }
            else
            {
                r_FormGame.Close();
            }
        }

        private string generateFinishMsg(eGameStatus i_GameStatus, Player i_Winner)
        {
            string msg = string.Empty;

            if (i_GameStatus == eGameStatus.Draw)
            {
                msg = "Tie!";
            }
            else
            {
                msg = $"The winner is {i_Winner.Name}!";
            }

            msg = msg + Environment.NewLine + "Would you like to play another round?";

            return msg;
        }

        private string generateFinishCaption(eGameStatus i_GameStatus)
        {
            string caption = string.Empty;

            if (i_GameStatus == eGameStatus.Draw)
            {
                caption = "A Tie!";
            }
            else
            {
                caption = "A Win!";
            }

            return caption;
        }
    }
}
