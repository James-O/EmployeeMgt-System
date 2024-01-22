using EmployeeMgtSystemAPI.Models;

namespace EmployeeMgtSystemAPI.Services
{
    public interface IJWTService
    {
        Task<string> GenerateToken(Employee employee);
    }
}
