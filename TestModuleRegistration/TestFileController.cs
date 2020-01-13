using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ModuleRegistration.Controllers;
using ModuleRegistration.Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TestModuleRegistration
{
    [TestClass]
    public class TestFileController
    {
        [TestMethod]
        public async Task ShouldReturnBadRequestWhenNotGoodCampusCode_PostStudentFile()
        {
            string badClassCode = "AB4";
            var mockRepo = new Mock<IDataRepository>();
            var mockLog = new Mock<ILogger<FileController>>();
            FileController controller = new FileController(mockRepo.Object, mockLog.Object);

            var result = await controller.PostStudentFile(badClassCode);
            Assert.IsInstanceOfType(result, typeof(BadRequestResult));

            string responseStr = result.ToString();

        }
    }
}
