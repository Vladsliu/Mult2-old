using System.ComponentModel.DataAnnotations;

namespace Mult2.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public string Photo { get; set; }
        public DateTime CreateDateTime { get; set;} = DateTime.Now;
    }
}
