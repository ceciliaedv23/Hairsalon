// Kod tillhörande lösningen till projektuppgiften i kurs DT191G, skapat av Cecilia Edvardsson

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Hairsalon.Data;
using Hairsalon.Models;
using Microsoft.AspNetCore.Authorization;

namespace Hairsalon.Controllers
{
    // Controller för bokningar
    public class BookingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookingController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        // Admins startsida för hantering av bokningar
        // Booking/Index
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.BookingModel.Include(b => b.TreatmentModel);
            return View(await applicationDbContext.ToListAsync());
        }

        // Admins sida för att se info om specifika bokningar
        // Booking/Details(id)
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingModel = await _context.BookingModel
                .Include(b => b.TreatmentModel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookingModel == null)
            {
                return NotFound();
            }

            return View(bookingModel);
        }

        // Admins sida för att lägga till bokning
        // Booking/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["TreatmentModelId"] = new SelectList(_context.Set<TreatmentModel>(), "Id", "Name");
            return View();
        }

        // Funktionalitet för att lägga till bokning
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Phonenr,AppointmentDate,AppointmentTime,Stylist,Status,TreatmentModelId")] BookingModel bookingModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookingModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TreatmentModelId"] = new SelectList(_context.Set<TreatmentModel>(), "Id", "Name", bookingModel.TreatmentModelId);
            return View(bookingModel);
        }

        // Publik sida där bokning kan skickas in
        // Booking/ClientCreate
        [Route("/bokatid")]
        public IActionResult ClientCreate()
        {
            ViewData["TreatmentModelId"] = new SelectList(_context.Set<TreatmentModel>(), "Id", "Name");
            return View();
        }

        // Funktionalitet för kund att göra bokning
        [HttpPost]
        [Route("Booking/Post")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClientCreate([Bind("Id,Name,Email,Phonenr,AppointmentDate,AppointmentTime,Stylist,Status,TreatmentModelId")] BookingModel bookingModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bookingModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Thanks));
            }
            ViewData["TreatmentModelId"] = new SelectList(_context.Set<TreatmentModel>(), "Id", "Name", bookingModel.TreatmentModelId);
            return View(bookingModel);
        }

        // Publik sida efter inskickad bokningsförfrågan
        // Booking/Thanks
        [Route("/tack")]
        public IActionResult Thanks()
        {
            return View();
        }

        // Admins sida för att ändra info om specifik bokning
        // Booking/Edit(id)
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingModel = await _context.BookingModel.FindAsync(id);
            if (bookingModel == null)
            {
                return NotFound();
            }
            ViewData["TreatmentModelId"] = new SelectList(_context.Set<TreatmentModel>(), "Id", "Name", bookingModel.TreatmentModelId);
            return View(bookingModel);
        }

        // Funktionalitet för att ändra info om specifik bokning
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Phonenr,AppointmentDate,AppointmentTime,Stylist,Status,TreatmentModelId")] BookingModel bookingModel)
        {
            if (id != bookingModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bookingModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingModelExists(bookingModel.Id))
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
            ViewData["TreatmentModelId"] = new SelectList(_context.Set<TreatmentModel>(), "Id", "Name", bookingModel.TreatmentModelId);
            return View(bookingModel);
        }

        // Admins sida för att radera specifik bokning
        // Booking/Delete(id)
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bookingModel = await _context.BookingModel
                .Include(b => b.TreatmentModel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bookingModel == null)
            {
                return NotFound();
            }

            return View(bookingModel);
        }

        // Funktionalitet för att radera specifik bokning
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bookingModel = await _context.BookingModel.FindAsync(id);
            if (bookingModel != null)
            {
                _context.BookingModel.Remove(bookingModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingModelExists(int id)
        {
            return _context.BookingModel.Any(e => e.Id == id);
        }
    }
}
