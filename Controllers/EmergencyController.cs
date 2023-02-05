using Microsoft.AspNetCore.Mvc;
using Mult2.Data;
using Mult2.Interfaces;
using Mult2.Models;
using Mult2.ViewModels;

namespace Mult2.Controllers
{
    public class EmergencyController : Controller
    {
        //private readonly ApplicationDbContext _context;
        private readonly IContextRepository _contextRepository;
        private readonly IPhotoService _photoService;

        public EmergencyController(IContextRepository contextRepository, IPhotoService photoService)
        {
            _contextRepository = contextRepository;
            _photoService = photoService;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Category> categories = await _contextRepository.GetAll();
            return View(categories);
        }
        public async Task<IActionResult> Detail(int id)
        {
            Category category = await _contextRepository.GetByIdAsync(id);
            return View(category);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryViewModel categoryVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(categoryVM.Photo);
                var category = new Category
                {
                    Name = categoryVM.Name,
                    Description = categoryVM.Description,
                    Photo = result.Url.ToString()
                };
                _contextRepository.Add(category);//??
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Upload photo failed");
            }
            return View(categoryVM);     
        }

        public async Task<IActionResult> Edit(int id)
        {
           var category = await _contextRepository.GetByIdAsync(id); 
            if (category == null) return View("Error");
            var categoryVM = new EditCategoryViewModel
            {
                Name = category.Name,
                Description = category.Description,
                URL = category.Photo

            };
            return View(categoryVM);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditCategoryViewModel categoryVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("","Failed to edit club");
                return View("Edit", categoryVM);
            }
            var userCategory = await _contextRepository.GetByIdAsyncNoTracking(id);

            if (userCategory != null)
            { 
                try
                {
                    await _photoService.DeletePhotoAsync(userCategory.Photo);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(categoryVM);
                }
                var photoResult = await _photoService.AddPhotoAsync(categoryVM.Photo);
                var category = new Category
                {
                    Id = id,
                    Name = categoryVM.Name,
                    Description = categoryVM.Description,
                    Photo = photoResult.Url.ToString()
                };
                _contextRepository.Update(category);
                return RedirectToAction("Index");
            }
            else
            {
                return View(categoryVM);
            }
        }
    }
}
