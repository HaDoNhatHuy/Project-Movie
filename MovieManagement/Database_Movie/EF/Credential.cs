using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database_Movie.EF
{
    [Table("Credential")]
    //[PrimaryKey(nameof(GroupId), nameof(RoleId))] //sử dụng cách này để khai báo khóa chính phức hợp từ EF Core7 trở lên
    public class Credential
    {
        //[Key]
        [Column(Order = 0)]
        [ForeignKey("GroupId")]
        public Guid GroupId { get; set; }
        public Group Group { get; set; }
        //[Key]
        [Column(Order = 1)]
        [ForeignKey("RoleId")]
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
    }
}
