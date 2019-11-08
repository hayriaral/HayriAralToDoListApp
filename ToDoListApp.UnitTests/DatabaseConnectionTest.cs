using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListApp.DataAccessLayer;
using ToDoListApp.BusinessLogicLayer.Repository;

namespace ToDoListApp.UnitTests
{
    [TestClass]
    public class DatabaseConnectionTest
    {
        [TestMethod]
        public void Same_Instance_Should_Return()
        {
            //Arrange
            var expected = ToDoItemRepository.connectedDb;

            //Act
            var actual = DatabaseConnection.GetConnection();

            //Assert
            Assert.AreEqual( expected, actual );
        }
        [TestMethod]
        public void Singleton_Pattern_Should_Work()
        {
            //Arrange
            var expected = ToDoItemRepository.connectedDb;

            //Act
            var actual = UserRepository.connectedDb;

            //Assert
            Assert.AreEqual( expected, actual );
        }
    }
}
