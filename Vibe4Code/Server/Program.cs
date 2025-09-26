
using Microsoft.EntityFrameworkCore;
namespace Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // EF Core SQL Server
            builder.Services.AddDbContext<Server.Data.AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Repositories
            builder.Services.AddScoped<Server.Repositories.Interfaces.IGenericRepository<Server.Models.LogEntry>, Server.Repositories.GenericRepository<Server.Models.LogEntry>>();
            builder.Services.AddScoped<Server.Repositories.Interfaces.IPatientRepository, Server.Repositories.PatientRepository>();
            builder.Services.AddScoped<Server.Repositories.Interfaces.IDoctorRepository, Server.Repositories.DoctorRepository>();

            // Services
            builder.Services.AddScoped<Server.Services.Interfaces.IPatientService, Server.Services.PatientService>();
            builder.Services.AddScoped<Server.Services.Interfaces.IDoctorService, Server.Services.DoctorService>();
            builder.Services.AddScoped<Server.Services.Interfaces.ILogService, Server.Services.LogService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
