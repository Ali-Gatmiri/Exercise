using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Tecsys.Exercise.WebUI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiController : ControllerBase
    {
        private IMediator _mediator;

        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        public void SetMediatorIfEmpty(IMediator mediator)
        {
            if (_mediator == null)
                _mediator = mediator;
        }
    }
}
