using System.ComponentModel.DataAnnotations;

namespace DiskStore.Contracts
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Name field is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Password field is required")]
        public string Password { get; set; }
    }
}
