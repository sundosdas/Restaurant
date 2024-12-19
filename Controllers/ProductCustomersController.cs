﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Restaurant.Models;

namespace Restaurant.Controllers
{
    public class ProductCustomersController : Controller
    {
        private readonly ModelContext _context;

        public ProductCustomersController(ModelContext context)
        {
            _context = context;
        }

        // GET: ProductCustomers
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.ProductCustomers.Include(p => p.Customer).Include(p => p.Product);
            return View(await modelContext.ToListAsync());
        }

        // GET: ProductCustomers/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null || _context.ProductCustomers == null)
            {
                return NotFound();
            }

            var productCustomer = await _context.ProductCustomers
                .Include(p => p.Customer)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productCustomer == null)
            {
                return NotFound();
            }

            return View(productCustomer);
        }

        // GET: ProductCustomers/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id");
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id");
            return View();
        }

        // POST: ProductCustomers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,CustomerId,Quantity,DateForm,DateTo")] ProductCustomer productCustomer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productCustomer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", productCustomer.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", productCustomer.ProductId);
            return View(productCustomer);
        }

        // GET: ProductCustomers/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null || _context.ProductCustomers == null)
            {
                return NotFound();
            }

            var productCustomer = await _context.ProductCustomers.FindAsync(id);
            if (productCustomer == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", productCustomer.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", productCustomer.ProductId);
            return View(productCustomer);
        }

        // POST: ProductCustomers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,ProductId,CustomerId,Quantity,DateForm,DateTo")] ProductCustomer productCustomer)
        {
            if (id != productCustomer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productCustomer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductCustomerExists(productCustomer.Id))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Id", productCustomer.CustomerId);
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", productCustomer.ProductId);
            return View(productCustomer);
        }

        // GET: ProductCustomers/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null || _context.ProductCustomers == null)
            {
                return NotFound();
            }

            var productCustomer = await _context.ProductCustomers
                .Include(p => p.Customer)
                .Include(p => p.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productCustomer == null)
            {
                return NotFound();
            }

            return View(productCustomer);
        }

        // POST: ProductCustomers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            if (_context.ProductCustomers == null)
            {
                return Problem("Entity set 'ModelContext.ProductCustomers'  is null.");
            }
            var productCustomer = await _context.ProductCustomers.FindAsync(id);
            if (productCustomer != null)
            {
                _context.ProductCustomers.Remove(productCustomer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductCustomerExists(decimal id)
        {
          return (_context.ProductCustomers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
