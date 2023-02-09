using System.ComponentModel.DataAnnotations;

namespace Mult2.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Names { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
