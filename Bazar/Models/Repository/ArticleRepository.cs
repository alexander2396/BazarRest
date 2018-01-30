using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Bazar.Models.Entities;

namespace Bazar.Models.Repository
{
    public class ArticleRepository
    {
        private EfDbContext context;
        public ArticleRepository(EfDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Article> Articles => context.Articles;
        public IQueryable<BlogCategory> BlogCategories => context.BlogCategories;

        public void SaveArticle(Article article)
        {
            Article dbEntry = context.Articles
                    .FirstOrDefault(a => a.ArticleId == article.ArticleId);

            if (dbEntry == null)
            {
                context.Articles.Add(article);
            }
            else
            {
                dbEntry.Title = article.Title;
                dbEntry.BlogCategoryId = article.BlogCategoryId;
                dbEntry.LinkUrl = article.LinkUrl;
                dbEntry.LinkTitle = article.LinkTitle;
                dbEntry.Content = article.Content;
                dbEntry.ImageFileName = article.ImageFileName;
                dbEntry.ImageSmallFileName = article.ImageSmallFileName;
                dbEntry.ImageTitle = article.ImageTitle;
                dbEntry.ImageAlt = article.ImageAlt;             
            }

            context.SaveChanges();
        }

        public void SaveBlogCategory(BlogCategory blogCategory)
        {
            BlogCategory dbEntry = context.BlogCategories
                    .FirstOrDefault(bc => bc.BlogCategoryId == blogCategory.BlogCategoryId);

            if (dbEntry == null)
            {
                context.BlogCategories.Add(blogCategory);
            }
            else
            {
                dbEntry.LinkUrl = blogCategory.LinkUrl;
                dbEntry.LinkTitle = blogCategory.LinkTitle;
                dbEntry.Color = blogCategory.Color;
            }

            context.SaveChanges();
        }

        public Article DeleteArticle(int articleId)
        {
            Article dbEntry = context.Articles
                .FirstOrDefault(a => a.ArticleId == articleId);

            if (dbEntry == null)
                throw new Exception($"Delete was not complete, because didn't find Article with ArticleId = {articleId}");

            if (dbEntry.ImageFileName != null)
            {
                File.Delete("wwwroot/DynamicImages/Article/" + dbEntry.ImageFileName);
            }

            if (dbEntry.ImageSmallFileName != null)
            {
                File.Delete("wwwroot/DynamicImages/Article/" + dbEntry.ImageSmallFileName);
            }

            context.Articles.Remove(dbEntry);
            context.SaveChanges();

            return dbEntry;
        }

        public BlogCategory DeleteBlogCategory(int blogCategoryId)
        {
            BlogCategory dbEntry = context.BlogCategories
                .FirstOrDefault(bc => bc.BlogCategoryId == blogCategoryId);

            if (dbEntry == null)
                throw new Exception($"Delete was not complete, because didn't find BlogCategory with BlogCategoryId = {blogCategoryId}");

            context.BlogCategories.Remove(dbEntry);
            context.SaveChanges();

            return dbEntry;
        }
    }
}
