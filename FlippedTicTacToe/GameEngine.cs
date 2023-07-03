﻿using System;
using System.Collections.Generic;

namespace FlippedTicTacToe
{
    public class GameEngine
    {
        private const int k_MinBoardSize = 4;
        private const int k_MaxBoardSize = 10;
        private GameBoard m_Board = null;
        private Player m_Player1 = null;
        private Player m_Player2 = null;
        private Player m_CurrentPlayer = null;
        private eGameStatus m_GameStatus = eGameStatus.InProgress;

        public event Action<Cell> MoveMade;

        public event Action<eGameStatus> GameFinished;

        public Player CurrentPlayer
        {
            get
            {
                return m_CurrentPlayer;
            }
        }

        public Player Player1
        {
            get
            {
                return m_Player1;
            }
        }

        public Player Player2
        {
            get
            {
                return m_Player2;
            }
        }

        public eGameStatus GameStatus
        {
            get
            {
                return m_GameStatus;
            }
        }

        public GameBoard GameBoard
        {
            get
            {
                return m_Board;
            }
        }

        public void SetGameBoardSize(uint i_BoardSize)
        {
            if (isBoardSizeValid(i_BoardSize))
            {
                m_Board = new GameBoard(i_BoardSize);
            }
            else
            {
                throw new ArgumentException($"Board size must be in range of {k_MinBoardSize}-{k_MaxBoardSize} (inclusive)!");
            }
        }

        public void SetFirstPlayer(string i_PlayerName)
        {
            m_Player1 = new Player(eSymbols.X, false, i_PlayerName);
            m_CurrentPlayer = m_Player1;
        }

        public void SetSecondPlayer(bool i_IsComputer, string i_PlayerName)
        {
            m_Player2 = new Player(eSymbols.O, i_IsComputer, i_PlayerName);
        }

        public void ForfeitCurrPlayer()
        {
            checkWhoLostAndUpdateGameSettings();
        }

        public void RestartGame()
        {
            m_Board.ResetBoard();
            initializeGameStatus();
            m_CurrentPlayer = m_Player1;
        }

        private void initializeGameStatus()
        {
            m_GameStatus = eGameStatus.InProgress;
        }

        public void MakeMove(Cell i_SelectedCell)
        {
            bool isCellEmpty = m_Board.CheckIfCellIsEmpty(i_SelectedCell);

            if(!isCellEmpty)
            {
                throw new ArgumentException("The specified cell is already taken");
            }

            m_Board.UpdateCell(i_SelectedCell, m_CurrentPlayer.Symbol);
            MoveMade?.Invoke(i_SelectedCell);
            updateGameStatusAndScoreIfNeeded(i_SelectedCell);
            switchCurrentPlayer();
            if(GameStatus != eGameStatus.InProgress)
            {
                GameFinished?.Invoke(GameStatus);
            }
        }

        public void MakeRandomMove()
        {
            List<Cell> availableCells = m_Board.GetAllAvailableCells();
            Cell selectedCell = selectRandomCellFromList(availableCells);

            m_Board.UpdateCell(selectedCell, m_CurrentPlayer.Symbol);
            MoveMade?.Invoke(selectedCell);
            updateGameStatusAndScoreIfNeeded(selectedCell);
            switchCurrentPlayer();
            if (GameStatus != eGameStatus.InProgress)
            {
                GameFinished?.Invoke(GameStatus);
            }
        }

        private static Cell selectRandomCellFromList(List<Cell> i_CellsList)
        {
            Random rand = new Random();
            int randomListItemIndex = rand.Next(i_CellsList.Count);

            return i_CellsList[randomListItemIndex];
        }

        private void updateGameStatusAndScoreIfNeeded(Cell i_SelectedCell)
        {
            bool isCurrentPlayerLoose = checkIfCurrentPlayerLoose(i_SelectedCell);
            bool isBoardFull = m_Board.IsBoardFull();

            if (isCurrentPlayerLoose)
            {
                checkWhoLostAndUpdateGameSettings();
            }
            else if (isBoardFull)
            {
                m_GameStatus = eGameStatus.Draw;
            }
        }

        private void checkWhoLostAndUpdateGameSettings()
        {
            if (m_CurrentPlayer == m_Player1)
            {
                m_GameStatus = eGameStatus.Player2Win;
                m_Player2.Score++;
            }
            else
            {
                m_GameStatus = eGameStatus.Player1Win;
                m_Player1.Score++;
            }
        }

        private void switchCurrentPlayer()
        {
            if (m_CurrentPlayer == m_Player1)
            {
                m_CurrentPlayer = m_Player2;
            }
            else
            {
                m_CurrentPlayer = m_Player1;
            }
        }

        private bool checkIfCurrentPlayerLoose(Cell i_SelectedCell)
        {
            bool isSingleSymbolFullSequenceFound = 
                m_Board.CheckForSingleSymbolFullSequenceInRow(i_SelectedCell.Row, m_CurrentPlayer.Symbol) ||
                m_Board.CheckForSingleSymbolFullSequenceInColumn(i_SelectedCell.Row, m_CurrentPlayer.Symbol) ||
                m_Board.CheckForSingleSymbolFullSequenceInDiagonal(i_SelectedCell, m_CurrentPlayer.Symbol);

            return isSingleSymbolFullSequenceFound;
        }

        private static bool isBoardSizeValid(uint i_BoardSize)
        {
            return i_BoardSize >= k_MinBoardSize && i_BoardSize <= k_MaxBoardSize;
        }

    }
}
