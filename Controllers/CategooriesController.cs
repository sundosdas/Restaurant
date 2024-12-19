using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restaurant.Models;

namespace Restaurant.Controllers
{
    public class CategooriesController : Controller
    {
        private readonly ModelContext _context;

        public CategooriesController(ModelContext context)
        {
            _context = context;
        }

        // GET: Categoories
        public async Task<IActionResult> Index()
        {
              return _context.Categoories != null ? 
                          View(await _context.Categoories.ToListAsync()) :
                          Problem("Entity set 'ModelContext.Categoories'  is null.");
        }

        // GET: Categoories/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.Categoories == null)
            {
                return NotFound();
            }

            var categoory = await _context.Categoories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoory == null)
            {
                return NotFound();
            }

            return View(categoory);
        }

        // GET: Categoories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categoories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,CategoryName,ImagePath")] Categoory categoory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(categoory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoory);
        }

        // GET: Categoories/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.Categoories == null)
            {
                return NotFound();
            }

            var categoory = await _context.Categoories.FindAsync(id);
            if (categoory == null)
            {
                return NotFound();
            }
            return View(categoory);
        }

        // POST: Categoories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,CategoryName,ImagePath")] Categoory categoory)
        {
            if (id != categoory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(categoory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategooryExists(categoory.Id))
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
            return View(categoory);
        }

        // GET: Categoories/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.Categoories == null)
            {
                return NotFound();
            }

            var categoory = await _context.Categoories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (categoory == null)
            {
                return NotFound();
            }

            return View(categoory);
        }

        // POST: Categoories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.Categoories == null)
            {
                return Problem("Entity set 'ModelContext.Categoories'  is null.");
            }
            var categoory = await _context.Categoories.FindAsync(id);
            if (categoory != null)
            {
                _context.Categoories.Remove(categoory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategooryExists(decimal id)
        {
          return (_context.Categoories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
