using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tic_Tac_Toe;

namespace Tic_Tac_ToeTests
{
    [TestClass]
    public class QLearningTests
    {
         
         // Symertry Operations

        [TestMethod]
        public void RotateGameBoard90DegClockwise()
        {
            // Arrange
            List<double> Gameboard = new List<double> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            List<double> Expected = new List<double> { 7, 4, 1, 8, 5, 2, 9, 6, 3 };

            // Act
            Qlearn qlearning = new Qlearn();
            qlearning.RotateGameBoard90Degrees(Gameboard);

            // Asert
            CollectionAssert.AreEqual(Expected, Gameboard);
        }
                
        [TestMethod]
        public void InvertGameBoard()
        {
            // Arrange
            List<double> Gameboard = new List<double> { 1, 0.5, 0.5, 0.5, 0, 0.5, 0.5, 0.5, 0.5 };
            List<double> Expected = new List<double> { 0, 0.5, 0.5, 0.5, 1, 0.5, 0.5, 0.5, 0.5 };

            // Act
            Qlearn qlearning = new Qlearn();
            qlearning.InvertGameBoard(Gameboard);

            // Print
            foreach (var Column in Gameboard)
            {
                Console.WriteLine(Column);
            }

            // Asert
            CollectionAssert.AreEqual(Expected, Gameboard);
        }        

        [TestMethod]
        public void GetRotations()
        {
            // Arrange
            List<double> Input = new List<double> { 1, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5 };
            List<List<double>> Expected = new List<List<double>> {
                new List<double> { 1, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5 },
                new List<double> { 0.5, 0.5, 1, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5 },
                new List<double> { 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 1 },
                new List<double> { 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 1, 0.5, 0.5 }
            };

            // Act
            Qlearn qlearning = new Qlearn();
            List<List<double>> Output = qlearning.GetRotations(Input);

            // Write Output
            bool Match = true;
            for (int Row = 0; Row < Output.Count; Row++)
            {
                for (int Column = 0; Column < Output[Row].Count; Column++)
                {
                    if (Output[Row][Column] != Expected[Row][Column]) { Match = false; }
                    Console.Write(Output[Row][Column] + " ");
                }
                Console.WriteLine();
            }

            // Assert
            Assert.IsTrue(Match);
        }

        [TestMethod]
        public void VerticalMirror()
        {
            // Arrange
            List<double> Input = new List<double> { 0.5, 1, 1, 0.5, 1, 0.5, 0.5, 0.5, 0.5 };
            List<double> Expected = new List<double> { 1, 1, 0.5, 0.5, 1, 0.5, 0.5, 0.5, 0.5 };

            // Act
            Qlearn qlearning = new Qlearn();
            List<double> Output = qlearning.VerticalMirror(Input);

            // Assert
            CollectionAssert.AreEqual(Expected, Output);
        }

        [TestMethod]
        public void GetReflections()
        {
            // Arrange
            List<double> Input = new List<double> { 0.5, 1, 1, 0.5, 1, 0.5, 0.5, 0.5, 0.5 };
            List<List<double>> Expected = new List<List<double>> {
                new List<double> { 1, 1, 0.5, 0.5, 1, 0.5, 0.5, 0.5, 0.5 },
                new List<double> { 0.5, 0.5, 0.5, 0.5, 1, 0.5, 0.5, 1, 1 },
                new List<double> { 0.5, 0.5, 0.5, 1, 1, 0.5, 1, 0.5, 0.5 },
                new List<double> { 0.5, 0.5, 1, 0.5, 1, 1, 0.5, 0.5, 0.5 }
            };

            // Act
            Qlearn qlearning = new Qlearn();
            List<List<double>> Output = qlearning.GetReflections(Input);

            // Write Output
            bool Match = true;
            for (int Row = 0; Row < Output.Count; Row++)
            {
                for (int Column = 0; Column < Output[Row].Count; Column++)
                {
                    if (Output[Row][Column] != Expected[Row][Column]) { Match = false; }
                    Console.Write(Output[Row][Column] + " ");
                }
                Console.WriteLine();
            }

            // Assert
            Assert.IsTrue(Match);
        }

        [TestMethod]
        public void RemoveDulpicates()
        {
            // Arrange
            List<List<double>> Input = new List<List<double>> {
                new List<double> { 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 1, 0.5, 0.5, 0.5, 0.5 },
                new List<double> { 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 1, 0.5, 0.5, 0.5, 0.5 },
                new List<double> { 0, 0.5, 0.5, 0.5, 1, 0.5, 0.5, 0.5, 0.5, 0, 0.5, 0.5, 0.5, 1, 0.5, 0.5, 1, 0.5 }
            };

            List<List<double>> Expected = new List<List<double>> {
                new List<double> { 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 1, 0.5, 0.5, 0.5, 0.5 },
                new List<double> { 0, 0.5, 0.5, 0.5, 1, 0.5, 0.5, 0.5, 0.5, 0, 0.5, 0.5, 0.5, 1, 0.5, 0.5, 1, 0.5 }
            };

            // Act
            Qlearn qlearning = new Qlearn();
            List<List<double>> Output = qlearning.RemoveDuplicatsFrom2DList(Input);

            // Write Output
            bool Match = true;
            for (int Row = 0; Row < Output.Count; Row++)
            {
                for (int Column = 0; Column < Output[Row].Count; Column++)
                {
                    if (Output[Row][Column] != Expected[Row][Column]) { Match = false; }
                    Console.Write(Output[Row][Column] + " ");
                }
                Console.WriteLine();
            }

            // Assert
            Assert.IsTrue(Match);
        }

        [TestMethod]
        public void SymetriseData()
        {
            // Arrange
            List<double> Input = new List<double> { 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 0.5, 1, 0.5, 0.5, 0.5, 0.5 };
            List<List<double>> Expected = new List<List<double>> { new List<double> { 0.5, 0.5, 0.5, 0.5, 1, 0.5, 0.5, 0.5, 0.5 } };
            
            // Act
            Qlearn qlearning = new Qlearn();
            List<List<double>> Output = qlearning.SymertriseData(Input);

            // Write Output
            bool Match = true;
            for (int Row = 0; Row < Output.Count; Row++)
            {
                for (int Column = 0; Column < Output[Row].Count; Column++)
                {
                    if (Output[Row][Column] != Expected[Row][Column]) { Match = false; }
                    Console.Write(Output[Row][Column] + " ");
                }
                Console.WriteLine();
            }

            // Assert
            Assert.IsTrue(Match);
        }
    }
}
