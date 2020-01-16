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
        private readonly IDataRepository _repo;
        private readonly ILogger<FileController> _logger;

        public FileController(IDataRepository context, ILogger<FileController> logger)
        {
            _repo = context;
            _logger = logger;
        }

        /**
         * Endpoint to upload CSV file containing modules.
         */
        //POST api/data/modules
        [HttpPost("modules")]
        public async Task<ActionResult> PostModuleFile()
        {
            /* Truncate all previous module data */
            await _repo.EmptyModuleData();

            /* Read from file replacing comma delimeter with custom delimeter (&#£!*) */
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

            /* Key set to avoid duplicates */
            List<Tuple<String, String, String>> keySet = new List<Tuple<String, String, String>>();

            /* module list to build up */
            List<Module> modulesToAdd = new List<Module>();

            String[] splitCsv = content.ToString().Split("&#£!*");
            const int yearCell = 0;
            const int codeCell = 3;
            const int nameCell = 5;
            const int class_codeCell = 9;
            const int coordinatorCell = 13;
            const int endCell = 15;
            String year = ""; String module_code = ""; String module_title = ""; String coordinator_id = ""; String class_code = "";
            for (int i = 16; i < splitCsv.Length - 1; i++)
            {
                if (i % 16 == yearCell)
                {
                    year = splitCsv[i];
                }
                else if (i % 16 == codeCell)
                {
                    module_code = splitCsv[i];
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
                    module.Code = module_code;
                    module.ClassCode = class_code;
                    module.Title = module_title;
                    module.CoordinatorUid = coordinator_id;

                    /* Checks for duplications during this request 
                       both in csv and in data context. */
                    bool moduleAlreadyExists = _repo.ModuleExists(module_code, year, class_code);
                    if (!moduleAlreadyExists)
                    {  
                        if (keySet.Any(r => r.Item1.Equals(year) && r.Item2.Equals(module_code) && r.Item3.Equals(class_code)) == false)
                        {
                            modulesToAdd.Add(module);
                            keySet.Add(new Tuple<String, String, String>(year, module_code, class_code));
                        }
                    }

                    year = "";
                    module_code = "";
                    class_code = "";
                    module_title = "";
                    coordinator_id = "";
                }
            }

            /* Add modules to database */
            await _repo.AddModulesAsync(modulesToAdd);

            return Ok();
        }

        /**
        * Endpoint to upload student list CSV file.
        */
        //POST api/data/students
        [HttpPost("students/{class_code}")]
        public async Task<ActionResult> PostStudentFile(String class_code)
        {
            if (!class_code.Equals("AB0") && !class_code.Equals("MU0") && !class_code.Equals("EX1"))
            {
                return BadRequest("Campus class code must be one of {AB0,MU0,EX1}");
            }  
            
            /**/
            var file = Request.Form.Files.First();
            StringBuilder content = new StringBuilder();            
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

            /* Key set to avoid duplicates in file */            
            List<String> keySet = new List<String>();

            /* students list to build up */
            List<Student> studentsToAdd = new List<Student>();

            /* Association list to build up */
            List<ModuleStudent> moduleStudentToAdd = new List<ModuleStudent>();

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

                    Student student = new Student();
                    student.Uid = user_id;
                    student.Forename = nameSplit[nameSplit.Length - 1];
                    student.Surname = nameSplit[0];

                    if (!_repo.StudentExists(user_id))
                    {
                        if (!keySet.Contains(user_id))
                        {
                            

                            studentsToAdd.Add(student);
                            keySet.Add(user_id);

                            /* Get module to associate student with */
                            var module = await _repo.GetModuleAsync(module_code, year, class_code);
                            if(module != null)
                            {
                                ModuleStudent ms = new ModuleStudent();
                                ms.Module = module;
                                ms.Student = student;
                                moduleStudentToAdd.Add(ms);
                            }
                            else
                            {
                                /* Do nothing & log inaction because module is not known in database */
                                object[] logParams = { module_code, year, user_id };
                                _logger.LogWarning("No module exists with code={0} for the year {1}, skipping CSV entry for student with UID={2}.", logParams);
                            }
                        }
                        else
                        {
                            var module = await _repo.GetModuleAsync(module_code, year, class_code);
                            if (module != null)
                            {
                                ModuleStudent ms = new ModuleStudent();
                                ms.Module = module;
                                ms.Student = student;
                                moduleStudentToAdd.Add(ms);
                            }
                    }

                    module_code = "";
                    year = "";
                    user_id = "";
                    name = "";
                }
            }

            /* Save student entities */
            await _repo.AddStudentsAsync(studentsToAdd);

            /* Save module student entities */
            await _repo.AddModuleStudentAsync(moduleStudentToAdd);

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
        public async Task<ActionResult> PostStaffFile(String class_code, String year)
        {
            if (!class_code.Equals("AB0") && !class_code.Equals("MU0") && !class_code.Equals("EX1"))
            {
                return BadRequest("Campus class code must be one of {AB0,MU0,EX1}");
            }

            /* Truncate all previous staff data */
            await _repo.EmptyStaffData();

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

            /* Key set to avoid duplicates in file */
            List<String> keySet = new List<String>();

            /* Staff entities to add */
            List<Staff> staffToAdd = new List<Staff>();

            /* ModuleStaff assocaition entities to add */
            List<ModuleStaff> moduleStaffToAdd = new List<ModuleStaff>();

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
                    Staff staff = null;
                    if (!keySet.Contains(staff_id))
                    {
                        staff = new Staff();
                        staff.Uid = staff_id;

                        staffToAdd.Add(staff);
                        keySet.Add(staff_id);
                    }
                    else
                    {
                        staff = staffToAdd.Find(s => s.Uid.Equals(staff_id));
                    }

                    /* Get module to associate student with */
                    var module = await _repo.GetModuleAsync(module_code, year, class_code);
                    if (module != null)
                    {
                        /* Create association entity */
                        ModuleStaff ms = new ModuleStaff();
                        ms.Module = module;
                        ms.Staff = staff;
                        moduleStaffToAdd.Add(ms);
                    }
                    else
                    {
                        /* Do nothing & log inaction because module is not known in database */
                        object[] logParams = { module_code, year, staff_id };
                        _logger.LogWarning("No module exists with code={0} for the year {1}, skipping CSV entry for staff with UID={2}.", logParams);
                    }

                    module_code = "";
                    staff_id = "";
                }
            }

            /* Save staff entities */
            await _repo.AddStaffAsync(staffToAdd);

            /* Save module staff entities */
            await _repo.AddModuleStaffAsync(moduleStaffToAdd);

            return Ok();
        }

    }
}