using EmployeeMgtSystemAPI.DTO;

namespace EmployeeMgtSystemAPI.Services
{
    public interface IAuthService
    {
        Task<RegisterResponseDto> Register(RegisterDto model);
        Task<LoginResponseDto> Login(LoginDto model);
    }
}
