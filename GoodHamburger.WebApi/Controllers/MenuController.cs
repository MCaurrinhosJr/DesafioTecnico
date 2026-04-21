using GoodHamburger.Core.Interfaces.Services;
using GoodHamburger.Core.Models;
using GoodHamburger.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoodHamburger.WebApi.Controllers
{
    /// <summary>
    /// Controller responsável por gerenciar o menu do restaurante. 
    /// Ele fornecerá informações sobre os itens disponíveis, suas descrições e preços.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class MenuController(IMenuService menuService) : ControllerBase
    {
        private readonly IMenuService _menuService = menuService;

        /// <summary>
        /// Retorna o menu do restaurante.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetMenu()
        {
            var menuItems = await _menuService.GetMenuItemsAsync();
            return Ok(menuItems);
        }
    }
}
