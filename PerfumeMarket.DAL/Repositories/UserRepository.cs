using PerfumeMarket.DAL.Interfaces;
using PerfumeMarket.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeMarket.DAL.Repositories
{
    public class UserRepository:IBaseRepository<User>
    {
        private readonly AppDbContext _db;
        public UserRepository(AppDbContext dbContext)
        {
           _db = dbContext;
        }

        public void Create(User entity)
        {
            _db.Users.Add(entity);
            _db.SaveChanges();
        }

        public void Delete(User entity)
        {
            _db.Users.Remove(entity);
            _db.SaveChanges();
        }

        public IQueryable<User> GetAll()
        {
            return _db.Users;    
        }

        public User Update(User entity)
        {
            _db.Users.Update(entity);
            _db.SaveChanges();
            
             return entity;
        }
    }
}
