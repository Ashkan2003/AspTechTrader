﻿using System.ComponentModel.DataAnnotations;

namespace AspTechTrader.Core.DTO
{
    public class UserUpdateRequestDTO
    {
        [Key]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "UserName cant be blank")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "EmailAddress cant be blank")]
        [EmailAddress(ErrorMessage = "EmailAddress should be a valid email")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        public int UserProperty { get; set; }


    }
}

