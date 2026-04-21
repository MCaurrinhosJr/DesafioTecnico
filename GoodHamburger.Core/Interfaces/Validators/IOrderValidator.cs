using GoodHamburger.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Core.Interfaces.Validators
{
    public interface IOrderValidator
    {
        Task ValidateAsync(Order order);
    }
}
