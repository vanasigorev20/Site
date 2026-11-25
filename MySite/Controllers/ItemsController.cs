using Microsoft.AspNetCore.Mvc;
using MySite.Repositories;

namespace MySite.Controllers
{
    public class ItemsController : Controller
    {
        private readonly ItemRepository _itemRepo;

        public ItemsController(ItemRepository itemRepo)
        {
            _itemRepo = itemRepo;
        }
        public async Task<IActionResult> Index()
        {
            var items = await _itemRepo.GetAllAsync();
            return View(items);
        }

        public async Task<IActionResult> Details(int id)
        {
            var item = await _itemRepo.GetByIdAsync(id);
            if (item == null) return NotFound(); 
            return View(item);
        }
    }
}