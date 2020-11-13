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
    public class GetCarsQuery : IRequest<List<CarVm>>
    {
    }

    public class GetCarsQueryHandler : IRequestHandler<GetCarsQuery, List<CarVm>>
    {
        private readonly IWingtiptoysDbContext _context;

        public GetCarsQueryHandler(IWingtiptoysDbContext context)
        {
            _context = context;
        }

        public async Task<List<CarVm>> Handle(GetCarsQuery request, CancellationToken cancellationToken)
        {
            //TODO: we can also use Automapper but I prefer not to use it.
            var list = await _context.Products
                .Where(t=>t.CategoryID == 1)
                .Select(p=>
                    new CarVm
                    {
                        ProductID = p.ProductID,
                        ProductName = p.ProductName,
                        ImagePath = p.ImagePath,
                        UnitPrice = p.UnitPrice
                    }
                ).ToListAsync(cancellationToken);
            return list;
        }
     
    }
}
