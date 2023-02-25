using Microsoft.AspNetCore.Mvc;
using Mult2.Data;
using Mult2.Models;

namespace Mult2.Controllers
{
    public class ContactController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ContactController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            
            return View();
        }

        public IActionResult SuccessfullMessage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SuccessfullMessage(Contact contact)
        {
            if (!ModelState.IsValid)
            {
            return View("Index");
            }
            _context.Add(contact);
            _context.SaveChanges();

            return RedirectToAction("SuccessfullMessage");
        }
    }

   
}
