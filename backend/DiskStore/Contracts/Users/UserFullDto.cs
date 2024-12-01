using DiskStore.Contracts.Disks;

namespace DiskStore.Contracts.Users
{
    public class UserFullDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public ICollection<DiskDto> Disks { get; set; }
    }
}
