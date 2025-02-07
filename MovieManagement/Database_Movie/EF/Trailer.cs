using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Movie.EF
{
    [Table("Trailer")]
    public class Trailer : IAuditable
    {
        [Key]
        public Guid TrailerId { get; set; }
        public string TrailerName { get; set; }
        public string Image { get; set; }
        public string TrailerUrl { get; set; }
        public bool Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
