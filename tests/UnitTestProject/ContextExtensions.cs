using System.Threading.Tasks;
using Tecsys.Exercise.Infrastructure.Persistence;
using Tecsys.Exercise.Infrastructure.Entities;


namespace Tecsys.Exercise.UnitTest
{
    public static class ContextExtensions
    {
        public static async Task<int> SeedCarsList(this WingtiptoysDbContext context)
        {
            Product product1 = new Product
            {
                ProductID = 1,
                ProductName = "Other-1",
                Description = "Description-Other-1",
                ImagePath = "other1.png",
                UnitPrice = 100,
                CategoryID = 2
            };
            Product product2 = new Product
            {
                ProductID = 2,
                ProductName = "Car-2",
                Description = "Description-Car-2",
                ImagePath = "car2.png",
                UnitPrice = 200,
                CategoryID = 1
            };
            Product product3 = new Product
            {
                ProductID = 3,
                ProductName = "Car-3",
                Description = "Description-Car-3",
                ImagePath = "car3.png",
                UnitPrice = 300,
                CategoryID = 1
            };
            Product product4 = new Product
            {
                ProductID = 4,
                ProductName = "Other-4",
                Description = "Description-Other-4",
                ImagePath = "other4.png",
                UnitPrice = 400,
                CategoryID = 2
            };
            context.Products.Add(product1);
            context.Products.Add(product2);
            context.Products.Add(product3);
            context.Products.Add(product4);
            return await context.SaveChangesAsync();
        }
    }
}
