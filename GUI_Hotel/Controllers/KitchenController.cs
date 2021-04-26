using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GUI_Hotel.Data;
using GUI_Hotel.Models.DataModels;
using Microsoft.AspNetCore.Authorization;
using GUI_Hotel.Models;
using GUI_Hotel.ViewModel;
using System.ComponentModel.DataAnnotations;

namespace GUI_Hotel.Controllers
{
    public class KitchenController : Controller
    {
        private readonly ApplicationDbContext _context;

        public KitchenController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Kitchen
        [Authorize(Policy = "IsKitchen")]
        public IActionResult Index()
        {

            return View();
        }

        [Authorize(Policy = "IsKitchen")]
        public IActionResult Find(DateTime Date)
        {

            var filterDate = Date.Date.AddDays(1);
            var res = _context.Bookings.Where(b => b.BookingDate >= Date.Date && b.BookingDate < filterDate).ToList();
            var vm = new KitchenViewModel()
            {
                Bookings = res,
                Date = Date.Date,
            };
            return View(vm);

        }
    }
}
