using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Movie.EF
{
    [Table("Movie")]
    public class Movie : IAuditable, IMeta
    {
        [Key]
        public Guid MovieId { get; set; }
        public string MovieName { get; set; }
        public string Image { get; set; }
        [Column(TypeName = "xml")]
        public string MoreImages { get; set; }
        [StringLength(50)]
        public string Actors { get; set; }
        [StringLength(50)]
        public string Directors { get; set; }
        public string Description { get; set; }
        [Column(TypeName = "text")]
        public string Time { get; set; }
        public int? Year { get; set; }
        public string Country { get; set; }
        public string MovieLink { get; set; }
        public string TrailerLink { get; set; }
        [ForeignKey("CategoryId")]
        public Guid? CategoryId { get; set; }
        public Category? Category { get; set; }
        public int? Rating { get; set; }
        [ForeignKey("TrailerId")]
        public Guid? TrailerId { get; set; }
        public Trailer? Trailer { get; set; }
        public int? Viewed { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public string? MetaKeywords { get; set; }
        public string? MetaDescription { get; set; }
        public bool Status { get; set; }
        public DateTime? TopHot { get; set; }
        public int? CountryId { get; set; }
        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    }
}
