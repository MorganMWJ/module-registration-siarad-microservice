using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using ModuleRegistration.Data;
using ModuleRegistration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ModuleRegistration.Controllers
{

    /*
     * curl --header "Content-Type:application/json" --header "Accept:application/json" --request PUT https://localhost:44377/api/modules/SEM5641 -d {"ModuleId":'SEM5645',"StaffId":'dop2',"ModuleTitle":'Testttt'}
     */
    [Route("api/[controller]")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        private readonly IDataRepository _repo;

        public ModulesController(IDataRepository dataRepository)
        {
            _repo = dataRepository;
        }

        /**
         * Gets all modules for a specific user
         */
        // GET api/modules/user/{uid}
        [HttpGet("user/{uid}")]
        public async Task<ActionResult<IEnumerable<Module>>> GetModulesByUser(string uid)
        {
            if (uid == null)
            {
                return NotFound();
            }

            var userModules = await _repo.ModulesByStudentListAsync(uid);

            /* If user not student */
            if(userModules.Count == 0)
            {
                userModules = await _repo.ModulesByStaffListAsync(uid);
            }

            return Ok(userModules);
        }

        /**
         * Gets all modules for a specific year
         */
        // GET api/modules/year/{year}
        [HttpGet("year/{year}")]
        public async Task<ActionResult<IEnumerable<Module>>> GetModulesByYear(String year)
        {
            /* Regular Expression to match year between 1900-2099 */
            string pattern = @"^(19|20)\d{2}$";
            Regex rg = new Regex(pattern);
            if (year == null)
            {
                return NotFound();
            }
            if (!rg.IsMatch(year))
            {
                return BadRequest();
            }
            var moduleList = await _repo.ModulesByYearListAsync(year);
            return Ok(moduleList);
        }

        /**
         * Gets the registered students for a specific module
         */
        // GET api/modules/{id}/students
        [HttpGet("{id}/students")]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudentsByModule(int id)
        {
            var module = await _repo.GetModuleAsync(id);
            if (module == null)
            {
                return NotFound();
            }

            var students = await _repo.StudentsByModuleAsync(id);

            return Ok(students);
        }

        /**
         * Gets the staff for a specific module
         */
        // GET api/modules/{id}/staff
        [HttpGet("{id}/staff")]
        public async Task<ActionResult<IEnumerable<Student>>> GetStaffByModule(int id)
        {
            var module = await _repo.GetModuleAsync(id);
            if (module == null)
            {
                return NotFound();
            }

            var staff = await _repo.StaffByModuleAsync(id);

            return Ok(staff);
        }

        /**
         * Gets all Modules.
         */
        // GET api/modules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Module>>> GetAllModules()
        {
            return Ok(await _repo.ModuleListAsync());
        }

        /**
         * Get Module by ID.
         */
        [HttpGet("{id}")]
        public async Task<ActionResult<Module>> GetModuleById(int id)
        {
            var module = await _repo.GetModuleAsync(id);
            if (module == null)
            {
                return NotFound();
            }
            return Ok(module);
        }

        /**
         * Returns a list of modules from a given year that is associated with a specific user.
         */
        // GET api/modules/year/{year}/{uid}
        [HttpGet("year/{year}/{uid}")]
        public async Task<ActionResult<IEnumerable<Module>>> GetModulesByYearAndUser(String year, String uid)
        {
            if (year == null || uid == null)
            {
                return NotFound();
            }

            IEnumerable<Module> modules = new List<Module>();
            if (_repo.StaffExists(uid))
            {
                modules = await _repo.ModulesByYearAndStaff(year, uid);
            }
            else if (_repo.StudentExists(uid))
            {
                modules = await _repo.ModulesByYearAndStudent(year, uid);
            }
            else
            {
                return NotFound("User not found");
            }            
      
            return Ok(modules);
        }

        /**
         * Returns a specific module given its module code and year.
         */
         //Get api/modules/year/{year}/code/{code}
        [HttpGet("year/{year}/code/{code}")]
        public async Task<ActionResult<Module>> GetModulesByYearAndCode(String year, String code)
        {
            if (year == null || code == null)
            {
                return NotFound();
            }
            var specificModule = await _repo.GetModuleAsync(year, code);
            if (specificModule == null)
            {
                return NotFound();
            }
            return Ok(specificModule);
        }

        /**
         * Create a new module.
         */
        // POST api/modules
        [HttpPost]
        public async Task<ActionResult<IEnumerable<Module>>> CreateModule([FromBody]Module module)
        {
            /* Bad Request if module already exists */
            var mod = await _repo.GetModuleAsync(module.Id);
            if (mod != null)
            {
                return BadRequest();
            }

            await _repo.AddModuleAsync(module);
            return Ok(await _repo.ModuleListAsync());
        }


        // PUT api/modules
        [HttpPut("{id}")]
        //Doesn't like the anti-forgery token, not sure why, need to clarify with Neil
        //Cannot use a put method to edit the primary key -> could be forced with delete/post methods but in that case, just use delete/post methods...
        //BadRequest should be given to the user if they attempt this.
        public async Task<ActionResult> UpdateModule(int id, [FromBody] Module module)
        {
            if (!id.Equals(module.Id))
            {
                return BadRequest();
            }
            if (ModelState.IsValid)  
            {
                try
                {
                    await _repo.UpdateModuleAsync(module);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_repo.ModuleExists(module.Id))
                    {
                        return BadRequest();
                    }
                    throw;
                }
            }
            return Ok(await _repo.ModuleListAsync());
        }

        //DELETE api/modules/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteModule(int id)
        {
            var module = await _repo.GetModuleAsync(id);
            if (module == null)
            {
                return NotFound();
            }
            try
            {
                await _repo.DeleteModuleAsync(module);
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }
            return Ok(await _repo.ModuleListAsync());
        }
    }
}
