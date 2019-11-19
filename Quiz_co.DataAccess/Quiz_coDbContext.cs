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
            modelBuilder.Entity<Quiz>().HasData(
               new Quiz
               {
                   Id = 1,
                   Title = "Test Your Halloween IQ!", 
                   ImageUrl = "https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.history.com%2Ftopics%2Fhalloween&psig=AOvVaw2JCtiYSkiYUwSMY2cKYkgm&ust=1574249677933000&source=images&cd=vfe&ved=0CAIQjRxqFwoTCPjBo6iX9uUCFQAAAAAdAAAAABAD",
                   UserId = bobUserId

               }
               );
            modelBuilder.Entity<Question>().HasData(
                new Question
                {
                    Id = 1,
                    Content = "On what day do we celebrate Halloween?",
                    QuizId = 1
                },
                new Question
                {
                    Id = 2,
                    Content = "The original spelling of the word ‘Halloween’ had an apostrophe. Where did it go?",
                    QuizId = 1
                }
                );
            modelBuilder.Entity<Answer>().HasData(
                new Answer
                {
                    Id = 1,
                    Content = "October 30",
                    IsCorrect = false,
                    QuestionId = 1
                },
                new Answer
                {
                    Id = 2,
                    Content = "November 31",
                    IsCorrect = false,
                    QuestionId = 1
                },
                new Answer
                {
                    Id = 3,
                    Content = "October 31",
                    IsCorrect = true,
                    QuestionId = 1
                },
                new Answer
                {
                    Id = 4,
                    Content = "Hallow’een",
                    IsCorrect = false,
                    QuestionId = 2
                },
                new Answer
                {
                    Id = 5,
                    Content = "H’alloween",
                    IsCorrect = false,
                    QuestionId = 2
                },
                new Answer
                {
                    Id = 6,
                    Content = "Hallowe’en",
                    IsCorrect = true,
                    QuestionId = 2
                }
                );

        }
    }
}
