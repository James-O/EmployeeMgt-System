using AutoMapper;
using EmployeeMgtSystemAPI.DTO;
using EmployeeMgtSystemAPI.Models;
using EmployeeMgtSystemAPI.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeMgtSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private IEmployeeRepository _employeeRepo;
        private readonly UserManager<Employee> _userManager;
        private readonly SignInManager<Employee> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private IMapper _mapper;
        public EmployeeController(IEmployeeRepository employeeRepo, IMapper mapper,
            UserManager<Employee> userManager, SignInManager<Employee> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _employeeRepo = employeeRepo;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("Assignrole")]
        public async Task<IActionResult> AsignRole(string employeeId, string role)
        {
            var emp =await _userManager.FindByIdAsync(employeeId);
            if(emp == null)
            {
                return NotFound();
            }
            var roleExist =await _roleManager.RoleExistsAsync(role);
            if(roleExist == null)
            {
                return BadRequest("Role dosen't exist");
            }
            var roles = new List<string> { role };

            var result = await _userManager.AddToRolesAsync(emp, roles);
            if (result.Succeeded)
                return Ok("Role asigned succesfully");
            return BadRequest("Role not assigned");
        }

        [HttpGet("Get-All")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Employee>))]
        public IActionResult GetEmployee()
        {
            var response = _employeeRepo.GetAll().ToList();
            if (response.Count <1)
            {
                return NotFound();
            }
            var result = response.Select(x => new EmployeeDto { Email=x.Email, Phone=x.Phone, UserName=x.UserName, Status=x.Status });
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateEmployee([FromBody] EmployeeDto employeeDto)
        {
            if (employeeDto == null)
                return BadRequest();

            var employeeMap = _mapper.Map<Employee>(employeeDto);
            var result = _employeeRepo.CreateEmployee(employeeMap);

            if (!result)
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }

            return Ok("Succesfully Created");
        }

        [HttpGet("employeeId")]
        [ProducesResponseType(200, Type = typeof(Employee))]
        public IActionResult GetEmployee(string employeeId)
        {
            if (!_employeeRepo.EmployeeExists(employeeId))
                return NotFound();

            var employeeMap = _employeeRepo.GetEmployee(employeeId);

            var response = _mapper.Map<EmployeeDto>(employeeMap);

            return Ok(response);
        }

        [HttpGet("getEmployeeByName")]
        [ProducesResponseType(200, Type = typeof(Employee))]
        public IActionResult GetEmployeeByName(string employeeName)
        {
            if (!_employeeRepo.EmployeeExists(employeeName))
                return NotFound();

            var employeeMap = _employeeRepo.GetEmployee(employeeName);

            return Ok(employeeMap);
        }

        [HttpPut("{employeeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateEmployee(string employeeId, [FromBody] UpdateDto employeeDto)
        {
            if (employeeDto == null)
                return BadRequest(ModelState);

            var response = _employeeRepo.UpdateEmployee(employeeId,employeeDto);

            if (!response)
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }

            return Ok("Record updated");
        }

        [HttpDelete("{employeeId}")]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        [ProducesResponseType(200)]
        public IActionResult DeleteEmployee(string employeeId)
        {
            if (!_employeeRepo.EmployeeExists(employeeId))
                return NotFound();

            

            var result = _employeeRepo.DeleteEmployee(employeeId);

            if (!result)
            {
                ModelState.AddModelError("", "Something went wrong");
                return StatusCode(500, ModelState);
            }

            return Ok("Record deleted Succesfully");
        }

        [HttpPut("updateStatus/{employeeId}")]
        public IActionResult UpdateEmployeeStatus(string employeeId)
        {
            var result = _employeeRepo.UpdateEmployeeStatus(employeeId);
            if (!result)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
