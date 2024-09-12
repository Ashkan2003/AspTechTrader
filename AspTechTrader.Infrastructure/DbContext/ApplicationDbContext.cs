using AspTechTrader.Core.Domain.Entities;
using AspTechTrader.Core.Domain.IdentityEntities;
using AspTechTrader.Core.Enums;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspTechTrader.Infrastructure.AppDbContext
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //define dbSets
        public virtual DbSet<Symbol> Symbols { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserSymbolProperty> UserSymbolProperties { get; set; }
        public virtual DbSet<UserWatchList> UserWatchLists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);



            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Symbol>().ToTable("Symbol");
            modelBuilder.Entity<UserSymbolProperty>().ToTable("UserSymbolProperty");

            modelBuilder.Entity<User>()
                .HasMany(e => e.UserWatchLists)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId);


            modelBuilder.Entity<UserWatchList>()
                .HasMany(e => e.Symbols)
                .WithMany(e => e.UserWatchList);




            // seed users data
            List<User> users = new List<User>()
            {
                new User
                {
                    UserId= Guid.Parse("8D75D5BF-EA72-4F50-B72F-9E077A49518C"),
                    UserName = "ashkan",
                    EmailAddress = "ashkan@email.com",
                    UserProperty = 160,
                },
                new User
                {
                    UserId= Guid.Parse("70E41BE8-EE03-4ED7-AA9E-7AD3D3B367B7"),
                    UserName = "jonas",
                    EmailAddress = "jonas@email.com",
                    UserProperty = 1160,
                },
                 new User
                {
                    UserId= Guid.Parse("43966394-6325-4E7D-A218-CF2D43FAAE24"),
                    UserName = "amin",
                    EmailAddress = "amin@email.com",
                    UserProperty = 10,
                }
            };

            modelBuilder.Entity<User>().HasData(users);

            //seed symbol data
            List<Symbol> symbolsList = new List<Symbol>() {

             new Symbol()
            {
                SymbolName = "دارایکم",
                SymbolId = Guid.Parse("C39734D9-125A-43FA-88FE-8E12A209F1B1"),
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
            },
             new Symbol()
            {
                SymbolName = "اختم",
                SymbolId = Guid.Parse("DF32447C-7578-46AF-A77A-73EFD458C201"),
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
            }
        };

            modelBuilder.Entity<Symbol>().HasData(symbolsList);


            // seed userProperty data

            List<UserSymbolProperty> userSymbolProperties = new List<UserSymbolProperty>()
            {
                new UserSymbolProperty
                {
                    UserSymbolPropertyId = Guid.Parse("2AF3944D-8DFA-4262-A9E0-BB96AD2C7166"),
                    SymbolPrice = 40,
                    SymbolQuantity = 170,
                    UserId= Guid.Parse("8D75D5BF-EA72-4F50-B72F-9E077A49518C"),
                    SymbolId = Guid.Parse("C39734D9-125A-43FA-88FE-8E12A209F1B1"),
                },
                 new UserSymbolProperty
                {
                    UserSymbolPropertyId = Guid.Parse("98818F76-E989-4BE4-AFB0-D61DB4BBD590"),
                    SymbolPrice = 450,
                    SymbolQuantity = 1370,
                    UserId= Guid.Parse("8D75D5BF-EA72-4F50-B72F-9E077A49518C"),
                    SymbolId = Guid.Parse("DF32447C-7578-46AF-A77A-73EFD458C201"),

                },
                 new UserSymbolProperty
                 {
                    UserSymbolPropertyId = Guid.Parse("A7D97C83-0C10-45BF-94E7-05C43D59257A"),
                    SymbolPrice = 450,
                    SymbolQuantity = 1370,
                    UserId= Guid.Parse("70E41BE8-EE03-4ED7-AA9E-7AD3D3B367B7"),
                    SymbolId = Guid.Parse("C39734D9-125A-43FA-88FE-8E12A209F1B1"),
                 },
                  new UserSymbolProperty
                 {
                    UserSymbolPropertyId = Guid.Parse("61B5469A-EE2E-4083-AB6A-975C8D2E4FE3"),
                    SymbolPrice = 450,
                    SymbolQuantity = 1370,
                    UserId= Guid.Parse("43966394-6325-4E7D-A218-CF2D43FAAE24"),
                    SymbolId = Guid.Parse("DF32447C-7578-46AF-A77A-73EFD458C201"),
                  },
            };

            modelBuilder.Entity<UserSymbolProperty>().HasData(userSymbolProperties);

            //seed userWatchList data
            List<UserWatchList> userWatchLists = new List<UserWatchList>()
            {
               new UserWatchList()
               {
                   UserWatchListId = Guid.Parse("BC9E6EA8-F280-476E-8502-8D96926CDE3E"),
                   userWatchListName = "سهام من",
                   UserId= Guid.Parse("43966394-6325-4E7D-A218-CF2D43FAAE24"),
               },
               new UserWatchList()
               {
                   UserWatchListId = Guid.Parse("DBB733B5-86E1-4B9B-B1D7-E9D26D77165D"),
                   userWatchListName = "سهامppppp",
                   UserId= Guid.Parse("43966394-6325-4E7D-A218-CF2D43FAAE24"),
               },
            };

            modelBuilder.Entity<UserWatchList>().HasData(userWatchLists);








        }





    }

}
