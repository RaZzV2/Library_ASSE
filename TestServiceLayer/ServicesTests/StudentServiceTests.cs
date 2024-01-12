using DataMapper;
using DomainModel;
using DomainModel.CustomValidationHelpers;
using Library.models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;
using ServiceLayer.IServices;
using ServiceLayer.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TestServiceLayer.ServicesTests
{
    [TestClass]
    public class StudentServiceTests
    {
        private IStudentIDAO mockStudentIDAO;
        private StudentService studentService;
        private Student student;

        private void AssertValidationException<T>(T instance, string expectedErrorMessage)
        {
            ModelValidationHelper.AssertValidationException(instance, expectedErrorMessage);
        }

        [TestInitialize]
        public void SetUp()
        {
            mockStudentIDAO = MockRepository.GenerateMock<IStudentIDAO>();
            studentService = new StudentService(mockStudentIDAO);
            student = new Student()
            {
                FirstName = "Test",
                LastName = "Test",
                Age = 23
            };
        }

        [TestMethod]
        public void AddValidStudentCallsIStudentIDAO()
        {
            studentService.Add(this.student);
            mockStudentIDAO.AssertWasCalled(mock => mock.Add(Arg<Student>.Is.Equal(this.student)), options => options.Repeat.Once());
        }

        [TestMethod]
        public void AddInvalidStudentCallsIStudentIDAO()
        {
            this.student.FirstName = "invalid_name++";
            var exception = Assert.ThrowsException<ValidationException>(() => studentService.Add(this.student));
            Assert.AreEqual("First name must not have special characters!", exception.Message);
        }

        [TestMethod]
        public void DeleteStudentCallsIStudentIDAO()
        {
            studentService.Delete(this.student);
            mockStudentIDAO.AssertWasCalled(mock => mock.Delete(Arg<Student>.Is.Equal(this.student)), options => options.Repeat.Once());
        }

        [TestMethod]
        public void UpdateStudentCallsIStudentIDAO()
        {
            studentService.Update(this.student);
            mockStudentIDAO.AssertWasCalled(mock => mock.Update(Arg<Student>.Is.Equal(this.student)), options => options.Repeat.Once());
        }
    }
}
