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
            r_FormGame.ButtonClicked += handleButtonClicked;
            r_GameEngine.MoveMade += handleMoveMade;
            r_GameEngine.GameFinished += handleGameFinished;
        }

        public void Run()
        {
            bool setGameSettingsWasOk = setGameSettings();

            if(setGameSettingsWasOk)
            {
                createGameBoard();
                r_FormGame.ShowDialog();
            }
        }

        private bool setGameSettings()
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

            return result == DialogResult.OK;
        }

        private void createGameBoard()
        {
            r_FormGame.CreateGameBoard((int)r_GameEngine.GameBoard.Size);
            r_FormGame.MakeCurrentPlayerLabelBold(r_GameEngine.CurrentPlayer == r_GameEngine.Player1);
            updatePlayerNamesAndScores();
        }

        private eSymbols handleButtonClicked(Cell i_Cell)
        {
            r_GameEngine.MakeMove(i_Cell);

            if(r_GameEngine.Player2.IsComputer && r_GameEngine.GameStatus == eGameStatus.InProgress)
            {
                r_GameEngine.MakeRandomMove();
            }

            if(r_GameEngine.GameStatus != eGameStatus.InProgress)
            {
                restartGame();
            }

            r_FormGame.MakeCurrentPlayerLabelBold(r_GameEngine.CurrentPlayer == r_GameEngine.Player1);
            r_FormGame.UpdatePlayerNamesAndScores(r_GameEngine.Player1.Name, r_GameEngine.Player1.Score, r_GameEngine.Player2.Name, r_GameEngine.Player2.Score);

            return r_GameEngine.CurrentPlayer.Symbol;
        }

        private void restartGame()
        {
            r_GameEngine.RestartGame();
            r_FormGame.ResetBoardButtons();
        }

        private void handleMoveMade(Cell i_Cell)
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
                player2Score);
        }

        private void handleGameFinished(eGameStatus i_GameStatus, Player i_Winner)
        {
            string msg = this.generateFinishMsg(i_GameStatus, i_Winner);
            string caption = this.generateFinishCaption(i_GameStatus);

            DialogResult result = MessageBox.Show(msg, caption, MessageBoxButtons.YesNo);

            if(result == DialogResult.No)
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
