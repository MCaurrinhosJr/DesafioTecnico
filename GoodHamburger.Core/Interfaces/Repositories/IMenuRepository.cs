using GoodHamburger.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Core.Interfaces.Repositories
{
    public interface IMenuRepository
    {
        Task<List<MenuItem>> GetMenuItemsAsync();
    }
}
