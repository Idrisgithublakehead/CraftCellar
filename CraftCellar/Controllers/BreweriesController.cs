using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CraftCellar.Data;
using CraftCellar.Models;
using Microsoft.AspNetCore.Authorization;

namespace CraftCellar.Controllers
{
    public class BreweriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BreweriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public async Task<IActionResult> Index()
        {
            return View(await _context.Breweries.ToListAsync());
        }

        // GET: Breweries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brewery = await _context.Breweries
                .FirstOrDefaultAsync(m => m.BreweryId == id);
            if (brewery == null)
            {
                return NotFound();
            }

            return View(brewery);
        }

        // GET: Breweries/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
        // authorize a get action for it to work
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("BreweryId,Name,YearFounded,Location")] Brewery brewery)
        {
            if (ModelState.IsValid)
            {
                _context.Add(brewery);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(brewery);
        }

        // GET: Breweries/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brewery = await _context.Breweries.FindAsync(id);
            if (brewery == null)
            {
                return NotFound();
            }
            return View(brewery);
        }

        // POST: Breweries/Edit/5, now again we need to allow for authorizeation so we add a [authorize]
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("BreweryId,Name,YearFounded,Location")] Brewery brewery)
        {
            if (id != brewery.BreweryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(brewery);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BreweryExists(brewery.BreweryId))
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
            return View(brewery);
        }

        // another get action for delete Breweries/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var brewery = await _context.Breweries
                .FirstOrDefaultAsync(m => m.BreweryId == id);
            if (brewery == null)
            {
                return NotFound();
            }

            return View(brewery);
        }

        // POST: Breweries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var brewery = await _context.Breweries.FindAsync(id);
            if (brewery != null)
            {
                _context.Breweries.Remove(brewery);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BreweryExists(int id)
        {
            return _context.Breweries.Any(e => e.BreweryId == id);
        }
    }
}