using AspTechTrader.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace AspTechTrader.Core.DTO
{
    public class UserAddRequestDTO
    {
        [Required(ErrorMessage = "UserName cant be blank")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "EmailAddress cant be blank")]
        [EmailAddress(ErrorMessage = "EmailAddress should be a valid email")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "UserRole cant be blank")]
        public UserRoleOptions UserRole { get; set; }




    }
}
