using System.Collections.Generic;
using System.Threading.Tasks;
using KakuWebApp.Areas.Employee.Models;


namespace KakuWebApp.Service.Interface
{
    public interface IEmployeeService
    {
        Task<bool> SaveEmployee(Employee employee);
        Task<IEnumerable<Employee>> GetAllEmployee();
        Task<Employee> GetEmployeeByID(int id);
        Task<bool> DeleteEmployeeById(int id);

    }
}
