using DiskStore.Application.Services;
using DiskStore.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;

namespace DiskStore.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<DiskStoreDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString(nameof(DiskStoreDbContext))));
            builder.Services.AddScoped<IUsersRepository, UserRepository>();
            builder.Services.AddScoped<IUsersService, UsersService>();

            var app = builder.Build();

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
