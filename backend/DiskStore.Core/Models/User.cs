using System.ComponentModel.DataAnnotations;

namespace DiskStore.Core.Models
{
    public class User
    {
        public const int MAX_NAME_LENGHT = 50;

        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name can not be empty.")]
        [MaxLength(MAX_NAME_LENGHT, ErrorMessage = "Username longer then 50 symbols.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email can not be empty.")]
        [EmailAddress(ErrorMessage = "Email is not valide")]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Password can not be empty.")]
        public string PasswordHash { get; set; }
    }
}
