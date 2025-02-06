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
        public string? PrimaryImage { get; set; }
        public string? MoreImage { get; set; }
        [ForeignKey("DirectorId")]
        public Guid? DirectorId { get; set; }
        public Director? Director { get; set; }
        public string Description { get; set; }
        public int Time { get; set; }
        public int? Year { get; set; }
        public string? MovieLink { get; set; }
        [ForeignKey("CategoryId")]
        public Guid? CategoryId { get; set; }
        public Category? Category { get; set; }
        [ForeignKey("CountryId")]
        public Guid? CountryId { get; set; }
        public Country? Country { get; set; }
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
        public ICollection<Review> Reviews { get; set; } = new HashSet<Review>();
        public ICollection<News> News { get; set; } = new HashSet<News>();
        public ICollection<MovieImage> MovieImages { get; set; }= new HashSet<MovieImage>();
        public ICollection<MovieActor> MovieActors { get; set; } = new HashSet<MovieActor>();
        public ICollection<WatchHistory> WatchHistories { get; set; } = new HashSet<WatchHistory>();
        public ICollection<FavoriteList> FavoriteLists { get; set; } = new HashSet<FavoriteList>();
    }
}
