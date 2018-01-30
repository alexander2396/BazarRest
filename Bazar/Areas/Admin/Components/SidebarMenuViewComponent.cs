using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

using Bazar.Areas.Admin.Infrastructure;

namespace Bazar.Areas.Admin.Components
{
    public class SidebarMenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(int? id)
        {
            string ControllerName = RouteData.Values.FirstOrDefault(x => x.Key == "controller").Value?.ToString().ToLower();
            string ActionName = RouteData.Values.FirstOrDefault(x => x.Key == "action").Value.ToString().ToLower();

            List<SidebarMenuItem> menu;

            menu = new List<SidebarMenuItem>
             {
                new SidebarMenuItem
                {
                    Title = "Слайдшоу",
                    Controller = "Slide",
                    Action = "index",
                    FaIcon = "fa-window-restore",
                },
                new SidebarMenuItem
                {
                    Title = "Товары",
                    Controller = "Product",
                    FaIcon = "fa-coffee",
                },
                new SidebarMenuItem
                {
                    Title = "Статьи",
                    Controller = "blog",
                    FaIcon = "fa-newspaper-o",
                },
                 new SidebarMenuItem
                {
                    Title = "Категории",
                    Controller = "blogcategory",
                    FaIcon = "fa-newspaper-o",
                },
                 new SidebarMenuItem
                {
                    Title = "Обратная связь",
                    Controller = "order",
                    Action = "index",
                    FaIcon = "fa-truck",
                },
                  new SidebarMenuItem
                {
                    Title = "Ресторан",
                    Controller = "restoran",
                    Action = "edit",
                    FaIcon = "fa-cutlery",
                }
            };

            foreach (SidebarMenuItem item in menu)
            {
                if (item.Controller == ControllerName)
                {
                    item.Selected = true;
                }
                if (item.Items != null)
                {
                    foreach (SidebarMenuItem i in item.Items)
                    {
                        if (i.Action == ActionName && i.Controller == ControllerName)
                        {
                            i.Selected = true;
                            item.Selected = true;
                        }
                    }
                }
            }

            return View(menu);
        }
    }
}
