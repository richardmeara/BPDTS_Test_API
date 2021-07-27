using BPDTS_Test.API.Tests.Services;
using BPDTS_Test_API.Models;
using BPDTS_Test_API.Models.Interfaces;
using BPDTS_Test_API.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace BPDTS_Test.API.Tests.Unit
{
    [TestClass]
    public class BPDTSTestApiServiceTests
    {
        private IHttpPipeline _httpPipeline;
        private IConfiguration _config;
        private BPDTSTestApiService _service;

        [TestInitialize]
        public void Initalize()
        {
            var config = new Mock<IConfiguration>();
            _config = config.Object;

            _httpPipeline = new HttpPipelineFake();
            _service = new BPDTSTestApiService(config: _config, httpPipeline: _httpPipeline);
        }

        [TestMethod]
        public void GetUsersByCity_ReturnsOkObjectResult()
        {
            var result = _service.GetUsersByCity("London");
            Assert.IsInstanceOfType(result.Result, typeof(List<User>));
        }

        [TestMethod]
        public void GetUsersByCity_ReturnsCorrectCount()
        {
            var result = _service.GetUsersByCity("London");
            var londonUsers = result.Result;
            Assert.AreEqual(1, londonUsers.Count);
        }

        [TestMethod]
        public void GetUsersByCity_NotExists()
        {
            var result = _service.GetUsersByCity("QWERTY");
            var londonUsers = result.Result;
            Assert.AreEqual(0, londonUsers.Count);
        }

        [TestMethod]
        public void GetUser_ReturnsOkObjectResult()
        {
            var result = _service.GetUser("1");
            Assert.IsInstanceOfType(result.Result, typeof(User));
        }

        [TestMethod]
        public void GetUser_Exists()
        {
            var result = _service.GetUser("1");
            var londonUser = result.Result;
            Assert.IsNotNull(londonUser);
        }

        [TestMethod]
        public void GetUser_NotExists()
        {
            var result = _service.GetUser("0");
            var londonUser = result.Result;
            Assert.IsNull(londonUser);
        }

        [TestMethod]
        public void GetUsers_ReturnsOkObjectResult()
        {
            var result = _service.GetUsers();
            Assert.IsInstanceOfType(result.Result, typeof(List<User>));
        }

        [TestMethod]
        public void GetUsers_ReturnsCorrectCount()
        {
            var result = _service.GetUsers();
            var londonUsers = result.Result;
            Assert.AreEqual(3, londonUsers.Count);
        }

        [TestMethod]
        public void GetUsersByLondonProximity_ReturnsOkObjectResult()
        {
            var result = _service.GetUsersByLondonProximity();
            Assert.IsInstanceOfType(result.Result, typeof(List<User>));
        }

        [TestMethod]
        public void GetUsersByLondonProximity_ReturnsCorrectCount()
        {
            var result = _service.GetUsersByLondonProximity();
            var londonUsers = result.Result;
            Assert.AreEqual(1, londonUsers.Count);
        }

        [TestMethod]
        public void GetLondonUsersByCityNameAndCoordinates_ReturnsOkObjectResult()
        {
            var result = _service.GetLondonUsersByCityNameAndCoordinates();
            Assert.IsInstanceOfType(result.Result, typeof(List<User>));
        }

        [TestMethod]
        public void GetLondonUsersByCityNameAndCoordinates_ReturnsCorrectCount()
        {
            var result = _service.GetLondonUsersByCityNameAndCoordinates();
            var londonUsers = result.Result;
            Assert.AreEqual(2, londonUsers.Count);
        }
    }
}