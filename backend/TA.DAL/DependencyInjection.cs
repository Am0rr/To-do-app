using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using TA.DAL.Persistence;
using Microsoft.EntityFrameworkCore;
using TA.DAL.Interfaces;
using TA.DAL.Repositories;

namespace TA.DAL;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddScoped<ITaskRepository, TaskRepository>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}