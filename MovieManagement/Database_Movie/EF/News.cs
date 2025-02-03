using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Movie.EF
{
    [Table("News")]
    public class News : IAuditable, IMeta
    {
        [Key]
        public Guid NewsId { get; set; }
        public string NewsTitle { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }

        [ForeignKey("CategoryId")]
        public Guid? CategoryId { get; set; }
        public Category? Category { get; set; }
        [ForeignKey("CountryId")]
        public Guid? CountryId { get; set; }
        public Country? Country { get; set; }
        [ForeignKey("TrailerId")]
        public Guid? TrailerId { get; set; }
        public Trailer? Trailer { get; set; }
        [ForeignKey("MovieId")]
        public Guid? MovieId { get; set; }
        public Movie? Movie { get; set; }
        public bool Status { get; set; }
        public DateTime? TopHot { get; set; }
        [ForeignKey("TagId")]
        public Guid? TagId { get; set; }
        public Tag? Tag { get; set; }
        public string? MoreDescription { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public string? MetaKeywords { get; set; }
        public string? MetaDescription { get; set; }
        public ICollection<NewsImage> NewsImages { get; set; } = new HashSet<NewsImage>();
    }
}
