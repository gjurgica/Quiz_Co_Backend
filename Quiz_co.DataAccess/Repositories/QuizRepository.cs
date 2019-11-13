using Microsoft.EntityFrameworkCore;
using Quiz_co.DataAccess.Interfaces;
using Quiz_co.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quiz_co.DataAccess.Repositories
{
    public class QuizRepository : BaseRepository<Quiz_coDbContext>, IRepository<Quiz>
    {
        public QuizRepository(Quiz_coDbContext context) : base(context) { }
        public int Delete(int id)
        {
            var entity = _context.Quizzes.FirstOrDefault(u => u.Id == id);
            if (entity == null)
                return -1;

            _context.Quizzes.Remove(entity);
            return _context.SaveChanges();
        }

        public IEnumerable<Quiz> GetAll()
        {
            return _context.Quizzes
                .Include(x => x.User)
                .Include(x => x.Questions)
                    .ThenInclude(x => x.Answers)
                .ToList();
        }

        public Quiz GetById(int id)
        {
            return _context.Quizzes
               .Include(x => x.User)
               .Include(x => x.Questions)
                   .ThenInclude(x => x.Answers)
                .FirstOrDefault(x => x.Id == id);
        }

        public int Insert(Quiz entity)
        {
            _context.Quizzes.Add(entity);
            return _context.SaveChanges();
        }

        public int Update(Quiz entity)
        {
            _context.Quizzes.Update(entity);
            return _context.SaveChanges();
        }
    }
}
