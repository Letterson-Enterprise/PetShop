using backend_petshop.DTOs;

namespace backend_petshop.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResponseDto> Login(LoginDto loginDto);
    }
}
