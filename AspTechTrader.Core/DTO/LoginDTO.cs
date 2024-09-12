using System.ComponentModel.DataAnnotations;

namespace AspTechTrader.Core.DTO
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Email cant be blank")]
        [EmailAddress(ErrorMessage = "Email should be a valid emailAddress")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "password cant be blank")]
        public string Password { get; set; } = string.Empty;
    }
}
