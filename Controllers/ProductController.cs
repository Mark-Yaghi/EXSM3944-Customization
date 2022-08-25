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
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
              return View(await _context.Inventoryproducts.ToListAsync());
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Inventoryproducts == null)
            {
                return NotFound();
            }

            var inventoryProduct = await _context.Inventoryproducts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inventoryProduct == null)
            {
                return NotFound();
            }

            return View(inventoryProduct);
        }

        // GET: Product/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Qoh")] InventoryProduct inventoryProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inventoryProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(inventoryProduct);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Inventoryproducts == null)
            {
                return NotFound();
            }

            var inventoryProduct = await _context.Inventoryproducts.FindAsync(id);
            if (inventoryProduct == null)
            {
                return NotFound();
            }
            return View(inventoryProduct);
        }

        // POST: Product/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Qoh")] InventoryProduct inventoryProduct)
        {
            if (id != inventoryProduct.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inventoryProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InventoryProductExists(inventoryProduct.Id))
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
            return View(inventoryProduct);
        }

        // GET: Product/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Inventoryproducts == null)
            {
                return NotFound();
            }

            var inventoryProduct = await _context.Inventoryproducts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inventoryProduct == null)
            {
                return NotFound();
            }

            return View(inventoryProduct);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Inventoryproducts == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Inventoryproducts'  is null.");
            }
            var inventoryProduct = await _context.Inventoryproducts.FindAsync(id);
            if (inventoryProduct != null)
            {
                _context.Inventoryproducts.Remove(inventoryProduct);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InventoryProductExists(int id)
        {
          return _context.Inventoryproducts.Any(e => e.Id == id);
        }
    }
}
