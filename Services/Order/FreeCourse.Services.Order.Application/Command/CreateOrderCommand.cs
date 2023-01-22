﻿using FreeCourse.Services.Order.Application.Dtos;
using FreeCourse.Shared;
using MediatR;
using System.Collections.Generic;

namespace FreeCourse.Services.Order.Application.Command
{
    public class CreateOrderCommand : IRequest<Response<CreatedOrderDto>>
    {
        public string BuyerId { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
        public AddressDto Address { get; set; }
    }
}
