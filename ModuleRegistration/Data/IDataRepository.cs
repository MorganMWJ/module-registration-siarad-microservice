using ModuleRegistration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModuleRegistration.Data
{
    public interface IDataRepository
    {
        Task<Module> GetModuleByIdAsync(int id);
        Task<Module> GetModuleByYearAndCode(string year, string code);
        Task<Student> GetStudentByUidAsync(string uid);
        Task<Staff> GetStaffByUidAsync(string uid);

        Task<List<Module>> ModuleListAsync();
        Task<List<Module>> ModulesByStudentListAsync(string uid);
        Task<List<Module>> ModulesByStaffListAsync(string uid);
        Task<List<Module>> ModulesByYearListAsync(string year);
        Task<List<Module>> ModulesByYearAndUserAsync(string year, string uid);
        Task<List<Student>> StudentListAsync();
        Task<List<Student>> StudentsByModuleAsync(int moduleId);
        Task<List<Staff>> StaffListAsync();
        Task<List<Staff>> StaffByModuleAsync(int moduleId);

        Task AddModuleAsync(Module module);
        Task AddStudentAsync(Student student);
        Task AddStaffAsync(Staff staff);

        Task UpdateModuleAsync(Module module);
        Task UpdateStudentAsync(Student student);
        Task UpdateStaffAsync(Staff staff);

        Task DeleteModuleAsync(Module module);
        Task DeleteStudentAsync(string uid);
        Task DeleteStaffAsync(string uid);

        bool ModuleExists(int id);
        bool StaffExists(string uid);
        bool StudentExists(string uid);
    }
}
