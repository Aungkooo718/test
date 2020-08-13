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
    public class TaxTransactionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TaxTransactionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TaxTransactions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TaxTransactions.Include(t => t.IncomeType);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: TaxTransactions/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taxTransaction = await _context.TaxTransactions
                .Include(t => t.IncomeType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taxTransaction == null)
            {
                return NotFound();
            }

            return View(taxTransaction);
        }

        // GET: TaxTransactions/Create
        public IActionResult Create()
        {
            ViewData["IncomeTypeId"] = new SelectList(_context.IncomeTypes, "Id", "Name");
            return View();
        }

        // POST: TaxTransactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IncomeTypeId,TotalIncome,TaxAmount")] TaxTransaction taxTransaction)
        {
            if (ModelState.IsValid)
            {
                             
                _context.Add(taxTransaction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IncomeTypeId"] = new SelectList(_context.IncomeTypes, "Id", "Name", taxTransaction.IncomeTypeId);
            return View(taxTransaction);
        }

        // GET: TaxTransactions/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taxTransaction = await _context.TaxTransactions.FindAsync(id);
            if (taxTransaction == null)
            {
                return NotFound();
            }
            ViewData["IncomeTypeId"] = new SelectList(_context.IncomeTypes, "Id", "Name", taxTransaction.IncomeTypeId);
            return View(taxTransaction);
        }

        // POST: TaxTransactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,IncomeTypeId,TotalIncome,TaxAmount")] TaxTransaction taxTransaction)
        {
            if (id != taxTransaction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taxTransaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaxTransactionExists(taxTransaction.Id))
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
            ViewData["IncomeTypeId"] = new SelectList(_context.IncomeTypes, "Id", "Name", taxTransaction.IncomeTypeId);
            return View(taxTransaction);
        }

        // GET: TaxTransactions/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taxTransaction = await _context.TaxTransactions
                .Include(t => t.IncomeType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taxTransaction == null)
            {
                return NotFound();
            }

            return View(taxTransaction);
        }

        // POST: TaxTransactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var taxTransaction = await _context.TaxTransactions.FindAsync(id);
            _context.TaxTransactions.Remove(taxTransaction);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaxTransactionExists(long id)
        {
            return _context.TaxTransactions.Any(e => e.Id == id);
        }
    }
}
