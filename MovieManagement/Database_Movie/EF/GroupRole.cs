using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Movie.EF
{
    [Table("GroupRole")]
    public class GroupRole
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("GroupId")]
        public Guid GroupId { get; set; }
        public Group Group { get; set; }

        [ForeignKey("RoleId")]
        public string RoleId { get; set; }
        public IdentityRole Role { get; set; }
    }
}
