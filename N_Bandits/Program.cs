using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace N_Bandits
{
    class Program
    {       
        static void Main(string[] args)
        {
            int NumberOfBandits = 10;
            int NumberOfEpochs = 100000;

            QLearn NBandits = new QLearn();
            NBandits.GenerateBanditList(NumberOfBandits);

            for (int Epoch = 0; Epoch < NumberOfEpochs; Epoch++)
            {
                List<double> BestBandit = NBandits.Play();

                if (Epoch % 100 == 0) { Console.WriteLine("Epoch: " + Epoch + "\tBest bandit: " + BestBandit[0] + "\t Win probility: " + BestBandit[1]); }
            }

            NBandits.GetActualResults();
        }
    }
}
