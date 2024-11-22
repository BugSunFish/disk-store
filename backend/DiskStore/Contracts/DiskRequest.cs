using DiskStore.Models;
using System.ComponentModel.DataAnnotations;

namespace DiskStore.Contracts
{
    public class DiskRequest
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Author { get; set; }
    }
}
