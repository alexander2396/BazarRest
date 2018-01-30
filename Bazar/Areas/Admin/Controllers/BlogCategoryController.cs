using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

using Bazar.Models.Entities;
using Bazar.Models.Repository;

namespace Bazar.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class BlogCategoryController : Controller
    {
        private ArticleRepository articleRepository;

        public BlogCategoryController(ArticleRepository articleRepo)
        {
            articleRepository = articleRepo;
        }

        public IActionResult Index()
        {
            IEnumerable<BlogCategory> categories = articleRepository.BlogCategories;

            return View(categories);
        }

        public IActionResult Create() => View("Edit", new BlogCategory());

        public IActionResult Edit(int id)
        {
            if (id == 0) return NotFound();

            BlogCategory blogCategory = articleRepository
                .BlogCategories
                .FirstOrDefault(bc => bc.BlogCategoryId == id);

            return View(blogCategory);
        }

        [HttpPost]
        public IActionResult Edit(BlogCategory blogCategory)
        {
            if (ModelState.IsValid)
            {
                articleRepository.SaveBlogCategory(blogCategory);

                TempData["message"] = "Категория сохранена";
                TempData["alertType"] = "alert-success";

                return RedirectToAction("Index");
            }
            else
            {
                //something wrong
                return View(blogCategory);
            }
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            articleRepository.DeleteBlogCategory(id);

            TempData["message"] = "Категория удалена";
            TempData["alertType"] = "alert-success";

            return RedirectToAction("Index");
        }
    }
}
