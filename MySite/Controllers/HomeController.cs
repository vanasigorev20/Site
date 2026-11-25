using Microsoft.AspNetCore.Mvc;
using MySite.Models;
using System.Diagnostics;
using System.Collections.Generic;

namespace MySite.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Error404()
        {
            Response.StatusCode = 404; 
            return View();
        }
        public IActionResult Index() => View();

        public IActionResult AdminPanel() => View();

        public IActionResult Privacy()
        {
            var services = new List<Service>
            {
                new Service
                {
                    Id = 1,
                    Title = "Разработка сайтов",
                    Description = "Создаем современные сайты под ключ. Адаптивный дизайн, высокая скорость работы.",
                    ImagePath = "/images/service1.jpg" 
                },
                new Service
                {
                    Id = 2,
                    Title = "Мобильные приложения",
                    Description = "Приложения для iOS и Android на заказ. Продуманный интерфейс и надежный код.",
                    ImagePath = "/images/service2.jpg" 
                },
                new Service
                {
                    Id = 3,
                    Title = "SEO-продвижение",
                    Description = "Выведем ваш сайт в топ поисковых систем. Привлечем новых клиентов.",
                    ImagePath = "/images/service3.jpg" 
                }
            };

            return View(services);
        }


        public IActionResult About()
        {
            return View();
        }
        public IActionResult Contacts()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Contacts(string name, string email, string message)
        {

            if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(message))
            {
                ViewData["SuccessMessage"] = $"Спасибо, {name}! Ваше сообщение отправлено. Мы свяжемся с вами по адресу {email}.";
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}