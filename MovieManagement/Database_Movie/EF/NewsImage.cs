using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Movie.EF
{
    [Table("NewsImage")]
    public class NewsImage
    {
        [Key]
        public Guid NewsImageId { get; set; }

        [ForeignKey("NewsId")]
        public Guid? NewsId { get; set; }
        public News? News { get; set; }

        public string? ImageUrl { get; set; }  // Đường dẫn ảnh
        public bool? IsPrimary { get; set; } // Đánh dấu ảnh chính
    }
}
