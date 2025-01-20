using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Movie.EF
{
    [Table("Group")]
    public class Group
    {
        [Key]
        public Guid GroupId { get; set; }
        public string Name { get; set; }
        public ICollection<Credential> Credentials { get; set; } = new HashSet<Credential>();
        public ICollection<User> Users { get; set; } = new HashSet<User>();
    }
}
