﻿using AspTechTrader.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace AspTechTrader.Core.Domain.Entities
{
    public class User
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

        public UserRoleOptions UserRole { get; set; } = UserRoleOptions.RegularUser;

        //relation one to many with userSymbolProperty
        public ICollection<UserSymbolProperty> UserSymbolProperties { get; set; }

        // relation one to many with userWatchList
        public ICollection<UserWatchList> UserWatchLists { get; set; } = new List<UserWatchList>();





    }
}
