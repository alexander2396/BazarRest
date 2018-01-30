using System;
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
    public class OrderController : Controller
    {
        private OrderRepository orderRepository;

        public OrderController(OrderRepository orderRepo)
        {
            orderRepository = orderRepo;
        }

        public IActionResult Index()
        {
            IEnumerable<Order> orders = orderRepository.Orders;

            return View(orders);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            orderRepository.DeleteOrder(id);

            TempData["message"] = "Заказ удален";
            TempData["alertType"] = "alert-success";

            return RedirectToAction("Index");
        }
    }
}
