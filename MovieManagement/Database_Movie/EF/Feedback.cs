using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Movie.EF
{
    [Table("Feedback")]
    public class Feedback
    {
        [Key]
        public Guid FeedbackId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Content { get; set; }
        public string Address { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool Status { get; set; }
    }
}
