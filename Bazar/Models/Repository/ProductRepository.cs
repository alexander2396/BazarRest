using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Bazar.Models.Entities;

namespace Bazar.Models.Repository
{
    public class ProductRepository
    {
        private EfDbContext context;
        public ProductRepository(EfDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Product> Products => context.Products;
        public IQueryable<ProductImage> ProductImages => context.ProductImages;

        public void SaveProduct(Product product)
        {
            Product dbEntry = context.Products
                    .FirstOrDefault(p => p.ProductId == product.ProductId);

            if (dbEntry == null)
            {
                product.Order = context.Products
                    .Select(p => p.Order)
                    .Max() + 1;

                context.Products.Add(product);
            }
            else
            {
                dbEntry.Order = product.Order;
                dbEntry.Title = product.Title;
                dbEntry.LinkUrl = product.LinkUrl;
                dbEntry.Description = product.Description;
                dbEntry.ImageFileName = product.ImageFileName;
                dbEntry.ImageSmallFileName = product.ImageSmallFileName;
                dbEntry.ImageTitle = product.ImageTitle;
                dbEntry.ImageAlt = product.ImageAlt;
                dbEntry.IconFileName = product.IconFileName;
                dbEntry.IconWhiteFileName = product.IconWhiteFileName;
                dbEntry.IconTitle = product.IconTitle;
                dbEntry.IconAlt = product.IconAlt;
            }

            context.SaveChanges();
        }

        public void SaveProductImage(ProductImage productImage)
        {
            ProductImage dbEntry = context.ProductImages
                    .FirstOrDefault(pi => pi.ProductImageId == productImage.ProductImageId);

            if(productImage.ProductId > 0)
            {
                productImage.Product = null;
            }

            if (dbEntry == null)
            {
                context.ProductImages.Add(productImage);
            }
            else
            {
                dbEntry.ProductId = productImage.ProductId;
                dbEntry.FileName = productImage.FileName;
                dbEntry.Link = productImage.Link;
                dbEntry.Title = productImage.Title;
                dbEntry.Alt = productImage.Alt;
            }

            context.SaveChanges();
        }

        public Product DeleteProduct(int productId)
        {
            Product dbEntry = context.Products
                .FirstOrDefault(p => p.ProductId == productId);

            if (dbEntry == null)
                throw new Exception($"Delete was not complete, because didn't find Product with Product = {productId}");

            if (dbEntry.ImageFileName != null)
            {
                File.Delete("wwwroot/DynamicImages/Product/" + dbEntry.ImageFileName);
            }

            if (dbEntry.ImageSmallFileName != null)
            {
                File.Delete("wwwroot/DynamicImages/Product/" + dbEntry.ImageSmallFileName);
            }

            if (dbEntry.IconFileName != null)
            {
                File.Delete("wwwroot/DynamicImages/Product/" + dbEntry.IconFileName);
            }

            if (dbEntry.IconWhiteFileName != null)
            {
                File.Delete("wwwroot/DynamicImages/Product/" + dbEntry.IconWhiteFileName);
            }

            context.Products.Remove(dbEntry);
            context.SaveChanges();

            return dbEntry;
        }

        public ProductImage DeleteProductImage(int productImageId)
        {
            ProductImage dbEntry = context.ProductImages
                .FirstOrDefault(pi => pi.ProductImageId == productImageId);

            if (dbEntry == null)
                throw new Exception($"Delete was not complete, because didn't find ProductImage with ProductImage = {productImageId}");

            if (dbEntry.FileName != null)
            {
                File.Delete("wwwroot/DynamicImages/Product/" + dbEntry.FileName);
            }

            context.ProductImages.Remove(dbEntry);
            context.SaveChanges();

            return dbEntry;
        }
    }
}
