using GoodHamburger.Core.Interfaces.Services;
using GoodHamburger.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Core.Services
{
    public class DiscountService : IDiscountService
    {
        public decimal CalculateDiscount(Order order, decimal subtotal)
        {
            var hasBurger = order.Items.Any(i => i.Type == OrderItemType.Burger);
            var hasSide = order.Items.Any(i => i.Type == OrderItemType.Side);
            var hasDrink = order.Items.Any(i => i.Type == OrderItemType.Drink);

            if (hasBurger && hasSide && hasDrink)
                return subtotal * 0.20m;

            if (hasBurger && hasDrink)
                return subtotal * 0.15m;

            if (hasBurger && hasSide)
                return subtotal * 0.10m;

            return 0;
        }
    }
}
