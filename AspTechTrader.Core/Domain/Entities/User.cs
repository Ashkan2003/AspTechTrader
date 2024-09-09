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


        //relation one to many
        public ICollection<UserSymbolProperty> UserSymbolProperties { get; set; }

        //relation many-to-many
        //public List<Symbol> Symbols { get; set; } = [];
        //public List<UserSymbol> UserSymbols { get; set; }



    }
}
