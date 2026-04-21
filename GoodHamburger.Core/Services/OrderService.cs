using GoodHamburger.Core.Interfaces.Repositories;
using GoodHamburger.Core.Interfaces.Services;
using GoodHamburger.Core.Interfaces.Validators;
using GoodHamburger.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Core.Services
{
    public class OrderService(
        IOrderRepository orderRepository,
        IOrderValidator validator,
        IDiscountService discountService
    ) : IOrderService
    {
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IOrderValidator _validator = validator;
        private readonly IDiscountService _discountService = discountService;
        public async Task<Order> CreateOrder(Order order)
        {
            _validator.Validate(order);

            ApplyPricing(order);

            return await _orderRepository.CreateOrder(order);
        }

        public async Task<Order> UpdateOrder(int id, Order order)
        {
            _validator.Validate(order);

            ApplyPricing(order);

            await _orderRepository.UpdateOrder(id, order);

            var updated = await _orderRepository.GetOrderByIdAsync(id);

            if (updated == null)
                throw new KeyNotFoundException("Pedido não encontrado");

            return updated;
        }

        private void ApplyPricing(Order order)
        {
            var subtotal = order.Items.Sum(i => i.Price);

            var discount = _discountService.CalculateDiscount(order, subtotal);

            order.Price = subtotal;
            order.Discount = discount;
            order.TotalPrice = subtotal - discount;
        }

        public async Task DeleteOrderAsync(int id)
        => await _orderRepository.DeleteOrderAsync(id);

        public async Task<List<Order>> GetAllOrdersAsync()
            => await _orderRepository.GetAllOrdersAsync();

        public async Task<Order?> GetOrderByIdAsync(int id)
            => await _orderRepository.GetOrderByIdAsync(id);

    }
}
