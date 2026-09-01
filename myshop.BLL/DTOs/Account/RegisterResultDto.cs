
namespace myshop.BLL.DTOs.Account
{
    public class RegisterResultDto
    {
        public bool Succeeded { get; set; }

        public IEnumerable<string> Errors { get; set; }
            = Enumerable.Empty<string>();

        public string Role { get; set; } = string.Empty;
    }
}
