using AspTechTrader.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AspTechTrader.Infrastructure.AppDbContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options)
            : base(options)
        {
        }

        //define dbSets
        public virtual DbSet<Symbol> Symbols { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
        }





    }

}
