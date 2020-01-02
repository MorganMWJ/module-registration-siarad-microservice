using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ModuleRegistration.Data;
using ModuleRegistration.Models;

namespace ModuleRegistration.Controllers
{
    [Route("api/data")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly ModuleRegistrationContext _context;
        private readonly ILogger<FileController> _logger;

        public FileController(ModuleRegistrationContext context, ILogger<FileController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /**
         * Endpoint to upload CSV file containing modules.
         */
        //POST api/data/modules
        [HttpPost("modules")]
        public async Task<ActionResult<Module>> PostCsvFile(IFormFile test)
        {
            _context.Database.ExecuteSqlCommand("TRUNCATE TABLE module_student RESTART IDENTITY CASCADE");
            _context.Database.ExecuteSqlCommand("TRUNCATE TABLE module_staff RESTART IDENTITY CASCADE");
            _context.Database.ExecuteSqlCommand("TRUNCATE TABLE module RESTART IDENTITY CASCADE");
            var file = Request.Form.Files.First();
            StringBuilder content = new StringBuilder();
            StreamReader sr = new StreamReader(file.OpenReadStream());
            while (!sr.EndOfStream)
            {
                String s = sr.ReadLine();
                string[] sa = Regex.Split(s, ",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
                for (int i = 0; i < sa.Length; i++)
                {
                    sa[i].Replace("\"", "");
                    sa[i] = sa[i] + "&#£!*"; //Unique enough key
                    content.Append(sa[i]);
                }
            }
            List<Tuple<String, String, String>> keySet = new List<Tuple<String, String, String>>();
            String[] splitCsv = content.ToString().Split("&#£!*");
            const int yearCell = 0;
            const int codeCell = 3;
            const int nameCell = 5;
            const int class_codeCell = 9;
            const int coordinatorCell = 13;
            const int endCell = 15;
            String year = ""; String module_id = ""; String module_title = ""; String coordinator_id = ""; String class_code = "";
            for (int i = 16; i < splitCsv.Length - 1; i++)
            {
                if (i % 16 == yearCell)
                {
                    year = splitCsv[i];
                }
                else if (i % 16 == codeCell)
                {
                    module_id = splitCsv[i];
                }
                else if (i % 16 == nameCell)
                {
                    module_title = splitCsv[i];
                }
                else if (i % 16 == class_codeCell)
                {
                    class_code = splitCsv[i];
                }
                else if (i % 16 == coordinatorCell)
                {
                    coordinator_id = splitCsv[i];
                }
                if (i % 16 == endCell)
                {
                    Module module = new Module();
                    module.Year = year;
                    module.Code = module_id;
                    module.ClassCode = class_code;
                    module.Title = module_title;
                    module.CoordinatorUid = coordinator_id;

                    /* Adds in module if it does not yet exist */
                    bool moduleAlreadyExists = _context.Modules.Any(m => m.Code.Equals(module_id) && m.Year.Equals(year) && m.ClassCode.Equals(class_code));
                    if (!moduleAlreadyExists)
                    {
                        //_context.Modules.Add(module);
                        /* Checks for pre-existing data prior to this request */
                        if (keySet.Any(r => r.Item1.Equals(year) && r.Item2.Equals(module_id) && r.Item3.Equals(class_code)) == false) //Checks for duplications during this request
                        {
                            _context.Add(module);
                            keySet.Add(new Tuple<String, String, String>(year, module_id, class_code));
                        }
                    }

                    year = "";
                    module_id = "";
                    class_code = "";
                    module_title = "";
                    coordinator_id = "";
                }
            }
            await _context.SaveChangesAsync();
            return Ok();
        }

        /**
        * Endpoint to upload student list CSV file.
        */
        //POST api/data/students
        [HttpPost("students/{class_code}")]
        public async Task<ActionResult<Module>> PostStudentFile(String class_code)
        {
            if (!class_code.Equals("AB0") && !class_code.Equals("MU0") && !class_code.Equals("EX1"))
            {
                return BadRequest("Campus class code must be one of {AB0,MU0,EX1}");
            }

            _context.Database.ExecuteSqlCommand("TRUNCATE TABLE module_student RESTART IDENTITY CASCADE");
            _context.Database.ExecuteSqlCommand("TRUNCATE TABLE student RESTART IDENTITY CASCADE");
            
            var file = Request.Form.Files.First();
            StringBuilder content = new StringBuilder();
            List<String> keySet = new List<String>();
            StreamReader sr = new StreamReader(file.OpenReadStream());
            while (!sr.EndOfStream)
            {
                String s = sr.ReadLine();
                string[] sa = Regex.Split(s, ",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
                for (int i = 0; i < sa.Length; i++)
                {
                    sa[i] = sa[i] + "&#£!*"; //Unique enough delimeter
                    content.Append(sa[i]);
                }
            }
            String[] splitCsv = content.ToString().Split("&#£!*");
            const int codeCell = 0;
            const int yearCell = 2;
            const int emailCell = 7;
            const int nameCell = 10;
            const int endCell = 10;
            String module_code = ""; String year = ""; String user_id = ""; String name = "";
            for (int i = 11; i < splitCsv.Length - 1; i++)
            {
                if (i % 11 == codeCell)
                {
                    module_code = splitCsv[i];
                }
                else if (i % 11 == yearCell)
                {
                    year = splitCsv[i];
                }
                else if (i % 11 == emailCell)
                {
                    user_id = splitCsv[i];
                }
                else if (i % 11 == nameCell)
                {
                    name = splitCsv[i];
                }
                if (i % 11 == endCell)
                {
                    name = Regex.Replace(name, @"(\[|""|\])", "");
                    String[] nameSplit = name.Split(',');
                    nameSplit[nameSplit.Length - 1] = nameSplit[nameSplit.Length - 1].Remove(0, 1);

                    try { 
                        Student student = new Student();
                        student.Uid = user_id;
                        student.Forename = nameSplit[nameSplit.Length - 1];
                        student.Surname = nameSplit[0];
                        if (_context.Students.Find(user_id) == null)
                        {
                            if (!keySet.Contains(user_id))
                            {
                                _context.Add(student);
                                keySet.Add(user_id);
                            }
                        }

                        /* Get module to associate student with */
                        var module = _context.Modules.First(m => m.Code.Equals(module_code) && m.Year.Equals(year) && m.ClassCode.Equals(class_code));

                        ModuleStudent ms = new ModuleStudent();
                        ms.Module = module;
                        ms.Student = student;
                        _context.Add(ms);                        
                    }
                    catch (InvalidOperationException)
                    {
                        /* Do nothing because module is not known in database */
                        Object[] logParams = { module_code, year, user_id };
                        _logger.LogWarning("No module exists with code={0} for the year {1}, skipping CSV entry for student with UID={2}.", logParams);
                    }

                    module_code = "";
                    year = "";
                    user_id = "";
                    name = "";
                }
            }
            await _context.SaveChangesAsync();
            return Ok();
        }

        /**
        * Endpoint to upload staff list CSV file.
        * 
        * Pass in year and campus (class_code) as extra data for identifying modules.
        */
        //Until Neil or Nigel sorts out the CSV file to contain year as a field, we'll take it manually
        //POST api/data/staff/{year}
        [HttpPost("staff/{class_code}/{year}")]
        public async Task<ActionResult<Module>> PostStaffFile(String class_code, String year)
        {
            if (!class_code.Equals("AB0") && !class_code.Equals("MU0") && !class_code.Equals("EX1"))
            {
                return BadRequest("Campus class code must be one of {AB0,MU0,EX1}");
            }

            _context.Database.ExecuteSqlCommand("TRUNCATE TABLE module_staff RESTART IDENTITY CASCADE");
            _context.Database.ExecuteSqlCommand("TRUNCATE TABLE staff RESTART IDENTITY CASCADE");

            var file = Request.Form.Files.First();
            StringBuilder content = new StringBuilder();
            StreamReader sr = new StreamReader(file.OpenReadStream());
            while (!sr.EndOfStream)
            {
                String s = sr.ReadLine();
                string[] sa = Regex.Split(s, ",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
                for (int i = 0; i < sa.Length; i++)
                {
                    sa[i] = sa[i] + "&#£!*"; //Unique enough key
                    content.Append(sa[i]);
                }
            }
            String[] splitCsv = content.ToString().Split("&#£!*");
            const int codeCell = 0;
            const int staff_idCell = 3;
            const int endCell = 3;
            String module_code = ""; String staff_id = "";
            for (int i = 4; i < splitCsv.Length - 1; i++)
            {
                if (i % 4 == codeCell)
                {
                    module_code = splitCsv[i];
                }
                else if (i % 4 == staff_idCell)
                {
                    staff_id = splitCsv[i];
                }
                if (i % 4 == endCell)
                {        
                     
                    try
                    {
                        /* Create the staff entity if it does not already exist */
                        Staff staff = _context.Staff.Find(staff_id);
                        if (staff == null)
                        {
                            staff = new Staff();
                            staff.Uid = staff_id;
                            _context.Add(staff);
                        }                        

                        /* If module already exists for the year associate staff to the module */
                        var module = _context.Modules.First(m => m.Code.Equals(module_code) && m.Year.Equals(year) && m.ClassCode.Equals(class_code));

                        /* Create association entity */
                        ModuleStaff ms = new ModuleStaff();
                        ms.Module = module;
                        ms.Staff = staff;
                        _context.Add(ms);
                    }
                    catch (InvalidOperationException)
                    {
                        /* Do nothing because module is not known in database */
                        Object[] logParams = { module_code, year, staff_id };
                        _logger.LogWarning("No module exists with code={0} for the year {1}, skipping CSV staff entry for staff with UID={2}.", logParams);
                    }
                   
                    module_code = "";
                    staff_id = "";
                }
            }
            await _context.SaveChangesAsync();
            return Ok();
        }

    }
}