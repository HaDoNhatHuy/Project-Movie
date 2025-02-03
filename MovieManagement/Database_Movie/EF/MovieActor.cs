using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Movie.EF
{
    [Table("MovieActor")]
    public class MovieActor
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("MovieId")]
        public Guid? MovieId { get; set; }
        public Movie? Movie { get; set; }
        [ForeignKey("ActorId")]
        public Guid? ActorId { get; set; }
        public Actor? Actor { get; set; }
    }
}
