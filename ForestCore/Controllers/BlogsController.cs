using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ForestCore.Data;
using ForestCore.Models;
using Newtonsoft.Json;

namespace ForestCore.Controllers
{
    [Produces("application/json")]

    public class BlogsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BlogsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Blogs
        [Route("api/Blogs/Get")]
        [HttpGet]
        public IActionResult GetBlogs()
        {
            return Ok(JsonConvert.SerializeObject(_context.Blogs));
        }



        // PUT: api/Blogs/5
        [Route("api/Blogs/Edit")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBlogs(int id, Blog blog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != blog.Id)
            {
                return BadRequest();
            }

            _context.Entry(blog).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!blogsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Blogs
        [Route("api/Blogs/Add")]
        [HttpPost]
        public async Task<IActionResult> Postblogs(Blog blog)
        {


            _context.Blogs.Add(blog);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getblogs", new { id = blog.Id }, blog);
        }

        // DELETE: api/Blogs/5
        [Route("api/Blogs/Delete")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBlogs(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var blog = await _context.Blogs.SingleOrDefaultAsync(m => m.Id == id);
            if (blog == null)
            {
                return NotFound();
            }

            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();

            return Ok(blog);
        }

        private bool blogsExists(int id)
        {
            return _context.Blogs.Any(e => e.Id == id);
        }
    }
}