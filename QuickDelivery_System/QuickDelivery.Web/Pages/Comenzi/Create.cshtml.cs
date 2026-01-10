using Humanizer;
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
           // Pentru produse: le luăm din bază împreună cu datele restaurantului lor
             var listaProduse = _context.Produs.Include(p => p.Restaurant).ToList();

            // Creăm SelectList-ul cu un parametru în plus pentru gruparea după numele restaurantului
            // Parametrii sunt: (sursa, valoare, text afișat, selectat, numele câmpului de grupare)
            ViewData["ProdusId"] = new SelectList(listaProduse, "Id", "Nume", null, "Restaurant.Nume");
            return Page();
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

            _context.Comanda.Add(Comanda);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
