// <copyright file="ConfigValueServiceTests.cs" company="Transilvania University of Brasov">
// Dragomir Razvan
// </copyright>

namespace TestServiceLayer.ServicesTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using ServiceLayer.Services;

    /// <summary>
    /// Unit tests for the ConfigValueService class.
    /// </summary>
    [TestClass]
    public class ConfigValueServiceTests
    {
        /// <summary>
        /// Represents an instance of the ConfigValueService, which provides operations related to configuration values.
        /// </summary>
        private ConfigValueService configValueService;

        /// <summary>
        /// Initializes a new instance of the ConfigValueService before each test.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            this.configValueService = new ConfigValueService();
        }

        /// <summary>
        /// Unit test for the ConfigValueService's LoadConfiguration method, which should load XML data when the file path is valid.
        /// </summary>
        [TestMethod]
        public void LoadConfiguration_ShouldLoadXmlData_WhenFilePathIsValid()
        {
            this.configValueService.LoadConfiguration("config.xml");
        }

        /// <summary>
        /// Unit test for the ConfigValueService's GetValue method, which should retrieve the expected value based on a given key after loading configuration data.
        /// </summary>
        [TestMethod]
        public void GetValue()
        {
            var expectedValue = "value";

            this.configValueService.LoadConfiguration("config.xml");

            var result = this.configValueService.GetValue<string>("key");

            Assert.AreEqual(expectedValue, result);
        }

        /// <summary>
        /// Unit test for the ConfigValueService's GetValue method with a default value, which should return the default value when the key is not found after loading configuration data.
        /// </summary>
        [TestMethod]
        public void GetValueDefault()
        {
            this.configValueService.LoadConfiguration("config.xml");

            var result = this.configValueService.GetValue<string>("invalidkey");

            Assert.AreEqual(default, result);
        }
    }
}
