using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TestCase.Context;
using TestCase.Entities;
using TestCase.Models;

namespace TestCase.Controllers
{
    public class HomeController : Controller
    {
        private readonly TestCaseDbContext _context;

        public HomeController(TestCaseDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateProduct(Product model)
        {
            if (ModelState.IsValid)
            {
               await _context.Products.AddAsync(model);
               _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult EditProduct(string id)
        {
            var editId = _context.Products.Include(c => c.Category).FirstOrDefault(e => e.Id == Guid.Parse(id));
            
            if (editId == null)
            {
                return BadRequest();
            }
            ViewBag.SelectedCategoryId = editId.CategoryId;
            return View(editId);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditProduct(Product model)
        {
            if (ModelState.IsValid)
            {
                var editID =  _context.Products.Include(c => c.Category).FirstOrDefault(e => e.Id == model.Id);
                if (editID == null)
                {
                    return NotFound();
                }
                editID.Title = model.Title;
                editID.Quantity = model.Quantity;
                editID.CategoryId = model.CategoryId;
                editID.Description = model.Description;
                _context.SaveChanges();
                return RedirectToAction("Index", "Home");

            }
            return View(model);
        }

        [HttpPost]
        public IActionResult Search(string searchInput, int? minStock, int? maxStock)
        {
            ViewBag.SearchInput = searchInput ;
            var products = _context.Products.Include(p => p.Category).AsQueryable();
            if (!string.IsNullOrWhiteSpace(searchInput))
            {
                var lowerInput = searchInput.ToLower();

                products = products.Where(p =>
                    p.Title.ToLower().Contains(lowerInput) ||
                    p.Description.ToLower().Contains(lowerInput) ||
                    p.Category.CategoryName.ToLower().Contains(lowerInput));
            }
            if (minStock.HasValue)
            {
                products = products.Where(p => p.Quantity >= minStock.Value);
            }

            if (maxStock.HasValue)
            {
                products = products.Where(p => p.Quantity <= maxStock.Value);
            }

            return View(products.ToList());
        }

        public ActionResult DeleteProduct(string id)
        {
            var delete = _context.Products.Find(Guid.Parse(id));
            if (delete != null)
            {
                _context.Products.Remove(delete);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

    }
}
