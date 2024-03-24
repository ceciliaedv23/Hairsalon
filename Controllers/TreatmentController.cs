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
    public class TreatmentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TreatmentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Publik sida där behandlingar listas
        // Treatment/ClientIndex
        [Route("/behandlingar")]
        public async Task<IActionResult> ClientIndex()
        {
            return View(await _context.TreatmentModel.ToListAsync());
        }

        // Admins startsida för hantering av behandlingar
        // Treatment/Index
        [Authorize]
        public async Task<IActionResult> Index()
        {
            return View(await _context.TreatmentModel.ToListAsync());
        }

        // Admins sida för att se info om specifik behandling
        // Treatment/Details(id)
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var treatmentModel = await _context.TreatmentModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (treatmentModel == null)
            {
                return NotFound();
            }

            return View(treatmentModel);
        }

        // Admins sida för att lägga till behandling
        // Treatment/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // Funktionalitet för att lägga till behandling
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Description")] TreatmentModel treatmentModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(treatmentModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(treatmentModel);
        }

        // Admins sida för att ändra info om specifik behandling
        // Treatment/Edit(id)
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var treatmentModel = await _context.TreatmentModel.FindAsync(id);
            if (treatmentModel == null)
            {
                return NotFound();
            }
            return View(treatmentModel);
        }

        // Funktionalitet för att ändra info om specifik behandling
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Description")] TreatmentModel treatmentModel)
        {
            if (id != treatmentModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(treatmentModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TreatmentModelExists(treatmentModel.Id))
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
            return View(treatmentModel);
        }

        // Admins sida för att radera specifik behandling
        // Treatment/Delete(id)
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var treatmentModel = await _context.TreatmentModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (treatmentModel == null)
            {
                return NotFound();
            }

            return View(treatmentModel);
        }

        // Funktionalitet för att radera specifik behandling
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var treatmentModel = await _context.TreatmentModel.FindAsync(id);
            if (treatmentModel != null)
            {
                _context.TreatmentModel.Remove(treatmentModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TreatmentModelExists(int id)
        {
            return _context.TreatmentModel.Any(e => e.Id == id);
        }
    }
}
