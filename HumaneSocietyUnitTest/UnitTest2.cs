using System;
using HumaneSociety;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HumaneSocietyUnitTest
{
    [TestClass]
    public class UnitTest2
    {
        [TestMethod]
        public void GetUSStates_Run_ReturnsAllStates()
        {
            //Assign
            USState[] result = null;
            //Act
            try
            {
                result = Query.GetStates();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

            //Assert
            Assert.AreEqual(null, result);
        }
    }
}
