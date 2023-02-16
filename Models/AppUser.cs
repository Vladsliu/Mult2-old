using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Identity;
using Mult2.Controllers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mult2.Models
{
    [Table("AspNetUsers")]
    public class AppUser : IdentityUser
    {
        
        public int? Id { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public string? City { get; set; }
        public string? EmailAddress { get; set; }
        public string? UserRoles { get; set; }
        public ICollection<Category> Categories { get; set; }


    }
}
