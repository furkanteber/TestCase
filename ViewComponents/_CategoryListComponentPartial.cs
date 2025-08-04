using Microsoft.AspNetCore.Mvc;
using TestCase.Context;

namespace TestCase.ViewComponents
{
    public class _CategoryListComponentPartial : ViewComponent
    {
        private readonly TestCaseDbContext _context;

        public _CategoryListComponentPartial(TestCaseDbContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var categoryList = _context.Categories.ToList();
            return View(categoryList);
        }
    }
}
