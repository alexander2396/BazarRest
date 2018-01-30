using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bazar.Models.Entities
{
    public class Restoran
    {
        public int RestoranId { get; set; }

        [Display(Name = "Заголовок")]
        [Required(ErrorMessage = "Обязателен: введите заголовок")]
        public string Title { get; set; }

        [Display(Name = "Заголовок текста")]
        [Required(ErrorMessage = "Обязателен: введите заголовок текста")]
        public string ContentTitle { get; set; }

        [Display(Name = "Текст")]
        [Required(ErrorMessage = "Обязателен: введите текст")]
        public string Content { get; set; }

        [Display(Name = "Текст кнопки")]
        [Required(ErrorMessage = "Обязателен: введите текст кнопки")]
        public string BtnTitle { get; set; }

        [Display(Name = "Ссылка кнопки")]
        [Required(ErrorMessage = "Обязателен: введите ссылку кнопки")]
        public string Url { get; set; }
    }
}
