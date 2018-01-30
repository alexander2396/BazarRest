using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using VisionuseTransliteration;
using Microsoft.AspNetCore.Http;

using Bazar.Models.Entities;
using Bazar.Models.Repository;

namespace Bazar.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class ProductController : Controller
    {
        private ProductRepository productRepository;

        public ProductController(ProductRepository productRepo)
        {
            productRepository = productRepo;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> products = productRepository
                .Products
                .Include(p => p.ProductImages)
                .OrderBy(p => p.Order);

            return View(products);
        }

        public IActionResult Create() => View("Edit", new Product());

        public IActionResult Edit(int id)
        {
            if (id == 0) return NotFound();

            Product product = productRepository
                .Products
                .Include(p => p.ProductImages)
                .FirstOrDefault(p => p.ProductId == id);

            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
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
                    product.ImageFileName = Transliteration.Transliterate(product.Title + "-main" + extension);
                    using (var stream = new FileStream("wwwroot/DynamicImages/Product/" + product.ImageFileName, FileMode.Create))
                    {
                        Image.CopyTo(stream);
                    }
                }

                //Save small image
                IFormFile ImageSmall = Request.Form.Files.Where(f => f.Name == "ImageSmall").First();
                if (ImageSmall.FileName != "")
                {
                    string extension;
                    switch (ImageSmall.ContentType)
                    {
                        case "image/jpeg": extension = ".jpg"; break;
                        case "image/png": extension = ".png"; break;
                        default: throw new Exception($"Изображение {ImageSmall.FileName} не было сохранено так как формат {ImageSmall.ContentType} не подерживается.");
                    }
                    product.ImageSmallFileName = Transliteration.Transliterate(product.Title + "-small" + extension);
                    using (var stream = new FileStream("wwwroot/DynamicImages/Product/" + product.ImageSmallFileName, FileMode.Create))
                    {
                        ImageSmall.CopyTo(stream);
                    }
                }

                //Save icon
                IFormFile Icon = Request.Form.Files.Where(f => f.Name == "Icon").First();
                if (Icon.FileName != "")
                {
                    string extension;
                    switch (Icon.ContentType)
                    {
                        case "image/jpeg": extension = ".jpg"; break;
                        case "image/png": extension = ".png"; break;
                        default: throw new Exception($"Иконка {Icon.FileName} не было сохранено так как формат {Icon.ContentType} не подерживается.");
                    }
                    product.IconFileName = Transliteration.Transliterate(product.Title + "-icon" + extension);
                    using (var stream = new FileStream("wwwroot/DynamicImages/Product/" + product.IconFileName, FileMode.Create))
                    {
                        Icon.CopyTo(stream);
                    }
                }

                //Save white icon
                IFormFile IconWhite = Request.Form.Files.Where(f => f.Name == "IconWhite").First();
                if (Icon.FileName != "")
                {
                    string extension;
                    switch (IconWhite.ContentType)
                    {
                        case "image/jpeg": extension = ".jpg"; break;
                        case "image/png": extension = ".png"; break;
                        default: throw new Exception($"Иконка {IconWhite.FileName} не было сохранено так как формат {IconWhite.ContentType} не подерживается.");
                    }
                    product.IconWhiteFileName = Transliteration.Transliterate(product.Title + "-icon-white" + extension);
                    using (var stream = new FileStream("wwwroot/DynamicImages/Product/" + product.IconWhiteFileName, FileMode.Create))
                    {
                        IconWhite.CopyTo(stream);
                    }
                }

                //Save product images
                if (product.ProductImages != null)
                {
                    for (int i = 0; i < product.ProductImages.Count(); i++)
                    {
                        if (product.ProductImages[i].ProductImageId == 0)
                        {
                            IFormFile imageFile = Request.Form.Files.Where(f => f.Name == "ProductImages[" + i + "].FileName").First();
                            if (imageFile.FileName != "")
                            {
                                string extension;
                                switch (imageFile.ContentType)
                                {
                                    case "image/jpeg": extension = ".jpg"; break;
                                    case "image/png": extension = ".png"; break;
                                    case "video/mp4": extension = ".mp4"; break;
                                    case "video/avi": extension = ".avi"; break;
                                    default: throw new Exception($"Иконка {imageFile.FileName} не было сохранено так как формат {imageFile.ContentType} не подерживается.");
                                }
                                product.ProductImages[i].FileName = Transliteration.Transliterate(product.Title + "-" + Guid.NewGuid().ToString().GetHashCode().ToString("x") + extension);
                                using (var stream = new FileStream("wwwroot/DynamicImages/Product/" + product.ProductImages[i].FileName, FileMode.Create))
                                {
                                    imageFile.CopyTo(stream);
                                }
                            }

                            product.ProductImages[i].ProductId = product.ProductId;
                            product.ProductImages[i].Product = product;
                            productRepository.SaveProductImage(product.ProductImages[i]);
                        }

                        if (product.ProductImages[i].IsDeleted == true)
                        {
                            productRepository.DeleteProductImage(product.ProductImages[i].ProductImageId);
                        }
                        else if (product.ProductImages[i].ProductImageId > 0)
                        {
                            ProductImage dbEntry = productRepository.ProductImages
                                .FirstOrDefault(pi => pi.ProductImageId == product.ProductImages[i].ProductImageId);

                            dbEntry.Link = product.ProductImages[i].Link;
                            dbEntry.Title = product.ProductImages[i].Title;
                            dbEntry.Alt = product.ProductImages[i].Alt;
                            productRepository.SaveProductImage(dbEntry);
                        }
                    }
                }

                if(product.LinkUrl == null)
                {
                    product.LinkUrl = Transliteration.Transliterate(product.Title);
                }

                productRepository.SaveProduct(product);

                TempData["message"] = "Товар сохранен";
                TempData["alertType"] = "alert-success";

                return RedirectToAction("Index");
            }
            else
            {
                //something wrong
                return View(product);
            }
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            productRepository.DeleteProduct(id);

            TempData["message"] = "Товар удален";
            TempData["alertType"] = "alert-success";

            return RedirectToAction("Index");
        }

        /****************************************************/
        /*****************   AJAX methods    ****************/
        /****************************************************/
        [Route("/api/product/saveorder")]
        [HttpPost]
        public void SaveOrder([FromBody]List<Product> Products)
        {
            foreach (Product product in Products)
            {
                Product dbEntry = productRepository.Products
                    .FirstOrDefault(p => p.ProductId == product.ProductId);

                if (dbEntry != null)
                {
                    dbEntry.Order = product.Order;
                    productRepository.SaveProduct(dbEntry);
                }
            }
        }
    }
}
