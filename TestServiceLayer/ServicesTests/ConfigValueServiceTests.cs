using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using ServiceLayer;
using ServiceLayer.IServices;
using ServiceLayer.Services;
using System.IO.Abstractions;
using System.Xml;

namespace TestServiceLayer.ServicesTests
{
    [TestClass]
    public class ConfigValueServiceTests
    {
        private ConfigValueService configValueService;

        [TestInitialize]
        public void Initialize()
        {
            configValueService = new ConfigValueService();
        }

        [TestMethod]
        public void LoadConfiguration_ShouldLoadXmlData_WhenFilePathIsValid()
        {
            // Act
            configValueService.LoadConfiguration("config.xml");
        }

        [TestMethod]
        public void GetValue()
        {
            // Arrange
            var expectedValue = "value";

            // Act
            configValueService.LoadConfiguration("config.xml");

            var result = configValueService.GetValue<string>("key");

            // Assert
            Assert.AreEqual(expectedValue, result);
        }

        [TestMethod]
        public void GetValueDefault()
        {

            // Act
            configValueService.LoadConfiguration("config.xml");

            var result = configValueService.GetValue<string>("invalidkey");

            // Assert
            Assert.AreEqual(default, result);
        }

    }
}
