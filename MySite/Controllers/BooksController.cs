using Microsoft.AspNetCore.Mvc;
using MySite.Repositories;

namespace MySite.Controllers
{
    public class BooksController : Controller
    {
        private readonly BookRepository _repository;

        public BooksController(BookRepository repository)
        {
            _repository = repository;
        }

        public async Task<IActionResult> Index()
        {
            var books = await _repository.GetBooksAsync();
            return View(books);
        }
    }
}