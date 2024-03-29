﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModuleRegistration.Controllers;
using ModuleRegistration.Data;
using ModuleRegistration.Models;
using Moq;

namespace TestModuleRegistration
{
    [TestClass]
    public class TestStudentsController
    {
        [TestMethod]
        public async Task ShouldListAllStudents()
        {
            var mockRepo = new Mock<IDataRepository>();
            mockRepo.Setup(repo => repo.StudentListAsync()).
                ReturnsAsync(GetStudentData());

            StudentsController controller = new StudentsController(mockRepo.Object);
            var result = await controller.GetAllRegisteredStudents();

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okResult = result.Result as OkObjectResult;

            var students = okResult.Value as List<Student>;
            Assert.AreEqual(2, students.Count);

            Assert.AreEqual("mwj7", students[0].Uid);
            Assert.AreEqual("Morgan", students[0].Forename);
            Assert.AreEqual("Jones", students[0].Surname);

            Assert.AreEqual("dop2", students[1].Uid);
            Assert.AreEqual("Dominic", students[1].Forename);
            Assert.AreEqual("Parr", students[1].Surname);

        }

        [TestMethod]
        public async Task ShouldListAllStudentUids()
        {
            var mockRepo = new Mock<IDataRepository>();
            mockRepo.Setup(repo => repo.StudentListAsync()).
                ReturnsAsync(GetStudentData());

            StudentsController controller = new StudentsController(mockRepo.Object);
            var result = await controller.GetAllStudentUids();

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okResult = result.Result as OkObjectResult;

            var uids = okResult.Value as List<string>;
            Assert.AreEqual(2, uids.Count);

            Assert.AreEqual("mwj7", uids[0]);
            Assert.AreEqual("dop2", uids[1]);
        }

        private List<Student> GetStudentData()
        {
            List<Student> students = new List<Student>();

            students.Add(new Student() { Uid = "mwj7", Forename = "Morgan", Surname = "Jones" });
            students.Add(new Student() { Uid = "dop2", Forename = "Dominic", Surname = "Parr" });

            return students;
        }
    }
}
