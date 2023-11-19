using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeMarket.DAL.Interfaces
{
    public interface IBaseRepository <T>
    {
        void Create(T entity);

        IQueryable<T> GetAll();

        void Delete(T entity);

        T Update(T entity);
    }
}
