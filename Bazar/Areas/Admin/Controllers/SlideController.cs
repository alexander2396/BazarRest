using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Bazar.Models.Entities;
using Bazar.Models.Repository;

namespace Bazar.Areas.Admin.Controllers
{
    [Authorize]
    [Area("Admin")]
    public class SlideController : Controller
    {
        private SlideRepository slideRepository;

        public SlideController(SlideRepository slideRepo)
        {
            slideRepository = slideRepo;
        }

        public IActionResult Index()
        {
            IEnumerable<Slide> slides = slideRepository
                .Slides
                .OrderBy(s => s.Order);

            return View(slides);
        }

        public IActionResult Create() => View("Edit", new Slide());

        public IActionResult Edit(int id)
        {
            if (id == 0) return NotFound();

            Slide slide = slideRepository.Slides.FirstOrDefault(s => s.SlideId == id);

            return View(slide);
        }

        [HttpPost]
        public IActionResult Edit(Slide slide)
        {
            if (ModelState.IsValid)
            {
                var Image = Request.Form.Files.Where(f => f.Name == "Image").First();
                if (Image.FileName != "")
                {
                    slide.ImgFileName = Image.FileName;
                    using (var stream = new FileStream("wwwroot/DynamicImages/Slides/" + slide.ImgFileName, FileMode.Create))
                    {
                        Image.CopyTo(stream);
                    }
                }

                slideRepository.SaveSlide(slide);

                TempData["message"] = "Слайд сохранен";

                return RedirectToAction("Index");
            }
            else
            {
                //something wrong
                return View(slide);
            }
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            slideRepository.DeleteSlide(id);

            TempData["message"] = "Слайд удален";

            return RedirectToAction("Index");
        }

        /****************************************************/
        /*****************   AJAX methods    ****************/
        /****************************************************/
        [Route("/api/slide/saveorder")]
        [HttpPost]
        public void SaveOrder([FromBody]List<Slide> slides)
        {
            foreach (Slide slide in slides)
            {
                Slide dbEntry = slideRepository.Slides
                    .FirstOrDefault(s => s.SlideId == slide.SlideId);

                if (dbEntry != null)
                {
                    dbEntry.Order = slide.Order;
                    slideRepository.SaveSlide(dbEntry);
                }
            }
        }
    }
}
