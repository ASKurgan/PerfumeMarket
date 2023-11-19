using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfumeMarket.Domain.ViewModel.Perfume
{
    public class PerfumeViewModel
    {
        public long Id { get; set; }

        [Display(Name = "Название")]
        [Required(ErrorMessage = "Введите имя")]
        [MinLength(2, ErrorMessage = "Минимальная длина должна быть больше двух символов")]
        public string Name { get; set; }

        [Display(Name = "Описание")]
        [MinLength(50, ErrorMessage = "Минимальная длина должна быть больше 50 символов")]
        public string Description { get; set; }

        [Display(Name = "Бренд")]
        [Required(ErrorMessage = "Укажите бренд")]
        [MinLength(2, ErrorMessage = "Минимальная длина должна быть больше двух символов")]
        public string Brand { get; set; }

        [Display(Name = "Группа аромата (древесные, свежие, восточные, акватические и тд)")]
        [Required(ErrorMessage = "Укажите группу (древесные, свежие, восточные, акватические и тд)")]
        [Range(0, 600, ErrorMessage = "Длина должна быть в диапазоне от 0 до 600")]
        public string GroupOfFragrances { get; set; }

        [Display(Name = "Стоимость")]
        [Required(ErrorMessage = "Укажите стоимость")]
        public decimal Price { get; set; }

        public string DateCreate { get; set; }

        [Display(Name = "Тип аромата")]
        [Required(ErrorMessage = "Выберите тип")]
        public string TypePerfume { get; set; }

        //public IFormFile Avatar { get; set; }

        public byte[]? Image { get; set; }

    }
}
