
using AspTechTrader.Core.Domain.IdentityEntities;
using AspTechTrader.Core.Domain.RepositoryContracts;
using AspTechTrader.Core.ServiceContracts;
using AspTechTrader.Core.Services;
using AspTechTrader.Infrastructure.AppDbContext;
using AspTechTrader.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text.Json.Serialization;

namespace AspTechTrader.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // connection-string
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            // Add services to the container.
            builder.Services.AddScoped<ISymbolsService, SymbolsService>();
            builder.Services.AddScoped<IUserService, UsersService>();
            builder.Services.AddScoped<IUserSymbolPropertyService, UserSymbolPropertyService>();
            builder.Services.AddScoped<IUserWatchListsService, UserWatchListsService>();
            builder.Services.AddTransient<IJwtService, JwtService>();
            
            builder.Services.AddScoped<ISymbolsRepository, SymbolsRepository>();
            builder.Services.AddScoped<IUsersRepository, UsersRepository>();
            builder.Services.AddScoped<IUserSymbolPropertyRepository, UserSymbolPropertyRepository>();
            builder.Services.AddScoped<IUserWatchListsRepository, UserWatchListsRepository>();

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                // add this code to prevent cycles when navigating bettven relation
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                // add configuration to set authentication to swaggeer
                //options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                //{
                //    In = ParameterLocation.Header,
                //    Name = "Authorization",
                //    Type = SecuritySchemeType.ApiKey
                //});
                //options.OperationFilter<SecurityRequirementsOperationFilter>();
            });

            //Cors // enable cors-policy
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policybuilder =>
                {   // add the front end domain to the origins
                    // get it from app.setting.json file
                    policybuilder.WithOrigins(
                        builder.Configuration.GetSection("AllowedOrigins").Get<string[]>())
                    // enable cors-policy with eg: authorization-header
                    .WithHeaders("Authorization", "origin", "accept", "content-type");
                });
            });

            //
            builder.Services.AddAuthorization();
            //builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            //Identity
            builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                options.Password.RequiredLength = 5;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = true;

            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders()
                .AddUserStore<UserStore<ApplicationUser, ApplicationRole, ApplicationDbContext, Guid>>()
                .AddRoleStore<RoleStore<ApplicationRole, ApplicationDbContext, Guid>>();


            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


            // add identity Apis
            //app.MapIdentityApi<ApplicationUser>();

            app.UseHttpsRedirection();

            app.UseRouting();

            //enable cors
            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
