using System;
using HumaneSociety;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HumaneSocietyUnitTest
{
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
        string email = "adam@test.com";
        int employeeNumber = 1;

        //Act
        Employee employee =  Query.RetrieveEmployeeUser(email, employeeNumber);
            //Assert
            Assert.IsNotNull(employee);
        }
    }
}
