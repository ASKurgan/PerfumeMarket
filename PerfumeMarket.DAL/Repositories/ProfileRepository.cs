using PerfumeMarket.DAL.Interfaces;
using PerfumeMarket.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeMarket.DAL.Repositories
{
    public class ProfileRepository : IBaseRepository<Profile>
    {
        private readonly AppDbContext _db;
        public ProfileRepository(AppDbContext db) 
        {
            _db = db;
        }
        public void Create(Profile entity)
        {
            _db.Profiles.Add(entity);
            _db.SaveChanges();
        }

        public void Delete(Profile entity)
        {
            _db.Profiles.Remove(entity);
            _db.SaveChanges();
        }

        public IQueryable<Profile> GetAll()
        {
            return _db.Profiles;
        }

        public Profile Update(Profile entity)
        {
            _db.Profiles.Update(entity);
            _db.SaveChanges();
            
            return entity;
        }
    }
}
