using EmployeeMgtSystemAPI.Data;
using EmployeeMgtSystemAPI.DTO;
using EmployeeMgtSystemAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMgtSystemAPI.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDbContext _context;
        public EmployeeRepository(EmployeeDbContext context)
        {
            _context = context;
        }
        public bool CreateEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            return Save();
        }

        public bool DeleteEmployee(string id)
        {
            var emp = _context.Employees.Find(id);
            if(emp != null) 
            {
                _context.Employees.Remove(emp);
               
            }
            return Save();
        }

        public bool EmployeeExists(string id)
        {
            return _context.Employees.Any(c => c.Id == id);
        }

        public IEnumerable<Employee> GetAll()
        {
            return _context.Employees.Include(x => x.JobRole);
        }

        public Employee GetEmployee(string id)
        {
            return _context.Employees.Where(x => x.Id == id).FirstOrDefault();
        }
        public Employee GetEmployeeByName(string name)
        {
            return _context.Employees.Where(x=>x.UserName == name).FirstOrDefault();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateEmployee(string id, UpdateDto updateDto)
        {
            var emp = _context.Employees.Find(id);
            if(emp != null) 
            {
                emp.Name = updateDto.Name;
                emp.Email = updateDto.Email;
                emp.Phone = updateDto.Phone;

                _context.Employees.Update(emp);  
            }
            return Save();
        }

        public bool UpdateEmployeeStatus(string id)
        {
            var emp = _context.Employees.Find(id);
            if (emp != null)
            {
                emp.Status = "Fired";
                _context.Employees.Update(emp);
            }
            return Save();
        }
       
    }
}
