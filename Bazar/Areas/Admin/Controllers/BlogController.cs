using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Bazar.Models.Entities;
using Bazar.Models.Repository;

namespace Bazar.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class BlogController : Controller
    {
        private ArticleRepository articleRepository;

        public BlogController(ArticleRepository articleRepo)
        {
            articleRepository = articleRepo;
        }

        public IActionResult Index()
        {
            IEnumerable<Article> articles = articleRepository.Articles;

            return View(articles);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(articleRepository.BlogCategories, "BlogCategoryId", "LinkTitle", null);

            return View("Edit", new Article());
        }

        public IActionResult Edit(int id)
        {
            if (id == 0) return NotFound();

            Article article = articleRepository
                .Articles
                .Include(a => a.BlogCategory)
                .FirstOrDefault(a => a.ArticleId == id);

            ViewBag.Categories = new SelectList(articleRepository.BlogCategories, "BlogCategoryId", "LinkTitle", article.BlogCategoryId);

            return View(article);
        }

        [HttpPost]
        public IActionResult Edit(Article article)
        {
            if (ModelState.IsValid)
            {
                //Save main image
                IFormFile Image = Request.Form.Files.Where(f => f.Name == "Image").First();
                if (Image.FileName != "")
                {
                    string extension;
                    switch (Image.ContentType)
                    {
                        case "image/jpeg": extension = ".jpg"; break;
                        case "image/png": extension = ".png"; break;
                        default: throw new Exception($"Изображение {Image.FileName} не было сохранено так как формат {Image.ContentType} не подерживается.");
                    }
                    article.ImageFileName = article.LinkUrl + extension;
                    using (var stream = new FileStream("wwwroot/DynamicImages/Article/" + article.ImageFileName, FileMode.Create))
                    {
                        Image.CopyTo(stream);
                    }
                }

                //Save small image
                IFormFile SmallImage = Request.Form.Files.Where(f => f.Name == "SmallImage").First();
                if (SmallImage.FileName != "")
                {
                    string extension;
                    switch (SmallImage.ContentType)
                    {
                        case "image/jpeg": extension = ".jpg"; break;
                        case "image/png": extension = ".png"; break;
                        default: throw new Exception($"Изображение {SmallImage.FileName} не было сохранено так как формат {SmallImage.ContentType} не подерживается.");
                    }
                    article.ImageSmallFileName = article.LinkUrl  + "-small" + extension;
                    using (var stream = new FileStream("wwwroot/DynamicImages/Article/" + article.ImageSmallFileName, FileMode.Create))
                    {
                        SmallImage.CopyTo(stream);
                    }
                }

                if(article.ArticleId == 0)
                {
                    article.PublishDate = DateTime.Now;
                }
                
                articleRepository.SaveArticle(article);

                TempData["message"] = "Статья сохранена";
                TempData["alertType"] = "alert-success";

                return RedirectToAction("Index");
            }
            else
            {
                //something wrong
                return View(article);
            }
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            articleRepository.DeleteArticle(id);

            TempData["message"] = "Статья удалена";
            TempData["alertType"] = "alert-success";

            return RedirectToAction("Index");
        }
    }
}
