using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PublishedBooks.Models;
using PublishedBooks.Infrastructure.Security;
using PublishedBooks.Controllers;
using PublishedBooksDAL.Repositories;
using PublishedBooksDAL.Entities;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PublishedBooks.Tests.Controllers
{
    [TestClass]
    public class AccountControllerTests
    {
        [TestMethod]
        public void Can_Login_With_Valid_Credentials()
        {
            // Arrange
            var mockAuth = new Mock<IAuthProvider>();
            var mockRepo = new Mock<IRepository<User>>();
            mockAuth.Setup(m => m.Authenticate("admin", "secret")).Returns(true);
            mockRepo.Setup(m => m.SearchFor(u => u.Username == "admin" && u.Password == "secret")).Returns(new List<User>() { new User() });

            LoginViewModel model = new LoginViewModel
            {
                Username = "admin",
                Password = "secret"
            };

            AccountController target = new AccountController(mockRepo.Object, mockAuth.Object);

            // Act
            ActionResult result = target.Login(model, "/MyURL");

            // Assert
            Assert.IsInstanceOfType(result, typeof(RedirectResult));
            Assert.AreEqual("/MyURL", ((RedirectResult)result).Url);
        }

        [TestMethod]
        public void Cannot_Login_With_Invalid_Credentials()
        {

            // Arrange
            var mockAuth = new Mock<IAuthProvider>();
            var mockRepo = new Mock<IRepository<User>>();
            mockAuth.Setup(m => m.Authenticate("badUser", "badPass")).Returns(false);
            mockRepo.Setup(m => m.SearchFor(u => u.Username == "badUser" && u.Password == "badPass")).Returns(new List<User>());

            LoginViewModel model = new LoginViewModel
            {
                Username = "badUser",
                Password = "badPass"
            };

            AccountController target = new AccountController(mockRepo.Object, mockAuth.Object);

            // Act
            ActionResult result = target.Login(model, "/MyURL");

            // Assert
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.IsFalse(((ViewResult)result).ViewData.ModelState.IsValid);
        }
    }
}
