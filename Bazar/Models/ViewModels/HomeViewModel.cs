using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Bazar.Models.Entities;

namespace Bazar.Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Slide> Slides { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Article> Articles { get; set; }
        public Restoran Restoran { get; set; }
        public bool MoreArticles { get; set; }
        public Order Order { get; set; }
    }
}
