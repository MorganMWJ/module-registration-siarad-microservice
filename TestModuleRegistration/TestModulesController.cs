﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModuleRegistration.Controllers;
using ModuleRegistration.Data;
using ModuleRegistration.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TestModuleRegistration
{
    [TestClass]
    public class TestModulesController
    {
        [TestMethod]
        public async Task ShouldGetAllModulesForStaffUser()
        {
            string staffUid = "nwh";
            var mockRepo = new Mock<IDataRepository>();
            List<Module> emptyList = new List<Module>();
            List<Module> moduleData = StaffModulesFornwh();
            mockRepo.Setup(repo => repo.ModulesByStudentListAsync(staffUid)).
                ReturnsAsync(emptyList);
            mockRepo.Setup(repo => repo.ModulesByStaffListAsync(staffUid)).
                ReturnsAsync(moduleData);

            ModulesController controller = new ModulesController(mockRepo.Object);
            var result = await controller.GetModulesByUser(staffUid);

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okResult = result.Result as OkObjectResult;

            var staffModules = okResult.Value as List<Module>;

            Assert.AreEqual(moduleData.Count, staffModules.Count);
            Assert.AreEqual(moduleData[0], staffModules[0]);
        }

        [TestMethod]
        public async Task ShouldGetAllModulesForStudentUser()
        {
            string studentUid = "mwj7";
            var mockRepo = new Mock<IDataRepository>();
            List<Module> emptyList = new List<Module>();
            List<Module> moduleData = StudentModulesFormwj7();
            mockRepo.Setup(repo => repo.ModulesByStudentListAsync(studentUid)).
                ReturnsAsync(emptyList);
            mockRepo.Setup(repo => repo.ModulesByStaffListAsync(studentUid)).
                ReturnsAsync(moduleData);

            ModulesController controller = new ModulesController(mockRepo.Object);
            var result = await controller.GetModulesByUser(studentUid);

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okResult = result.Result as OkObjectResult;

            var staffModules = okResult.Value as List<Module>;

            Assert.AreEqual(moduleData.Count, staffModules.Count);
            Assert.AreEqual(moduleData[0], staffModules[0]);
        }

        

        [TestMethod]
        public async Task ShouldReturnNotFoundWhenPassedNull_GetModulesByUser()
        {
            var mockRepo = new Mock<IDataRepository>();
            ModulesController controller = new ModulesController(mockRepo.Object);
            var result = await controller.GetModulesByUser(null);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }
        
        [TestMethod]
        public async Task ShouldGetModulesForValidYear_GetModulesByYear()
        {
            string year = "2020";
            var mockRepo = new Mock<IDataRepository>();
            mockRepo.Setup(repo => repo.ModulesByYearListAsync(year)).
                ReturnsAsync(ModulesInYear2020());

            ModulesController controller = new ModulesController(mockRepo.Object);
            var result = await controller.GetModulesByYear(year);
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));

            var okResult = result.Result as OkObjectResult;
            var modules = okResult.Value as List<Module>;

            Assert.AreEqual(2, modules.Count);

            foreach (Module m in modules)
            {
                Assert.AreEqual("2020", m.Year);
            }
        }

        

        [TestMethod]
        public async Task ShouldReturnOkWithEmptyListIfNoModulesForGivenYear_GetModulesByYear()
        {
            string futureYear = "2040";
            List<Module> emptyResult = new List<Module>();
            var mockRepo = new Mock<IDataRepository>();
            mockRepo.Setup(repo => repo.ModulesByYearListAsync(futureYear)).
                ReturnsAsync(emptyResult);

            ModulesController controller = new ModulesController(mockRepo.Object);

            var result = await controller.GetModulesByYear(futureYear);
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));

            var okResult = result.Result as OkObjectResult;
            var modules = okResult.Value as List<Module>;

            Assert.AreEqual(0, modules.Count);
        }

        [TestMethod]
        public async Task ShouldReturnNotFoundWhenPassedNull_GetModulesByYear()
        {
            var mockRepo = new Mock<IDataRepository>();
            ModulesController controller = new ModulesController(mockRepo.Object);
            var result = await controller.GetModulesByYear(null);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task ShouldReturnBadRequestWhenPassedInvalidYear_GetModulesByYear()
        {
            string futureYear = "badyear";
            var mockRepo = new Mock<IDataRepository>();

            ModulesController controller = new ModulesController(mockRepo.Object);

            var result = await controller.GetModulesByYear(futureYear);
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task ShouldReturnStudentsOnAModuleWithStudents_GetStudentsByModule()
        {
            Module module = new Module() { Id = 3, Code = "SEM5640", Year = "2020", ClassCode = "AB0", CoordinatorUid = "nst", Title = "Developing Advanced Internet Based-Applications" };
            List<Student> studentData = new List<Student>();
            studentData.Add(new Student() { Uid = "mwj7", Forename = "Morgan", Surname = "Jones"});
            studentData.Add(new Student() { Uid = "dop2", Forename = "Dominic", Surname = "Parr" });

            var mockRepo = new Mock<IDataRepository>();
            mockRepo.Setup(repo => repo.GetModuleAsync(module.Id)).
                ReturnsAsync(module);
            mockRepo.Setup(repo => repo.StudentsByModuleAsync(module.Id)).
                ReturnsAsync(studentData);

            ModulesController controller = new ModulesController(mockRepo.Object);

            var result = await controller.GetStudentsByModule(module.Id);
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));

            var okResult = result.Result as OkObjectResult;
            var students = okResult.Value as List<Student>;

            Assert.AreEqual(2, students.Count);

            int i = 0;
            foreach (Student m in students)
            {
                Assert.AreEqual(studentData[i].Uid, m.Uid);
                Assert.AreEqual(studentData[i].Forename, m.Forename);
                Assert.AreEqual(studentData[i].Surname, m.Surname);
                i++;
            }
        }

        [TestMethod]
        public async Task ShouldReturnOkWithEmptyListIfModuleHasZeroStudents_GetStudentsByModule()
        {
            Module module = new Module() { Id= 3, Code = "SEM5640", Year = "2020", ClassCode = "AB0", CoordinatorUid = "nst", Title = "Developing Advanced Internet Based-Applications" };
            List<Student> studentData = new List<Student>();
            var mockRepo = new Mock<IDataRepository>();
            mockRepo.Setup(repo => repo.GetModuleAsync(module.Id)).
               ReturnsAsync(module);
            mockRepo.Setup(repo => repo.StudentsByModuleAsync(module.Id)).
                ReturnsAsync(studentData);

            ModulesController controller = new ModulesController(mockRepo.Object);

            var result = await controller.GetStudentsByModule(module.Id);
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));

            var okResult = result.Result as OkObjectResult;
            var students = okResult.Value as List<Student>;

            Assert.AreEqual(0, students.Count);
        }

        [TestMethod]
        public async Task ShouldReturnNotFoundIfModuleDoesntExist_GetStudentsByModule()
        {
            int id = 3;
            var mockRepo = new Mock<IDataRepository>();
            mockRepo.Setup(repo => repo.GetModuleAsync(id)).
                ReturnsAsync((Module)null);

            ModulesController controller = new ModulesController(mockRepo.Object);

            var result = await controller.GetStudentsByModule(id);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task ShouldGetAllModules_GetAllModules()
        {
            var mockRepo = new Mock<IDataRepository>();
            List<Module> moduleData = Modules();
            mockRepo.Setup(repo => repo.ModuleListAsync()).
               ReturnsAsync(moduleData);

            ModulesController controller = new ModulesController(mockRepo.Object);

            var result = await controller.GetAllModules();
            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));

            var okResult = result.Result as OkObjectResult;
            var modules = okResult.Value as List<Module>;

            Assert.AreEqual(2, modules.Count);

            Assert.AreEqual(moduleData[0], modules[0]);
            Assert.AreEqual(moduleData[1], modules[1]);
        }

        [TestMethod]
        public async Task ShouldReturnNotFoundIfDoesNotExist_GetModuleById()
        {
            int id = 56;
            var mockRepo = new Mock<IDataRepository>();
            mockRepo.Setup(repo => repo.GetModuleAsync(id)).
                ReturnsAsync((Module)null);

            ModulesController controller = new ModulesController(mockRepo.Object);

            var result = await controller.GetModuleById(id);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task ShouldReturnBadRequestIfModuleExists_CreateModule()
        {
            var module = new Module() { Id = 5, Code = "SEM5640", Year = "2020", ClassCode = "AB0", CoordinatorUid = "nst", Title = "Developing Advanced Internet Based-Applications" };

            var mockRepo = new Mock<IDataRepository>();
            mockRepo.Setup(repo => repo.GetModuleAsync(module.Id)).
                ReturnsAsync(module);

            ModulesController controller = new ModulesController(mockRepo.Object);

            var result = await controller.CreateModule(module);
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task ShouldReturnBadRequestIfModuleIdDoesntMatch_UpdateModule()
        {
            int wrongId = 46;
            var module = new Module() { Id = 5, Code = "SEM5640", Year = "2020", ClassCode = "AB0", CoordinatorUid = "nst", Title = "Developing Advanced Internet Based-Applications" };

            var mockRepo = new Mock<IDataRepository>();
            ModulesController controller = new ModulesController(mockRepo.Object);

            var result = await controller.UpdateModule(wrongId, module);
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task ShouldReturnNotFoundIfModuleDoesntExist_DeleteModule()
        {
            int wrongId = 46;
            
            var mockRepo = new Mock<IDataRepository>();
            ModulesController controller = new ModulesController(mockRepo.Object);

            var result = await controller.DeleteModule(wrongId);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        private List<Module> StaffModulesFornwh()
        {
            List<Module> modules = new List<Module>();

            modules.Add(new Module() { Code = "SEM5640", Year = "2020", ClassCode = "AB0", CoordinatorUid = "nst", Title = "Developing Advanced Internet Based-Applications" });

            return modules;
        }

        private List<Module> StudentModulesFormwj7()
        {
            List<Module> modules = new List<Module>();

            modules.Add(new Module() { Code = "SEM5640", Year = "2020", ClassCode = "AB0", CoordinatorUid = "nst", Title = "Developing Advanced Internet Based-Applications" });
            modules.Add(new Module() { Code = "CSM1620", Year = "2020", ClassCode = "AB0", CoordinatorUid = "tjn2", Title = "Fundamentals of Intelligent Systems" });

            return modules;
        }

        private List<Module> ModulesInYear2020()
        {
            List<Module> modules = new List<Module>();

            modules.Add(new Module() { Code = "SEM5640", Year = "2020", ClassCode = "AB0", CoordinatorUid = "nst", Title = "Developing Advanced Internet Based-Applications" });
            modules.Add(new Module() { Code = "CSM1620", Year = "2020", ClassCode = "AB0", CoordinatorUid = "tjn2", Title = "Fundamentals of Intelligent Systems" });

            return modules;
        }

        private List<Module> Modules()
        {
            List<Module> modules = new List<Module>();

            modules.Add(new Module() { Code = "SEM5640", Year = "2020", ClassCode = "AB0", CoordinatorUid = "nst", Title = "Developing Advanced Internet Based-Applications" });
            modules.Add(new Module() { Code = "CSM1620", Year = "2020", ClassCode = "AB0", CoordinatorUid = "tjn2", Title = "Fundamentals of Intelligent Systems" });

            return modules;
        }
    }
}
