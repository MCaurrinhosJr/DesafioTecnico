using GoodHamburger.Core.Models;

namespace GoodHamburger.Core.Interfaces.Services
{
    public interface IMenuService
    {
        Task<List<MenuItem>> GetMenuItemsAsync();
    }
}