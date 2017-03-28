using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    public class Neuron
    {
        // properties
        public List<double> Weights { get; set; } = new List<double> { };

        public double Output { get; set; } = 0;

        public List<double> Inputs { get; set; } = new List<double> { };

        public double Error { get; set; } = 0;

        public List<double> WeightUpdate { get; set; } = new List<double> { };

        public List<double> PreviousWeightUpdate { get; set; } = new List<double> { };

        // Constructor
        public Neuron(int Number_Of_Connections, int Seed)
        {
            for (int i = 0; i < Number_Of_Connections; i++)
            {
                Weights.Add(Random_GaussianDist(Seed + i, 0, 1));
                Inputs.Add(0);
                WeightUpdate.Add(0);
                PreviousWeightUpdate.Add(0);
            }

            // Add additional value to represent bias
            Weights.Add(0.1);
            Inputs.Add(0);
            WeightUpdate.Add(0);
            PreviousWeightUpdate.Add(0);
        }

        // Generate a random number with a gaussian distribution
        // from stack exchange - http://stackoverflow.com/questions/218060/random-gaussian-variables
        private double Random_GaussianDist(int Seed, double mean, double stdDev)
        {
            Random rand = new Random(Seed); //reuse this if you are generating many
            double u1 = 1.0 - rand.NextDouble(); //uniform(0,1] random doubles
            double u2 = 1.0 - rand.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                         Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            double randNormal =
                         mean + stdDev * randStdNormal; //random normal(mean,stdDev^2)
            return randNormal;
        }
    }
}