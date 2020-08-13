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
    public class TaxPayersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TaxPayersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TaxPayers
        public async Task<IActionResult> Index(long? RegionId,long? TownshipId,string SearchString)
        {

            ViewData["RegionId"] = new SelectList(_context.Regions, "Id", "Name");
            ViewData["TownshipId"] = new SelectList(_context.Townships, "Id", "Name");
            var township = from t in _context.Townships.Include(t => t.RegionId) select t;

            var applicationDbContext = _context.TaxPayers.Include(t => t.Township);
            
            var taxpayer = from s in applicationDbContext select s;
            
            if (RegionId!=null && TownshipId != null)
            {
                
            }
          
            if (TownshipId != null)
            {
                taxpayer = taxpayer.Where(t => t.TownshipId==TownshipId);
            }
            
            
                if (!string.IsNullOrEmpty(SearchString))
            {
                taxpayer = taxpayer.Where(t => t.Name.Contains(SearchString) || t.Township.Name.Contains(SearchString));
            }
            return View(await taxpayer.ToListAsync());
        }
        //public ActionResult GetTownshipByRegion(int rid)
        //{
        //    //  var township = _context.Regions.Include(r=>r.Townships).Where(r=>r.)
        //    List<Township> townships = new List<Township>();
        //    townships = (from township in _context.Townships where township.RegionId == rid select township).ToList();

        //    return new JsonResult(townships);
        //}
        public ActionResult GetTownshipByRegion(int rid)
        {
            var townlist = _context.Townships.Include(t => t.Region).Where(t => t.RegionId == rid).Select(t => new
            {
                // id = b.CustomerId,
                id=t.Id,
                name=t.Name
            }).ToList();
            return new JsonResult(townlist);
        }
        

        // GET: TaxPayers/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taxPayer = await _context.TaxPayers
                .Include(t => t.Township)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taxPayer == null)
            {
                return NotFound();
            }

            return View(taxPayer);
        }

        // GET: TaxPayers/Create
        public IActionResult Create()
        {
            ViewData["TownshipId"] = new SelectList(_context.Townships, "Id", "Name");
            ViewData["RegionId"] = new SelectList(_context.Townships.Include(t=>t.RegionId), "Id", "Name");
            return View();
        }

        // POST: TaxPayers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,TinNumber,Address,Nrc,TaxType,TownshipId")] TaxPayer taxPayer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taxPayer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TownshipId"] = new SelectList(_context.Townships, "Id", "Id", taxPayer.TownshipId);
            return View(taxPayer);
        }

        // GET: TaxPayers/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taxPayer = await _context.TaxPayers.FindAsync(id);
            if (taxPayer == null)
            {
                return NotFound();
            }
            ViewData["TownshipId"] = new SelectList(_context.Townships, "Id", "Id", taxPayer.TownshipId);
            return View(taxPayer);
        }

        // POST: TaxPayers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Name,TinNumber,Address,Nrc,TaxType,TownshipId")] TaxPayer taxPayer)
        {
            if (id != taxPayer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taxPayer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaxPayerExists(taxPayer.Id))
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
            ViewData["TownshipId"] = new SelectList(_context.Townships, "Id", "Id", taxPayer.TownshipId);
            return View(taxPayer);
        }

        // GET: TaxPayers/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var taxPayer = await _context.TaxPayers
                .Include(t => t.Township)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (taxPayer == null)
            {
                return NotFound();
            }

            return View(taxPayer);
        }

        // POST: TaxPayers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var taxPayer = await _context.TaxPayers.FindAsync(id);
            _context.TaxPayers.Remove(taxPayer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaxPayerExists(long id)
        {
            return _context.TaxPayers.Any(e => e.Id == id);
        }
    }
}
