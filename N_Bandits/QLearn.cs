using System;
using System.Collections.Generic;
using System.Linq;

namespace N_Bandits
{
    public class QLearn
    {
        private Random RND = new Random();
        private List<Bandit> BanditList { get; set; } = new List<Bandit> { };

        // Main Functions

        public void GenerateBanditList(int NumberOfBandits)
        {
            for (int bandit = 0; bandit < NumberOfBandits; bandit++)
            {
                Bandit Bnd = new Bandit(bandit);
                BanditList.Add(Bnd);
            }
        }

        public List<double> Play()
        {
            int ChoosenBandit = GeneratedWeighted_RND(GetCurrentWinRates());
            double Payout = BanditList[ChoosenBandit].GetPayOut();

            // Update win rate for chosen bandit
            UpdateBanditWinInfo(ChoosenBandit, Payout, 0.01);

            List<double> NewBestBandit = GetBestBandit();

            return NewBestBandit;
        }

        public void GetActualResults()
        {
            Console.Write(" \n\nResults\n---------\n\nBandit\tActual Probability\tObserved Probability\tNumber of times played\n");
            for (int bandit = 0; bandit < BanditList.Count; bandit++)
            {
                Console.Write(bandit + "\t");
                Console.Write($"{ BanditList[bandit].WinProbability,18}"+"\t");
                Console.Write($"{BanditList[bandit].ObservedWinProbability,18}" + "\t");
                Console.Write(BanditList[bandit].NumberOfTimesPlayed + "\n");
            }
        }

        // Support Functions

        private int GeneratedWeighted_RND(List<double> Data)
        {  
            ApplySoftMax(Data, 0.9);

            double CumulativeProbability = RND.NextDouble();

            for (int DataElement = 0; DataElement < Data.Count; DataElement++)
            {
                if ((CumulativeProbability -= Data[DataElement]) <= 0)
                    return DataElement;
            }

            throw new InvalidOperationException();
        }

        private List<double> GetCurrentWinRates()
        {
            List<double> CurrentWinRates = new List<double> { };
            for (int Bandit = 0; Bandit < BanditList.Count; Bandit++) { CurrentWinRates.Add(BanditList[Bandit].ObservedWinProbability); }
            return CurrentWinRates;
        }

        private void UpdateBanditWinInfo(int Bandit, double Payout, double LearningRate)
        {
            BanditList[Bandit].NumberOfTimesPlayed++;

            // Solving using clumative frequency
            //BanditList[Bandit].ObservedWinProbability += ((Payout - BanditList[Bandit].ObservedWinProbability) / BanditList[Bandit].NumberOfTimesPlayed);

            // Qlearn
            BanditList[Bandit].ObservedWinProbability = BanditList[Bandit].ObservedWinProbability + LearningRate * (Payout - BanditList[Bandit].ObservedWinProbability);
        }

        private List<double> GetBestBandit()
        {
            List<double> WinRates = GetCurrentWinRates();
            List<double> BestBandit = new List<double> { };
            BestBandit.Add(WinRates.IndexOf(WinRates.Max()));
            BestBandit.Add(WinRates.Max());

            return BestBandit;
        }

        public void ApplySoftMax(List<double> Data, double Tau)
        {
            double Sum = 0;

            for (int DataElement = 0; DataElement < Data.Count; DataElement++)
            {
                Sum += Math.Exp(Data[DataElement] / Tau);
            }

            for (int DataElement = 0; DataElement < Data.Count; DataElement++)
            {
                Data[DataElement] = Math.Exp(Data[DataElement] / Tau) / Sum;
            }
        }
    }
}