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
        private readonly ModuleRegistrationContext _context;

        public StudentsController(ModuleRegistrationContext context)
        {
            _context = context;
        }

        /**
        * Returns all student entities.
        */
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetAllRegisteredStudents()
        {
            return Ok(await _context.Students.ToListAsync());
        }

        /**
         *  Return a list of all student uids.
         */
        [HttpGet("uids")]
        public async Task<ActionResult<IEnumerable<string>>> GetAllStudentUids()
        {            
            List<String> uids = new List<String>();
            var students = await _context.Students.ToListAsync();
            foreach(Student s in students)
            {
                uids.Add(s.Uid);
            }
            return Ok(uids);
        }
    }
}