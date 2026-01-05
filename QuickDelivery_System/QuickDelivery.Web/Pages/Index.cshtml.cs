using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuickDelivery.Web.Data;
using QuickDelivery.Web.Models;

namespace QuickDelivery.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly QuickDeliveryWebContext _context;

        public IndexModel(QuickDeliveryWebContext context)
        {
            _context = context;
        }

        public IList<Restaurant> Restaurante { get; set; } = default!;

        public async Task OnGetAsync()
        {
            // Luăm primele 6 restaurante pentru prima pagină
            Restaurante = await _context.Restaurant.Take(6).ToListAsync();
        }
    }
}