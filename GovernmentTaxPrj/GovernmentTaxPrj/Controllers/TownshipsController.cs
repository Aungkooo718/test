using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GovernmentTaxPrj.Data;
using GovernmentTaxPrj.Models;

namespace GovernmentTaxPrj.Controllers
{
    public class TownshipsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TownshipsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Townships
        public async Task<IActionResult> Index()
        {
            return View(await _context.Townships.Include(t=>t.Region).ToListAsync());
        }

        // GET: Townships/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var township = await _context.Townships
                .FirstOrDefaultAsync(m => m.Id == id);
            if (township == null)
            {
                return NotFound();
            }

            return View(township);
        }

        // GET: Townships/Create
        public IActionResult Create()
        {
            ViewData["RegionId"] = new SelectList(_context.Regions, "Id", "Name");
            return View();
        }

        // POST: Townships/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,RegionId")] Township township)
        {
            if (ModelState.IsValid)
            {
                _context.Add(township);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(township);
        }

        // GET: Townships/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var township = await _context.Townships.FindAsync(id);
            if (township == null)
            {
                return NotFound();
            }
            return View(township);
        }

        // POST: Townships/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,RegionId")] Township township)
        {
            if (id != township.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(township);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TownshipExists(township.Id))
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
            return View(township);
        }

        // GET: Townships/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var township = await _context.Townships
                .FirstOrDefaultAsync(m => m.Id == id);
            if (township == null)
            {
                return NotFound();
            }

            return View(township);
        }

        // POST: Townships/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var township = await _context.Townships.FindAsync(id);
            _context.Townships.Remove(township);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TownshipExists(long id)
        {
            return _context.Townships.Any(e => e.Id == id);
        }
    }
}
