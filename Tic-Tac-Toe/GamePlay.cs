using System;
using System.Collections.Generic;

namespace Tic_Tac_Toe
{
    public class GamePlay
    {
        public List<int> Gameboard { get; set; } = new List<int> { };

        public int GameboardColumns { get; set; } = 3;
        public int GameboardRows { get; set; } = 3;

        public enum Player { Player1 = 1, Player2 = -1 };

        public Player CurrentPlayer { get; set; } = Player.Player1;

        public enum Playertype { Human, Random, Easy, Hard, NeuralNet };

        public Playertype Player1Type { get; set; } = Playertype.Human;
        public Playertype Player2Type { get; set; } = Playertype.Human;

        private string Result = "";

        public void SetupGame()
        {
            Gameboard.Clear();
            CurrentPlayer = Player.Player1;
            for (int i = 0; i < (GameboardRows * GameboardColumns); i++)
            {
                Gameboard.Add(0);
            }
        }

        public void SwichPlayers()
        {
            if (CurrentPlayer == Player.Player1) { CurrentPlayer = Player.Player2; }
            else { CurrentPlayer = Player.Player1; }
        }

        public void AIMove(Playertype AIPlayer)
        {
            GameAI AI = new GameAI(GameboardRows, GameboardColumns);

            switch (AIPlayer)
            {
                case (Playertype.Random):
                    AI.RandomPlayer(Gameboard, CurrentPlayer);
                    return;

                case (Playertype.Easy):
                    AI.EasyPlayer(Gameboard, CurrentPlayer);
                    return;

                case (Playertype.Hard):
                    AI.HardPlayer(Gameboard, CurrentPlayer);
                    return;

                case (Playertype.NeuralNet):
                    AI.NeuralNetPlayer(Gameboard, CurrentPlayer);
                    return;

                default:
                    AI.RandomPlayer(Gameboard, CurrentPlayer);
                    return;
            }
        }

        public bool EndGame()
        {      
            List<int> RowTotal = SumRows();
            for (int i = 0; i < GameboardRows; i++)
            {
                if (Math.Abs(RowTotal[i]) == GameboardRows) { Result = "Win"; return true;  }
            }

            List<int> ColumnTotal = SumColumns();
            for (int i = 0; i < GameboardColumns; i++)
            {
                if (Math.Abs(ColumnTotal[i]) == GameboardColumns) { Result = "Win"; return true; }
            }

            if (GameboardColumns == GameboardRows)
            {
                List<int> DiagnalTotal = SumDiagnals();
                if ((Math.Abs(DiagnalTotal[0]) == GameboardColumns) || (Math.Abs(DiagnalTotal[1]) == GameboardColumns)) { Result = "Win"; return true; }
            }

            if (GameDraw()) { Result = "Draw"; return true; }

            return false;
        }
        
        public  string GetGameResult(Player Player)
        {
            if (Result == "Win" && CurrentPlayer != Player) { return "Win"; }
            else if (Result == "Win" && CurrentPlayer == Player) { return "Loose"; }
            else { return "Draw"; }
        }

        // Support functions

        private List<int> SumRows()
        {
            List<int> RowTotal = new List<int> { };
            for (int i = 0; i < GameboardRows; i++)
            {
                int SumOfRow = 0;
                for (int j = 0; j < GameboardColumns; j++)
                {
                    SumOfRow += Gameboard[i * GameboardRows + j];
                }
                RowTotal.Add(SumOfRow);
            }

            return RowTotal;
        }

        private List<int> SumColumns()
        {
            List<int> Vertical = new List<int> { };
            for (int i = 0; i < GameboardColumns; i++)
            {
                int SumOfColumn = 0;
                for (int j = 0; j < GameboardRows; j++)
                {
                    SumOfColumn += Gameboard[i + j * GameboardRows];
                }
                Vertical.Add(SumOfColumn);
            }

            return Vertical;
        }

        private List<int> SumDiagnals()
        {
            List<int> Diagnols = new List<int> { 0, 0 };
            for (int i = 0; i < Gameboard.Count; i += (GameboardRows + 1)) { Diagnols[0] += Gameboard[i]; }

            for (int i = GameboardRows-1; i < Gameboard.Count-1; i += (GameboardRows - 1)) {
                Diagnols[1] += Gameboard[i];
            }

            return Diagnols;
        }

        private bool GameDraw()
        {
            int sum = 0;

            for (int i = 0; i < Gameboard.Count; i++) { if (Gameboard[i] == 0) { sum++; } }

            if (sum == 0) { return true; }
            else { return false; }
        }

        
    }
}