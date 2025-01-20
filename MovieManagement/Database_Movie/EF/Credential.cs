using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Movie.EF
{
    [Table("Credential")]
    public class Credential
    {
        [Key]
        [Column(Order = 0)]
        [ForeignKey("GroupId")]
        public Guid GroupId { get; set; }
        public Group Group { get; set; }
        [Key]
        [Column(Order = 1)]
        [ForeignKey("RoleId")]
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
    }
}
