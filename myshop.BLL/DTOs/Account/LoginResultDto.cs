
namespace myshop.BLL.DTOs.Account
{
    public class LoginResultDto
    {
        public bool Succeeded { get; set; }

        public bool IsLockedOut { get; set; }

        public string Role { get; set; } = string.Empty;

        public IEnumerable<string> Errors { get; set; }
            = Enumerable.Empty<string>();
    }
}
