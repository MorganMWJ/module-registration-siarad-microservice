﻿using ModuleRegistration.Models;
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
        Task<ModuleStudent> GetModuleStudentAsync(int id, string uid);
        Task<ModuleStaff> GetModuleStaffAsync(int id, string uid);

        Task<List<Module>> ModuleListAsync();
        Task<List<Module>> ModulesByStudentListAsync(string uid);
        Task<List<Module>> ModulesByStaffListAsync(string uid);
        Task<List<Module>> ModulesByYearListAsync(string year);
        Task<List<Module>> ModulesByYearAndStudentAsync(string year, string uid);
        Task<List<Module>> ModulesByYearAndStaffAsync(string year, string uid);
        Task<List<Student>> StudentListAsync();
        Task<List<Student>> StudentsByModuleAsync(int moduleId);
        Task<List<Staff>> StaffListAsync();
        Task<List<Staff>> StaffByModuleAsync(int moduleId);

        Task AddModuleAsync(Module module);
        Task AddStudentAsync(Student student);
        Task AddStaffAsync(Staff staff);

        Task AddModulesAsync(List<Module> modules);
        Task AddModuleStudentAsync(ModuleStudent moduleStudents);
        Task AddModuleStudentAsync(List<ModuleStudent> moduleStudents);
        Task AddModuleStaffAsync(ModuleStaff moduleStaff);
        Task AddModuleStaffAsync(List<ModuleStaff> moduleStaff);
        Task AddStudentsAsync(List<Student> students);
        Task AddStaffAsync(List<Staff> staff);

        Task UpdateModuleAsync(Module module);
        Task UpdateStudentAsync(Student student);
        Task UpdateStaffAsync(Staff staff);

        Task DeleteModuleAsync(Module module);
        Task DeleteStudentAsync(string uid);
        Task DeleteStaffAsync(string uid);
        Task DeleteModuleStudentsAsync(int id);
        Task DeleteModuleStaffAsync(int id);

        Task DeleteAllStudentAsync();
        Task DeleteAllStaffAsync();

        bool ModuleExists(int id);
        bool ModuleExists(string code, string year, string classCode);
        bool StaffExists(string uid);
        bool StudentExists(string uid);

        Task EmptyModuleData();
        Task EmptyStudentData();
        Task EmptyStaffData();
    }
}
