using Flavorsyncserver.Data;
using Flavorsyncserver.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Flavorsyncserver
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configure Identity Core services
            builder.Services
                .AddIdentityCore<User>()
                .AddEntityFrameworkStores<flavorsyncDbContext>()
                .AddDefaultTokenProviders();

            // Configure Identity options
            builder.Services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.User.RequireUniqueEmail = true;
            });

            // Configure DbContext with SQL Server
            builder.Services.AddDbContext<flavorsyncDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DevDB")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Configure CORS
            app.UseCors(options =>
                options.WithOrigins("http://localhost:4200")
                       .AllowAnyMethod()
                       .AllowAnyHeader());

            app.UseAuthorization();

            app.MapControllers();

            // Configure Identity API
            app.MapGroup("/api")
               .MapIdentityApi<User>();

            // Sign up route
            app.MapPost("/api/signup", async (
                UserManager<User> userManager,
                [FromBody] UserRegistrationModel userRegistrationModel
            ) =>
            {
                User user = new User
                {
                    UserName = userRegistrationModel.Email,
                    Email = userRegistrationModel.Email,
                    FullName = userRegistrationModel.FullName,
                };

                var result = await userManager.CreateAsync(user, userRegistrationModel.Password);

                if (result.Succeeded)
                    return Results.Ok(result);
                else
                    return Results.BadRequest(result);
            });

            app.Run();
        }
    }

    // User registration model class
    public class UserRegistrationModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
    }
}
