using DiskStore.Models;
using System.ComponentModel.DataAnnotations;

namespace DiskStore.Contracts
{
    public class DiskDTO
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public DateTime Updated { get; set; }

        [Required]
        public Guid PublisherId { get; set; }
    }
}
