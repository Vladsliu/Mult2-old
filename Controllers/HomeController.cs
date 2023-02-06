using Microsoft.AspNetCore.Mvc;
using Mult2.Interfaces;
using Mult2.Models;
using System.Diagnostics;

namespace Mult2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IContextRepository _contextRepository;

        public HomeController(ILogger<HomeController> logger, IContextRepository contextRepository)
        {
            _logger = logger;
            _contextRepository = contextRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contacts()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}