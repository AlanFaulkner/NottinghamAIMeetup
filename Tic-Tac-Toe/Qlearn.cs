using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NeuralNetwork;

namespace Tic_Tac_Toe
{
    public class Qlearn
    {
        private Random RND = new Random(1);
        private ANeuralNetwork ANN = new ANeuralNetwork();
        public List<List<double>> EpisodeData = new List<List<double>> { };        

        // Usfule functions for interacting with NeuralNet

        public void CreateNetwork(List<int> Description)
        {
            ANN.Create_Network(Description);
        }

        public void LoadNetwork(string Filename)
        {
            ANN.Load_Network(Filename);
        }

        public void SaveNetwork(string Filename)
        {
            ANN.Save_Network(Filename);
        }

        // Main Functions               

        public List<int> MakeMove(List<int> GameState, int Player, bool Training=false)
        {
            // Get all vaild moves
            List<double> GameBoard = GameState.Select<int, double>(i => i).ToList();
            NormaliseData(GameBoard);
            List<double> MoveList = ANN.Get_Network_Output(GameBoard);

            // Remove impossible moves
            for (int Move = 0; Move < MoveList.Count; Move++) { if (GameState[Move] != 0) { MoveList[Move] = 0; } }

            // Select move
            int SelectedMove;

            if (Training == true)
            {
                //SelectedMove = MakeRandomMove(MoveList); // returns random move -> un directed search for optimal move
                //SelectedMove = MoveList.IndexOf(MoveList.Max()); // Selects only best move -> limits seach for optimal moves
                SelectedMove = SelectMoveBasedOnProbability(MoveList); // Employs sofmax function to choose move (only applies softmax to non 0 elements of MoveList). -> randomly selects move based on probility distrubution

                // Save move and score to episodedata to retrain network later
                List<double> ActionRow = new List<double> { };
                ActionRow.AddRange(GameBoard);
                ActionRow.AddRange(MoveList);
                ActionRow.Add((SelectedMove + 9));
                EpisodeData.Add(ActionRow);
            }

            else
            {
                SelectedMove = MoveList.IndexOf(MoveList.Max()); // Selects only best move -> limits seach for optimal moves
            }            

            // build resulting game board
            List<int> NewGameBoard = new List<int> { };
            for (int Square = 0; Square < GameBoard.Count; Square++) {
                if (Square == SelectedMove) { NewGameBoard.Add(Player); }
                else { NewGameBoard.Add(Convert.ToInt32(GameState[Square])); }
            }

            return NewGameBoard;
        }

        public void UpdateEpisodeDataOutputs(string Result, double LearningRate = 0.5, double DiscountFactor = 0.5)
        {
            // Set rewards for terminal states
            double Reward;
            if (Result == "Win") { Reward = 10; }
            else if (Result == "Loose") { Reward = -20; }
            else { Reward = 5; }

            // update terminal state
            int MoveMade = Convert.ToInt32(EpisodeData[EpisodeData.Count - 1][EpisodeData[EpisodeData.Count - 1].Count - 1]);
            double UpdatedScore = -1 * Math.Log(1 + 1.0 / EpisodeData[EpisodeData.Count-1][MoveMade]);
            UpdatedScore = UpdatedScore+LearningRate*(Reward-UpdatedScore);
            EpisodeData[EpisodeData.Count - 1][MoveMade] = 1.0 / (1 + Math.Exp(-UpdatedScore));

            // update non terminal states
            for (int i = EpisodeData.Count - 2; i >= 0; i--)
            {
                MoveMade = Convert.ToInt32(EpisodeData[i][EpisodeData[i].Count - 1]);
                double Max = 0;
                for (int Score = 9; Score < 18; Score++) { if (Max < EpisodeData[i+1][Score]) Max = EpisodeData[i+1][Score]; }
                Max = -Math.Log((1 / Max) - 1);
                UpdatedScore = -1 * Math.Log(1 + 1.0 / EpisodeData[i][MoveMade]);
                UpdatedScore = UpdatedScore+LearningRate * (DiscountFactor*Max-UpdatedScore);
                EpisodeData[i][MoveMade] = 1.0 / (1 + Math.Exp( - UpdatedScore));
            }

            // Remove referecnce for move made from episode data
            for (int row = 0; row < EpisodeData.Count; row++) { EpisodeData[row].RemoveAt(EpisodeData[row].Count-1); }
        }

        public void UpdateNetwork()
        {
            List<List<double>> TrainingInputs = new List<List<double>> { };
            List<List<double>> TrainingOutput = new List<List<double>> { };
            
            for (int Row = 0; Row < EpisodeData.Count; Row++)
            {
                List<List<double>> SymertrisedRow = SymertriseData(EpisodeData[Row]);
                for (int DataRow =0; DataRow < SymertrisedRow.Count; DataRow++)
                {
                    List<List<double>> Results = SplitInputsFromOutputs(SymertrisedRow[DataRow], 9);
                    TrainingInputs.Add(Results[0]);
                    TrainingOutput.Add(Results[1]);
                }                
            }

            ANN.Train_MiniBatch(TrainingInputs, TrainingOutput, TrainingInputs.Count, "QlearnTraining.txt", 0.001, 0.7, 1e6, 1e-8, false);
            EpisodeData.Clear();
        }
     
        // Move Selection (Policies)
        // Tau in softmax is set to 1.5 by default. - This is a hyper parameter and needs to be tuned by hand there is no reason why 1.5 should be used.

        private int MakeRandomMove(List<double> Data)
        {
            int Move;
            do
            {
                Move = RND.Next(0,Data.Count);
            }

            while (Data[Move] != 0);

            return Move;
        }

        private int SelectMoveBasedOnProbability(List<double> Data)
        {
            ApplySoftMaxFunction(Data, 0.9);

            double CumulativeProbability = RND.NextDouble();

            for (int DataElement = 0; DataElement < Data.Count; DataElement++)
            {
                if ((CumulativeProbability -= Data[DataElement]) <= 0)
                    return DataElement;
            }

            throw new InvalidOperationException();
        }

        private void ApplySoftMaxFunction(List<double> Data, double Tau)
        {
            double Sum = 0;

            for (int DataElement = 0; DataElement < Data.Count; DataElement++)
            {
                if (Data[DataElement] != 0) { Sum += Math.Exp(Data[DataElement] / Tau); }
            }

            for (int DataElement = 0; DataElement < Data.Count; DataElement++)
            {
                if (Data[DataElement] != 0) { Data[DataElement] = Math.Exp(Data[DataElement] / Tau) / Sum; }
            }
        }

        private List<List<double>> SplitInputsFromOutputs(List<double> Data, int NumberOfInputs)
        {
            // seperates input data into training inputs and training outputs - to be used to update neural network
            List<List<double>> Output = new List<List<double>> { };
            List<double> Inputs = new List<double> { };
            List<double> Outputs = new List<double> { };

            for (int Element = 0; Element < Data.Count; Element++)
            {
                if (Element < NumberOfInputs) { Inputs.Add(Data[Element]); }
                else { Outputs.Add(Data[Element]); }
            }              
            
            Output.Add(Inputs);
            Output.Add(Outputs);

            return Output;
        }

        // Symertry functions

         public List<List<double>> SymertriseData(List<double> Data)
        {
            List<List<double>> RowData = SplitInputsFromOutputs(Data,9);
            List<List<double>> SymGameBoard = new List<List<double>> { };
            List<List<double>> SymOutputs = new List<List<double>> { };

            SymGameBoard.AddRange(GetReflections(RowData[0]));
            SymGameBoard.AddRange(GetRotations(RowData[0]));

            SymOutputs.AddRange(GetReflections(RowData[1]));
            SymOutputs.AddRange(GetRotations(RowData[1]));

            // recombine data so that sym output match game boards this is important as need to remove identical gameboard that have resulted from symertry so as not to bias the training data
            // it is unlikly that for most of the training process that the outputs will be symetrical so cant be easily removed leeding to a mismatch between the number of inputs and outputs.

            for (int i = 0; i < SymGameBoard.Count; i++)
            {
                SymGameBoard[i].AddRange(SymOutputs[i]);
            }

            // Get Only unique boards
            List<List<double>> Unique = RemoveDuplicatsFrom2DList(SymGameBoard);

            return Unique;
        }

        public List<List<double>> GetRotations(List<double> Data)
        {
            List<List<double>> RotationList = new List<List<double>> { };

            for (int Rotations = 0; Rotations < 4; Rotations++)
            {
                // Rotate GGameboard 90 degrees clockwise
                List<double> Rotation = new List<double> { };
                Rotation.AddRange(Data);
                RotationList.Add(Rotation);
                RotateGameBoard90Degrees(Data);
            }

            return RotationList;
        }

        public void RotateGameBoard90Degrees(List<double> GameState)
        {
            List<List<double>> Board = new List<List<double>> {
                new List<double> { 0, 0, 0 },
                new List<double> { 0, 0, 0 },
                new List<double> { 0, 0, 0 },
            };

            // convert to 2d board rep
            int a = 0;
            int b = 0;
            for (int i = 0; i < GameState.Count; i++)
            {
                Board[a][b] = GameState[i];
                b++;
                if ((i + 1) % 3 == 0) { a++; b = 0; }
            }

            GameState.Clear();

            // convert 2d board back to 1d list
            for (int i = 0; i < Board.Count; i++)
            {
                for (int j = 0; j < Board.Count; j++)
                {
                    GameState.Add(Board[(Board.Count - 1) - j][i]);
                }
            }

        }

        public List<List<double>> GetReflections(List<double> Data)
        {
            List<List<double>> Reflections = new List<List<double>> { };

            Reflections.Add(VerticalMirror(Data));
            Reflections.Add(HorizantalMirror(Data));
            Reflections.Add(LeftDiagnolMirror(Data));
            Reflections.Add(RigthDiagnolMirror(Data));

            return Reflections;
        }

        public List<double> VerticalMirror(List<double> GameState)
        {
            List<double> Rotated = new List<double> { };
            Rotated.AddRange(GameState);

            for (int i = 0; i < 9; i += 3)
            {
                double temp = Rotated[i];
                Rotated[i] = Rotated[i + 2];
                Rotated[i + 2] = temp;
            }

            return Rotated;
        }

        private List<double> LeftDiagnolMirror(List<double> GameState)
        {
            List<double> Reflection = new List<double> { };
            Reflection.AddRange(GameState);

            Reflection = HorizantalMirror(Reflection);
            RotateGameBoard90Degrees(Reflection);

            return Reflection;
        }

        private List<double> RigthDiagnolMirror(List<double> GameState)
        {
            List<double> Reflection = new List<double> { };
            Reflection.AddRange(GameState);

            Reflection = VerticalMirror(Reflection);
            RotateGameBoard90Degrees(Reflection);

            return Reflection;
        }

        private List<double> HorizantalMirror(List<double> GameState)
        {
            List<double> Reflection = new List<double> { };
            Reflection.AddRange(GameState);

            for (int i = 0; i < 3; i++)
            {
                double temp = Reflection[i];
                Reflection[i] = Reflection[i + 6];
                Reflection[i + 6] = temp;
            }

            return Reflection;
        }

        // General Functions for minlipulation of data

        public List<List<double>> RemoveDuplicatsFrom2DList(List<List<double>> InputData)
        {
            List<List<double>> Unique = new List<List<double>> { };
            
            for (int row =0; row<InputData.Count; row++)
            {
                if (row == 0) { Unique.Add(InputData[row]); }
                else
                {
                    bool NotPresent = false;
                    for (int i = 0; i < Unique.Count; i++)
                    {
                        for (int Square = 0; Square < 9; Square++)
                        {
                            if (InputData[row][Square] != Unique[i][Square]) { NotPresent = true; break; }
                        }
                    }
                    if (NotPresent == true) { Unique.Add(InputData[row]); }
                }
            }

            return Unique;
        }

        public void InvertGameBoard(List<double> Data)
        {
            for (int Item = 0; Item < Data.Count; Item++)
            {
                if (Data[Item] == 0) { Data[Item] = 1; }
                else if (Data[Item] == 1) { Data[Item] = 0; }
            }
        }
        
        private void NormaliseData(List<double> Data)
        { 
            for (int Value = 0; Value < Data.Count; Value++)
            {
                if (Data[Value] == -1) { Data[Value] = 0; }
                else if (Data[Value] == 0) { Data[Value] = 0.5; }
                else Data[Value] = 1;
            }
        }
        
    }
}
