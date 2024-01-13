using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using ServiceLayer.IServices;
using ServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace TestServiceLayer.ServicesTests
{
    [TestClass]
    public class ConfigValueServiceTests
    {
        private IConfigValue mockConfigValue;
        string filePath;

        [TestInitialize]
        public void SetUp()
        {
            mockConfigValue = MockRepository.GenerateMock<IConfigValue>();
            filePath = "configuration.xml";
        }

        [TestMethod]
        public void Value_ReturnsCorrectValueForKey()
        {
            string keyToTest = "C";
            int expectedValue = 4;

            mockConfigValue.Stub(c => c.Value(keyToTest,filePath)).Return(expectedValue);

            int actualValue = mockConfigValue.Value(keyToTest, filePath);

            Assert.AreEqual(expectedValue, actualValue);
        }

        [TestMethod]
        public void Value_ReturnsDefaultForInvalidFilePath()
        {
            string keyToTest = "C";
            string invalidFilePath = "invalid.xml";

            mockConfigValue.Stub(c => c.Value(keyToTest, invalidFilePath)).Return(0);

            int actualValue = mockConfigValue.Value(keyToTest, invalidFilePath);

            Assert.AreEqual(0, actualValue);
        }

        [TestMethod]
        public void Value_ReturnsValueNoExistForValidPath()
        {
            string keyToTest = "X";
            int expectedValue = 0;

            mockConfigValue.Stub(c => c.Value(keyToTest, filePath)).Return(expectedValue);

            int actualValue = mockConfigValue.Value(keyToTest, filePath);

            Assert.AreEqual(expectedValue, actualValue);
        }
    }
}
