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

    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [Route("api/Categories/Get")]
        [HttpGet]
        public IActionResult GetCategories()
        {
            return Ok(JsonConvert.SerializeObject(_context.Categories));
        }


       



        // PUT: api/Categories/5
        [Route("api/Categories/Edit")]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != category.Id)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!categoryExists(id))
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

        // POST: api/Categories
        [Route("api/Categories/Add")]
        [HttpPost]
        public async Task<IActionResult> PostCategory(Category category)
        {


            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getcategory", new { id = category.Id }, category);
        }

        // DELETE: api/Categories/5
        [Route("api/Categories/Delete")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await _context.Categories.SingleOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return Ok(category);
        }

        private bool categoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}