using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MVC_Demo.Data;
using MVC_Demo.Models;

namespace MVC_Demo.Controllers
{
    public class InvoiceController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvoiceController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Invoice
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Orderinvoices.Include(o => o.Customer);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Invoice/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Orderinvoices == null)
            {
                return NotFound();
            }

            var orderInvoice = await _context.Orderinvoices
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderInvoice == null)
            {
                return NotFound();
            }

            return View(orderInvoice);
        }

        // GET: Invoice/Create
        public IActionResult Create()
        {
            ViewData["Customerid"] = new SelectList(_context.Customers, "Id", "Id");
            return View();
        }

        // POST: Invoice/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Customerid")] OrderInvoice orderInvoice)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderInvoice);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Customerid"] = new SelectList(_context.Customers, "Id", "Id", orderInvoice.Customerid);
            return View(orderInvoice);
        }

        // GET: Invoice/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Orderinvoices == null)
            {
                return NotFound();
            }

            var orderInvoice = await _context.Orderinvoices.FindAsync(id);
            if (orderInvoice == null)
            {
                return NotFound();
            }
            ViewData["Customerid"] = new SelectList(_context.Customers, "Id", "Id", orderInvoice.Customerid);
            return View(orderInvoice);
        }

        // POST: Invoice/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Customerid")] OrderInvoice orderInvoice)
        {
            if (id != orderInvoice.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderInvoice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderInvoiceExists(orderInvoice.Id))
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
            ViewData["Customerid"] = new SelectList(_context.Customers, "Id", "Id", orderInvoice.Customerid);
            return View(orderInvoice);
        }

        // GET: Invoice/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Orderinvoices == null)
            {
                return NotFound();
            }

            var orderInvoice = await _context.Orderinvoices
                .Include(o => o.Customer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderInvoice == null)
            {
                return NotFound();
            }

            return View(orderInvoice);
        }

        // POST: Invoice/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Orderinvoices == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Orderinvoices'  is null.");
            }
            var orderInvoice = await _context.Orderinvoices.FindAsync(id);
            if (orderInvoice != null)
            {
                _context.Orderinvoices.Remove(orderInvoice);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderInvoiceExists(int id)
        {
          return _context.Orderinvoices.Any(e => e.Id == id);
        }
    }
}
