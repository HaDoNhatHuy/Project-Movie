using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Movie.EF
{
    [Table("Actor")]
    public class Actor
    {
        [Key]
        public Guid ActorId { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime? DateOfBirth { get; set; }
        public string? Nationality { get; set; }
        public string? Biography { get; set; }
        public string? ProfileImage { get; set; }

        // Danh sách phim mà diễn viên tham gia        
        public ICollection<MovieActor> MovieActors { get; set; } = new HashSet<MovieActor>();
    }
}
