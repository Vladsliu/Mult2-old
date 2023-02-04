using Microsoft.AspNetCore.Mvc;
using Mult2.Data;
using Mult2.Models;

namespace Mult2.Controllers
{
    public class EmergencyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EmergencyController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var emergencyTask = _context.Categories.ToList();
            return View(emergencyTask);
        }
        public IActionResult Detail(int id)
        {
            Category category = _context.Categories.FirstOrDefault(c => c.Id == id);
            return View(category);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            _context.Categories.Add(category);/*???*/
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
