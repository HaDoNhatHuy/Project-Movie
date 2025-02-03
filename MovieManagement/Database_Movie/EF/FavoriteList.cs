using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Movie.EF
{
    [Table("FavoriteList")]
    public class FavoriteList
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("UserId")]
        public string? UserId { get; set; }
        public AppUserModel? User { get; set; }

        [ForeignKey("MovieId")]
        public Guid? MovieId { get; set; }
        public Movie? Movie { get; set; }
        public DateTime AddedAt { get; set; }
    }
}
