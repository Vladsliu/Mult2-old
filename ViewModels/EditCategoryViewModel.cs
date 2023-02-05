namespace Mult2.ViewModels
{
    public class EditCategoryViewModel
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile Photo { get; set; }
        public string? URL { get; set; }
    }
}
