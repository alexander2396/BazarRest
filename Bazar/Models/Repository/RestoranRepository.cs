using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Bazar.Models.Entities;

namespace Bazar.Models.Repository
{
    public class RestoranRepository
    {
        private EfDbContext context;
        public RestoranRepository(EfDbContext ctx)
        {
            context = ctx;
        }

        public Restoran Restoran => context.Restoran.FirstOrDefault();   

        public void SaveRestoran(Restoran restoran)
        {
            if(Restoran == null)
            {
                context.Restoran.Add(restoran);
            }
            else
            {
                Restoran.Title = restoran.Title;
                Restoran.ContentTitle = restoran.ContentTitle;
                Restoran.Content = restoran.Content;
                Restoran.BtnTitle = restoran.BtnTitle;
                Restoran.Url = restoran.Url;
            }

            context.SaveChanges();
        }
    }
}
