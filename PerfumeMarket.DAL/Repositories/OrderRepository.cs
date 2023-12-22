using PerfumeMarket.DAL.Interfaces;
using PerfumeMarket.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeMarket.DAL.Repositories
{
    public class OrderRepository : IBaseRepository<Order>
    {
        private readonly AppDbContext _db;
        public OrderRepository(AppDbContext db) 
        {
            _db = db;
        }
        public void Create(Order entity)
        {
            _db.Orders.Add(entity);
            _db.SaveChanges(); 
        }

        public void Delete(Order entity)
        {
            _db.Orders.Remove(entity);
            _db.SaveChanges();
        }

        public IQueryable<Order> GetAll()
        {
            return _db.Orders;
        }

        public Order Update(Order entity)
        {
            _db.Orders.Update(entity);
            _db.SaveChanges();

            return entity;
        }
    }
}
