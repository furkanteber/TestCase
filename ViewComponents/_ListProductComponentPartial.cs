using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestCase.Context;

namespace TestCase
{
    public class _ListProductComponentPartial : ViewComponent
    {
        private readonly TestCaseDbContext _context;

        public _ListProductComponentPartial(TestCaseDbContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var list = _context.Products.Include(c => c.Category).ToList();
            return View(list);
        }

    }
}
