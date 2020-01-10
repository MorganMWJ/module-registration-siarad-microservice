using ModuleRegistration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModuleRegistration.Data
{
    public class MockRepository : IDataRepository
    {
        public Task AddModuleAsync(Module module)
        {
            throw new NotImplementedException();
        }

        public Task AddStaffAsync(Staff staff)
        {
            throw new NotImplementedException();
        }

        public Task AddStudentAsync(Student student)
        {
            throw new NotImplementedException();
        }

        public Task DeleteModuleAsync(Module module)
        {
            throw new NotImplementedException();
        }

        public Task DeleteStaffAsync(string uid)
        {
            throw new NotImplementedException();
        }

        public Task DeleteStudentAsync(string uid)
        {
            throw new NotImplementedException();
        }

        public Task<Module> GetModuleByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Module> GetModuleByYearAndCode(string year, string code)
        {
            throw new NotImplementedException();
        }

        public Task<Staff> GetStaffByUidAsync(string uid)
        {
            throw new NotImplementedException();
        }

        public Task<Student> GetStudentByUidAsync(string uid)
        {
            throw new NotImplementedException();
        }

        public bool ModuleExists(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Module>> ModuleListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<List<Module>> ModulesByStaffListAsync(string uid)
        {
            throw new NotImplementedException();
        }

        public Task<List<Module>> ModulesByStudentListAsync(string uid)
        {
            throw new NotImplementedException();
        }

        public Task<List<Module>> ModulesByYearAndUserAsync(string year, string uid)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Module>> ModulesByYearListAsync(string year)
        {
            List<Module> modules = new List<Module>();

            modules.Add(new Module() { Code = "SEM5640", Year = "2020", ClassCode = "AB0", CoordinatorUid = "nst", Title = "Developing Advanced Internet Based-Applications" });
            modules.Add(new Module() { Code = "CSM1620", Year = "2020", ClassCode = "AB0", CoordinatorUid = "tjn2", Title = "Fundamentals of Intelligent Systems" });

            return modules;
        }

        public Task<List<Staff>> StaffByModuleAsync(int moduleId)
        {
            throw new NotImplementedException();
        }

        public bool StaffExists(string uid)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Staff>> StaffListAsync()
        {
            List<Staff> staff = new List<Staff>();

            staff.Add(new Staff() { Uid = "nst", Forename = "Neil", Surname = "Taylor" });
            staff.Add(new Staff() { Uid = "nwh" });

            return staff;
        }

        public bool StudentExists(string uid)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Student>> StudentListAsync()
        {
            List<Student> students = new List<Student>();

            students.Add(new Student() { Uid = "mwj7", Forename = "Morgan", Surname = "Jones" });
            students.Add(new Student() { Uid = "dop2", Forename = "Dominic", Surname = "Parr" });

            return students;
        }

        public Task<List<Student>> StudentsByModuleAsync(int moduleId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateModuleAsync(Module module)
        {
            throw new NotImplementedException();
        }

        public Task UpdateStaffAsync(Staff staff)
        {
            throw new NotImplementedException();
        }

        public Task UpdateStudentAsync(Student student)
        {
            throw new NotImplementedException();
        }
    }
}
