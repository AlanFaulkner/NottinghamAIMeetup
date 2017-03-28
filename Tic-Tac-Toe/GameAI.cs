using System;
using System.Collections.Generic;
using System.Linq;

namespace Tic_Tac_Toe
{
    public class GameAI 
    {
        private int NumberOfRows = 3; // Allows AI to play on boards of various sizes.
        private int NumberOfColumns = 3;
        private Random RND = new Random(1);
        public Qlearn QlearningPlayer = new Qlearn();

        public GameAI(int Rows, int Columns)
        {
            NumberOfRows = Rows;
            NumberOfColumns = Columns;
        }

        public void RandomPlayer(List<int> Gameboard, GamePlay.Player player)
        {
            int Move;

            do
            {
                Move = RND.Next(0, Gameboard.Count);
            }
            while (Gameboard[Move] != 0);
            
            Gameboard[Move] = (int)player;
        }

        public void EasyPlayer(List<int> Gameboard, GamePlay.Player player)
        {
            if (RowMove(Gameboard, (int)player)) { return; }
            else if (ColumnMove(Gameboard, (int)player)) { return; }
            else if (NumberOfRows == NumberOfColumns)
            {
                if (DiagnalMove(Gameboard, (int)player)) { return; }
                else { RandomPlayer(Gameboard, player); }
            }
            else { RandomPlayer(Gameboard, player); }
        }

        public void HardPlayer(List<int> Gameboard, GamePlay.Player player)
        {
            if ((int)player == 1 && Gameboard[0] == 0) { Gameboard[0] = 1; return; }
            else if ((int)player == -1 && Gameboard[4] == 0) { Gameboard[4] = -1; return; }
            else { EasyPlayer(Gameboard, player); }
        }

        public void NeuralNetPlayer(List<int> Gameboard, GamePlay.Player player)
        {
            QlearningPlayer.LoadNetwork("Tic-Tac-Toe.net");
            List<int> NewGameboard = QlearningPlayer.MakeMove(Gameboard, (int)player);
            Gameboard.Clear();
            Gameboard.AddRange(NewGameboard);
        }

        // Assess rows and prefencially select a winning move over a blocking move
        private bool RowMove(List<int> Gameboard, int player)
        {
            List<int> RowScores = new List<int> { };
            for (int Row = 0; Row < NumberOfRows; Row++)
            {
                int SumOfRow = 0;
                for (int Column = 0; Column < NumberOfColumns; Column++)
                {
                    SumOfRow += Gameboard[Row * NumberOfRows + Column];
                }
                RowScores.Add(SumOfRow);
            }

            if (RowMoveWin(Gameboard, RowScores, player)) { return true; }
            else if (RowMoveBlock(Gameboard, RowScores, player)) { return true; }
            else { return false; }
        }

        private bool RowMoveWin(List<int> Gameboard, List<int> RowScores, int Player)
        {
            for (int Row = 0; Row < NumberOfRows; Row++)
            {
                if (RowScores[Row] == Player * 2)
                {
                    for (int Column = 0; Column < NumberOfColumns; Column++)
                    {
                        if (Gameboard[Row * NumberOfRows + Column] == 0)
                        {
                            Gameboard[Row * NumberOfRows + Column] = Player;
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private bool RowMoveBlock(List<int> Gameboard, List<int> RowScores, int Player)
        {
            for (int Row = 0; Row < NumberOfRows; Row++)
            {
                if (RowScores[Row] == -Player * 2)
                {
                    for (int Column = 0; Column < NumberOfColumns; Column++)
                    {
                        if (Gameboard[Row * NumberOfRows + Column] == 0)
                        {
                            Gameboard[Row * NumberOfRows + Column] = Player;
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        // Assess columns make a winning move over a blocking move
        private bool ColumnMove(List<int> Gameboard, int player)
        {
            List<int> ColumnScores = new List<int> { };
            for (int Column = 0; Column < NumberOfColumns; Column++)
            {
                int SumOfColumn = 0;
                for (int Row = 0; Row < NumberOfRows; Row++)
                {
                    SumOfColumn += Gameboard[Column + Row * NumberOfRows];
                }
                ColumnScores.Add(SumOfColumn);
            }

            if (ColumnMoveWin(Gameboard, ColumnScores, player)) { return true; }
            else if (ColumnMoveBlock(Gameboard, ColumnScores, player)) { return true; }
            else { return false; }
        }

        private bool ColumnMoveWin(List<int> Gameboard, List<int> ColumnScores, int Player)
        {
            for (int Column = 0; Column < NumberOfColumns; Column++)
            {
                if (ColumnScores[Column] == Player * 2)
                {
                    for (int Row = 0; Row < NumberOfRows; Row++)
                    {
                        if (Gameboard[Column + Row * NumberOfRows] == 0)
                        {
                            Gameboard[Column + Row * NumberOfRows] = Player;
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private bool ColumnMoveBlock(List<int> Gameboard, List<int> ColumnScores, int Player)
        {
            for (int Column = 0; Column < NumberOfColumns; Column++)
            {
                if (ColumnScores[Column] == -Player * 2)
                {
                    for (int Row = 0; Row < NumberOfRows; Row++)
                    {
                        if (Gameboard[Column + Row * NumberOfRows] == 0)
                        {
                            Gameboard[Column + Row * NumberOfRows] = Player;
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        private bool DiagnalMove(List<int> Gameboard, int player)
        {
            int LeftDiagnoal = 0;
            int RightDiagnoal = 0;
            for (int i = 0; i < Gameboard.Count; i += (NumberOfRows + 1)) { LeftDiagnoal += Gameboard[i]; }
            for (int i = NumberOfRows; i < Gameboard.Count; i += (NumberOfRows - 1)) { RightDiagnoal += Gameboard[i]; }

            if (Math.Abs(LeftDiagnoal) == 2)
            {
                for (int i = 0; i < Gameboard.Count; i += (NumberOfRows + 1))
                {
                    if (Gameboard[i] == 0)
                    {
                        Gameboard[i] = player;
                        return true;
                    }
                }
            }
            else if (Math.Abs(RightDiagnoal) == 2)
            {
                for (int i = 0; i < Gameboard.Count; i += (NumberOfRows + 1))
                {
                    if (Gameboard[i] == 0)
                    {
                        Gameboard[i] = player;
                        return true;
                    }
                }
            }

            return false;
        }
    }
}