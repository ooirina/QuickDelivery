using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuickDelivery.Web.Data;
using QuickDelivery.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace QuickDelivery.Web.Pages.Comenzi
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly QuickDelivery.Web.Data.QuickDeliveryWebContext _context;

        public CreateModel(QuickDelivery.Web.Data.QuickDeliveryWebContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {  //Încărcăm clienții și restaurantele normal
            ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Nume");
            ViewData["RestaurantId"] = new SelectList(_context.Restaurant, "Id", "Nume");
            ViewData["ProdusId"] = new SelectList(_context.Produs, "Id", "Nume");
            return Page();
        }
        public JsonResult OnGetProduseFiltrate(int restaurantId)
        {
            // Luăm doar produsele care aparțin restaurantului selectat
            var produse = _context.Produs
                .Where(p => p.RestaurantId == restaurantId)
                .Select(p => new {
                    id = p.Id,
                    nume = p.Nume,
                    pret = p.Pret
                })
                .ToList();

            return new JsonResult(produse);
        }

        [BindProperty]
        public Comanda Comanda { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Nume");
                ViewData["ProdusId"] = new SelectList(_context.Produs, "Id", "Nume");
                ViewData["RestaurantId"] = new SelectList(_context.Restaurant, "Id", "Nume");
                return Page();
            }
            if (!User.IsInRole("Admin"))
            {
                Comanda.Status = "In preparare"; // Valoare implicită pentru clienți
            }

            _context.Comanda.Add(Comanda);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
