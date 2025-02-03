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
                });
        }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Ads> Advs { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Trailer> Trailers { get; set; }
    }
}
