using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Movie.EF
{
    [Table("Contact")]
    public class Contact
    {
        [Key]
        public Guid ContactId { get; set; }
        public string Content { get; set; }
        public bool Status { get; set; }
    }
}
