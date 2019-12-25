using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using KakuWebApp.Areas.Employee.Models;
using KakuWebApp.Data;
using KakuWebApp.Service.Interface;
using Microsoft.EntityFrameworkCore;

namespace KakuWebApp.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _context;

        public EmployeeService(ApplicationDbContext _context)
        {
            this._context = _context;
        }



        public async Task<bool> SaveEmployee(Employee employee)
        {
            if (employee.id != 0)
            {
                _context.Employees.Update(employee);
                 
                 return 1 == await _context.SaveChangesAsync();
            }

            await _context.Employees.AddAsync(employee); 

             return 1 == await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Employee>> GetAllEmployee()
        {
            return await _context.Employees.AsNoTracking().ToListAsync();
        }

        public async Task<Employee> GetEmployeeByID(int id)
        {
           Employee model = await _context.Employees.FindAsync(id);

           return model;
        }

        public async Task<bool> DeleteEmployeeById(int id)
        {
            _context.Employees.Remove(_context.Employees.Find(id));

            return 1 == await _context.SaveChangesAsync();
        }
    }
}
