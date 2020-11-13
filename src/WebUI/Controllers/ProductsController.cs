using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tecsys.Exercise.Application.Products.Queries;

namespace Tecsys.Exercise.WebUI.Controllers
{
    public class ProductsController : ApiController
    {
        [HttpGet]
        [Route("Search/{searchText}")]
        public async Task<ActionResult<List<ProductVm>>> Search(string searchText)
        {
            return await Mediator.Send(new GetProductWithTextSearchQuery { SearchText = searchText });
        }
    }
}
