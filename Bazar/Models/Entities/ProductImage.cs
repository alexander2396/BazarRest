using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bazar.Models.Entities
{
    public class ProductImage
    {
        public int ProductImageId { get; set; }

        public int ProductId { get; set; }

        [Display(Name = "Изображение")]
        public string FileName { get; set; }

        [Display(Name = "Ссылка")]
        public string Link { get; set; }

        [Display(Name = "Title изображения")]
        public string Title { get; set; }

        [Display(Name = "Alt изображения")]
        public string Alt { get; set; }

        [NotMapped]
        public bool IsDeleted { get; set; } = false;

        public Product Product { get; set; }
    }
}
