using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using QuickDelivery.Web.Data;
using QuickDelivery.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickDelivery.Web.Pages.Produse
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly QuickDelivery.Web.Data.QuickDeliveryWebContext _context;

        public CreateModel(QuickDelivery.Web.Data.QuickDeliveryWebContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int? restId) 
        {
            // Dacă venim de pe pagina unui restaurant, restId va avea o valoare
            if (restId.HasValue)
            {
                ViewData["RestaurantId"] = new SelectList(_context.Restaurant, "Id", "Nume", restId.Value);

                ViewData["CategorieId"] = new SelectList(_context.Categorie, "Id", "Nume");
            }
            else
            {
                ViewData["RestaurantId"] = new SelectList(_context.Restaurant, "Id", "Nume");
                ViewData["CategorieId"] = new SelectList(_context.Categorie, "Id", "Nume");
            }

            return Page();
        }
        [BindProperty]
        public Produs Produs { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["RestaurantId"] = new SelectList(_context.Restaurant, "Id", "Nume");
                ViewData["CategorieId"] = new SelectList(_context.Categorie, "Id", "Nume");
                return Page();
            }

            _context.Produs.Add(Produs);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
