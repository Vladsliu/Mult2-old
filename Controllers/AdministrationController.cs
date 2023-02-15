using Microsoft.AspNetCore.Mvc;

namespace Mult2.Controllers
{
    public class AdministrationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
