using Microsoft.VisualStudio.TestTools.UnitTesting;
using CRUD_application_2.Controllers;
using CRUD_application_2.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CRUD_application_2.Tests
{
    [TestClass]
    public class UserControllerTests
    {
        private UserController controller;
        private List<User> users;

        [TestInitialize]
        public void TestInitialize()
        {
            users = new List<User>
            {
                new User { Id = 1, Name = "Test1", Email = "test1@test.com" },
                new User { Id = 2, Name = "Test2", Email = "test2@test.com" }
            };
            UserController.userlist = users;
            controller = new UserController();
        }

        [TestMethod]
        public void IndexTest()
        {
            // Act
            var result = controller.Index() as ViewResult;
            var model = result?.Model as List<User>;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(users.Count, model?.Count);
        }

        [TestMethod]
        public void DetailsTest()
        {
            // Arrange
            var user = users.First();

            // Act
            var result = controller.Details(user.Id) as ViewResult;
            var model = result?.Model as User;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(user, model);
        }

        [TestMethod]
        public void CreateTest()
        {
            // Arrange
            var user = new User { Name = "Test3", Email = "test3@test.com" };

            // Act
            controller.Create(user);
            var result = UserController.userlist.FirstOrDefault(u => u.Name == user.Name && u.Email == user.Email);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(user.Name, result.Name);
            Assert.AreEqual(user.Email, result.Email);
        }

        [TestMethod]
        public void EditTest()
        {
            // Arrange
            var user = users.First();
            user.Name = "Updated";

            // Act
            controller.Edit(user.Id, user);
            var result = UserController.userlist.FirstOrDefault(u => u.Id == user.Id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Updated", result.Name);
        }

        [TestMethod]
        public void DeleteTest()
        {
            // Arrange
            var user = users.First();

            // Act
            controller.Delete(user.Id, null);
            var result = UserController.userlist.FirstOrDefault(u => u.Id == user.Id);

            // Assert
            Assert.IsNull(result);
        }
    }
}
