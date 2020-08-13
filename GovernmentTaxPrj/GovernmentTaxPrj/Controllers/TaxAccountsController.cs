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
    public class TaxAccountsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TaxAccountsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TaxAccounts
        public async Task<IActionResult> Index()
        {
            return View(await _context.TaxAccounts.ToListAsync());
        }

        // GET: TaxAccounts/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taxAccount = await _context.TaxAccounts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taxAccount == null)
            {
                return NotFound();
            }

            return View(taxAccount);
        }

        // GET: TaxAccounts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TaxAccounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Amount,OpenDate")] TaxAccount taxAccount)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taxAccount);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(taxAccount);
        }

        // GET: TaxAccounts/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taxAccount = await _context.TaxAccounts.FindAsync(id);
            if (taxAccount == null)
            {
                return NotFound();
            }
            return View(taxAccount);
        }

        // POST: TaxAccounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Amount,OpenDate")] TaxAccount taxAccount)
        {
            if (id != taxAccount.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taxAccount);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaxAccountExists(taxAccount.Id))
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
            return View(taxAccount);
        }

        // GET: TaxAccounts/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taxAccount = await _context.TaxAccounts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taxAccount == null)
            {
                return NotFound();
            }

            return View(taxAccount);
        }

        // POST: TaxAccounts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var taxAccount = await _context.TaxAccounts.FindAsync(id);
            _context.TaxAccounts.Remove(taxAccount);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaxAccountExists(long id)
        {
            return _context.TaxAccounts.Any(e => e.Id == id);
        }
    }
}
