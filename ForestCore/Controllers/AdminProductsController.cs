using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ForestCore.Data;
using ForestCore.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace ForestCore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IHostingEnvironment _environment;

        public AdminProductsController(ApplicationDbContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: AdminProducts
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Products.Include(p => p.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AdminProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: AdminProducts/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View();
        }

        // POST: AdminProducts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Title,Description,Price,Top,CategoryId")] Product product)
        {


            if (Request.Form.Files.Count == 0 || Request.Form.Files[0].FileName == "")
            {
                ModelState.AddModelError("File", "Вы должны добавить фото");
            }

            if (ModelState.IsValid)
            {

                var images = Path.Combine(_environment.WebRootPath, "images");
                int count = 0;
                List<ProductPhotos> imagesProduct = new List<ProductPhotos>();
                foreach (var file in Request.Form.Files)
                {

                    if (file.Length > 0)
                    {
                        string fileName = Guid.NewGuid().ToString() + file.FileName;
                        using (var fileStream = new FileStream(Path.Combine(images, fileName), FileMode.Create))
                        {

                            imagesProduct.Add(new ProductPhotos() { Path = fileName });
                            await file.CopyToAsync(fileStream);
                        }

                        if (count == 0)
                        {
                            product.MainPhoto = fileName;
                        }

                        count++;
                    }
                }

                product.Images = imagesProduct.AsEnumerable();



                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: AdminProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.SingleOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // POST: AdminProducts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Title,Description,Price,Top,CategoryId")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                string images = Path.Combine(_environment.WebRootPath, "images\\");

                List<ProductPhotos> imagesProduct = new List<ProductPhotos>();

                var imagesDel = _context.ProductPhotos.Where(m => m.ProductId == product.Id);
                if (Request.Form.Files.Count != 0 && Request.Form.Files[0].FileName != "")
                {
                    foreach (var item in imagesDel)
                    {
                        _context.ProductPhotos.Remove(item);

                        if (System.IO.File.Exists(images + item.Path))
                        {
                            System.IO.File.Delete(images + item.Path);
                        }
                    }


                    int count = 0;
                    foreach (var file in Request.Form.Files)
                    {
                        if (file.Length > 0)
                        {
                            string fileName = Guid.NewGuid().ToString() + file.FileName;
                            using (var fileStream = new FileStream(Path.Combine(images, fileName), FileMode.Create))
                            {
                                imagesProduct.Add(new ProductPhotos() { Path = fileName });
                                await file.CopyToAsync(fileStream);
                            }

                            if (count == 0)
                            {
                                product.MainPhoto = fileName;
                            }

                            count++;
                        }
                    }

                    product.MainPhoto = Request.Form.Files[0].FileName;
                    product.Images = imagesProduct.AsEnumerable();
                }






                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", product.CategoryId);
            return View(product);
        }

        // GET: AdminProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: AdminProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {


            var imagesPath = Path.Combine(_environment.WebRootPath, "images\\");

            var product = await _context.Products.SingleOrDefaultAsync(m => m.Id == id);
            var images = _context.ProductPhotos.Where(m => m.ProductId == id);

            var productParams = _context.ProductParams.Where(m => m.ProductId == id);

            foreach (var item in images)
            {
                if (System.IO.File.Exists(imagesPath + item.Path))
                {
                    System.IO.File.Delete(imagesPath + item.Path);
                }
                _context.ProductPhotos.Remove(item);
            }

            foreach (var item in productParams)
            {
                _context.ProductParams.Remove(item);
            }
            await _context.SaveChangesAsync();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
