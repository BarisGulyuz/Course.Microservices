using FreeCourse.Services.Order.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FreeCourse.Services.Order.Domain.OrderAggregate
{
    public class Order : EntityBase, IAggregateRoot
    {
        public Order()
        {

        }
        public Order(string buyerId, Address address)
        {
            BuyerId = buyerId;
            Address = address;
            _orderItems = new List<OrderItem>();
            CreatedDate = DateTime.Now;
        }
        public DateTime CreatedDate { get; private set; }
        public string BuyerId { get; private set; }
        public Address Address { get; private set; }

        private readonly List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItems => _orderItems;


        public decimal TotalPrice => _orderItems.Sum(x => x.Price * x.Quantity);
        public void AddOrderItem(string productId, string productName, decimal price, string pictureUrl)
        {
            bool isExist = _orderItems.Any(x => x.ProductId == productId);
            if (!isExist)
            {
                OrderItem orderItemForAdd = new OrderItem(productId, productName, pictureUrl, price, 1);
                _orderItems.Add(orderItemForAdd);
            }
        }
    }
}
