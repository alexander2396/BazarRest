using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Bazar.Models.Entities;
using Bazar.Models.Repository;
using Bazar.Models.ViewModels;

namespace Bazar.Controllers
{
    public class BlogController : Controller
    {
        private ArticleRepository articleRepository;

        public BlogController(ArticleRepository articleRepo)
        {
            articleRepository = articleRepo;
        }

        public IActionResult Index(string linkCategory)
        {
            IEnumerable<Article> articles = articleRepository
                .Articles
                .Include(a => a.BlogCategory)
                .Where(a => a.BlogCategory.LinkUrl == linkCategory);

            BlogCategory category = articleRepository
                .BlogCategories
                .FirstOrDefault(bc => bc.LinkUrl == linkCategory);

            IEnumerable<BlogCategory> categories = articleRepository
                .BlogCategories;

            BlogListViewModel model = new BlogListViewModel
            {
                Articles = articles,
                BlogCategories = categories,
                CurrentCategory = category
            };

            return View(model);
        }

        public new IActionResult View(string linkUrl)
        {
            Article article = articleRepository
                .Articles
                .Include(a => a.BlogCategory)
                .FirstOrDefault(a => a.LinkUrl == linkUrl);

            IEnumerable<Article> articles = articleRepository
                .Articles
                .Include(a => a.BlogCategory)
                .Where(a => a.ArticleId != article.ArticleId)
                .OrderBy(b => Guid.NewGuid())
                .Take(3);

            BlogViewModel model = new BlogViewModel
            {
                Article = article,
                Articles = articles
            };

            return View(model);
        }
    }
}
