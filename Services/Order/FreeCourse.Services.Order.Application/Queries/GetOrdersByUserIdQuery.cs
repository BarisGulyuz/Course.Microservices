using FreeCourse.Services.Order.Application.Dtos;
using FreeCourse.Shared;
using MediatR;
using System.Collections.Generic;

namespace FreeCourse.Services.Order.Application.Queries
{
    public class GetOrdersByUserIdQuery : IRequest<Response<List<OrderDto>>>
    {
        public GetOrdersByUserIdQuery(string buyerId)
        {
            BuyerId = buyerId;
        }
        public string BuyerId { get; set; }
    }

}
