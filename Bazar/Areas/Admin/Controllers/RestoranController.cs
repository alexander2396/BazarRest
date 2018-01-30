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
    public class RestoranController : Controller
    {
        private RestoranRepository restoranRepository;

        public RestoranController(RestoranRepository restoranRepo)
        {
            restoranRepository = restoranRepo;
        }

        public IActionResult Edit()
        {
            Restoran restoran = restoranRepository.Restoran;
            return View(restoran);
        }

        [HttpPost]
        public IActionResult Edit(Restoran restoran)
        {
            
            if (ModelState.IsValid)
            {
                restoranRepository.SaveRestoran(restoran);

                TempData["message"] = "Данные сохранены";
                TempData["alertType"] = "alert-success";
            }

            return View(restoran);
        }
    }
}
