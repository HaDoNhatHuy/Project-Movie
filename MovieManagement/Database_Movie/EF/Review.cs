using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Movie.EF
{
    [Table("Review")]
    public class Review : IAuditable
    {
        [Key]
        public Guid ReviewId { get; set; }

        [ForeignKey("MovieId")]
        public Guid MovieId { get; set; }
        public Movie Movie { get; set; }

        [ForeignKey("UserId")]
        public string UserId { get; set; }
        public AppUserModel User { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
