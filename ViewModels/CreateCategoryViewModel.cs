using System.ComponentModel.DataAnnotations;

namespace Mult2.ViewModels
{
    public class CreateCategoryViewModel
    {
    
        public int Id { get; set; }    
        public string Name { get; set; }
        public string Description { get; set; }
       
        public IFormFile Photo { get; set; }
    }
}
