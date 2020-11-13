using Tecsys.Exercise.Application.Common.Interfaces;
using Tecsys.Exercise.Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tecsys.Exercise.WebUI;
using Tecsys.Exercise.Infrastructure.Persistence;

namespace Tecsys.Exercise.UnitTest
{
    public class OneTimeSetUp
    {
        private readonly IDateTime _dateTime;
        private readonly IServiceScopeFactory _scopeFactory;
        private WingtiptoysDbContext _dbContext;
        public OneTimeSetUp()
        {
            _dateTime = new DateTimeService();

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();

            var _configuration = builder.Build();

            var startup = new Startup(_configuration);

            var services = new ServiceCollection();

            services.AddSingleton(Mock.Of<IWebHostEnvironment>(w =>
                w.EnvironmentName == "Development" &&
                w.ApplicationName == "Tecsys.Exercise.WebUI"));

            services.AddLogging();

            startup.ConfigureServices(services);
            _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();
        }

        public IDateTime DateTime => _dateTime;

        public async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetService<IMediator>();

            return await mediator.Send(request);
        }

        public async Task<WingtiptoysDbContext> GetDbContext()
        {
            if (_dbContext == null) {
                var scope = _scopeFactory.CreateScope();
                _dbContext = scope.ServiceProvider.GetService<WingtiptoysDbContext>();
                await _dbContext.SeedCarsList();
            }
            return _dbContext;
        }
    }
}
