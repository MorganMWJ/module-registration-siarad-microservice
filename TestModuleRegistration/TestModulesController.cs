using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModuleRegistration.Controllers;
using ModuleRegistration.Data;
using ModuleRegistration.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MockRepository = ModuleRegistration.Data.MockRepository;

namespace TestModuleRegistration
{
    [TestClass]
    public class TestModulesController
    {
        //[TestMethod]
        //public async Task ShouldGetAllModulesForStaffUser()
        //{
        //    string staffUid = "nwh";
        //    IDataRepository mockRepo = new MockRepository();
        //    ModulesController controller = new ModulesController(mockRepo);            
        //    var result = await controller.GetModulesByUser(staffUid);

        //    Assert.IsInstanceOfType(result.Result, typeof(OkResult));
        //    var okResult = result.Result as OkObjectResult;

        //    var staffModules = okResult.Value as List<Module>;

        //    //more asserts here TODO too much logic 
        //    // in DataRepo's ModulesByStudentListAsync maybe move it to constroller
        //}

        //[TestMethod]
        //public async Task ShouldGetAllModulesForStudentUser()
        //{
        //    string studentUid = "mwj7";
        //    IDataRepository mockRepo = new MockRepository();
        //    ModulesController controller = new ModulesController(mockRepo);
        //    var result = await controller.GetModulesByUser(studentUid);

        //    //more asserts here TODO too much logic 
        //    // in DataRepo's ModulesByStudentListAsync maybe move it to constroller
        //}

        [TestMethod]
        public async Task ShouldReturnNotFoundWhenPassedNull_GetModulesByUser()
        {            
            IDataRepository mockRepo = new MockRepository();
            ModulesController controller = new ModulesController(mockRepo);
            var result = await controller.GetModulesByUser(null);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }
        
        [TestMethod]
        public async Task ShouldGetModulesForValidYear_GetModulesByYear()
        {
            string year = "2020";
            IDataRepository mockRepo = new MockRepository();
            ModulesController controller = new ModulesController(mockRepo);
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
            IDataRepository mockRepo = new MockRepository();
            ModulesController controller = new ModulesController(mockRepo);
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
            mockRepo.Setup(repo => repo.GetModuleByIdAsync(id)).
                ReturnsAsync((Module)null);

            ModulesController controller = new ModulesController(mockRepo.Object);

            var result = await controller.GetStudentsByModule(id);
            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }
    }
}
