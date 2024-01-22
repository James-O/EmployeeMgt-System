using EmployeeMgtSystemAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EmployeeMgtSystemAPI.Services
{
    public class JWTService : IJWTService
    {
        private readonly UserManager<Employee> _userManager;
        private readonly IConfiguration _config;

        public JWTService(UserManager<Employee> userManager, IConfiguration config)
        {
            _userManager = userManager;
            _config = config;
        }
        public async Task<string> GenerateToken(Employee employee)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, employee.Name),
                new Claim(ClaimTypes.Email, employee.Email),
                new Claim(ClaimTypes.Name, employee.UserName)
            };
            var roles = await _userManager.GetRolesAsync(employee);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var symmetricSecurity = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("JWT:Key").Value));
            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(10),
                Issuer = _config.GetSection("JWT:Issuer").Value,
                Audience = _config.GetSection("JWT:Audience").Value,
                SigningCredentials = new SigningCredentials(symmetricSecurity, SecurityAlgorithms.HmacSha256)
            };

            var securityTokenHandler = new JwtSecurityTokenHandler();
            var token = securityTokenHandler.CreateToken(securityTokenDescriptor);

            return securityTokenHandler.WriteToken(token);
        }
    }
}
