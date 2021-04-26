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
        [Authorize(Policy = "IsKitchen")]
        public async Task<IActionResult> Index()
        {

        // GET: Kitchen/Details/5
        [Authorize(Policy = "IsKitchen")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .FirstOrDefaultAsync(m => m.BookingId == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // GET: Kitchen/Create
        [Authorize(Policy = "IsKitchen")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Kitchen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "IsKitchen")]
        public async Task<IActionResult> Create([Bind("BookingId,BookingDate,RoomNumber,AdultsBooked,AdultsCheckedIn,ChildrenBooked,ChildrenCheckedIn")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(booking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(booking);
        }

        // GET: Kitchen/Edit/5
        [Authorize(Policy = "IsKitchen")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var filterDate = Date.Date.AddDays(1);
            var res = _context.Bookings.Where(b => b.BookingDate >= Date.Date && b.BookingDate < filterDate).ToList();
            var vm = new KitchenViewModel()
            {
                Bookings = res,
                Date = Date.Date,
            };
            return View(vm);

        // POST: Kitchen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "IsKitchen")]
        public async Task<IActionResult> Edit(int id, [Bind("BookingId,BookingDate,RoomNumber,AdultsBooked,AdultsCheckedIn,ChildrenBooked,ChildrenCheckedIn")] Booking booking)
        {
            if (id != booking.BookingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.BookingId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(booking);
        }

        // GET: Kitchen/Delete/5
        [Authorize(Policy = "IsKitchen")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings
                .FirstOrDefaultAsync(m => m.BookingId == id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Kitchen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "IsKitchen")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.BookingId == id);
        }
    }
}
