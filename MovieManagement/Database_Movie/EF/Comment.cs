using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Movie.EF
{
    [Table("Comment")]
    public class Comment : IAuditable, IMeta
    {
        [Key]
        public Guid CommentId { get; set; }
        [ForeignKey("MovieId")]
        public Guid MovieId { get; set; }
        public Movie Movie { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        [ForeignKey("UserId")]
        public string? UserId { get; set; }
        public AppUserModel? AppUserModel { get; set; }
        public bool Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public string? MetaKeywords { get; set; }
        public string? MetaDescription { get; set; }
    }
}
