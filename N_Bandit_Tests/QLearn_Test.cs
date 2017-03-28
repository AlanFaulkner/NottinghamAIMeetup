using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace N_Bandit_Tests
{
    [TestClass]
    public class QLearn_Test
    {
        [TestMethod]
        public void SoftMax_NoNull()
        {
            // Arrange
            N_Bandits.QLearn qlearn = new N_Bandits.QLearn();
            List<double> Input = new List<double> { 0, 0, 0, 0, 0 };
            List<double> NotDesired = new List<double> { 0, 0, 0, 0, 0 };

            // Act
            qlearn.ApplySoftMax(Input, 0.9);

            // Assert
            CollectionAssert.AreNotEqual(NotDesired, Input);
        }
    }
}
