using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Bazar.Models.Entities;

namespace Bazar.Models.ViewModels
{
    public class BlogListViewModel
    {
        public IEnumerable<Article> Articles { get; set; }
        public IEnumerable<BlogCategory> BlogCategories { get; set; }
        public BlogCategory CurrentCategory { get; set; }
    }
}
