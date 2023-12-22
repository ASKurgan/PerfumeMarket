using PerfumeMarket.DAL.Interfaces;
using PerfumeMarket.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeMarket.DAL.Repositories
{
    public class BasketRepository : IBaseRepository<Basket>
    {
        private readonly AppDbContext _db;
        public BasketRepository(AppDbContext db) 
        {
            _db = db;
        }

        public void Create(Basket entity)
        {
           _db.Baskets.Add(entity);
           _db.SaveChanges();
        }

        public void Delete(Basket entity)
        {
            _db.Baskets.Remove(entity);
            _db.SaveChanges();
        }

        public IQueryable<Basket> GetAll()
        {
           return  _db.Baskets;
        }

        public Basket Update(Basket entity)
        {
            _db.Baskets.Update(entity);
            _db.SaveChanges();

            return entity;
        }
    }
}
