﻿using System.ComponentModel.DataAnnotations;

namespace DiskStore.Models
{
    public class User 
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [Required]
        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }

        public ICollection<Disk> Disks { get; set; } = new List<Disk>();

    }
}
