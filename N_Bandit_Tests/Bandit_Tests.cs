using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace N_Bandit_Tests
{
    [TestClass]
    public class Bandit_Tests
    {
        [TestMethod]
        public void PayoutNotNull()
        {
            // Arrange
            N_Bandits.Bandit bandit = new N_Bandits.Bandit(1);

            // Act
            double Result = bandit.GetPayOut();

            // Assert
            Assert.IsNotNull(Result);
        }
    }
}
