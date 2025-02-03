using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Database_Movie.EF
{
    public class AppUserModel : IdentityUser, IAuditable
    {
        public string FullName { get; set; }
        public string? Address { get; set; }
        public string? Avatar { get; set; }
        public string? RoleId { get; set; }
        public string? Token { get; set; }
        [ForeignKey("GroupId")]
        public Guid? GroupId { get; set; }
        public Group? Group { get; set; }
        public bool Status { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? ModifiedBy { get; set; }
        [NotMapped]
        public string NewPassword { get; set; }
        [NotMapped]
        public string ConfirmPassword { get; set; }
        public ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    }
}
