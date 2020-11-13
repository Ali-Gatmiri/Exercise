using Tecsys.Exercise.Application.Common.Interfaces;
using Tecsys.Exercise.Infrastructure.Common;
using Tecsys.Exercise.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Tecsys.Exercise.Infrastructure.Persistence
{
    public class WingtiptoysDbContext : DbContext, IWingtiptoysDbContext
    {
        private readonly IDateTime _dateTime;

        public WingtiptoysDbContext(
            DbContextOptions options,
            IDateTime dateTime) : base(options)
        {
            _dateTime = dateTime;
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {

            //TODO: If I implement security, each object that inherite from AuditableEntity, has extra information for how and when create and last modify. I think it's good. but I also use CDC in some projects for track changes in sensitive tables.
            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        //entry.Entity.CreatedBy = _currentUserService.UserId;
                        entry.Entity.Created = _dateTime.Now;
                        break;

                    case EntityState.Modified:
                        //entry.Entity.LastModifiedBy = _currentUserService.UserId;
                        entry.Entity.LastModified = _dateTime.Now;
                        break;
                }
            }

            int result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

    }
}