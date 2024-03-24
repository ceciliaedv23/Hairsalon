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
    public class StylistController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StylistController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Publik sida där frisörer listas
        // Stylist/ClientIndex
        [Route("/frisorer")]
        public async Task<IActionResult> ClientIndex()
        {
            return View(await _context.StylistModel.ToListAsync());
        }

        // Admins startsida för hantering av frisörer
        // Stylist/Index
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.StylistModel.ToListAsync());
        }
    
        // Admins sida för att se info om specifik frisör
        // Stylist/Details(id)
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stylistModel = await _context.StylistModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stylistModel == null)
            {
                return NotFound();
            }

            return View(stylistModel);
        }

        // Admins sida för att lägga till frisör
        // Stylist/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // Funktionalitet för att lägga till frisör
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Firstname,Lastname,Biography")] StylistModel stylistModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stylistModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stylistModel);
        }

        // Admins sida för att ändra info om specifik frisör
        // Stylist/Edit(id)
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stylistModel = await _context.StylistModel.FindAsync(id);
            if (stylistModel == null)
            {
                return NotFound();
            }
            return View(stylistModel);
        }

        // Funktionalitet för att ändra info om specifik frisör
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Firstname,Lastname,Biography")] StylistModel stylistModel)
        {
            if (id != stylistModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stylistModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StylistModelExists(stylistModel.Id))
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
            return View(stylistModel);
        }

        // Admins sida för att radera specifik frisör
        // Stylist/Delete(id)
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stylistModel = await _context.StylistModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stylistModel == null)
            {
                return NotFound();
            }

            return View(stylistModel);
        }

        // Funktionalitet för att radera specifik frisör
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stylistModel = await _context.StylistModel.FindAsync(id);
            if (stylistModel != null)
            {
                _context.StylistModel.Remove(stylistModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StylistModelExists(int id)
        {
            return _context.StylistModel.Any(e => e.Id == id);
        }
    }
}
