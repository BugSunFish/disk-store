using DiskStore.Models;
using System.ComponentModel.DataAnnotations;

namespace DiskStore.Contracts
{
    public class DiskCreate
    {
        [Required(ErrorMessage = "Title is required")]
        [MaxLength(250, ErrorMessage = "The title field cannot be greater than 250")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [MaxLength(250, ErrorMessage = "The description field cannot be greater than 250")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Author is required")]
        [MaxLength(250, ErrorMessage = "The author field cannot be greater than 250")]
        public string Author { get; set; }

    }
}
