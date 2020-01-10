using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModuleRegistration.Controllers;
using ModuleRegistration.Data;
using ModuleRegistration.Models;
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
            IDataRepository mockRepo = new MockRepository();
            StaffController controller = new StaffController(mockRepo);
            var result = await controller.GetAllStaffMembers();

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okResult = result.Result as OkObjectResult;

            var staff = okResult.Value as List<Staff>;
            Assert.AreEqual(2, staff.Count);
        }

        [TestMethod]
        public async Task ShouldListAllStaffUids()
        {
            IDataRepository mockRepo = new MockRepository();
            StaffController controller = new StaffController(mockRepo);
            var result = await controller.GetAllStaffUids();

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okResult = result.Result as OkObjectResult;

            var uids = okResult.Value as List<string>;
            Assert.AreEqual(2, uids.Count);

            Assert.AreEqual("nst", uids[0]);
            Assert.AreEqual("nwh", uids[1]);
        }
    }
}
