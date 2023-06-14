using CarService.Models;

namespace CarService.Services
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto dto);
        string GenerateJwt(LoginDto dto);
        bool ValidateLoginUser(string emailAddress, string password);
        string GetUserName();
    }
}