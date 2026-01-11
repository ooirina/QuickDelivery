using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QuickDelivery.Web.Data;
using QuickDelivery.Web.Models;

namespace QuickDelivery.Web.Pages.Recenzii
{
    public class EditModel : PageModel
    {
        private readonly QuickDelivery.Web.Data.QuickDeliveryWebContext _context;

        public EditModel(QuickDelivery.Web.Data.QuickDeliveryWebContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Recenzie Recenzie { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Includem Clientul pentru a putea verifica adresa de email a autorului recenziei
            var recenzie = await _context.Recenzii
                .Include(r => r.Client)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (recenzie == null)
            {
                return NotFound();
            }

            // --- LOGICA DE SECURITATE ---
            // Preluăm identitatea utilizatorului logat
            var userEmail = User.Identity.Name;

            // Verificăm permisiunile: dacă NU e Admin și NU este recenzia lui
            if (!User.IsInRole("Admin") && recenzie.Client.Email != userEmail)
            {
                // Blocăm accesul dacă un client încearcă să editeze recenzia altcuiva
                return Forbid();
            }
            // ------------------------------------

            Recenzie = recenzie;

            // Listele pentru Dropdown-uri
            ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Email");
            ViewData["RestaurantId"] = new SelectList(_context.Restaurant, "Id", "Nume"); // Am schimbat în Nume pentru claritate

            return Page();
        }
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                
                ViewData["RestaurantId"] = new SelectList(_context.Restaurant, "Id", "Nume");
                return Page();
            }

        
            _context.Attach(Recenzie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecenzieExists(Recenzie.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            
            return RedirectToPage("./Index");
        }
        private bool RecenzieExists(int id)
        {
            return _context.Recenzii.Any(e => e.Id == id);
        }
    }
}
