using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Bazar.Models.Entities
{
    public class BlogCategory
    {
        public int BlogCategoryId { get; set; }

        [Display(Name = "Url ссылки")]
        [StringLength(100, ErrorMessage = "Макс. длина 100 символов")]
        [Required(ErrorMessage = "Обязателен: Введите url ссылки")]
        public string LinkUrl { get; set; }

        [Display(Name = "Заголовок ссылки")]
        [StringLength(50, ErrorMessage = "Макс. длина 50 символов")]
        [Required(ErrorMessage = "Обязателен: Введите заголовок ссылки")]
        public string LinkTitle { get; set; }

        [Display(Name = "Цвет")]
        public string Color { get; set; }

        public List<Article> Articles { get; set; }
    }
}
