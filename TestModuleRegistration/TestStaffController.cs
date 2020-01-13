using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModuleRegistration.Controllers;
using ModuleRegistration.Data;
using ModuleRegistration.Models;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestModuleRegistration
{
    [TestClass]
    public class TestStaffController
    {
        [TestMethod]
        public async Task ShouldListAllStaff()
        {
            var mockRepo = new Mock<IDataRepository>();
            mockRepo.Setup(repo => repo.StaffListAsync()).
                ReturnsAsync(GetStaffData());

            StaffController controller = new StaffController(mockRepo.Object);
            var result = await controller.GetAllStaffMembers();

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okResult = result.Result as OkObjectResult;

            var staff = okResult.Value as List<Staff>;
            Assert.AreEqual(2, staff.Count);

            Assert.AreEqual("nst", staff[0].Uid);
            Assert.AreEqual("Neil", staff[0].Forename);
            Assert.AreEqual("Taylor", staff[0].Surname);

            Assert.AreEqual("nwh", staff[1].Uid);
            Assert.IsNull(staff[1].Forename);
            Assert.IsNull(staff[1].Surname);
        }

        [TestMethod]
        public async Task ShouldListAllStaffUids()
        {
            var mockRepo = new Mock<IDataRepository>();
            mockRepo.Setup(repo => repo.StaffListAsync()).
                ReturnsAsync(GetStaffData());

            StaffController controller = new StaffController(mockRepo.Object);
            var result = await controller.GetAllStaffUids();

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okResult = result.Result as OkObjectResult;

            var uids = okResult.Value as List<string>;
            Assert.AreEqual(2, uids.Count);

            Assert.AreEqual("nst", uids[0]);
            Assert.AreEqual("nwh", uids[1]);
        }

        private List<Staff> GetStaffData()
        {
            List<Staff> staff = new List<Staff>();

            staff.Add(new Staff() { Uid = "nst", Forename = "Neil", Surname = "Taylor" });
            staff.Add(new Staff() { Uid = "nwh" });

            return staff;
        }
    }
}
