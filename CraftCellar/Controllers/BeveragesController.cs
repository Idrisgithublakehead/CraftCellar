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
    public class BeveragesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BeveragesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // this is for the get  Beverages action
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Beverages.Include(b => b.Brewery);
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beverage = await _context.Beverages
                .Include(b => b.Brewery)
                .FirstOrDefaultAsync(m => m.BeverageId == id);
            if (beverage == null)
            {
                return NotFound();
            }

            return View(beverage);
        }


        // now to actually authorize and allow for each edit we need to add [authorize] infront of each get action
        // anither get action but for create . Beverages/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["BreweryId"] = new SelectList(_context.Breweries, "BreweryId", "Name");
            return View();
        }

        // POST: Beverages/Create
        // 
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("BeverageId,Name,Type,Price,AlcoholContent,BreweryId")] Beverage beverage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(beverage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BreweryId"] = new SelectList(_context.Breweries, "BreweryId", "Name", beverage.BreweryId);
            return View(beverage);
        }

        //  here is the edit /Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beverage = await _context.Beverages.FindAsync(id);
            if (beverage == null)
            {
                return NotFound();
            }
            ViewData["BreweryId"] = new SelectList(_context.Breweries, "BreweryId", "Name", beverage.BreweryId);
            return View(beverage);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("BeverageId,Name,Type,Price,AlcoholContent,BreweryId")] Beverage beverage)
        {
            if (id != beverage.BeverageId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(beverage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BeverageExists(beverage.BeverageId))
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
            ViewData["BreweryId"] = new SelectList(_context.Breweries, "BreweryId", "Name", beverage.BreweryId);
            return View(beverage);
        }

        // GET: Beverages/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var beverage = await _context.Beverages
                .Include(b => b.Brewery)
                .FirstOrDefaultAsync(m => m.BeverageId == id);
            if (beverage == null)
            {
                return NotFound();
            }

            return View(beverage);
        }

        
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var beverage = await _context.Beverages.FindAsync(id);
            if (beverage != null)
            {
                _context.Beverages.Remove(beverage);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BeverageExists(int id)
        {
            return _context.Beverages.Any(e => e.BeverageId == id);
        }
    }
}