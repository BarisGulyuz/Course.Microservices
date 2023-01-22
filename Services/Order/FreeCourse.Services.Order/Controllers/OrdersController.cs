using FreeCourse.Services.Order.Application.Command;
using FreeCourse.Services.Order.Application.Queries;
using FreeCourse.Shared;
using FreeCourse.Shared.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IIdentityService _identityService;

        public OrdersController(IMediator mediator, IIdentityService identityService)
        {
            _mediator = mediator;
            _identityService = identityService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(CreateOrderCommand createOrderCommand)
        {
            createOrderCommand.BuyerId = _identityService.UserId;
            var response = await _mediator.Send(createOrderCommand);
            return Result(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            GetOrdersByUserIdQuery query = new GetOrdersByUserIdQuery(_identityService.UserId);
            var response = await _mediator.Send(query);
            return Result(response);
        }
    }
}
