using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WebApplication_scuffolding_reverse.Models;

namespace WebApplication_scuffolding_reverse
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers()
               .AddJsonOptions(options =>
               {
                  options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
               });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<AdventureWorksLt2019Context >(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connesione non avvenuta")));

            builder.Services.AddCors(
                options =>
            {
                options.AddPolicy("CorsPolicia",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader()
                               .SetIsOriginAllowed(host => true);
                    });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors("CorsPolicia");

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
