using EmployeeMgtSystemAPI.Data;
using EmployeeMgtSystemAPI.DTO;
using EmployeeMgtSystemAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace EmployeeMgtSystemAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<Employee> _userManager;
        private readonly SignInManager<Employee> _signInManager;
        private readonly IJWTService _jwtService;

        public AuthService(UserManager<Employee> userManager, SignInManager<Employee> signInManager, IJWTService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
        }

        public async Task<LoginResponseDto> Login(LoginDto model)
        {
            LoginResponseDto login = new LoginResponseDto();

            var response = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

            if (response.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                var token = await _jwtService.GenerateToken(user);

                login.Token = token;
                login.IsSuccess = true;
                login.Message = "Login Succesful";
                login.StatusCode = 200;
                return login;
            }

            login.IsSuccess = false;
            login.Message = "Login not Succesful";
            login.StatusCode = 400;
            return login;
  
        }

        public async Task<RegisterResponseDto> Register(RegisterDto model)
        {
            RegisterResponseDto response = new RegisterResponseDto();

            var checkUser = await _userManager.FindByEmailAsync(model.Email);
            if (checkUser != null)
            {
                return new RegisterResponseDto() { IsSuccess = false, Message = "user already exist", StatusCode = 400 };
            }

            var employee = new Employee();
            employee.Email = model.Email;
            employee.Name = model.Name;
            employee.UserName = model.Email;

            var result = await _userManager.CreateAsync(employee, model.Password);
            if (!result.Succeeded)
            {
                response.IsSuccess = false;
                response.Message = "User wasn't Created";
                response.StatusCode = 400;
                return response;
            }
            response.IsSuccess = true;
            response.Message = "User Created Succesfully";
            response.StatusCode = 200;
            return response;
        }
    }
}
