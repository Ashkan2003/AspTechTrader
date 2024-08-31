using System.ComponentModel.DataAnnotations;

namespace AspTechTrader.Core.Domain.Entities
{
    public class User
    {
        [Key]
        public Guid userId { get; set; }

        [Required]
        public string userName { get; set; }

        [Required]
        public string userLastName { get; set; }


    }
}
