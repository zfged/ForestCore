using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ForestCore.Data;
using ForestCore.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;

namespace ForestCore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminBlogsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private IHostingEnvironment _environment;

        public AdminBlogsController(ApplicationDbContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        // GET: AdminBlogs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Blogs.ToListAsync());
        }

        // GET: AdminBlogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs
                .SingleOrDefaultAsync(m => m.Id == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // GET: AdminBlogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AdminBlogs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Photo")] Blog blog)
        {


            if (Request.Form.Files.Count == 0 || Request.Form.Files[0].FileName == "")
            {
                ModelState.AddModelError("File", "Вы должны добавить фото");
            }

            if (ModelState.IsValid)
            {
                var images = Path.Combine(_environment.WebRootPath, "images");
                var file = Request.Form.Files[0];
                blog.Photo = Guid.NewGuid().ToString() + file.FileName;

                using (var fileStream = new FileStream(Path.Combine(images, blog.Photo), FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }

                _context.Add(blog);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(blog);
        }

        // GET: AdminBlogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs.SingleOrDefaultAsync(m => m.Id == id);
            if (blog == null)
            {
                return NotFound();
            }
            return View(blog);
        }

        // POST: AdminBlogs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Photo")] Blog blog)
        {
            if (id != blog.Id)
            {
                return NotFound();
            }



            if (ModelState.IsValid)
            {

                string imagesDel = Path.Combine(_environment.WebRootPath, "images\\");

               
               

                if (Request.Form.Files.Count != 0 && Request.Form.Files[0].FileName != "")
                {

                    if (System.IO.File.Exists(imagesDel + blog.Photo))
                    {
                        System.IO.File.Delete(imagesDel + blog.Photo);
                    }
                    var images = Path.Combine(_environment.WebRootPath, "images");
                    var file = Request.Form.Files[0];
                    blog.Photo = Guid.NewGuid().ToString() + file.FileName;

                    using (var fileStream = new FileStream(Path.Combine(images, blog.Photo), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }


                try
                {
                    _context.Update(blog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogExists(blog.Id))
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
            return View(blog);
        }

        // GET: AdminBlogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs
                .SingleOrDefaultAsync(m => m.Id == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // POST: AdminBlogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            string imagesDel = Path.Combine(_environment.WebRootPath, "images\\");
            var blog = await _context.Blogs.SingleOrDefaultAsync(m => m.Id == id);
            if (System.IO.File.Exists(imagesDel + blog.Photo))
            {
                System.IO.File.Delete(imagesDel + blog.Photo);
            }
            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool BlogExists(int id)
        {
            return _context.Blogs.Any(e => e.Id == id);
        }
    }
}
