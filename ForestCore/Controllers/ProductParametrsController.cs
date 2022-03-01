using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ForestCore.Data;
using ForestCore.Models;
using Microsoft.AspNetCore.Authorization;

namespace ForestCore.Controllers
{

    [Authorize(Roles = "Admin")]
    public class ProductParametrsController : Controller
    {
        private readonly ApplicationDbContext _context;

       

        public ProductParametrsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: ProductParametrs
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProductParams.Include(p => p.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ProductParametrs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productParametrs = await _context.ProductParams
                .Include(p => p.Product)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (productParametrs == null)
            {
                return NotFound();
            }

            return View(productParametrs);
        }

        // GET: ProductParametrs/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Description");
            return View();
        }

        // POST: ProductParametrs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ProductId")] ProductParametrs productParametrs)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productParametrs);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Description", productParametrs.ProductId);
            return View(productParametrs);
        }

        // GET: ProductParametrs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productParametrs = await _context.ProductParams.SingleOrDefaultAsync(m => m.Id == id);
            if (productParametrs == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Description", productParametrs.ProductId);
            return View(productParametrs);
        }

        // POST: ProductParametrs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ProductId")] ProductParametrs productParametrs)
        {
            if (id != productParametrs.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productParametrs);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductParametrsExists(productParametrs.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Description", productParametrs.ProductId);
            return View(productParametrs);
        }

        // GET: ProductParametrs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productParametrs = await _context.ProductParams
                .Include(p => p.Product)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (productParametrs == null)
            {
                return NotFound();
            }

            return View(productParametrs);
        }

        // POST: ProductParametrs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productParametrs = await _context.ProductParams.SingleOrDefaultAsync(m => m.Id == id);
            _context.ProductParams.Remove(productParametrs);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ProductParametrsExists(int id)
        {
            return _context.ProductParams.Any(e => e.Id == id);
        }
    }
}
