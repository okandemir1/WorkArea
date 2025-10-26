using WorkArea.Persistence;
using WorkArea.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WorkArea.Application.ExternalService;
using WorkArea.Application.Services;

namespace WorkArea.Infrastructure;

public static class NativeInjectorBootStrapper
{
    public static void AddContextInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<WorkAreaDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));
    }

    public static void RegisterServices(this IServiceCollection services
        , IConfiguration configuration)
    {
        services.AddTransient<UserService>();
        
        services.AddScoped<EmailService>();
        //services.AddScoped<PushNotificationService>();
        

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<WorkAreaDbContext>();
    }
}