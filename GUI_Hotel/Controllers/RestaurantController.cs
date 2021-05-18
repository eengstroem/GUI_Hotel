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

namespace GUI_Hotel.Controllers
{
    public class RestaurantController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RestaurantController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Restaurant
        [Authorize(Policy = "IsRestaurant")]
        public async Task<IActionResult> Index()
        {
          
            return View(await _context.Bookings.Where(b=>b.BookingDate == DateTime.Today).ToListAsync());
            //return View(await _context.Bookings.ToListAsync());
            
        }

        // GET: Restaurant/Details/5
        [Authorize(Policy = "IsRestaurant")]
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

        // GET: Restaurant/Create
        [Authorize(Policy = "IsRestaurant")]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Policy = "IsRestaurant")]
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


        // POST: Restaurant/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "IsRestaurant")]
        public async Task<IActionResult> Edit(int id, [Bind("BookingId,BookingDate,RoomNumber,AdultsCheckedIn,ChildrenCheckedIn")] Booking booking)
        {
            if (id != booking.BookingId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var Booking = await _context.Bookings.FindAsync(id);

                    Booking.AdultsCheckedIn = booking.AdultsCheckedIn;
                    Booking.ChildrenCheckedIn = booking.ChildrenCheckedIn;

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

        // GET: Restaurant/Delete/5
        [Authorize(Policy = "IsRestaurant")]
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

        // POST: Restaurant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "IsRestaurant")]
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
