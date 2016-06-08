using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PublishedBooksDAL.Repositories;
using System.Collections.Generic;
using PublishedBooksDAL.Entities;
using PublishedBooks.Controllers;
using System.Net.Http;
using System.Web.Http;
using System.Linq.Expressions;

namespace PublishedBooks.Tests.Controllers
{
    [TestClass]
    public class BookControllerTests
    {
        [TestMethod]
        public void GetAllBooks_ShouldReturnAllBooks()
        {
            // Arrange
            var mockRepo = new Mock<IRepository<Book>>();
            mockRepo.Setup(m => m.GetAll()).Returns(new List<Book>() { new Book() { Title = "test title"} });

            BookController target = new BookController(mockRepo.Object);
            target.Request = new HttpRequestMessage();
            target.Request.SetConfiguration(new HttpConfiguration());
            target.Request.RequestUri = new Uri("http://localhost/api/Get");
            // Act
            IEnumerable<Book> result = target.Get();

            // Assert
            Assert.AreEqual("test title", ((List<Book>) result)[0].Title);
        }

        [TestMethod]
        public void GetAllBooks_ShouldReturnFilteredBooks()
        {
            // Arrange
            var mockRepo = new Mock<IRepository<Book>>();
            mockRepo.Setup(m => m.SearchFor(It.IsAny<Expression<Func<Book, bool>>>())).Returns(new List<Book>() { new Book() { Title = "title1" }, new Book() { Title = "title2" } });

            BookController target = new BookController(mockRepo.Object);
            target.Request = new HttpRequestMessage();
            target.Request.SetConfiguration(new HttpConfiguration());
            target.Request.RequestUri = new Uri("http://localhost/api/Get?title=title2");
            // Act
            IEnumerable<Book> result = target.Get();

            // Assert
            mockRepo.Verify(m => m.SearchFor(It.IsAny<Expression<Func<Book, bool>>>()));
        }
    }
}
