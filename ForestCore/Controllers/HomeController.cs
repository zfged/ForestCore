using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ForestCore.Models;
using ForestCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ForestCore.Controllers
{
    public class HomeController : Controller
    {

        private readonly ApplicationDbContext _context;

      



        public HomeController(ApplicationDbContext context)
        {
            _context = context;
           

        }

        public IActionResult Index()
        {
            IEnumerable<Product> products  =_context.Products.Where(m => m.Top == true);
          
            return View(products);
        }

        public IActionResult Menu()
        {
            return PartialView(_context.Categories.ToList());
        }

        public async Task<IActionResult> Product(int id)
        {

            Product product = await _context.Products.FindAsync(id);
            IEnumerable<ProductPhotos> Images = _context.ProductPhotos.Where(m => m.ProductId == id);
            IEnumerable<ProductParametrs> Params = _context.ProductParams.Where(m => m.ProductId == id);

            IEnumerable<Category> Categories = await _context.Categories.ToListAsync();

            foreach (var item in Categories)
            {

                item.Children = _context.Categories.Where(p => p.ParentId == item.Id);
                if (item.Children.Count() == 0)
                {
                    item.ParentId = 0;
                }
            }

            ViewBag.Сategories = Categories;

            ViewBag.Images = Images;
            ViewBag.Params = Params;


            return  View(product);
        }


        public async Task<IActionResult> CategorySingle(int? id, int page = 1)
        {
            IEnumerable<Category> Categories = await _context.Categories.ToListAsync();

            foreach (var item in Categories)
            {

                item.Children = _context.Categories.Where(p => p.ParentId == item.Id);
                if (item.Children.Count() == 0)
                {
                    item.ParentId = 0;
                }
            }

            ViewBag.Сategories = Categories;

            if (id == null)
            {
                return NotFound();
            }

            var products = _context.Products.Where(m => m.CategoryId == id);

            if (products == null)
            {
                return NotFound();
            }

            int pageSize = 9;

            var count = await products.CountAsync();
            var items = await products.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModelPage viewModel = new IndexViewModelPage
            {
                PageViewModel = pageViewModel,
                Products = items
            };
            return View(viewModel);

        }


        public async Task<IActionResult> Blog()
        {
            return View(await _context.Blogs.ToListAsync());
        }

        public IActionResult Contact()
        {
            return View();
        }

        public async Task<IActionResult> GetCategories()
        {

            return PartialView(await _context.Categories.Where(p => p.ParentId == null).ToListAsync());
        }

        public async Task<IActionResult> BlogSingle(int? id)
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



       

        public IActionResult Error()
        {
            return View();
        }
    }
}
