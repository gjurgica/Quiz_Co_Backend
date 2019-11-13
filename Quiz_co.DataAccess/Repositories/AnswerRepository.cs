using Microsoft.EntityFrameworkCore;
using Quiz_co.DataAccess.Interfaces;
using Quiz_co.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quiz_co.DataAccess.Repositories
{
    public class AnswerRepository : BaseRepository<Quiz_coDbContext>, IRepository<Answer>
    {
        public AnswerRepository(Quiz_coDbContext context) : base(context) { }
        public int Delete(int id)
        {
            var entity = _context.Answers.FirstOrDefault(u => u.Id == id);
            if (entity == null)
                return -1;

            _context.Answers.Remove(entity);
            return _context.SaveChanges();
        }

        public IEnumerable<Answer> GetAll()
        {
            return _context.Answers
                .Include(x => x.Question)
                    .ThenInclude(x => x.Quiz)
                .ToList();
        }

        public Answer GetById(int id)
        {
            return _context.Answers
                .Include(x => x.Question)
                    .ThenInclude(x => x.Quiz)
                .FirstOrDefault(x => x.Id == id);
        }

        public int Insert(Answer entity)
        {
            _context.Answers.Add(entity);
            return _context.SaveChanges();
        }

        public int Update(Answer entity)
        {
            _context.Answers.Update(entity);
            return _context.SaveChanges();
        }
    }
}
