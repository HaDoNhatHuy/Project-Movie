using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database_Movie.EF
{
    public class MovieContext : IdentityDbContext<AppUserModel>
    {
        public MovieContext(DbContextOptions<MovieContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Bỏ tiền tố AspNet của các bảng: mặc định
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }
            // Seed dữ liệu mẫu Group
            var adminGroup = new Group()
            {
                GroupId = Guid.Parse("0157CB76-B588-4312-BD72-37F414A9193C"),
                Name = "Admin"
            };
            modelBuilder.Entity<Group>().HasData(adminGroup);

            // Seed dữ liệu mẫu User
            var user = new AppUserModel()
            {
                Id = "3453C2B8-58C8-439C-927F-BBB7B42A4C37",
                FullName = "Hà Đỗ Nhật Huy",
                Avatar = "my-avatar.jpg",
                Status = true,
                UserName = "nhathuy",
                PhoneNumber = "0399539455",
                Address = "384/5, Ấp Bà Phổ, xã Bình Thạnh, huyện Thủ Thừa, tỉnh Long An",
                NormalizedUserName = "NHATHUY",
                Email = "nhathuy.hado@gmail.com",
                NormalizedEmail = "NHATHUY.HADO@GMAIL.COM",
                EmailConfirmed = true,
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                GroupId = adminGroup.GroupId
            };

            // Hash mật khẩu
            var passwordHasher = new PasswordHasher<AppUserModel>();
            user.PasswordHash = passwordHasher.HashPassword(user, "123456");

            modelBuilder.Entity<AppUserModel>().HasData(user);

            modelBuilder.Entity<Category>().HasData(
                new Category()
                {
                    CategoryId = Guid.Parse("E8086321-EE35-47B6-9806-5C96373C2D11"),
                    CategoryName = "ROOT",
                    ParentId = null,
                    Status = true,
                },
                new Category()
                {
                    CategoryId = Guid.Parse("0922C247-A6DC-42AA-855B-42BDFB6926E1"),
                    CategoryName = "THỂ LOẠI",
                    ParentId = Guid.Parse("E8086321-EE35-47B6-9806-5C96373C2D11"),
                    Status = true,
                },
                new Category()
                {
                    CategoryId = Guid.Parse("77EC7193-B9BD-4220-9E52-78615BA520F5"),
                    CategoryName = "KHOA HỌC",
                    ParentId = Guid.Parse("0922C247-A6DC-42AA-855B-42BDFB6926E1"),
                    Status = true,
                },
                new Category()
                {
                    CategoryId = Guid.Parse("16FBEAAD-0F2A-4517-BBAE-C60EC5E26227"),
                    CategoryName = "HÀNH ĐỘNG",
                    ParentId = Guid.Parse("0922C247-A6DC-42AA-855B-42BDFB6926E1"),
                    Status = true,
                },
                new Category()
                {
                    CategoryId = Guid.Parse("A9C0985F-E364-4674-84FE-971D3F8265E5"),
                    CategoryName = "PHIÊU LƯU",
                    ParentId = Guid.Parse("0922C247-A6DC-42AA-855B-42BDFB6926E1"),
                    Status = true,
                },
                new Category()
                {
                    CategoryId = Guid.Parse("39B35EC7-C610-48C0-835C-7A72A51A1D81"),
                    CategoryName = "KINH DỊ",
                    ParentId = Guid.Parse("0922C247-A6DC-42AA-855B-42BDFB6926E1"),
                    Status = true,
                },
                new Category()
                {
                    CategoryId = Guid.Parse("3EC595F3-716C-4B89-88A5-13C900A60A2B"),
                    CategoryName = "HÀI HƯỚC",
                    ParentId = Guid.Parse("0922C247-A6DC-42AA-855B-42BDFB6926E1"),
                    Status = true,
                },
                new Category()
                {
                    CategoryId = Guid.Parse("E9785170-EEB3-4B34-BFB7-6DA5869A9CA6"),
                    CategoryName = "VIỄN TƯỞNG",
                    ParentId = Guid.Parse("0922C247-A6DC-42AA-855B-42BDFB6926E1"),
                    Status = true,
                },
                new Category()
                {
                    CategoryId = Guid.Parse("C96B394B-5DF2-425D-8313-936390B49698"),
                    CategoryName = "TÂM LÝ",
                    ParentId = Guid.Parse("0922C247-A6DC-42AA-855B-42BDFB6926E1"),
                    Status = true,
                },
                new Category()
                {
                    CategoryId = Guid.Parse("DD6CA578-90FF-4F4A-8A7F-7536102E943D"),
                    CategoryName = "HOẠT HÌNH",
                    ParentId = Guid.Parse("0922C247-A6DC-42AA-855B-42BDFB6926E1"),
                    Status = true,
                },
                new Category()
                {
                    CategoryId = Guid.Parse("AF4C7644-0B75-49A5-98B2-2570E56A34BC"),
                    CategoryName = "TÀI LIỆU",
                    ParentId = Guid.Parse("0922C247-A6DC-42AA-855B-42BDFB6926E1"),
                    Status = true,
                },
                new Category()
                {
                    CategoryId = Guid.Parse("452328B9-1F8A-4948-8D68-8E57AEA7D117"),
                    CategoryName = "CHIẾN TRANH",
                    ParentId = Guid.Parse("0922C247-A6DC-42AA-855B-42BDFB6926E1"),
                    Status = true,
                },
                new Category()
                {
                    CategoryId = Guid.Parse("3202DB0B-4211-4C88-9612-90A95B8063D4"),
                    CategoryName = "ÂM NHẠC",
                    ParentId = Guid.Parse("0922C247-A6DC-42AA-855B-42BDFB6926E1"),
                    Status = true,
                },
                new Category()
                {
                    CategoryId = Guid.Parse("9DED756D-B173-4B6D-AD1E-44CF47AB88EC"),
                    CategoryName = "TỘI PHẠM",
                    ParentId = Guid.Parse("0922C247-A6DC-42AA-855B-42BDFB6926E1"),
                    Status = true,
                });
            modelBuilder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "view-movies", NormalizedName = "VIEW-MOVIES" },
            new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "edit-news", NormalizedName = "EDIT-NEWS" },
            new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "delete-comments", NormalizedName = "DELETE-COMMENTS" }
            );
        }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<MovieActor> MoviesActors { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieImage> MoviesImages { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Ads> Advs { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupRole> GroupRoles { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<NewsImage> NewsImages { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Trailer> Trailers { get; set; }
        public DbSet<Help> Helps { get; set; }
        public DbSet<FavoriteList> FavoriteLists { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<WatchHistory> WatchHistories { get; set; }
    }
}
