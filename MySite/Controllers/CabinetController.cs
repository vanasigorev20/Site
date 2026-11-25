using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MySite.Models;
using MySite.Repositories;
using System.Security.Claims;

namespace MySite.Controllers
{
    [Authorize] 
    public class CabinetController : Controller
    {
        private readonly ItemRepository _itemRepo;

        public CabinetController(ItemRepository itemRepo)
        {
            _itemRepo = itemRepo;
        }

       
        private int GetCurrentUserId()
        {
            var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userIdStr)) return 0;
            return int.Parse(userIdStr);
        }

        public async Task<IActionResult> Index()
        {
            var userId = GetCurrentUserId();
            var myItems = await _itemRepo.GetByUserIdAsync(userId);
            return View(myItems);
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Item item)
        {
            if (!ModelState.IsValid) return View(item);

            item.UserId = GetCurrentUserId();
            await _itemRepo.CreateAsync(item);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = GetCurrentUserId();
            var item = await _itemRepo.GetByIdAsync(id);

            if (item == null || item.UserId != userId) return NotFound();

            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Item item)
        {
            if (!ModelState.IsValid) return View(item);

            var userId = GetCurrentUserId();
            await _itemRepo.UpdateAsync(item, userId);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = GetCurrentUserId();
            await _itemRepo.DeleteAsync(id, userId);
            return RedirectToAction("Index");
        }
    }
}