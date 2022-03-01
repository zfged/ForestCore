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
    public class AdminProductPhotosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminProductPhotosController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: AdminProductPhotos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ProductPhotos.Include(p => p.Product);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AdminProductPhotos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productPhotos = await _context.ProductPhotos
                .Include(p => p.Product)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (productPhotos == null)
            {
                return NotFound();
            }

            return View(productPhotos);
        }

        // GET: AdminProductPhotos/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Description");
            return View();
        }

        // POST: AdminProductPhotos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Path,ProductId")] ProductPhotos productPhotos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productPhotos);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Description", productPhotos.ProductId);
            return View(productPhotos);
        }

        // GET: AdminProductPhotos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productPhotos = await _context.ProductPhotos.SingleOrDefaultAsync(m => m.Id == id);
            if (productPhotos == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Description", productPhotos.ProductId);
            return View(productPhotos);
        }

        // POST: AdminProductPhotos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Path,ProductId")] ProductPhotos productPhotos)
        {
            if (id != productPhotos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productPhotos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductPhotosExists(productPhotos.Id))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Description", productPhotos.ProductId);
            return View(productPhotos);
        }

        // GET: AdminProductPhotos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productPhotos = await _context.ProductPhotos
                .Include(p => p.Product)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (productPhotos == null)
            {
                return NotFound();
            }

            return View(productPhotos);
        }

        // POST: AdminProductPhotos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productPhotos = await _context.ProductPhotos.SingleOrDefaultAsync(m => m.Id == id);
            _context.ProductPhotos.Remove(productPhotos);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ProductPhotosExists(int id)
        {
            return _context.ProductPhotos.Any(e => e.Id == id);
        }
    }
}
