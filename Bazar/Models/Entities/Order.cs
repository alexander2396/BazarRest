using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bazar.Models.Entities
{
    public class Order
    {
        public int OrderId { get; set; }

        [StringLength(100, ErrorMessage = "Макс. длина - 200 символов")]
        [Required(ErrorMessage = "Введіть ім'я")]
        public string Name { get; set; }

        [StringLength(100, ErrorMessage = "Макс. длина - 30 символов")]
        public string Email { get; set; }

        public string Theme { get; set; }

        [NotMapped]
        public int? ThemeId { get; set; }

        [NotMapped]
        public List<Theme> Themes => new List<Theme>()
        {
            new Theme
            {
                ThemeId = 1,
                Title = "стати постачальником"
            },
            new Theme
            {
                ThemeId = 2,
                Title = "пропозиція"
            },
            new Theme
            {
                ThemeId = 3,
                Title = "замовити столик"
            },
            new Theme
            {
                ThemeId = 4,
                Title = "інше"
            }
        };

        [StringLength(100, ErrorMessage = "Макс. длина - 30 символов")]
        [Required(ErrorMessage = "Введіть номер")]
        public string PhoneNumber { get; set; }

        [StringLength(500, ErrorMessage = "Макс. длина - 500 символов")]
        public string Message { get; set; }

        public DateTime Date { get; set; }
    }

    public class Theme
    {
        public int ThemeId { get; set; }

        public string Title { get; set; }
    }
}
