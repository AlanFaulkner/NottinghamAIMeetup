using System;
using System.Collections.Generic;
using System.IO;

namespace NeuralNetwork
{
    public class ANeuralNetwork
    {
        public Random RND = new Random(1);
        public List<int> NetworkDescription { get; set; } = new List<int> { };                 // Definines the number of neurons in each layer
        public List<List<Neuron>> NeuralNetwork { get; set; } = new List<List<Neuron>> { };    // Holds the information about each neuron in the network

        // Main Functions
        public void Create_Network(List<int> Network_Description)
        {
            Dispose_Of_Network();

            NetworkDescription = Network_Description;

            // Check loaded data validity
            if (NetworkDescription.Count < 2)
            {
                throw new Exception("The description of neural net you have entered is not valid!\n\nA valid description must contain at least two values:\n   The number of inputs into the network\n   The number of output neurons\n\n In both cases the minimum value allowed is 1!\n\n");
            }

            for (int Layer = 0; Layer < NetworkDescription.Count; Layer++)
            {
                if (NetworkDescription[Layer] < 1)
                {
                    throw new Exception("The description of neural net you have entered is not valid!\n\nA valid description must contain at least two values:\n   The number of inputs into the network\n   The number of output neurons\n\n In both cases the minimum value allowed is 1!\n\n");
                }
            }

            // Build network
            BuildNetwork();

            return;
        }

        public void Save_Network(string Filename)
        {
            using (System.IO.StreamWriter Out = new System.IO.StreamWriter("../../" + Filename, false))
            {
                // Save Network Description
                for (int Layer = 0; Layer < NetworkDescription.Count; Layer++)
                {
                    Out.Write(NetworkDescription[Layer] + Environment.NewLine);
                }

                Out.Write("Data" + Environment.NewLine);

                // Save Weights
                for (int Layer = 0; Layer < NeuralNetwork.Count; Layer++)
                {
                    for (int Neuron = 0; Neuron < NeuralNetwork[Layer].Count; Neuron++)
                    {
                        for (int Weight = 0; Weight < NeuralNetwork[Layer][Neuron].Weights.Count; Weight++)
                        {
                            Out.Write(NeuralNetwork[Layer][Neuron].Weights[Weight] + Environment.NewLine);
                        }
                    }
                }
            }
        }

        public void Load_Network(string Filename)
        {
            // Delete any existing network information
            Dispose_Of_Network();

            // Check File Exisits
            if (!File.Exists(@"../../" + Filename)) { throw new Exception("File does not exist!"); }
            else
            {
                StreamReader FileStream = new StreamReader(@"../../" + Filename);
                LoadNetworkDescription(FileStream);
                LoadNetworkWeights(FileStream);
                FileStream.Close();
            }
        }

        public List<List<double>> Load_Data(string Filename)
        {
            List<List<double>> LoadedData = new List<List<double>> { };

            if (!File.Exists(@"../../" + Filename)) { throw new Exception("File does not exist!"); }
            else
            {
                string[] Lines = File.ReadAllLines(@"../../" + Filename);
                foreach (string line in Lines)
                {
                    List<double> LoadedDataRow = new List<double> { };

                    string[] Col = line.Split(new char[] { ',' });
                    foreach (string item in Col)
                    {
                        LoadedDataRow.Add(Convert.ToDouble(item));
                    }
                    LoadedData.Add(LoadedDataRow);
                }
                Console.WriteLine("Data loaded complete");
                Console.WriteLine("Data consists of " + LoadedData.Count + " rows each containing " + LoadedData[0].Count + " elements.");
            }
            return LoadedData;
        }

        public List<List<List<double>>> SplitDataSet(List<List<double>> SourceData, int ChunkSize)
        {
            // Splits source data into two sets - use one for trianing one for validation
            // Returns split data in list of format {training inputs}, {trainging outputs}, {validation inputs}, {validation outputs};

            if (ChunkSize == 0) { throw new Exception("Training data size can not evaluate to 0!"); }
            else if (ChunkSize > SourceData.Count) { throw new Exception("Training data size can not exceed source data size!"); }
            else
            {
                List<List<List<double>>> Output = SeperateTrainingFromValidation(SourceData, ChunkSize);
                return Output;
            }
        }

        public List<double> Get_Network_Output(List<double> Data)
        {
            // version 1.0 only calculates a single data set output
            List<double> Input_Data = new List<double> { };
            List<double> Output = new List<double> { };
            Input_Data.AddRange(Data); // sets values of the new input data list to be the same as they are in data. note using = just makes a pointer
            Input_Data.Add(1); // default input for the bias

            for (int Layer = 0; Layer < NeuralNetwork.Count; Layer++)
            {
                if (Layer == NeuralNetwork.Count - 1)
                {
                    for (int Neuron = 0; Neuron < NeuralNetwork[Layer].Count; Neuron++)
                    {
                        // iterate through input data and multiple by the weights for each neuron and store the sum
                        // apply activation function to sum
                        // set neuron output to result.

                        double Sum = 0;
                        for (int InputValue = 0; InputValue < Input_Data.Count; InputValue++)
                        {
                            NeuralNetwork[Layer][Neuron].Inputs[InputValue] = Input_Data[InputValue];
                            Sum += NeuralNetwork[Layer][Neuron].Weights[InputValue] * Input_Data[InputValue];
                        }

                        Sum = Activation_Function(Sum, false);      // apply desired activation function
                        NeuralNetwork[Layer][Neuron].Output = Sum;  // set the output value for neuron
                        Output.Add(Sum);
                    }
                }
                else
                {
                    for (int Neuron = 0; Neuron < NeuralNetwork[Layer].Count; Neuron++)
                    {
                        // iterate through input data and multiple by the weights for each neuron and store the sum
                        // apply activation function to sum

                        double Sum = 0;
                        for (int InputValue = 0; InputValue < Input_Data.Count; InputValue++)
                        {
                            NeuralNetwork[Layer][Neuron].Inputs[InputValue] = Input_Data[InputValue];
                            Sum += NeuralNetwork[Layer][Neuron].Weights[InputValue] * Input_Data[InputValue];
                        }

                        Sum = Activation_Function(Sum, false);      // apply desired activation function
                        NeuralNetwork[Layer][Neuron].Output = Sum;  // set the output value for neuron
                    }

                    // clear current input data and refill it with the outputs from previous layer to generate inputs into next layer.
                    Input_Data.Clear();
                    for (int j = 0; j < NeuralNetwork[Layer].Count; j++)
                    {
                        Input_Data.Add(NeuralNetwork[Layer][j].Output);
                    }
                    Input_Data.Add(1); // Bias input
                }
            }

            return Output;
        }

        public void Train_MiniBatch(List<List<double>> Training_Data, List<List<double>> Target_Data, int BatchSize, string OutputFilename, double TrainingRate = 0.01, double Momentum = 0.7, double TotalNumberOfEpochs = 1e6, double TargetError = 1e-8, bool Print = true)
        {
            int Epochs = 0;
            double MSENetworkError = 0;
            using (StreamWriter OutputFileStream = new StreamWriter("../../" + OutputFilename, false))
            {
                do
                {
                    int RndSeed = RND.Next();
                    ShuffleTrainingData(Training_Data, RndSeed);
                    ShuffleTrainingData(Target_Data, RndSeed);

                    // build training batch from training data
                    List<List<double>> TrainingBatchInputs = new List<List<double>> { };
                    List<List<double>> TrainingBatchTargets = new List<List<double>> { };

                    for (int DataElement = 0; DataElement < BatchSize; DataElement++)
                    {
                        TrainingBatchInputs.Add(Training_Data[DataElement]);
                        TrainingBatchTargets.Add(Target_Data[DataElement]);
                    }

                    MSENetworkError = BackpropagationAlgorithm(TrainingBatchInputs, TrainingBatchTargets, TrainingRate, Momentum);

                    if (Epochs % 1000 == 0 && Print == true)
                    {
                        Console.Write("Epochs = " + Epochs + "    Error = " + MSENetworkError + Environment.NewLine);
                        OutputFileStream.WriteLine("Epochs = " + Epochs + "    Error = " + MSENetworkError);
                    }
                    Epochs++;
                }
                while (Epochs <= TotalNumberOfEpochs && MSENetworkError > TargetError);
            }
        }

        public void Data_Validation(List<List<double>> ValadationInputs, List<List<double>> ValadationOutputs)
        {
            // Assesss a vaildation set of data on a pre trainined network
            // Outputs are rounded to 0 decimal places - this is to mainly be used for classification.

            int ElementPassed = 0;
            double MSENetworkError = 0;

            for (int DataElement = 0; DataElement < ValadationInputs.Count; DataElement++)
            {
                Get_Network_Output(ValadationInputs[DataElement]);
                MSENetworkError += CalculateErrorForElement(ValadationOutputs[DataElement]);

                List<double> NetworkOutput = new List<double> { };
                for (int Neuron = 0; Neuron < ValadationOutputs.Count; Neuron++)
                {
                    NetworkOutput.Add(Math.Round(NeuralNetwork[NeuralNetwork.Count - 1][Neuron].Output, 0));
                }
                if (NetworkOutput.Equals(ValadationOutputs[DataElement])) { ElementPassed++; }
            }

            MSENetworkError *= (1.0 / (2.0 * ValadationOutputs.Count));
            Console.WriteLine("Network Error: " + MSENetworkError);
            Console.WriteLine("Data Elements Passed: " + ElementPassed + " / " + ValadationOutputs.Count);
        }

        public void Dispose_Of_Network()
        {
            NetworkDescription.Clear();
            NeuralNetwork.Clear();
        }
        // Support functions

        private void LoadNetworkDescription(StreamReader FileStream)
        {
            string line;
            while ((line = FileStream.ReadLine()) != null && line != "Data")
            {
                // convert string to int
                int x;
                Int32.TryParse(line, out x);
                NetworkDescription.Add(x);
            }

            // Check loaded data validity
            if (NetworkDescription.Count < 2)
            {
                throw new Exception("The description of neural net you have entered is not valid!\n\nA valid description must contain at least two values:\n   The number of inputs into the network\n   The number of output neurons\n\n In both cases the minimum value allowed is 1!\n\n");
            }

            for (int Layer = 0; Layer < NetworkDescription.Count; Layer++)
            {
                if (NetworkDescription[Layer] < 1)
                {
                    throw new Exception("The description of neural net you have entered is not valid!\n\nA valid description must contain at least two values:\n   The number of inputs into the network\n   The number of output neurons\n\n In both cases the minimum value allowed is 1!\n\n");
                }
            }
        }

        private void LoadNetworkWeights(StreamReader FileStream)
        {
            string Line;
            for (int Layer = 1; Layer < NetworkDescription.Count; Layer++)
            {
                List<Neuron> LayerData = new List<Neuron>() { };
                for (int Neuron = 0; Neuron < NetworkDescription[Layer]; Neuron++)
                {
                    if (Layer == 1)
                    {
                        Neuron neuron = new Neuron(NetworkDescription[0], 0);
                        for (int Weight = 0; Weight < neuron.Weights.Count; Weight++)
                        {
                            double x;
                            double.TryParse((Line = FileStream.ReadLine()), out x);
                            neuron.Weights[Weight] = x;
                        }
                        LayerData.Add(neuron);
                    }
                    else
                    {
                        Neuron neuron = new Neuron(NetworkDescription[Layer - 1], 0);
                        for (int z = 0; z < neuron.Weights.Count; z++)
                        {
                            double x;
                            double.TryParse((Line = FileStream.ReadLine()), out x);
                            neuron.Weights[z] = x;
                        }
                        LayerData.Add(neuron);
                    }
                }
                NeuralNetwork.Add(LayerData);
            }
        }

        private void BuildNetwork()
        {
            Random Rnd = new Random(1);

            for (int CurrentLayer = 1; CurrentLayer < NetworkDescription.Count; CurrentLayer++)
            {
                List<Neuron> LayerDescription = new List<Neuron>() { };
                for (int Neuron = 0; Neuron < NetworkDescription[CurrentLayer]; Neuron++)
                {
                    if (CurrentLayer == 1)
                    {
                        Neuron neuron = new Neuron(NetworkDescription[0], Rnd.Next());
                        LayerDescription.Add(neuron);
                    }
                    else
                    {
                        Neuron neuron = new Neuron(NetworkDescription[CurrentLayer - 1], Rnd.Next());
                        LayerDescription.Add(neuron);
                    }
                }

                NeuralNetwork.Add(LayerDescription);
            }
        }

        private void CalculateGradient(List<double> TargetOutputData, int TrainingElement, double TrainingRate)
        {
            CalculateGradientOutputLayer(TargetOutputData, TrainingElement, TrainingRate);
            CalculateGradientHiddenLayers(TrainingElement, TrainingRate);
        }

        private void CalculateGradientOutputLayer(List<double> TargetOutputData, int TrainingElement, double TrainingRate)
        {
            for (int Neuron = 0; Neuron < TargetOutputData.Count; Neuron++)
            {
                NeuralNetwork[NeuralNetwork.Count - 1][Neuron].Error *= Activation_Function(NeuralNetwork[NeuralNetwork.Count - 1][Neuron].Output, true);

                for (int Weight = 0; Weight < NeuralNetwork[NeuralNetwork.Count - 1][Neuron].WeightUpdate.Count; Weight++)
                {
                    if (TrainingElement == 0) { NeuralNetwork[NeuralNetwork.Count - 1][Neuron].WeightUpdate[Weight] = NeuralNetwork[NeuralNetwork.Count - 1][Neuron].Inputs[Weight] * NeuralNetwork[NeuralNetwork.Count - 1][Neuron].Error * TrainingRate; }
                    else { NeuralNetwork[NeuralNetwork.Count - 1][Neuron].WeightUpdate[Weight] += NeuralNetwork[NeuralNetwork.Count - 1][Neuron].Inputs[Weight] * NeuralNetwork[NeuralNetwork.Count - 1][Neuron].Error * TrainingRate; }
                }
            }
        }

        private void CalculateGradientHiddenLayers(int TrainingElement, double TrainingRate)
        {
            for (int Layer = NeuralNetwork.Count - 2; Layer >= 0; Layer--)
            {
                for (int Neuron = 0; Neuron < NeuralNetwork[Layer].Count; Neuron++)
                {
                    for (int Weight = 0; Weight < NeuralNetwork[Layer + 1].Count; Weight++)
                    {
                        NeuralNetwork[Layer][Neuron].Error += NeuralNetwork[Layer + 1][Weight].Error * NeuralNetwork[Layer + 1][Weight].Weights[Neuron];
                    }
                    NeuralNetwork[Layer][Neuron].Error *= Activation_Function(NeuralNetwork[Layer][Neuron].Output, true);
                    for (int Weight = 0; Weight < NeuralNetwork[Layer][Neuron].WeightUpdate.Count; Weight++)
                    {
                        if (TrainingElement == 0) { NeuralNetwork[Layer][Neuron].WeightUpdate[Weight] = NeuralNetwork[Layer][Neuron].Inputs[Weight] * NeuralNetwork[Layer][Neuron].Error * TrainingRate; }
                        else { NeuralNetwork[Layer][Neuron].WeightUpdate[Weight] += NeuralNetwork[Layer][Neuron].Inputs[Weight] * NeuralNetwork[Layer][Neuron].Error * TrainingRate; }
                    }

                }
            }
        }

        private void UpdateWeights(double Momentum)
        {
            for (int Layer = 0; Layer < NeuralNetwork.Count; Layer++)
            {
                for (int Neuron = 0; Neuron < NeuralNetwork[Layer].Count; Neuron++)
                {
                    for (int Weight = 0; Weight < NeuralNetwork[Layer][Neuron].Weights.Count; Weight++)
                    {
                        NeuralNetwork[Layer][Neuron].Weights[Weight] += NeuralNetwork[Layer][Neuron].WeightUpdate[Weight] + NeuralNetwork[Layer][Neuron].PreviousWeightUpdate[Weight];
                        NeuralNetwork[Layer][Neuron].PreviousWeightUpdate[Weight] = NeuralNetwork[Layer][Neuron].WeightUpdate[Weight] * Momentum;
                    }
                }
            }
        }

        private void ShuffleTrainingData(List<List<double>> InputData, int Seed)
        {
            for (int Item = 0; Item < InputData.Count; Item++)
            {
                Random LocalRND = new Random(Seed);
                int SeletedItem = LocalRND.Next(0, InputData.Count);
                List<double> TempInput = InputData[Item];
                InputData[Item] = InputData[SeletedItem];
                InputData[SeletedItem] = TempInput;
            }
        }

        private double Activation_Function(double X, bool DyDx)
        {
            // Sigmoid function [0 -> 1]
            if (DyDx == true) { return X * (1 - X); }
            else return 1 / (1 + Math.Exp(-X));
        }

        private double BackpropagationAlgorithm(List<List<double>> TrainingData, List<List<double>> TargetData, double TrainingRate, double Momentum)
        {
            double MSENetworkError = 0;

            for (int TrainingInput = 0; TrainingInput < TrainingData.Count; TrainingInput++)
            {
                Get_Network_Output(TrainingData[TrainingInput]);
                MSENetworkError += CalculateErrorForElement(TargetData[TrainingInput]);
                CalculateGradient(TargetData[TrainingInput], TrainingInput, TrainingRate);
            }

            UpdateWeights(Momentum);
            MSENetworkError *= (1.0 / (2.0 * TrainingData.Count));

            return MSENetworkError;
        }

        private double CalculateErrorForElement(List<double> TargetData)
        {
            double ErrorInDataElement = 0;

            for (int Neuron = 0; Neuron < TargetData.Count; Neuron++)
            {
                NeuralNetwork[NeuralNetwork.Count - 1][Neuron].Error = (TargetData[Neuron] - NeuralNetwork[NeuralNetwork.Count - 1][Neuron].Output);
                ErrorInDataElement += NeuralNetwork[NeuralNetwork.Count - 1][Neuron].Error * NeuralNetwork[NeuralNetwork.Count - 1][Neuron].Error;
            }

            return ErrorInDataElement;
        }

        private List<List<List<double>>> SeperateTrainingFromValidation(List<List<double>> SourceData, int TrainingSetSize)
        {
            // Returns split data in list of format {training data}, {validation data};
            List<List<List<double>>> Output = new List<List<List<double>>> { };
            List<List<double>> TrainingSetInput = new List<List<double>> { };
            List<List<double>> TrainingSetOutput = new List<List<double>> { };
            List<List<double>> ValidationSetInput = new List<List<double>> { };
            List<List<double>> ValidationSetOutput = new List<List<double>> { };

            for (int Row = 0; Row < SourceData.Count; Row++)
            {
                if (Row < TrainingSetSize)
                {
                    List<List<double>> SplitResult = SplitInputsFromOutputs(SourceData[Row]);
                    TrainingSetInput.Add(SplitResult[0]);
                    TrainingSetOutput.Add(SplitResult[1]);
                }
                else
                {
                    List<List<double>> SplitResult = SplitInputsFromOutputs(SourceData[Row]);
                    ValidationSetInput.Add(SplitResult[0]);
                    ValidationSetOutput.Add(SplitResult[1]);
                }
            }

            Output.Add(TrainingSetInput);
            Output.Add(TrainingSetOutput);
            Output.Add(ValidationSetInput);
            Output.Add(ValidationSetOutput);

            return Output;
        }

        private List<List<double>> SplitInputsFromOutputs(List<double> SourceDataRow)
        {
            List<List<double>> Output = new List<List<double>> { };
            List<double> InputData = new List<double> { };
            List<double> OutputData = new List<double> { };

            for (int Column = 0; Column < SourceDataRow.Count; Column++)
            {
                if (Column < NetworkDescription[0]) { InputData.Add(SourceDataRow[Column]); }
                else { OutputData.Add(SourceDataRow[Column]); }
            }

            Output.Add(InputData);
            Output.Add(OutputData);

            return Output;
        }
    }
}