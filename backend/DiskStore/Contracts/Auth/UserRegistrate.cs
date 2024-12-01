using DiskStore.Models;
using System.ComponentModel.DataAnnotations;

namespace DiskStore.Contracts.Auth
{
    public class UserRegistrate
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(250, ErrorMessage = "The name field cannot be greater than 250")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [MaxLength(250, ErrorMessage = "The email field cannot be greater than 250")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MaxLength(250, ErrorMessage = "The password field cannot be greater than 250")]
        public string Password { get; set; }

    }
}
