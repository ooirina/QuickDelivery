using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using QuickDelivery.Web.Data;
using QuickDelivery.Web.Models;

namespace QuickDelivery.Web.Pages.Produse
{
    public class CreateModel : PageModel
    {
        private readonly QuickDelivery.Web.Data.QuickDeliveryWebContext _context;

        public CreateModel(QuickDelivery.Web.Data.QuickDeliveryWebContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["RestaurantId"] = new SelectList(_context.Set<Restaurant>(), "Id", "Adresa");
            return Page();
        }

        [BindProperty]
        public Produs Produs { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Produs.Add(Produs);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
