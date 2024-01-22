namespace EmployeeMgtSystemAPI.DTO
{
    public class LoginResponseDto
    {
        public string Id { get; set; }
        public string Message { get; set; }
        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public string Token { get; set; }
    }
}
