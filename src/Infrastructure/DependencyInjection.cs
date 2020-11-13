using Tecsys.Exercise.Infrastructure.Persistence;
using Tecsys.Exercise.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tecsys.Exercise.Application.Common.Interfaces;
using System;

namespace Tecsys.Exercise.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                string dbName = Guid.NewGuid().ToString();
                services.AddDbContext<WingtiptoysDbContext>(options =>
                    options.UseInMemoryDatabase(dbName));
            }
            else
            {
                services.AddDbContext<WingtiptoysDbContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            }

            services.AddScoped<IWingtiptoysDbContext>(provider => provider.GetService<WingtiptoysDbContext>());

            services.AddTransient<IDateTime, DateTimeService>();

            return services;
        }
    }
}