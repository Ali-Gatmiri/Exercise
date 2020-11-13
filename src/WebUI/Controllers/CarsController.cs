using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tecsys.Exercise.Application.Products.Queries;

namespace Tecsys.Exercise.WebUI.Controllers
{
    public class CarsController : ApiController
    {
        [HttpGet]
        public async Task<ActionResult<List<CarVm>>> Get()
        {
            var result = await Mediator.Send(new GetCarsQuery());
            return result;
        }
    }
}
