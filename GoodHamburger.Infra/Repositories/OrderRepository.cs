using GoodHamburger.Core.Models;
using GoodHamburger.Infra.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GoodHamburger.Core.Interfaces.Repositories;

namespace GoodHamburger.Infra.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ContextDb _context;

        public OrderRepository(ContextDb context)
        {
            _context = context;
        }

        public async Task<Order> CreateOrder(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .AsNoTracking()
                .Include(o => o.Items)
                .ToListAsync();
        }

        public async Task<Order?> GetOrderByIdAsync(int id)
        {
            return await _context.Orders
                .AsNoTracking()
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task UpdateOrder(int id, Order order)
        {
            var existingOrder = await _context.Orders
                .Include(o => o.Items)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (existingOrder == null)
                throw new KeyNotFoundException("Pedido não encontrado");

            existingOrder.Price = order.Price;
            existingOrder.Discount = order.Discount;
            existingOrder.TotalPrice = order.TotalPrice;

            _context.OrderItems.RemoveRange(existingOrder.Items);

            existingOrder.Items = order.Items;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await _context.Orders
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                throw new KeyNotFoundException("Pedido não encontrado");

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }
    }
}
