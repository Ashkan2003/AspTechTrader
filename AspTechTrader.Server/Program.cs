
using AspTechTrader.Core.Domain.RepositoryContracts;
using AspTechTrader.Core.ServiceContracts;
using AspTechTrader.Core.Services;
using AspTechTrader.Infrastructure.AppDbContext;
using AspTechTrader.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

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
            builder.Services.AddScoped<ISymbolsRepository, SymbolsRepository>();


            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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

            app.UseAuthorization();


            app.MapControllers();

            app.MapFallbackToFile("/index.html");

            app.Run();
        }
    }
}
