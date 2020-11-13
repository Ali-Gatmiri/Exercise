using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using Tecsys.Exercise.Infrastructure.Entities;

namespace Tecsys.Exercise.Application.Common.Interfaces
{
    public interface IWingtiptoysDbContext
    {
        DbSet<Product> Products { get; set; }

        DbSet<Category> Categories { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
