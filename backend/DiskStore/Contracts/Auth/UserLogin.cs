using System.ComponentModel.DataAnnotations;

namespace DiskStore.Contracts.Auth
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(250, ErrorMessage = "The name field cannot be greater than 250")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [MaxLength(250, ErrorMessage = "The password field cannot be greater than 250")]
        public string Password { get; set; }

    }
}
