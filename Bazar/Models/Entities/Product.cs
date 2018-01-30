using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bazar.Models.Entities
{
    public class Product
    {
        public int ProductId { get; set; }

        [Display(Name = "Заголовок")]
        [StringLength(100, ErrorMessage = "Макс. длина 100 символов")]
        [Required(ErrorMessage = "Обязателен: введите заголовок")]
        public string Title { get; set; }

        [Display(Name = "Url ссылки")]
        [StringLength(100, ErrorMessage = "Макс. длина 100 символов")]
        public string LinkUrl { get; set; }

        [Display(Name = "Порядок")]
        public int Order { get; set; }

        [StringLength(4000, ErrorMessage = "Макс. длина 4000 символов")]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Display(Name = "Изображение")]
        public string ImageFileName { get; set; }

        [Display(Name = "Изображение на главной")]
        public string ImageSmallFileName { get; set; }

        [Display(Name = "Title изображения")]
        [StringLength(100, ErrorMessage = "Макс. длина - 100 символов")]
        public string ImageTitle { get; set; }

        [Display(Name = "Alt изображения")]
        [StringLength(100, ErrorMessage = "Макс. длина - 100 символов")]
        public string ImageAlt { get; set; }

        [Display(Name = "Иконка")]
        public string IconFileName { get; set; }

        [Display(Name = "Иконка")]
        public string IconWhiteFileName { get; set; }

        [Display(Name = "Title иконки")]
        [StringLength(100, ErrorMessage = "Макс. длина - 100 символов")]
        public string IconTitle { get; set; }

        [Display(Name = "Alt иконки")]
        [StringLength(100, ErrorMessage = "Макс. длина - 100 символов")]
        public string IconAlt { get; set; }

        public List<ProductImage> ProductImages { get; set; }
    }
}
