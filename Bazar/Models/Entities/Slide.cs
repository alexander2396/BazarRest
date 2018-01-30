using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bazar.Models.Entities
{
    public class Slide
    {
        public int SlideId { get; set; }

        [Display(Name = "Порядок")]
        public int Order { get; set; }

        [Display(Name = "Изображение")]
        public string ImgFileName { get; set; }

        [Display(Name = "Title изображения")]
        [StringLength(100, ErrorMessage = "Макс. длина - 100 символов")]
        public string Title { get; set; }

        [Display(Name = "Alt изображения")]
        [StringLength(100, ErrorMessage = "Макс. длина - 100 символов")]
        public string Alt { get; set; }

        [Display(Name = "Ссылка")]
        [Required(ErrorMessage = "Обязателен: введите ссылку")]
        [StringLength(100, ErrorMessage = "Макс. длина - 100 символов")]
        public string Url { get; set; }
    }
}
