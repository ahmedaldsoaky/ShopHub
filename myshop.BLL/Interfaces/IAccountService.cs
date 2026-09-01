using myshop.BLL.DTOs.Account;

namespace myshop.BLL.Interfaces
{
    public interface IAccountService
    {
        Task<RegisterResultDto> RegisterAsync(RegisterDto dto);

        Task<LoginResultDto> LoginAsync(LoginDto dto);

        Task LogoutAsync();
    }
}
