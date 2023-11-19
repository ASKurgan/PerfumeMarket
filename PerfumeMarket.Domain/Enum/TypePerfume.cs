using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeMarket.Domain.Enum
{
    public enum TypePerfume
    {
        [Display(Name = "Eau De Cologne")]
        Cologne = 0,

        [Display(Name = "Eau De Toilette")]
        EDT = 1,

        [Display(Name = "Eau De Perfume")]
        EDP = 2,

        [Display(Name = "Selective")]
        Selective = 3,

        [Display(Name = "Mayal perfumes on tap")]
        MayalPerfumesOnTap = 4,

    }
}
