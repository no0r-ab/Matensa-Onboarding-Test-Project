using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Application.Common.Interfaces;
using Infrastructure.Repositories; // Adjust based on your repository namespace
using Domain.Users;
using Application.Services.PasswordHasher; // Ensure this namespace is referenced for User

namespace Infrastructure
{
    public static class DependenceInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlite("Data Source=app.db")); // Adjust the connection string as needed

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserTokenRepository, UserTokenRepository>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<ITransactionRepository, TransactionRepository>();

            return services;
        }
    }
}