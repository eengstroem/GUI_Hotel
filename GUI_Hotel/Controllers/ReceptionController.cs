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
using Microsoft.Extensions.Logging;

namespace GUI_Hotel.Controllers
{
    public class ReceptionController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public ReceptionController(ApplicationDbContext context, ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Reception
        [Authorize(Policy = "IsReception")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "IsReception")]
        public async Task<IActionResult> ReservationsForDate([Bind("BookingDate")] Booking booking)
        {
            if(booking.BookingDate == null)
            {
                _logger.LogWarning("null date");
                return NotFound();
            }
            var bookings = await _context.Bookings.Where(b => b.BookingDate == booking.BookingDate).OrderBy(b => b.RoomNumber).ToListAsync();
            
            return View(bookings);
        }

        [Authorize(Policy = "IsReception")]
        public async Task<IActionResult> Reservations([Bind("BookingDate")] Booking booking)
        {
            if (booking.BookingDate == null)
            {
                _logger.LogWarning("null date");
                return NotFound();
            }
            return View(await _context.Bookings.Where(b => b.BookingDate == booking.BookingDate).OrderBy(b=>b.RoomNumber).ToListAsync());
        }

        // GET: Reception/Details/5
        [Authorize(Policy = "IsReception")]
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

        // GET: Reception/Create
        [Authorize(Policy = "IsReception")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Reception/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "IsReception")]
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

        // GET: Reception/Edit/5
        [Authorize(Policy = "IsReception")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }
            return View(booking);
        }

        // POST: Reception/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "IsReception")]
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

        // GET: Reception/Delete/5
        [Authorize(Policy = "IsReception")]
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

        // POST: Reception/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "IsReception")]
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
