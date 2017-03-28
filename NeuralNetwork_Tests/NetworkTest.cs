using System;
using NeuralNetwork;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace NeuralNetwork_Tests
{
    [TestClass]
    public class NetworkTest
    {
        [TestMethod]
        public void SaveNetwork()
        {
            // Arrange
            ANeuralNetwork ANN = new ANeuralNetwork();
            ANN.Create_Network(new List<int> { 2, 3, 1 });
            Exception expectedException = null;

            // Act
            try
            {
                ANN.Save_Network("TestSave.net");
            }
            catch (Exception ex)
            {
                expectedException = ex;
            }

            // Assert
            Assert.IsNull(expectedException);
        }

        [TestMethod]
        public void LoadInputFile_FileExists()
        {
            // Arrange
            string Filename = "Test.net";
            ANeuralNetwork ANN = new ANeuralNetwork();
            Exception expectedException = null;

            // Act
            try
            {
                ANN.Load_Network(Filename);
            }
            catch (Exception ex)
            {
                expectedException = ex;
            }

            // Assert
            Assert.IsNull(expectedException);
        }

        [TestMethod]
        public void LoadInputFile_FileNotExists()
        {
            // Arrange
            string Filename = "DoesNotExist.net";
            ANeuralNetwork ANN = new ANeuralNetwork();
            Exception expectedException = null;

            // Act
            try
            {
                ANN.Load_Network(Filename);
            }
            catch (Exception ex)
            {
                expectedException = ex;
            }

            // Assert
            Assert.IsNotNull(expectedException);
        }

        [TestMethod]
        public void LoadInputFile_InvaildNetworkDescription()
        {
            // Arrange
            string Filename = "Test_InvalidNetworkDescription.net";
            ANeuralNetwork ANN = new ANeuralNetwork();
            Exception expectedException = null;

            // Act
            try
            {
                ANN.Load_Network(Filename);
            }
            catch (Exception ex)
            {
                expectedException = ex;
            }

            // Assert
            Assert.IsNotNull(expectedException);
        }

        [TestMethod]
        public void LoadInputFile_CheckLoadedWeights()
        {
            // Arrange
            string Filename = "Test.net";
            ANeuralNetwork ANN = new ANeuralNetwork();
            List<double> ExpectedWeights = new List<double> { 0.1, 0.2, 0.1 };

            // Act
            ANN.Load_Network(Filename);
            List<double> Weights = new List<double> { };
            for (int Weight = 0; Weight < ANN.NeuralNetwork[0][0].Weights.Count; Weight++)
            {
                Weights.Add(ANN.NeuralNetwork[0][0].Weights[Weight]);
            }
            
            // Assert
            CollectionAssert.AreEqual(ExpectedWeights, Weights);
        }

        [TestMethod]
        public void LoadData_FileExists()
        {
            // Arrange
            string Filename = "TestDataInput.dat";
            ANeuralNetwork ANN = new ANeuralNetwork();
            Exception expectedException = null;

            // Act
            try
            {
                ANN.Load_Data(Filename);
            }
            catch (Exception ex)
            {
                expectedException = ex;
            }

            // Assert
            Assert.IsNull(expectedException);
        }

        [TestMethod]
        public void LoadData_FileNotExists()
        {
            // Arrange
            string Filename = "DoesNotExist.net";
            ANeuralNetwork ANN = new ANeuralNetwork();
            Exception expectedException = null;

            // Act
            try
            {
                ANN.Load_Data(Filename);
            }
            catch (Exception ex)
            {
                expectedException = ex;
            }

            // Assert
            Assert.IsNotNull(expectedException);
        }

        [TestMethod]
        public void LoadData_Check()
        {
            // Arrange
            ANeuralNetwork ANN = new ANeuralNetwork();
            List<List<double>> ExpectedOutput = new List<List<double>> { new List<double> { 1, 2, 3 }, new List<double> { 4, 5, 6 } };

            // Act
            List<List<double>> Output = ANN.Load_Data("TestDataInput.dat");

            // Asert
            bool ItemsMatch = true;
            for (int row = 0; row < Output.Count; row++)
                {
                    for (int column = 0; column < Output[row].Count; column++)
                    {
                        Console.Write(Output[row][column] + " ");
                        if (ExpectedOutput[row][column] != Output[row][column]) { ItemsMatch = false; }
                    }
                    Console.WriteLine();                
            }
            Assert.IsTrue(ItemsMatch);
        }
        
        [TestMethod]
        public void GetNetworkOutput_CheckOutputCorrect()
        {
            // Arrange
            ANeuralNetwork ANN = new ANeuralNetwork();
            ANN.Load_Network("Test.net");
            List<double> InputData = new List<double> { 0.1, 0.9 };
            double DesiredOutput = 0.60761339;

            // Act
            List<double> Output = ANN.Get_Network_Output(InputData);

            // Assert
            Assert.AreEqual(DesiredOutput, Math.Round(Output[0],8));
        }

        [TestMethod]
        public void TrainMiniBatch_ConvergenceTest()
        {
            // Arrange
            ANeuralNetwork ANN = new ANeuralNetwork();
            ANN.Load_Network("Test.net");
            List<List<double>> InputData = new List<List<double>> { new List<double> { 0.1, 0.9 } };
            List<List<double>> OutputData = new List<List<double>> { new List<double> { 0.9 } };

            // Act
            ANN.Train_MiniBatch(InputData, OutputData, InputData.Count, "MiniBatch_Convergence.dat");
            List<double> Result = ANN.Get_Network_Output(InputData[0]);
            
            // Assert
            Console.Write("Output = " + Result[0]);
            Assert.AreEqual(OutputData[0][0], Math.Round(Result[0], 1));
        }

        [TestMethod]
        public void TrainMiniBatch_ConvergenceTest_ValuesNOT01()
        {
            // Arrange
            ANeuralNetwork ANN = new ANeuralNetwork();
            ANN.Load_Network("Test.net");
            List<List<double>> InputData = new List<List<double>> { new List<double> { 10, 10 } };
            List<List<double>> OutputData = new List<List<double>> { new List<double> { 10 } };

            // Act
            ANN.Train_MiniBatch(InputData, OutputData, InputData.Count, "MiniBatch_Convergence.dat");
            List<double> Result = ANN.Get_Network_Output(InputData[0]);

            // Assert
            Console.Write("Output = " + Result[0]);
            Assert.AreNotEqual(OutputData[0][0], Math.Round(Result[0], 1));
        }

        [TestMethod]
        public void SplitData()
        {
            // Arrange
            ANeuralNetwork ANN = new ANeuralNetwork();
            List<List<double>> TestData = new List<List<double>> { new List<double> { 1, 2, 3, 4 }, new List<double> { 5, 6, 7, 8 } };
            List<List<List<double>>> ExpectedOutput = new List<List<List<double>>> { new List<List<double>> { new List<double> { 1, 2 } }, new List<List<double>> { new List<double> { 3, 4 } }, new List<List<double>> { new List<double> { 5, 6 } }, new List<List<double>> { new List<double> { 7, 8 } } };

            // Act
            ANN.Create_Network(new List<int>{ 2,1,2});
            List<List<List<double>>> Output = ANN.SplitDataSet(TestData, 1);

            // Assert
            bool ItemsMatch = true;
            for (int ItemInOutput = 0; ItemInOutput < Output.Count; ItemInOutput++)
            {
                for (int row = 0; row < Output[ItemInOutput].Count; row++)
                {
                    for (int column = 0; column < Output[ItemInOutput][row].Count; column++)
                    {
                        Console.Write(Output[ItemInOutput][row][column] + " ");
                        if (ExpectedOutput[ItemInOutput][row][column] != Output[ItemInOutput][row][column]) { ItemsMatch = false; }
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();                
            }
            Assert.IsTrue(ItemsMatch);
        }

        [TestMethod]
        public void SplitData_SampleSize0()
        {
            // Arrange
            ANeuralNetwork ANN = new ANeuralNetwork();
            List<List<double>> TestData = new List<List<double>> { new List<double> { 1, 2 }, new List<double> { 3, 4 }, new List<double> { 5, 6 }, new List<double> { 7, 8 } };
            Exception expectedException = null;

            // Act
            try
            {
                ANN.SplitDataSet(TestData, 0);
            }
            catch (Exception ex)
            {
                expectedException = ex;
            }

            // Assert
            Assert.IsNotNull(expectedException);
        }

        [TestMethod]
        public void SplitData_SampleSizeLargerThanDataSize()
        {
            // Arrange
            ANeuralNetwork ANN = new ANeuralNetwork();
            List<List<double>> TestData = new List<List<double>> { new List<double> { 1, 2 }, new List<double> { 3, 4 }, new List<double> { 5, 6 }, new List<double> { 7, 8 } };
            Exception expectedException = null;

            // Act
            try
            {
                ANN.SplitDataSet(TestData, 5);
            }
            catch (Exception ex)
            {
                expectedException = ex;
            }

            // Assert
            Assert.IsNotNull(expectedException);
        }

        // Logic Gate Tests using various batch sizes and truth tables as inputs and validation
        // Defualt network configuration {2,3,1}

        [TestMethod]
        public void TrainMiniBatch_ANDGateTest_SingleBatch()
        {
            // Arrange
            ANeuralNetwork ANN = new ANeuralNetwork();
            ANN.Create_Network(new List<int> { 2, 3, 1 });
            List<List<double>> InputData = new List<List<double>> { new List<double> { 1, 1 }, new List<double> { 0, 1 }, new List<double> { 1, 0 }, new List<double> { 0, 0 }, };
            List<List<double>> OutputData = new List<List<double>> { new List<double> { 1 }, new List<double> { 0 }, new List<double> { 0 }, new List<double> { 0 } };
            List<List<double>> ValidationData = new List<List<double>> { new List<double> { 1, 1 }, new List<double> { 0, 1 }, new List<double> { 1, 0 }, new List<double> { 0, 0 }, };
            List<double> ExpectedOutput = new List<double> { 1, 0, 0, 0 };

            // Act
            ANN.Train_MiniBatch(InputData, OutputData,InputData.Count, "AND_SinleBatch.dat");
            List<double> Results = new List<double> { };
            for (int i = 0; i < 4; i++)
            {
                List<double> result = ANN.Get_Network_Output(ValidationData[i]);
                Results.Add(Math.Round(result[0], 0));
                Console.Write("Output = " + Results[i] + " ");
            }

            // Assert           
            CollectionAssert.AreEqual(ExpectedOutput, Results);
        }

        [TestMethod]
        public void TrainMiniBatch_ANDGateTest_BatchSize2()
        {
            // Arrange
            ANeuralNetwork ANN = new ANeuralNetwork();
            ANN.Create_Network(new List<int> { 2, 3, 1 });
            List<List<double>> InputData = new List<List<double>> { new List<double> { 1, 1 }, new List<double> { 0, 1 }, new List<double> { 1, 0 }, new List<double> { 0, 0 }, };
            List<List<double>> OutputData = new List<List<double>> { new List<double> { 1 }, new List<double> { 0 }, new List<double> { 0 }, new List<double> { 0 } };
            List<List<double>> ValidationData = new List<List<double>> { new List<double> { 1, 1 }, new List<double> { 0, 1 }, new List<double> { 1, 0 }, new List<double> { 0, 0 }, };
            List<double> ExpectedOutput = new List<double> { 1, 0, 0, 0 };

            // Act
            ANN.Train_MiniBatch(InputData, OutputData, 2, "AND_BatchSize2.dat");
            List<double> Results = new List<double> { };
            for (int i = 0; i < 4; i++)
            {
                List<double> result = ANN.Get_Network_Output(ValidationData[i]);
                Results.Add(Math.Round(result[0], 0));
                Console.Write("Output = " + Results[i] + " ");
            }

            // Assert           
            CollectionAssert.AreEqual(ExpectedOutput, Results);
        }

        [TestMethod]
        public void TrainMiniBatch_ANDGateTest_Online()
        {
            // Arrange
            ANeuralNetwork ANN = new ANeuralNetwork();
            ANN.Create_Network(new List<int> { 2, 3, 1 });
            List<List<double>> InputData = new List<List<double>> { new List<double> { 1, 1 }, new List<double> { 0, 1 }, new List<double> { 1, 0 }, new List<double> { 0, 0 }, };
            List<List<double>> OutputData = new List<List<double>> { new List<double> { 1 }, new List<double> { 0 }, new List<double> { 0 }, new List<double> { 0 } };
            List<List<double>> ValidationData = new List<List<double>> { new List<double> { 1, 1 }, new List<double> { 0, 1 }, new List<double> { 1, 0 }, new List<double> { 0, 0 }, };
            List<double> ExpectedOutput = new List<double> { 1, 0, 0, 0 };

            // Act
            ANN.Train_MiniBatch(InputData, OutputData, 1, "AND_Online.dat");
            List<double> Results = new List<double> { };
            for (int i = 0; i < 4; i++)
            {
                List<double> result = ANN.Get_Network_Output(ValidationData[i]);
                Results.Add(Math.Round(result[0], 0));
                Console.Write("Output = " + Results[i] + " ");
            }

            // Assert           
            CollectionAssert.AreEqual(ExpectedOutput, Results);
        }

        [TestMethod]
        public void TrainMiniBatch_ORGateTest_SingleBatch()
        {
            // Arrange
            ANeuralNetwork ANN = new ANeuralNetwork();
            ANN.Create_Network(new List<int> { 2, 3, 1 });
            List<List<double>> InputData = new List<List<double>> { new List<double> { 1, 1 }, new List<double> { 0, 1 }, new List<double> { 1, 0 }, new List<double> { 0, 0 }, };
            List<List<double>> OutputData = new List<List<double>> { new List<double> { 1 }, new List<double> { 1 }, new List<double> { 1 }, new List<double> { 0 } };
            List<List<double>> ValidationData = new List<List<double>> { new List<double> { 1, 1 }, new List<double> { 0, 1 }, new List<double> { 1, 0 }, new List<double> { 0, 0 }, };
            List<double> ExpectedOutput = new List<double> { 1, 1, 1, 0 };

            // Act
            ANN.Train_MiniBatch(InputData, OutputData, InputData.Count, "OR_SinleBatch.dat");
            List<double> Results = new List<double> { };
            for (int i = 0; i < 4; i++)
            {
                List<double> result = ANN.Get_Network_Output(ValidationData[i]);
                Results.Add(Math.Round(result[0], 0));
                Console.Write("Output = " + Results[i] + " ");
            }

            // Assert           
            CollectionAssert.AreEqual(ExpectedOutput, Results);
        }

        [TestMethod]
        public void TrainMiniBatch_ORGateTest_BatchSize2()
        {
            // Arrange
            ANeuralNetwork ANN = new ANeuralNetwork();
            ANN.Create_Network(new List<int> { 2, 3, 1 });
            List<List<double>> InputData = new List<List<double>> { new List<double> { 1, 1 }, new List<double> { 0, 1 }, new List<double> { 1, 0 }, new List<double> { 0, 0 }, };
            List<List<double>> OutputData = new List<List<double>> { new List<double> { 1 }, new List<double> { 1 }, new List<double> { 1 }, new List<double> { 0 } };
            List<List<double>> ValidationData = new List<List<double>> { new List<double> { 1, 1 }, new List<double> { 0, 1 }, new List<double> { 1, 0 }, new List<double> { 0, 0 }, };
            List<double> ExpectedOutput = new List<double> { 1, 1, 1, 0 };

            // Act
            ANN.Train_MiniBatch(InputData, OutputData, 2, "OR_BatchSize2.dat");
            List<double> Results = new List<double> { };
            for (int i = 0; i < 4; i++)
            {
                List<double> result = ANN.Get_Network_Output(ValidationData[i]);
                Results.Add(Math.Round(result[0], 0));
                Console.Write("Output = " + Results[i] + " ");
            }

            // Assert           
            CollectionAssert.AreEqual(ExpectedOutput, Results);
        }

        [TestMethod]
        public void TrainMiniBatch_ORGateTest_Online()
        {
            // Arrange
            ANeuralNetwork ANN = new ANeuralNetwork();
            ANN.Create_Network(new List<int> { 2, 3, 1 });
            List<List<double>> InputData = new List<List<double>> { new List<double> { 1, 1 }, new List<double> { 0, 1 }, new List<double> { 1, 0 }, new List<double> { 0, 0 }, };
            List<List<double>> OutputData = new List<List<double>> { new List<double> { 1 }, new List<double> { 1 }, new List<double> { 1 }, new List<double> { 0 } };
            List<List<double>> ValidationData = new List<List<double>> { new List<double> { 1, 1 }, new List<double> { 0, 1 }, new List<double> { 1, 0 }, new List<double> { 0, 0 }, };
            List<double> ExpectedOutput = new List<double> { 1, 1, 1, 0 };

            // Act
            ANN.Train_MiniBatch(InputData, OutputData, 1, "OR_Online.dat");
            List<double> Results = new List<double> { };
            for (int i = 0; i < 4; i++)
            {
                List<double> result = ANN.Get_Network_Output(ValidationData[i]);
                Results.Add(Math.Round(result[0], 0));
                Console.Write("Output = " + Results[i] + " ");
            }

            // Assert           
            CollectionAssert.AreEqual(ExpectedOutput, Results);
        }

        [TestMethod]
        public void TrainMiniBatch_NANDGateTest_SingleBatch()
        {
            // Arrange
            ANeuralNetwork ANN = new ANeuralNetwork();
            ANN.Create_Network(new List<int> { 2, 3, 1 });
            List<List<double>> InputData = new List<List<double>> { new List<double> { 1, 1 }, new List<double> { 0, 1 }, new List<double> { 1, 0 }, new List<double> { 0, 0 }, };
            List<List<double>> OutputData = new List<List<double>> { new List<double> { 0 }, new List<double> { 1 }, new List<double> { 1 }, new List<double> { 1 } };
            List<List<double>> ValidationData = new List<List<double>> { new List<double> { 1, 1 }, new List<double> { 0, 1 }, new List<double> { 1, 0 }, new List<double> { 0, 0 }, };
            List<double> ExpectedOutput = new List<double> { 0, 1, 1, 1 };

            // Act
            ANN.Train_MiniBatch(InputData, OutputData, InputData.Count, "NAND_SinleBatch.dat");
            List<double> Results = new List<double> { };
            for (int i = 0; i < 4; i++)
            {
                List<double> result = ANN.Get_Network_Output(ValidationData[i]);
                Results.Add(Math.Round(result[0], 0));
                Console.Write("Output = " + Results[i] + " ");
            }

            // Assert           
            CollectionAssert.AreEqual(ExpectedOutput, Results);
        }

        [TestMethod]
        public void TrainMiniBatch_NANDGateTest_BatchSize2()
        {
            // Arrange
            ANeuralNetwork ANN = new ANeuralNetwork();
            ANN.Create_Network(new List<int> { 2, 3, 1 });
            List<List<double>> InputData = new List<List<double>> { new List<double> { 1, 1 }, new List<double> { 0, 1 }, new List<double> { 1, 0 }, new List<double> { 0, 0 }, };
            List<List<double>> OutputData = new List<List<double>> { new List<double> { 0 }, new List<double> { 1 }, new List<double> { 1 }, new List<double> { 1 } };
            List<List<double>> ValidationData = new List<List<double>> { new List<double> { 1, 1 }, new List<double> { 0, 1 }, new List<double> { 1, 0 }, new List<double> { 0, 0 }, };
            List<double> ExpectedOutput = new List<double> { 0, 1, 1, 1 };

            // Act
            ANN.Train_MiniBatch(InputData, OutputData, 2, "NAND_BatchSize2.dat");
            List<double> Results = new List<double> { };
            for (int i = 0; i < 4; i++)
            {
                List<double> result = ANN.Get_Network_Output(ValidationData[i]);
                Results.Add(Math.Round(result[0], 0));
                Console.Write("Output = " + Results[i] + " ");
            }

            // Assert           
            CollectionAssert.AreEqual(ExpectedOutput, Results);
        }

        [TestMethod]
        public void TrainMiniBatch_NANDGateTest_Online()
        {
            // Arrange
            ANeuralNetwork ANN = new ANeuralNetwork();
            ANN.Create_Network(new List<int> { 2, 3, 1 });
            List<List<double>> InputData = new List<List<double>> { new List<double> { 1, 1 }, new List<double> { 0, 1 }, new List<double> { 1, 0 }, new List<double> { 0, 0 }, };
            List<List<double>> OutputData = new List<List<double>> { new List<double> { 0 }, new List<double> { 1 }, new List<double> { 1 }, new List<double> { 1 } };
            List<List<double>> ValidationData = new List<List<double>> { new List<double> { 1, 1 }, new List<double> { 0, 1 }, new List<double> { 1, 0 }, new List<double> { 0, 0 }, };
            List<double> ExpectedOutput = new List<double> { 0, 1, 1, 1 };

            // Act
            ANN.Train_MiniBatch(InputData, OutputData, 1, "NAND_Online.dat");
            List<double> Results = new List<double> { };
            for (int i = 0; i < 4; i++)
            {
                List<double> result = ANN.Get_Network_Output(ValidationData[i]);
                Results.Add(Math.Round(result[0], 0));
                Console.Write("Output = " + Results[i] + " ");
            }

            // Assert           
            CollectionAssert.AreEqual(ExpectedOutput, Results);
        }

        [TestMethod]
        public void TrainMiniBatch_XORGateTest_SingleBatch()
        {
            // Arrange
            ANeuralNetwork ANN = new ANeuralNetwork();
            ANN.Create_Network(new List<int> { 2, 3, 1 });
            List<List<double>> InputData = new List<List<double>> { new List<double> { 1, 1 }, new List<double> { 0, 1 }, new List<double> { 1, 0 }, new List<double> { 0, 0 }, };
            List<List<double>> OutputData = new List<List<double>> { new List<double> { 0 }, new List<double> { 1 }, new List<double> { 1 }, new List<double> { 0 } };
            List<List<double>> ValidationData = new List<List<double>> { new List<double> { 1, 1 }, new List<double> { 0, 1 }, new List<double> { 1, 0 }, new List<double> { 0, 0 }, };
            List<double> ExpectedOutput = new List<double> { 0, 1, 1, 0 };

            // Act
            ANN.Train_MiniBatch(InputData, OutputData, InputData.Count, "XOR_SinleBatch.dat");
            List<double> Results = new List<double> { };
            for (int i = 0; i < 4; i++)
            {
                List<double> result = ANN.Get_Network_Output(ValidationData[i]);
                Results.Add(Math.Round(result[0], 0));
                Console.Write("Output = " + Results[i] + " ");
            }

            // Assert           
            CollectionAssert.AreEqual(ExpectedOutput, Results);
        }

        [TestMethod]
        public void TrainMiniBatch_XORGateTest_BatchSize2()
        {
            // Arrange
            ANeuralNetwork ANN = new ANeuralNetwork();
            ANN.Create_Network(new List<int> { 2, 3, 1 });
            List<List<double>> InputData = new List<List<double>> { new List<double> { 1, 1 }, new List<double> { 0, 1 }, new List<double> { 1, 0 }, new List<double> { 0, 0 }, };
            List<List<double>> OutputData = new List<List<double>> { new List<double> { 0 }, new List<double> { 1 }, new List<double> { 1 }, new List<double> { 0 } };
            List<List<double>> ValidationData = new List<List<double>> { new List<double> { 1, 1 }, new List<double> { 0, 1 }, new List<double> { 1, 0 }, new List<double> { 0, 0 }, };
            List<double> ExpectedOutput = new List<double> { 0, 1, 1, 0 };

            // Act
            ANN.Train_MiniBatch(InputData, OutputData, 2, "XOR_BatchSize2.dat");
            List<double> Results = new List<double> { };
            for (int i = 0; i < 4; i++)
            {
                List<double> result = ANN.Get_Network_Output(ValidationData[i]);
                Results.Add(Math.Round(result[0], 0));
                Console.Write("Output = " + Results[i] + " ");
            }

            // Assert           
            CollectionAssert.AreEqual(ExpectedOutput, Results);
        }

        [TestMethod]
        public void TrainMiniBatch_XORGateTest_Online()
        {
            // Arrange
            ANeuralNetwork ANN = new ANeuralNetwork();
            ANN.Create_Network(new List<int> { 2, 3, 1 });
            List<List<double>> InputData = new List<List<double>> { new List<double> { 1, 1 }, new List<double> { 0, 1 }, new List<double> { 1, 0 }, new List<double> { 0, 0 }, };
            List<List<double>> OutputData = new List<List<double>> { new List<double> { 0 }, new List<double> { 1 }, new List<double> { 1 }, new List<double> { 0 } };
            List<List<double>> ValidationData = new List<List<double>> { new List<double> { 1, 1 }, new List<double> { 0, 1 }, new List<double> { 1, 0 }, new List<double> { 0, 0 }, };
            List<double> ExpectedOutput = new List<double> { 0, 1, 1, 0 };

            // Act
            ANN.Train_MiniBatch(InputData, OutputData, 1, "XOR_Online.dat");
            List<double> Results = new List<double> { };
            for (int i = 0; i < 4; i++)
            {
                List<double> result = ANN.Get_Network_Output(ValidationData[i]);
                Results.Add(Math.Round(result[0], 0));
                Console.Write("Output = " + Results[i] + " ");
            }

            // Assert           
            CollectionAssert.AreEqual(ExpectedOutput, Results);
        }

        // Logic Gate test using larger sample data - generated using excel
        // Sample data split into training data and validation data 
    }
}
