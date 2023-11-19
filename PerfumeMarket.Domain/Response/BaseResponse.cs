using PerfumeMarket.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeMarket.Domain.Response
{
    public class BaseResponse<T> : IBaseResponse<T>

    {
        /// <summary>
        /// Описание ошибки или предупреждения
        /// </summary>
        public string Description { get ; set ; }

        /// <summary>
        /// Результат нашего запроса к базе данных
        /// </summary>
        public T Data { get; set ; }

        /// <summary>
        /// Статус ошибки
        /// </summary>
        public StatusCode StatusCode { get; set; }
    }

    public interface IBaseResponse <T>
    
    {
        /// <summary>
        /// Описание ошибки или предупреждения
        /// </summary>
        string Description { get; set; }
        /// <summary>
        /// Результат нашего запроса к базе данных
        /// </summary>
        T Data { get; set; }

        /// <summary>
        /// Статус ошибки
        /// </summary>
        StatusCode StatusCode { get; set; }
    }
}
