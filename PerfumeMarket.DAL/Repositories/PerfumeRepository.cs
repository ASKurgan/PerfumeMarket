using NPOI.OpenXmlFormats;
using NPOI.SS.Formula.Functions;
using PerfumeMarket.DAL.Interfaces;
using PerfumeMarket.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeMarket.DAL.Repositories
{
    public class PerfumeRepository : IBaseRepository<Perfume>
    {
        private readonly AppDbContext _db;
        public PerfumeRepository(AppDbContext db) 
        {
            _db = db; 
        }
        public void Create(Perfume entity)
        {
          _db.Perfumes.Add(entity) ;
          _db.SaveChanges() ;
        }

        public void Delete(Perfume entity)
        {
            _db.Perfumes.Remove(entity);
            _db.SaveChanges() ;
        }

        public IQueryable<Perfume> GetAll()
        {
            return _db.Perfumes;
        }

        public Perfume Update(Perfume entity)
        {
            _db.Perfumes.Update(entity) ;
            _db.SaveChanges();

            return entity ;
        }
    }
}
