using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModuleRegistration.Data;
using ModuleRegistration.Models;

namespace ModuleRegistration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly IDataRepository _repo;

        public StudentsController(IDataRepository repo)
        {
            _repo = repo;
        }

        /**
        * Returns all student entities.
        */
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetAllRegisteredStudents()
        {
            return Ok(await _repo.StudentListAsync());
        }

        /**
         *  Return a list of all student uids.
         */
        [HttpGet("uids")]
        public async Task<ActionResult<IEnumerable<string>>> GetAllStudentUids()
        {            
            List<String> uids = new List<String>();
            var students = await _repo.StudentListAsync();
            foreach(Student s in students)
            {
                uids.Add(s.Uid);
            }
            return Ok(uids);
        }
        //api/students/{uid}
        [HttpDelete("{uid}")]
        public async Task<ActionResult> DeleteStudent(string uid)
        {
            if (!_repo.StudentExists(uid))
            {
                return NotFound();
            }
            await _repo.DeleteStudentAsync(uid);
            return Ok();
        }
        //api/students
        [HttpPost]
        public async Task<ActionResult> PostStudent([FromBody]Student student)
        {
            if (_repo.StudentExists(student.Uid))
            {
                return BadRequest();
            }
            await _repo.AddStudentAsync(student);
            return Ok();
        }
        //api/students
        [HttpDelete]
        public async Task<ActionResult> DeleteStudent()
        {
            await _repo.DeleteAllStudentAsync();
            return Ok();
        }

        //api/students/{uid}/{mid}
        [HttpGet("{uid}/{mid}")]
        public async Task<ActionResult> GetStudentModuleLink(string uid, int mid)
        {
            await _repo.GetModuleStudentAsync(mid, uid);
            return Ok();
        }
        //api/students/{uid}/{mid]
        [HttpPost("{uid}/{mid}")]
        public async Task<ActionResult> AddStudentModuleLink(string uid, int mid)
        {
            var student = await _repo.GetStudentAsync(uid);
            var module = await _repo.GetModuleAsync(mid);
            if(student == null || module == null)
            {
                return NotFound();
            }
            var moduleStudent = new ModuleStudent
            {
                Module = module,
                Student = student
            };
            await _repo.AddModuleStudentAsync(moduleStudent);
            return Ok();
        }
        //api/students/{uid}/{mid}
        [HttpDelete("{uid}/{mid}")]
        public async Task<ActionResult> DeleteStudentModuleLink(string uid, int mid)
        {
            var studentModuleLink = await _repo.GetModuleStudentAsync(mid, uid);
            if(studentModuleLink == null)
            {
                return NotFound();
            }
            await _repo.DeleteModuleStudentsAsync(studentModuleLink.Id);
            return Ok();
        }
    }
}