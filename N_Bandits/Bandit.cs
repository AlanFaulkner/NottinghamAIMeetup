using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Bandits
{
    public class Bandit
    {        
        public int NumberOfTimesPlayed { get; set; } = 0;
        public double ObservedWinProbability { get; set; } = 0; 
        public double WinProbability { get; set; }

        public Bandit(int Seed)
        {
            Random Rnd = new Random(Seed);
            WinProbability = Rnd.NextDouble();
        }

        public double GetPayOut()
        {
            double Reward = 0;
            Random rnd = new Random();
            for (int i = 0; i < 10; i++)
            {
                if (rnd.NextDouble() < WinProbability) { Reward++; }
            }
            return Reward/10.0;
        }
    }
}
