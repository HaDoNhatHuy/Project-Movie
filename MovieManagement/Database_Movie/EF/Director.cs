using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Movie.EF
{
    [Table("Director")]
    public class Director
    {
        [Key]
        public Guid DirectorId { get; set; }

        [Required]
        public string Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Nationality { get; set; }
        public string? Biography { get; set; }
        public string? ProfileImage { get; set; }

        // Danh sách phim mà đạo diễn đã thực hiện
        public ICollection<Movie> Movies { get; set; } = new HashSet<Movie>();
    }
}
