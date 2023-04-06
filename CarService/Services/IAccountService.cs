using CarService.Models;

namespace CarService.Services
{
    public interface IAccountService
    {
        void RegisterUser(RegisterUserDto dto);
        string GenerateJwt(LoginDto dto);
        string GetUserName();
    }
}