using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quiz_co.DataAccess.Interfaces
{
    public interface IUserRepository<T> where T : IdentityUser
    {
        IEnumerable<T> GetAll();
        T GetById(string id);
        T GetByUsername(string username);
        int Insert(T entity);
        int Update(T entity);
        int Delete(string id);
    }
}
