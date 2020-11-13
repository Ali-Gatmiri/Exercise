using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tecsys.Exercise.Application.Common.Interfaces;

namespace Tecsys.Exercise.Application.Products.Queries
{
    public class GetProductWithTextSearchQuery : IRequest<List<ProductVm>>
    {
        public string SearchText { get; set; }
    }

    public class GetProductWithTextSearchQueryHandler : IRequestHandler<GetProductWithTextSearchQuery, List<ProductVm>>
    {
        private readonly IWingtiptoysDbContext _context;

        public GetProductWithTextSearchQueryHandler(IWingtiptoysDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductVm>> Handle(GetProductWithTextSearchQuery request, CancellationToken cancellationToken)
        {
            var list = await _context.Products.Include(t=>t.Category)
                .Where(t=>t.ProductName.Contains(request.SearchText) || t.Description.Contains(request.SearchText))
                .Select(p=>
                    new ProductVm
                    {
                        ProductID = p.ProductID,
                        ProductName = p.ProductName,
                        UnitPrice = p.UnitPrice,
                        Description = p.Description,
                        CategoryName = p.Category.CategoryName
                    }
                ).ToListAsync(cancellationToken);
            return list;
        }
     
    }
}
