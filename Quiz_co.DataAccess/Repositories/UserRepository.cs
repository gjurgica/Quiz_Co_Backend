using Quiz_co.DataAccess.Interfaces;
using Quiz_co.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Quiz_co.DataAccess.Repositories
{
    public class UserRepository : BaseRepository<Quiz_coDbContext>, IUserRepository<User>
    {
        public UserRepository(Quiz_coDbContext context) : base(context) { }
        public int Delete(string id)
        {
            var entity = _context.Users.FirstOrDefault(u => u.Id == id);
            if (entity == null)
                return -1;

            _context.Users.Remove(entity);
            return _context.SaveChanges();
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public User GetById(string id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
        }

        public User GetByUsername(string username)
        {
            return _context.Users.FirstOrDefault(x => x.UserName == username);
        }

        public int Insert(User entity)
        {
            _context.Users.Add(entity);
            return _context.SaveChanges();
        }

        public int Update(User entity)
        {
            User user = _context.Users.FirstOrDefault(u => u.Id == entity.Id);
            user.ImageUrl = entity.ImageUrl;
            user.FirstName = entity.FirstName;
            user.LastName = entity.LastName;
            user.UserName = entity.UserName;
            user.Email = entity.Email;

            return _context.SaveChanges();
        }
    }
}
