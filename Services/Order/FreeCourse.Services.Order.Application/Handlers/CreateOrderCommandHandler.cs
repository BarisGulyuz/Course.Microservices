using FreeCourse.Services.Order.Application.Command;
using FreeCourse.Services.Order.Application.Dtos;
using FreeCourse.Services.Order.Domain.OrderAggregate;
using FreeCourse.Services.Order.Infrastructre;
using FreeCourse.Shared;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FreeCourse.Services.Order.Application.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, Response<CreatedOrderDto>>
    {
        private readonly OrderDbContext _orderDbContext;

        public CreateOrderCommandHandler(OrderDbContext orderDbContext)
        {
            _orderDbContext = orderDbContext;
        }

        public async Task<Response<CreatedOrderDto>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {

            Address address = new Address(request.Address.City, request.Address.District, request.Address.Street,
                                          request.Address.ZipCode, request.Address.AddressLine);

            var order = new Domain.OrderAggregate.Order(request.BuyerId, address);
            foreach (OrderItemDto orderItem in request.OrderItems)
            {
                order.AddOrderItem(orderItem.ProductId, orderItem.ProductName, orderItem.Price, orderItem.PictureUrl);
            }

            await _orderDbContext.Orders.AddAsync(order);

            int effectedRows = await _orderDbContext.SaveChangesAsync(cancellationToken);
            return effectedRows > 0 ? Response<CreatedOrderDto>.Success(new CreatedOrderDto { OrderId = order.Id }, 200)
                                    : Response<CreatedOrderDto>.Success(500, "Error Occured");
        }
    }
}
