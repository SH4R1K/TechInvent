using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebMVC.Data;

namespace WebMVC.ViewComponents
{
    public class SearchViewComponent : ViewComponent
    {
        private readonly TechInventContext _context;

        public SearchViewComponent(TechInventContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var cabinets = await _context.Cabinets.Include(c => c.Workplaces).ToListAsync();
            return View(cabinets);
        }
    }
}
