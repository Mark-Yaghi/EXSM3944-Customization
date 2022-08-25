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
    public class InvoiceProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public InvoiceProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: InvoiceProduct
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Orderinventories.Include(o => o.Order).Include(o => o.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: InvoiceProduct/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Orderinventories == null)
            {
                return NotFound();
            }

            var orderInventory = await _context.Orderinventories
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderInventory == null)
            {
                return NotFound();
            }

            return View(orderInventory);
        }

        // GET: InvoiceProduct/Create
        public IActionResult Create()
        {
            ViewData["Orderid"] = new SelectList(_context.Orderinvoices, "Id", "Id");
            ViewData["Inventoryid"] = new SelectList(_context.Inventoryproducts, "Id", "Id");
            return View();
        }

        // POST: InvoiceProduct/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Orderid,Inventoryid,Quantity")] OrderInventory orderInventory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderInventory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Orderid"] = new SelectList(_context.Orderinvoices, "Id", "Id", orderInventory.Orderid);
            ViewData["Inventoryid"] = new SelectList(_context.Inventoryproducts, "Id", "Id", orderInventory.Inventoryid);
            return View(orderInventory);
        }

        // GET: InvoiceProduct/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Orderinventories == null)
            {
                return NotFound();
            }

            var orderInventory = await _context.Orderinventories.FindAsync(id);
            if (orderInventory == null)
            {
                return NotFound();
            }
            ViewData["Orderid"] = new SelectList(_context.Orderinvoices, "Id", "Id", orderInventory.Orderid);
            ViewData["Inventoryid"] = new SelectList(_context.Inventoryproducts, "Id", "Id", orderInventory.Inventoryid);
            return View(orderInventory);
        }

        // POST: InvoiceProduct/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Orderid,Inventoryid,Quantity")] OrderInventory orderInventory)
        {
            if (id != orderInventory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderInventory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderInventoryExists(orderInventory.Id))
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
            ViewData["Orderid"] = new SelectList(_context.Orderinvoices, "Id", "Id", orderInventory.Orderid);
            ViewData["Inventoryid"] = new SelectList(_context.Inventoryproducts, "Id", "Id", orderInventory.Inventoryid);
            return View(orderInventory);
        }

        // GET: InvoiceProduct/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Orderinventories == null)
            {
                return NotFound();
            }

            var orderInventory = await _context.Orderinventories
                .Include(o => o.Order)
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderInventory == null)
            {
                return NotFound();
            }

            return View(orderInventory);
        }

        // POST: InvoiceProduct/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Orderinventories == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Orderinventories'  is null.");
            }
            var orderInventory = await _context.Orderinventories.FindAsync(id);
            if (orderInventory != null)
            {
                _context.Orderinventories.Remove(orderInventory);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderInventoryExists(int id)
        {
          return _context.Orderinventories.Any(e => e.Id == id);
        }
    }
}
