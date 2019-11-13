using Microsoft.EntityFrameworkCore;
using Quiz_co.DataAccess.Interfaces;
using Quiz_co.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quiz_co.DataAccess.Repositories
{
    public class QuestionRepository : BaseRepository<Quiz_coDbContext>, IRepository<Question>
    {
        public QuestionRepository(Quiz_coDbContext context) : base(context) { }
        public int Delete(int id)
        {
            var entity = _context.Questions.FirstOrDefault(u => u.Id == id);
            if (entity == null)
                return -1;

            _context.Questions.Remove(entity);
            return _context.SaveChanges();
        }

        public IEnumerable<Question> GetAll()
        {
            return _context.Questions
                .Include(x => x.Answers)
                .Include(x => x.Quiz)
                .ToList();
        }

        public Question GetById(int id)
        {
            return _context.Questions
                .Include(x => x.Answers)
                .Include(x => x.Quiz)
                .FirstOrDefault(x => x.Id == id);
        }

        public int Insert(Question entity)
        {
            _context.Questions.Add(entity);
            return _context.SaveChanges();
        }

        public int Update(Question entity)
        {
            _context.Questions.Update(entity);
            return _context.SaveChanges();
        }
    }
}
