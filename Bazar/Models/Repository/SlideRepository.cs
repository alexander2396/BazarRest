using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Bazar.Models.Entities;

namespace Bazar.Models.Repository
{
    public class SlideRepository
    {
        private EfDbContext context;
        public SlideRepository(EfDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Slide> Slides => context.Slides;

        public void SaveSlide(Slide slide)
        {
            Slide dbEntry = context.Slides
                    .FirstOrDefault(s => s.SlideId == slide.SlideId);

            if (dbEntry == null)
            {
                slide.Order = context.Slides
                    .Select(s => s.Order)
                    .Max() + 1;              

                context.Slides.Add(slide);
            }
            else
            {
                dbEntry.Order = slide.Order;
                dbEntry.ImgFileName = slide.ImgFileName;
                dbEntry.Url = slide.Url;
                dbEntry.Title = slide.Title;
                dbEntry.Alt = slide.Alt;
            }

            context.SaveChanges();
        }

        public Slide DeleteSlide(int slideId)
        {
            Slide dbEntry = context.Slides
                .FirstOrDefault(s => s.SlideId == slideId);

            if (dbEntry == null)
                throw new Exception($"Delete was not complete, because didn't find Slide with SlideId = {slideId}");

            context.Slides.Remove(dbEntry);
            context.SaveChanges();

            return dbEntry;
        }
    }
}
