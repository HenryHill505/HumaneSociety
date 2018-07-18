using System;
using HumaneSociety;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HumaneSocietyUnitTest
{
<<<<<<< HEAD
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CheckEmployeeUserNameExists_UsernameDoesNotExist_ReturnFalse()
        {
            //Arrange
            string testname = "Henry";
            bool result;
            //Act
            result = Query.CheckEmployeeUserNameExist(testname);
            //Assert
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void CheckEmployeeUserNameExists_UsernameDoesExist_ReturnTrue()
        {
            //Arrange
            string testname = "";
            bool result;
            //Act
            result = Query.CheckEmployeeUserNameExist(testname);
            //Assert
            Assert.AreEqual(true, result);
        }

        [TestMethod]

        public void RetrieveEmployeeUser_EmployeeExists_ReturnEmployee()
        {
        //Arrange
        string email = "";
        int employeNumber = 0;

        //Act
        Query.RetrieveEmployeeUser(email, employeNumber);
        
        //Assert
        }
    }
=======
    //[TestClass]
    //public class UnitTest1
    //{
    //    [TestMethod]
    //    public void CheckEmployeeUserNameExists_UsernameDoesNotExist_ReturnFalse()
    //    {
    //        //Arrange
    //        string testname = "Henry";
    //        bool result;
    //        //Act
    //        result = Query.CheckEmployeeUserNameExist(testname);
    //        //Assert
    //        Assert.AreEqual(false, result);
    //    }
    //}
>>>>>>> 1939007cc3a6ddfb8b25186d4e9460f61ad27860
}
