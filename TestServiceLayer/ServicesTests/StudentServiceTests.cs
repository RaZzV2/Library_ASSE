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
using System.Linq;
using System.Text;

namespace TestServiceLayer.ServicesTests
{
    [TestClass]
    public class StudentServiceTests
    {
        private IStudentIDAO mockStudentIDAO;
        private StudentService studentService;
        private Student student;

        [TestInitialize]
        public void SetUp()
        {
            mockStudentIDAO = MockRepository.GenerateMock<IStudentIDAO>();
            studentService = new StudentService(mockStudentIDAO);
            student = new Student()
            {
                Id = 1,
                FirstName = "Test",
                LastName = "Test",
                Age = 23
            };
        }

        [TestMethod]
        public void GetById_ReturnsCorrectStudent()
        {
            int studentId = 1;
            
            mockStudentIDAO.Stub(x => x.GetById(Arg<int>.Is.Anything)).Return(this.student);

            var result = studentService.GetById(studentId);

            Assert.IsNotNull(result);
            Assert.AreEqual(this.student, result);
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
        public void GetAllStudentsCallsIStudentIDAO()
        {
            var expectedStudents = new List<Student>
            {
                new Student
                {
                    FirstName = "Test",
                    LastName = "Test",
                    Age = 23
                },
            };

            mockStudentIDAO.Stub(x => x.GetAll()).Return(expectedStudents);

            var result = studentService.GetAll();

            mockStudentIDAO.AssertWasCalled(mock => mock.GetAll(), options => options.Repeat.Once());
            CollectionAssert.AreEqual(expectedStudents, result.ToList());
        }

        [TestMethod]
        public void UpdateStudentCallsIStudentIDAO()
        {
            studentService.Update(this.student);
            mockStudentIDAO.AssertWasCalled(mock => mock.Update(Arg<Student>.Is.Equal(this.student)), options => options.Repeat.Once());
        }
    }
}
