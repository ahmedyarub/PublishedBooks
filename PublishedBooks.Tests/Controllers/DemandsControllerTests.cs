using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PublishedBooksDAL.Repositories;
using Moq;
using PublishedBooksDAL.Entities;
using System.Collections.Generic;
using PublishedBooks.Controllers;
using System.Web.Mvc;
using System.Web;
using System.Security.Principal;
using System.Linq.Expressions;

namespace PublishedBooks.Tests.Controllers

{
    [TestClass]
    public class DemandsControllerTests
    {
        [TestMethod]
        public void Can_Add_New_Demand()
        {
            // Arrange - create some test products
            var mockRepo = new Mock<IRepository<User>>();
            mockRepo.Setup(m => m.SearchFor(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(new List<User>() { new User() { Username = "admin", Password = "secret", Demands = null } });

            string demand = "test demand";

            var fakeHttpContext = new Mock<HttpContextBase>();
            var fakeIdentity = new GenericIdentity("admin");
            var principal = new GenericPrincipal(fakeIdentity, null);

            fakeHttpContext.Setup(t => t.User).Returns(principal);
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.Setup(t => t.HttpContext).Returns(fakeHttpContext.Object);

            DemandsController target = new DemandsController(mockRepo.Object);
            target.ControllerContext = controllerContext.Object;

            // Act
            ActionResult result = target.Add(demand);

            // Assert
            mockRepo.Verify(m => m.Update(It.IsAny<User>()));
        }

        [TestMethod]
        public void Can_Add_Delete_Demand()
        {
            // Arrange - create some test products
            var mockRepo = new Mock<IRepository<User>>();
            mockRepo.Setup(m => m.SearchFor(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(new List<User>() { new User() { Username = "admin", Password = "secret", Demands = new List<string>() { "test demand" } } });

            string demand = "test demand";

            var fakeHttpContext = new Mock<HttpContextBase>();
            var fakeIdentity = new GenericIdentity("admin");
            var principal = new GenericPrincipal(fakeIdentity, null);

            fakeHttpContext.Setup(t => t.User).Returns(principal);
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.Setup(t => t.HttpContext).Returns(fakeHttpContext.Object);

            DemandsController target = new DemandsController(mockRepo.Object);
            target.ControllerContext = controllerContext.Object;

            // Act
            ActionResult result = target.Remove(demand);

            // Assert
            mockRepo.Verify(m => m.Update(It.IsAny<User>()));
        }
    }
}
