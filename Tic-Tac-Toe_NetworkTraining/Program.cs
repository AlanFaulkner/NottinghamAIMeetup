using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tic_Tac_Toe_NetworkTraining
{
    public class Program
    {
        public static Tic_Tac_Toe.GamePlay Game = new Tic_Tac_Toe.GamePlay();
        public static Tic_Tac_Toe.Qlearn QlearningPlayer = new Tic_Tac_Toe.Qlearn();

        static void Main(string[] args)
        {
            Game.Player2Type = Tic_Tac_Toe.GamePlay.Playertype.Random;
            QlearningPlayer.CreateNetwork(new List<int> { 9, 18, 9 });
            QlearningPlayer.SaveNetwork("Inital.net");
            for (int GameNumber = 1; GameNumber <= 10000; GameNumber++)
            {
                Game.SetupGame();
                Game.CurrentPlayer = Tic_Tac_Toe.GamePlay.Player.Player1;

                do
                {
                    if (Game.CurrentPlayer == Tic_Tac_Toe.GamePlay.Player.Player2) { Game.AIMove(Game.Player2Type); }
                    else
                    {
                        Game.Gameboard = QlearningPlayer.MakeMove(Game.Gameboard, (int)Game.CurrentPlayer, true);
                    }
                    Game.SwichPlayers();

                } while (Game.EndGame() == false);

                QlearningPlayer.UpdateEpisodeDataOutputs(Game.GetGameResult(Tic_Tac_Toe.GamePlay.Player.Player2),0.01, 0.9);
                QlearningPlayer.UpdateNetwork();

                if ((double)GameNumber % 100 == 0)
                {
                    QlearningPlayer.SaveNetwork("TrainingAttemp1.net");
                    Console.WriteLine("Number of Games Complete: " + GameNumber + "\n");
                }
            }
        }

    }    
}
