using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using ModuleRegistration.Data;
using ModuleRegistration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly ModuleRegistrationContext _context;

        public ModulesController(ModuleRegistrationContext context)
        {
            _context = context;
        }

        /**
         * Gets all modules for a specific year
         */
        // GET api/modules/year/{year}
        [HttpGet("year/{year}")]
        public ActionResult<Module> GetModulesByYear(String year)
        {
            if (year == null)
            {
                return NotFound();
            }
            var moduleList = _context.Modules.Where(m => m.Year.Equals(year));
            if (moduleList == null)
            {
                return NotFound();
            }
            return Ok(moduleList);
        }

        /**
         * Gets the registered students for a specific module
         */
        // GET api/modules/{id}/students
        [HttpGet("{module_id}/students")]
        public ActionResult<IEnumerable<Student>> GetStudentsByModule(int module_id)
        {        
            var module = _context.Modules.Where(m => m.Id.Equals(module_id)).Include(m => m.ModuleStudents).ThenInclude(ms => ms.Student).ToList();
            if (module == null)
            {
                return NotFound();
            }

            /* Get registered students from module */
            List<Student> students = new List<Student>();
            foreach (var moduleStudent in module.ElementAt(0).ModuleStudents){
                students.Add(moduleStudent.Student);
            }

            return Ok(students);
        }

        /**
         * Gets all the data within modules
         */
        // GET api/modules
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Module>>> GetAllModules()
        {
            return Ok(await _context.Modules.ToListAsync());
        }

        /**
         * Get Module by ID.
         */
        [HttpGet("{id}")]
        public async Task<ActionResult<Module>> GetModuleById(int id)
        {
            var module = await _context.Modules.FirstOrDefaultAsync(m => m.Id.Equals(id));
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
        public ActionResult<IEnumerable<Module>> GetModulesByYearAndUser(String year, String uid)
        {
            if (year == null || uid == null)
            {
                return NotFound();
            }
            List<Module> moduleByYearForUser = new List<Module>();
            var modulesByYear = _context.Modules.Where(m => m.Year.Equals(year)).Include(m => m.ModuleStudents).ThenInclude(ms => ms.Student).ToList();

            foreach (Module m in modulesByYear)
            {
                bool isUserOnModule = m.ModuleStudents.Where(ms => ms.Student.Uid.Equals(uid)).ToList().Count > 0;
                if (isUserOnModule)
                {
                    moduleByYearForUser.Add(m);
                }
            }       
      
            return Ok(moduleByYearForUser);
        }

        /**
         * Returns a specific module given its module code and year.
         */
         //Get api/modules/year/{year}/code/{code}
        [HttpGet("year/{year}/code/{code}")]
        public ActionResult<string> GetSpecificModuleForSpecificYear(String year, String code)
        {
            if (year == null || code == null)
            {
                return NotFound();
            }
            var specificModule = _context.Modules.Where(r => r.Year.Equals(year) && r.Code.Equals(code));
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
            var mod = await _context.Modules.FirstOrDefaultAsync(m => m.Id.Equals(module.Id));
            if (mod != null)
            {
                return BadRequest();
            }

            _context.Add(module);
            await _context.SaveChangesAsync();
            return Ok(await _context.Modules.ToListAsync());
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
                    _context.Update(module);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ModuleExists(module.Id))
                    {
                        return BadRequest();
                    }
                    throw;
                }
            }
            return Ok(await _context.Modules.ToListAsync());
        }

        //DELETE api/modules/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteModule(int id)
        {          
            var module = await _context.Modules.FirstOrDefaultAsync(m => m.Id.Equals(id));
            if (module == null)
            {
                return NotFound();
            }
            try
            {
                _context.Modules.Remove(module);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }
            return Ok(await _context.Modules.ToListAsync());
        }

        private bool ModuleExists(int id)
        {
            return _context.Modules.Any(m => m.Id.Equals(id));
        }
    }
}
