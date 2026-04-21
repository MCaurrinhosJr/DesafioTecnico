using GoodHamburger.Core.Interfaces.Repositories;
using GoodHamburger.Core.Models;
using GoodHamburger.Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodHamburger.Infra.Repositories
{
    public class MenuRepository : IMenuRepository
    {
        private readonly ContextDb _context;

        public MenuRepository(ContextDb context)
        {
            _context = context;
        }

        public async Task<List<MenuItem>> GetMenuItemsAsync()
        {
            return await _context.MenuItems
                .AsNoTracking()
                .OrderBy(i => i.Type)
                .ToListAsync();
        }
    }
}
