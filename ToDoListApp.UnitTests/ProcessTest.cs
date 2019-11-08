using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListApp.BusinessLogicLayer;

namespace ToDoListApp.UnitTests
{
    [TestClass]
    public class ProcessTest
    {
        [TestMethod]
        public void User_Cannot_Login_With_Invalid_Name()
        {
            //Arrange
            var process = new Process();
            string username = string.Empty;
            string password = "Test Password";
            string expectedMessage = "Username or password is invalid!";

            //Act
            string actualMessage = process.SignIn(username,password);

            //Assert
            Assert.AreEqual( expectedMessage, actualMessage );
        }

        [TestMethod]
        public void User_Cannot_Login_With_Invalid_Password()
        {
            //Arrange
            var process = new Process();
            string username = "Test User";
            string password = string.Empty;
            string expectedMessage = "Username or password is invalid!";

            //Act
            string actualMessage = process.SignIn(username,password);

            //Assert
            Assert.AreEqual( expectedMessage, actualMessage );
        }

        [TestMethod]
        public void User_Cannot_Register_With_Invalid_Name()
        {
            //Arrange
            var process = new Process();
            string username = string.Empty;
            string password = "Test Password";
            string expectedMessage = "Username and password are required fields.";

            //Act
            string actualMessage = process.SignUp(username,password);

            //Assert
            Assert.AreEqual( expectedMessage, actualMessage );
        }

        [TestMethod]
        public void User_Cannot_Register_With_Invalid_Password()
        {
            //Arrange
            var process = new Process();
            string username = "Test User";
            string password = string.Empty;
            string expectedMessage = "Username and password are required fields.";

            //Act
            string actualMessage = process.SignUp(username,password);

            //Assert
            Assert.AreEqual( expectedMessage, actualMessage );
        }
    }
}
