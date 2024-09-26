using AspTechTrader.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace AspTechTrader.Core.DTO
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "RersonName cant be blank")]
        public string PersonName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email cant be blank")]
        [EmailAddress(ErrorMessage = "Email should be a valid emailAddress")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "phoneNumber cant be blank")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "password cant be blank")]
        public string Password { get; set; } = string.Empty;

        public UserRoleOptions UserRole { get; set; } = UserRoleOptions.RegularUser;
    }
}
