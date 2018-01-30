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
    public class ProductController : Controller
    {
        private ProductRepository productRepository;

        public ProductController(ProductRepository productRepo)
        {
            productRepository = productRepo;
        }

        public IActionResult Index(string linkUrl)
        {
            Product product = productRepository
                .Products
                .Include(p => p.ProductImages)
                .FirstOrDefault(p => p.LinkUrl == linkUrl);

            return View(product);
        }
    }
}
