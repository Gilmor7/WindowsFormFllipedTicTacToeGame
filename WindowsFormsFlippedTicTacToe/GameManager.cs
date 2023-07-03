using System;
using System.Windows.Forms;
using FlippedTicTacToe;

namespace WindowsFormsFlippedTicTacToe
{
    public sealed class GameManager
    {
        private readonly GameEngine r_GameEngine = new GameEngine();
        private FormGame formGame;

        public void Run()
        {
            setGameSettings();
            createGameBoard();
            r_GameEngine.MoveMade += HandleMoveMade;
            r_GameEngine.GameFinished += HandleGameFinished;
            formGame.ShowDialog();
        }

        private void HandleGameFinished(eGameStatus i_GameStatus)
        {
            formGame.GenerateFinishMsg(i_GameStatus, r_GameEngine.CurrentPlayer);
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
            if (formGame == null)
            {
                formGame = new FormGame();
                formGame.ButtonClicked += HandleButtonClicked;
                formGame.ComputerMoved += HandleComputerMoved;
            }
            formGame.CreateGameBoard((int)r_GameEngine.GameBoard.Size);
            UpdatePlayerNamesAndScores();
        }

        private eSymbols HandleButtonClicked(Cell cell)
        {
            r_GameEngine.MakeMove(cell);

            if (r_GameEngine.Player2.IsComputer && r_GameEngine.GameStatus == eGameStatus.InProgress)
            {
                r_GameEngine.MakeRandomMove();
            }

            formGame.UpdatePlayerNamesAndScores(r_GameEngine.Player1.Name, r_GameEngine.Player1.Score, r_GameEngine.Player2.Name, r_GameEngine.Player2.Score, r_GameEngine.CurrentPlayer == r_GameEngine.Player1);

            return r_GameEngine.CurrentPlayer.Symbol;
        }

        private void HandleMoveMade(Cell cell)
        {
            formGame.UpdateCell(cell, r_GameEngine.CurrentPlayer.Symbol);
            UpdatePlayerNamesAndScores();
        }

        private void HandleComputerMoved(Cell cell)
        {
            UpdatePlayerNamesAndScores();
        }


        private void UpdatePlayerNamesAndScores()
        {
            string player1Name = r_GameEngine.Player1.Name;
            uint player1Score = r_GameEngine.Player1.Score;
            string player2Name = r_GameEngine.Player2.Name;
            uint player2Score = r_GameEngine.Player2.Score;
            bool isPlayer1Turn = r_GameEngine.CurrentPlayer == r_GameEngine.Player1;

            formGame.UpdatePlayerNamesAndScores(
                player1Name,
                player1Score,
                player2Name,
                player2Score,
                isPlayer1Turn);
        }
    }
}
