
using AspTechTrader.Core.Domain.IdentityEntities;
using AspTechTrader.Core.Domain.RepositoryContracts;
using AspTechTrader.Core.ServiceContracts;
using AspTechTrader.Core.Services;
using AspTechTrader.Infrastructure.AppDbContext;
using AspTechTrader.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
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
            builder.Services.AddScoped<ISymbolsRepository, SymbolsRepository>();
            builder.Services.AddScoped<IUsersRepository, UsersRepository>();
            builder.Services.AddScoped<IUserSymbolPropertyRepository, UserSymbolPropertyRepository>();
            builder.Services.AddScoped<IUserWatchListsRepository, UserWatchListsRepository>();
            builder.Services.AddScoped<IJwtService, JwtService>();

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

            //Jwt 
            // we can authenticate users by jwt or cookie-authentication
            builder.Services.AddAuthentication(options =>
            {
                // set the default authentication-schema to jwt
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                // when the user make a http-request with jwt-token in header , with this code we validate and give the permishion to get the data or post the data
                .AddJwtBearer(options =>
                {
                    //options.UseSecurityTokenValidators = true;
                    //options.RequireHttpsMetadata = false;
                    //options.SaveToken = true;
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        LogValidationExceptions = true,

                        // validate issuer
                        ValidateIssuer = true,
                        // validate Audiance
                        ValidateAudience = true,
                        // validate exprationDate
                        ValidateLifetime = true,

                        ValidAudience = builder.Configuration["Jwt:Audience"],

                        ValidIssuer = builder.Configuration["Jwt:Issuer"],


                        // validate signature
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                });

            //
            builder.Services.AddAuthorization();

            var app = builder.Build();

            app.UseDefaultFiles();
            app.UseStaticFiles();


            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }


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
