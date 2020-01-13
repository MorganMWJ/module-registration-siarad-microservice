using ModuleRegistration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModuleRegistration.Data
{
    public interface IDataRepository
    {
        Task<Module> GetModuleAsync(int id);
        Task<Module> GetModuleAsync(string year, string code);
        Task<Module> GetModuleAsync(string code, string year, string classCode);
        Task<Student> GetStudentAsync(string uid);
        Task<Staff> GetStaffAsync(string uid);

        Task<List<Module>> ModuleListAsync();
        Task<List<Module>> ModulesByStudentListAsync(string uid);
        Task<List<Module>> ModulesByStaffListAsync(string uid);
        Task<List<Module>> ModulesByYearListAsync(string year);
        Task<List<Module>> ModulesByYearAndStudent(string year, string uid);
        Task<List<Module>> ModulesByYearAndStaff(string year, string uid);
        Task<List<Student>> StudentListAsync();
        Task<List<Student>> StudentsByModuleAsync(int moduleId);
        Task<List<Staff>> StaffListAsync();
        Task<List<Staff>> StaffByModuleAsync(int moduleId);

        Task AddModuleAsync(Module module);
        Task AddStudentAsync(Student student);
        Task AddStaffAsync(Staff staff);

        Task AddModulesAsync(List<Module> modules);
        Task AddModuleStudentAsync(List<ModuleStudent> moduleStudents);
        Task AddModuleStaffAsync(List<ModuleStaff> moduleStaff);
        Task AddStudentsAsync(List<Student> students);
        Task AddStaffAsync(List<Staff> staff);

        Task UpdateModuleAsync(Module module);
        Task UpdateStudentAsync(Student student);
        Task UpdateStaffAsync(Staff staff);

        Task DeleteModuleAsync(Module module);
        Task DeleteStudentAsync(string uid);
        Task DeleteStaffAsync(string uid);

        bool ModuleExists(int id);
        bool ModuleExists(string code, string year, string classCode);
        bool StaffExists(string uid);
        bool StudentExists(string uid);

        Task EmptyModuleData();
        Task EmptyStudentData();
        Task EmptyStaffData();
    }
}
