using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace Bazar.Models.Entities
{
    public class Article
    {
        public int ArticleId { get; set; }

        [Display(Name = "Категория")]
        [Required(ErrorMessage = "Обязателен: выберите категорию")]
        public int BlogCategoryId { get; set; }  

        [Display(Name = "Заголовок")]
        [StringLength(100, ErrorMessage = "Макс. длина 100 символов")]
        [Required(ErrorMessage = "Обязателен: введите заголовок")]
        public string Title { get; set; }

        [Display(Name = "Url ссылки")]
        [StringLength(100, ErrorMessage = "Макс. длина 100 символов")]
        [Required(ErrorMessage = "Обязателен: Введите url ссылки")]
        public string LinkUrl { get; set; }

        [Display(Name = "Заголовок ссылки")]
        [StringLength(50, ErrorMessage = "Макс. длина 50 символов")]
        [Required(ErrorMessage = "Обязателен: Введите заголовок ссылки")]
        public string LinkTitle { get; set; }

        [Display(Name = "Контент")]
        [Required(ErrorMessage = "Обязателен: Контент является содержанием статьи")]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }

        [Display(Name = "Изображение")]
        public string ImageFileName { get; set; }

        [Display(Name = "Изображение")]
        public string ImageSmallFileName { get; set; }

        [Display(Name = "Title изображения")]
        [StringLength(100, ErrorMessage = "Макс. длина - 100 символов")]
        public string ImageTitle { get; set; }

        [Display(Name = "Alt изображения")]
        [StringLength(100, ErrorMessage = "Макс. длина - 100 символов")]
        public string ImageAlt { get; set; }

        public DateTime PublishDate { get; set; }

        [NotMapped]
        public string FormattedDate
        {
            get
            {
                if((DateTime.Now - PublishDate).TotalHours < 12)
                {
                    int hours = (int)(DateTime.Now - PublishDate).TotalHours;
                    if (hours == 0) hours = 1;
                    return hours.ToString() + "г тому";
                }
                else
                {
                    return PublishDate.ToString("dd MMM yyyy", new CultureInfo("uk-UA"));
                }  
            }
        }

        public BlogCategory BlogCategory { get; set; }
    }
}
