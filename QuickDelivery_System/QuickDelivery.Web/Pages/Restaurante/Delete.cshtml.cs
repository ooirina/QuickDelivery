using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuickDelivery.Web.Data;
using QuickDelivery.Web.Models;

namespace QuickDelivery.Web.Pages.Restaurante
{
    public class DeleteModel : PageModel
    {
        private readonly QuickDelivery.Web.Data.QuickDeliveryWebContext _context;

        public DeleteModel(QuickDelivery.Web.Data.QuickDeliveryWebContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Restaurant Restaurant { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurant.FirstOrDefaultAsync(m => m.Id == id);

            if (restaurant == null)
            {
                return NotFound();
            }
            else
            {
                Restaurant = restaurant;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant = await _context.Restaurant.FindAsync(id);
            if (restaurant != null)
            {
                Restaurant = restaurant;
                _context.Restaurant.Remove(Restaurant);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
