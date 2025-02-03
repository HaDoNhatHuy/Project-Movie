using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Movie.EF
{
    [Table("MovieImage")]
    public class MovieImage
    {
        [Key]
        public Guid MovieImageId { get; set; }

        [ForeignKey("MovieId")]
        public Guid? MovieId { get; set; }
        public Movie? Movie { get; set; }

        public string? ImageUrl { get; set; }  // Đường dẫn ảnh
        public bool? IsPrimary { get; set; } // Đánh dấu ảnh chính
    }
}
