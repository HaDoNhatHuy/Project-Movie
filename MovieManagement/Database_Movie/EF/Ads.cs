using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Movie.EF
{
    [Table("Ads")]
    public class Ads : IAuditable, IMeta
    {
        [Key]
        public Guid AdsId { get; set; }
        public string AdsName { get; set; }
        public string AdsImage { get; set; }
        public string AdsUrl { get; set; }
        public bool Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public string? MetaKeywords { get; set; }
        public string? MetaDescription { get; set; }
    }
}
