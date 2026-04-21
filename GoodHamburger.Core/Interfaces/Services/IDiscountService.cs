using GoodHamburger.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Core.Interfaces.Services
{
    public interface IDiscountService
    {
        decimal CalculateDiscount(Order order, decimal subtotal);
    }
}
