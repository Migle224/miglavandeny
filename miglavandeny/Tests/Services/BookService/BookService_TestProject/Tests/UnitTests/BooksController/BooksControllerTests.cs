using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace BookService_TestProject.Tests.UnitTests.BooksController
{
    class BooksControllerTests
    {
        [Test]
        public void GetsAllBooks() { Assert.IsTrue(false); }
        [Test]
        public void GetsBookByIdWhenIdExists() { Assert.IsTrue(false); }
        [Test]
        public void ReturnsEmptyWhenBookIdDoesnotExist() { Assert.IsTrue(false); }
        [Test]
        public void GetsBookdByAuthor() { Assert.IsTrue(false); }
        [Test]
        public void ReturnsEmptyWhenAuthorDoesnotExist() { Assert.IsTrue(false); }
        [Test]
        public void ReturnsEmptyWhenAuthorDoesnotHaveBooks() { Assert.IsTrue(false); }
        [Test]
        public void GetsBooksByTag() { Assert.IsTrue(false); }
        [Test]
        public void ReturnsEmptyWhenTagDoesntExist() { Assert.IsTrue(false); }
        [Test]
        public void ReturnsEmptyWhenTagIsnotAssignToBooks() { Assert.IsTrue(false); }
        [Test]
        public void ReturnsNewAddedBook() { Assert.IsTrue(false); }
        [Test]
        public void ReturnsErrorWhenBookIdalreadyExists() { Assert.IsTrue(false); }
        [Test]
        public void CreatesNewAuthorWhenAuthorsDoesnotExistsWhenAddsBook() { Assert.IsTrue(false); }
        [Test]
        public void UseExistingAuthorWhenAddsBook() { Assert.IsTrue(false); }
        [Test]
        public void DeletesAllBooks() { Assert.IsTrue(false); }
        [Test]
        public void DeletesBookById() { Assert.IsTrue(false); }
        [Test]
        public void DoesnDeleteAnyBookWhenIdDoesntExist() { Assert.IsTrue(false); }
        [Test]
        public void UpdatesExistingBook() { Assert.IsTrue(false); }
        [Test]
        public void UpdatesAuthorOfBook() { Assert.IsTrue(false); }
        [Test]
        public void ReturnsEroorWhenTriesToUpdateNotExistingBook() { Assert.IsTrue(false); }
        [Test]
        public void DeletesBookTag() { Assert.IsTrue(false); }
        [Test]
        public void AddsBookTagWhenTagExists() { Assert.IsTrue(false); }
        [Test]
        public void CreatedAndAddsTagwhenTagDoesntExist() { Assert.IsTrue(false); }
        [Test]
        public void DeletesBookAuthor() { Assert.IsTrue(false); }

    }
}
