using GoodHamburger.Core.Interfaces.Repositories;
using GoodHamburger.Core.Interfaces.Services;
using GoodHamburger.Core.Models;

namespace GoodHamburger.Core.Services
{
    public class MenuService(IMenuRepository menuRepository) : IMenuService
    {
        private readonly IMenuRepository _menuRepository = menuRepository;
        public async Task<List<MenuItem>> GetMenuItemsAsync()
        {
            return await _menuRepository.GetMenuItemsAsync();
        }
    }
}