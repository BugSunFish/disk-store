using DiskStore.Contracts.Users;

namespace DiskStore.Contracts.Disks
{
    public class DiskFullDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public UserDto User { get; set; }
    }
}
