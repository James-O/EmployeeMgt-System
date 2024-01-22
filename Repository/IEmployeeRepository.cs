using EmployeeMgtSystemAPI.DTO;
using EmployeeMgtSystemAPI.Models;
using System.Collections.Specialized;

namespace EmployeeMgtSystemAPI.Repository
{
    public interface IEmployeeRepository
    {
        Employee GetEmployee(string id);
        Employee GetEmployeeByName(string name);
        IEnumerable<Employee> GetAll();
        bool UpdateEmployeeStatus(string id);
        bool CreateEmployee(Employee employee);
        bool UpdateEmployee(string id, UpdateDto updateDto);
        bool DeleteEmployee(string id);
        bool EmployeeExists(string id);
        bool Save();

    }
}
