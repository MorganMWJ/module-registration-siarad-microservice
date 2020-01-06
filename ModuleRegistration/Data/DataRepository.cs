﻿using Microsoft.EntityFrameworkCore;
using ModuleRegistration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModuleRegistration.Data
{
    public class DataRepository : IDataRepository
    {
        private readonly ModuleRegistrationContext _context;

        public DataRepository(ModuleRegistrationContext context)
        {
            _context = context;
        }

        public async Task AddModuleAsync(Module module)
        {
            _context.Add(module);
            await _context.SaveChangesAsync();
        }

        public Task AddStaffAsync(Staff staff)
        {
            throw new NotImplementedException();
        }

        public Task AddStudentAsync(Student student)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteModuleAsync(Module module)
        {
            _context.Modules.Remove(module);
            await _context.SaveChangesAsync();
        }

        public Task DeleteStaffAsync(string uid)
        {
            throw new NotImplementedException();
        }

        public Task DeleteStudentAsync(string uid)
        {
            throw new NotImplementedException();
        }

        public async Task<Module> GetModuleByIdAsync(int id)
        {
            return await _context.Modules.FirstOrDefaultAsync(m => m.Id.Equals(id));
        }

        public async Task<Module> GetModuleByYearAndCode(string year, string code)
        {
            var modules = await ModuleListAsync();
            var module = modules.FirstOrDefault(r => r.Year.Equals(year) && r.Code.Equals(code));
            return module;
        }

        public async Task<Staff> GetStaffByUidAsync(string uid)
        {
            return await _context.Staff.FirstOrDefaultAsync(s => s.Uid.Equals(uid));
        }

        public async Task<Student> GetStudentByUidAsync(string uid)
        {
            return await _context.Students.FirstOrDefaultAsync(s => s.Uid.Equals(uid));
        }

        public bool ModuleExists(int id)
        {
            return _context.Modules.Any(m => m.Id.Equals(id));
        }

        public async Task<List<Module>> ModulesByYearAndUserAsync(string year, string uid)
        {
            /* collection to return */
            List<Module> moduleByYearForUser = new List<Module>();

            var modulesByYear = await _context.Modules.Where(m => m.Year.Equals(year)).Include(m => m.ModuleStudents).ThenInclude(ms => ms.Student).ToListAsync();

            foreach (Module m in modulesByYear)
            {
                bool isUserOnModule = m.ModuleStudents.Where(ms => ms.Student.Uid.Equals(uid)).ToList().Count > 0;
                if (isUserOnModule)
                {
                    moduleByYearForUser.Add(m);
                }
            }

            return moduleByYearForUser;
        }

        public async Task<List<Module>> ModulesByStaffListAsync(string uid)
        {
            /* Collection to return */
            List<Module> userModules = new List<Module>();

            /* Add all modules for a staff member */
            var moduleStaff = await _context.ModuleStaff.Include(ms => ms.Module).Include(ms => ms.Staff).ToListAsync();
            foreach (ModuleStaff ms in moduleStaff)
            {
                if (ms.Staff.Uid.Equals(uid))
                {
                    userModules.Add(ms.Module);
                }
            }

            return userModules;
        }

        public async Task<List<Module>> ModulesByStudentListAsync(string uid)
        {
            /* Collection to return */
            List<Module> userModules = new List<Module>();

            /* Add all modules for a student */
            var moduleStudents = await _context.ModuleStudents.Include(ms => ms.Module).Include(ms => ms.Student).ToListAsync();
            foreach (ModuleStudent ms in moduleStudents)
            {
                if (ms.Student.Uid.Equals(uid))
                {
                    userModules.Add(ms.Module);
                }
            }

            return userModules;
        }

        public async Task<List<Module>> ModuleListAsync()
        {
            return await _context.Modules.ToListAsync();
        }

        public bool StaffExists(string uid)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Staff>> StaffListAsync()
        {
            return await _context.Staff.ToListAsync();
        }
        public async Task<List<Staff>> StaffByModuleAsync(int moduleId)
        {
            var module = await _context.Modules.Where(m => m.Id.Equals(moduleId)).Include(m => m.ModuleStaff).ThenInclude(ms => ms.Staff).ToListAsync();
            if (module == null)
            {
                return null;
            }

            /* Get registered staff from module */
            List<Staff> staff = new List<Staff>();
            foreach (var moduleStaff in module.ElementAt(0).ModuleStaff)
            {
                staff.Add(moduleStaff.Staff);
            }

            return staff;
        }

        public bool StudentExists(string uid)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Student>> StudentListAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<List<Student>> StudentsByModuleAsync(int moduleId)
        {
            var module = await _context.Modules.Where(m => m.Id.Equals(moduleId)).Include(m => m.ModuleStudents).ThenInclude(ms => ms.Student).ToListAsync();
            if (module == null)
            {
                return null;
            }

            /* Get registered students from module */
            List<Student> students = new List<Student>();
            foreach (var moduleStudent in module.ElementAt(0).ModuleStudents)
            {
                students.Add(moduleStudent.Student);
            }

            return students;
        }

        public async Task UpdateModuleAsync(Module module)
        {
            _context.Update(module);
            await _context.SaveChangesAsync();
        }

        public Task UpdateStaffAsync(Staff staff)
        {
            throw new NotImplementedException();
        }

        public Task UpdateStudentAsync(Student student)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Module>> ModulesByYearListAsync(string year)
        {
            return await _context.Modules.Where(m => m.Year.Equals(year)).ToListAsync();
        }
    }
}
