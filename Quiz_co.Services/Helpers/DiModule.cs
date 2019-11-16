using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Quiz_co.DataAccess;
using Quiz_co.DataAccess.Interfaces;
using Quiz_co.DataAccess.Repositories;
using Quiz_co.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz_co.Services.Helpers
{
    public class DiModule
    {
        public static IServiceCollection RegisterModules(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IUserRepository<User>, UserRepository>();
            services.AddTransient<IRepository<Quiz>, QuizRepository>();
            services.AddTransient<IRepository<Question>, QuestionRepository>();
            services.AddTransient<IRepository<Answer>, AnswerRepository>();


            services.AddDbContext<Quiz_coDbContext>(ob => ob.UseSqlServer(
                connectionString
            ));

            services.AddIdentity<User, IdentityRole>(opts =>
            {
                opts.User.RequireUniqueEmail = true;
                opts.Password.RequireNonAlphanumeric = true;
            })
         .AddRoleManager<RoleManager<IdentityRole>>()
         .AddEntityFrameworkStores<Quiz_coDbContext>()
         .AddDefaultTokenProviders();

            return services;
        }
    }
}
