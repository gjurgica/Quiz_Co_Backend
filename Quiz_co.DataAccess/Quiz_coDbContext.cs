using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Quiz_co.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz_co.DataAccess
{
    public class Quiz_coDbContext : IdentityDbContext<User>
    {
        public Quiz_coDbContext(DbContextOptions options)
          : base(options) { }

        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<User>()
               .HasMany(u => u.Quizzes)
               .WithOne(o => o.User)
               .HasForeignKey(o => o.UserId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Quiz>()
                .HasMany(q => q.Questions)
                .WithOne(q => q.Quiz)
                .HasForeignKey(q => q.QuizId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Question>()
                .HasMany(q => q.Answers)
                .WithOne(a => a.Question)
                .HasForeignKey(q => q.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            string adminRoleId = Guid.NewGuid().ToString();
            string userRoleId = Guid.NewGuid().ToString();

            modelBuilder.Entity<IdentityRole>().HasData(
             new IdentityRole
             {
                 Id = adminRoleId,
                 Name = "admin",
                 NormalizedName = "ADMIN"
             },
             new IdentityRole
             {
                 Id = userRoleId,
                 Name = "user",
                 NormalizedName = "USER"
             }
         );

            string bobUserId = Guid.NewGuid().ToString();

            var hasher = new PasswordHasher<User>();

            modelBuilder.Entity<User>()
           .HasData(
                new User()
                {
                    Id = bobUserId,
                    FirstName = "Bob",
                    LastName = "Bobsky",
                    UserName = "Boby",
                    NormalizedUserName = "BOBY",
                    Email = "bob@gmail.com",
                    NormalizedEmail = "BOB@GMAIL.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Bob123456"),
                    SecurityStamp = string.Empty,
                    Joined = DateTime.Now
                }
                );

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                    new IdentityUserRole<string>
                    {
                        RoleId = adminRoleId,
                        UserId = bobUserId
                    }
                );

        }
    }
}
