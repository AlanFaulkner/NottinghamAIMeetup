using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    class Program
    {
        static void Main(string[] args)
        {
            // Typical use of ANeuralNetwork class

            ANeuralNetwork ANN = new ANeuralNetwork();

            // Either create a new network or load exisiting network
            ANN.Create_Network(new List<int> { 2, 3, 1 });

            // Load in training data
            // The data here was generated in excel. It contains two random input values between 0 and 1 and one output value.
            // The data represent an XOR gate. values above 0.5 are considered as 1 and below 0.5 as 0.
            List<List<double>> LoadedData = ANN.Load_Data("XORData.csv");

            // Split the training data into [training inputs], [Training outputs], [Validaton Input], [Validation Outputs]
            List<List<List<double>>> SplitData = ANN.SplitDataSet(LoadedData, 750);

            // Training
            ANN.Train_MiniBatch(SplitData[0], SplitData[1], 20, "XORDataTrainingResults.txt", 0.1, 0.9);

            // Validate network
            ANN.Data_Validation(SplitData[2], SplitData[3]);
        }
    }
}
