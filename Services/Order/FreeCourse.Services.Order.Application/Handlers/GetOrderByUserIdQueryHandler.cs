using FreeCourse.Services.Order.Application.Dtos;
using FreeCourse.Services.Order.Application.Mapping;
using FreeCourse.Services.Order.Application.Queries;
using FreeCourse.Services.Order.Infrastructre;
using FreeCourse.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Application.Handlers
{
    public class GetOrderByUserIdQueryHandler : IRequestHandler<GetOrdersByUserIdQuery, Response<List<OrderDto>>>
    {
        private readonly OrderDbContext _orderDbContext;

        public GetOrderByUserIdQueryHandler(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public async Task<Response<List<OrderDto>>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderDbContext.Orders.Include(x => x.OrderItems)
                .Where(x => x.BuyerId == request.BuyerId)
                .ToListAsync();

            List<OrderDto> orderDtos = new List<OrderDto>();
            if (orders.Any())
            {
                orderDtos = ObjectMapper.Mapper.Map<List<OrderDto>>(orders);
            }

            return Response<List<OrderDto>>.Success(200);
        }
    }
}
