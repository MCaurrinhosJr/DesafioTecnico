using GoodHamburger.Core.Interfaces.Validators;
using GoodHamburger.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Core.Validators
{
    public class OrderValidator : IOrderValidator
    {
        public void Validate(Order order)
        {
            if (order.Items == null || !order.Items.Any())
                throw new ArgumentException("Pedido deve conter pelo menos um item");

            if (order.Items.Count(i => i.Type == OrderItemType.Burger) > 1)
                throw new ArgumentException("Apenas um sanduíche é permitido");

            if (order.Items.Count(i => i.Type == OrderItemType.Side) > 1)
                throw new ArgumentException("Apenas uma batata é permitida");

            if (order.Items.Count(i => i.Type == OrderItemType.Drink) > 1)
                throw new ArgumentException("Apenas um refrigerante é permitido");
        }
    }
}
