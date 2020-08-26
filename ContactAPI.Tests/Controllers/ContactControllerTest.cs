using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Http.Routing;
using ContactAPI.Controllers;
using ContactAPI.Data;
using ContactAPI.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace ContactAPI.Tests.Controllers
{
    [TestClass]
    public class ContactControllerTest
    {
        private readonly IDataRepository<Contact> _dataRepository;
        ContactEntities contactEntities = new ContactEntities();


        public ContactControllerTest()
        {
            _dataRepository = new ContactManager(contactEntities);
        }

        [TestMethod]
        public void GetContactById()
        {           
            // Arrange
            ContactController controller = new ContactController(_dataRepository);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            var response = controller.Get(1);

            // Assert
            Contact contact;
            Assert.IsTrue(response.TryGetContentValue<Contact>(out contact));            
            Assert.AreEqual(1, contact.Id);
        }


        [TestMethod]
        public void PostContact()
        {
            // This version uses a mock UrlHelper.

            // Arrange
            ContactController controller = new ContactController(_dataRepository);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();
            
            // Act
            Contact contact = new Contact() { FirstName = "Test 1", LastName = "Test 1", Email = "justin@gmail.com", PhoneNumber = "9860577874", Status = false };
            var response = controller.Post(contact);

            Contact result;
            Assert.IsTrue(response.TryGetContentValue<Contact>(out result));
            Assert.AreEqual(1014, result.Id);

        }


        [TestMethod]
        public void PutContact()
        {
            // This version uses a mock UrlHelper.

            // Arrange
            ContactController controller = new ContactController(_dataRepository);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act
            Contact contact = new Contact() { Id=1014, FirstName = "Test 1", LastName = "Test 2", Email = "test1@gmail.com", PhoneNumber = "9860577874", Status = false };
            var response = controller.Put(1014, contact);

            // Act
            var getresponse = controller.Get(1014);

            // Assert
            Contact getcontact;
            Assert.IsTrue(getresponse.TryGetContentValue<Contact>(out getcontact));
            Assert.AreEqual(1014, getcontact.Id);
        }

        [TestMethod]
        public void DeleteContact()
        {
            // This version uses a mock UrlHelper.

            // Arrange
            ContactController controller = new ContactController(_dataRepository);
            controller.Request = new HttpRequestMessage();
            controller.Configuration = new HttpConfiguration();

            // Act            
            var response = controller.Delete(1011);

            Assert.IsInstanceOfType(response.Result.StatusCode, typeof(HttpStatusCode));
        }


    }
}

