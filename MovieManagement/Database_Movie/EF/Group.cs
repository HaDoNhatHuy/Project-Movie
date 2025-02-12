using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database_Movie.EF
{
    [Table("Group")]
    public class Group
    {
        [Key]
        public Guid GroupId { get; set; }
        public string Name { get; set; }
        public ICollection<AppUserModel> Users { get; set; } = new HashSet<AppUserModel>();
        public ICollection<GroupRole> GroupRoles { get; set; } = new HashSet<GroupRole>(); // Quan hệ với Role
    }
}
