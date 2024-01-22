using Azure;
using EmployeeMgtSystemAPI.Data;
using EmployeeMgtSystemAPI.DTO;
using EmployeeMgtSystemAPI.Models;
using EmployeeMgtSystemAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeMgtSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class AdminDashboardController : ControllerBase
    {

        //- Provide an admin dashboard with endpoints to:
        //-Retrieve total employees.
        //-Retrieve total available roles.
        //- Create and delete job roles.
        private readonly IEmployeeRepository _repository;
        private readonly EmployeeDbContext _context;
        public AdminDashboardController(IEmployeeRepository repository, EmployeeDbContext context)
        {
            _repository = repository;
            _context = context;
        }
        [HttpGet("GetAllEmployee")]
        public IActionResult GetAllEmployee() 
        {
            var response = _repository.GetAll().ToList();
            if (response.Count < 1)
            {
                return NotFound();
            }
            var result = response.Select(x => new EmployeeDto { Email=x.Email, Phone=x.Phone, UserName=x.UserName, Status=x.Status });
            return Ok(result);
        }
        [HttpGet("TotalAvailableRoles")]
        public IActionResult TotalAvailableRoles() 
        {
            var availableRoles = _context.JobRoles.ToList();
            return Ok(availableRoles);
        }
        [HttpPost("CreateJobRole")]
        public IActionResult CreateJobRole(JobRoleDto jobRoleDto)
        {
            var roleExist = _context.JobRoles.FirstOrDefault(x=>x.Name==jobRoleDto.Name);
            if (roleExist != null)
                return BadRequest("Role already Exist");
            var jobrole = new JobRole { Name = jobRoleDto.Name,Description=jobRoleDto.Description};
            _context.JobRoles.Add(jobrole);
            var i = _context.SaveChanges();
            if (i>0)
                return Ok();
            return BadRequest();
            
        }
        [HttpDelete("DeleteJobRole")]
        public IActionResult DeleteJobRole(int id)
        {
            var jobRoleToDelete = _context.JobRoles.Find(id);
            if (jobRoleToDelete ==null)
                return NotFound();
            _context.JobRoles.Remove(jobRoleToDelete);
            _context.SaveChanges();
            return Ok();
        }
    }
}
