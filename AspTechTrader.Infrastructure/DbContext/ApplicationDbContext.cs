using AspTechTrader.Core.Domain.Entities;
using AspTechTrader.Core.Enums;
using Microsoft.EntityFrameworkCore;

namespace AspTechTrader.Infrastructure.AppDbContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //define dbSets
        public virtual DbSet<Symbol> Symbols { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Fluent Api //relation => optional one to many relation
            modelBuilder.Entity<User>()
                .HasMany(e => e.Symbols)
                .WithMany(e => e.Users)
                .UsingEntity<UserSymbol>();


            ////seed data
            List<Symbol> symbolsList = new List<Symbol>();

            Symbol symbol1 = new Symbol()
            {
                SymbolName = "دارایکم",
                SymbolId = Guid.NewGuid(),
                Volume = 100,
                LastDeal = 100,
                LastDealPercentage = 100,
                LastPrice = 100,
                LastPricePercentage = 100,
                TheFirst = 100,
                TheLeast = 100,
                TheMost = 100,
                DemandVolume = 100,
                DemandPrice = 100,
                OfferPrice = 100,
                OfferVolume = 100,
                State = StateOptions.NOTALLOWED,
                ChartNumber = "100",
            };

            Symbol symbol2 = new Symbol()
            {
                SymbolName = "اختم",
                SymbolId = Guid.NewGuid(),
                Volume = 120,
                LastDeal = 600,
                LastDealPercentage = 100,
                LastPrice = 150,
                LastPricePercentage = 100,
                TheFirst = 100,
                TheLeast = 110,
                TheMost = 100,
                DemandVolume = 100,
                DemandPrice = 100,
                OfferPrice = 100,
                OfferVolume = 100,
                State = StateOptions.NOTALLOWED,
                ChartNumber = "100",
            };

            symbolsList.Add(symbol1);
            symbolsList.Add(symbol2);
            //User user1 = new User()
            //{
            //    UserId = Guid.Parse("1515B290-A769-4AB7-9F54-D3673AFF1B25"),
            //    UserName = "Ashkan",
            //    EmailAddress = "test@test.com",
            //    UserProperty = 400000,
            //    Symbols = symbolsList,
            //};

            //modelBuilder.Entity<User>().HasData(user1);
            modelBuilder.Entity<Symbol>().HasData(symbolsList);


        }





    }

}
