using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuickDelivery.Web.Data;
using QuickDelivery.Web.Models;

namespace QuickDelivery.Web.Pages.Comenzi
{
    public class DeleteModel : PageModel
    {
        private readonly QuickDelivery.Web.Data.QuickDeliveryWebContext _context;

        public DeleteModel(QuickDelivery.Web.Data.QuickDeliveryWebContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Comanda Comanda { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Adăugăm .Include(c => c.Client) pentru a putea verifica proprietarul comenzii
            var comanda = await _context.Comanda
                .Include(c => c.Client)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (comanda == null)
            {
                return NotFound();
            }

            // --- LOGICA DE SECURITATE ---
            // Preluăm email-ul utilizatorului logat
            var userEmail = User.Identity.Name;

            // Dacă utilizatorul NU este Admin ȘI email-ul clientului din comandă NU este cel al utilizatorului logat
            if (!User.IsInRole("Admin") && comanda.Client.Email != userEmail)
            {
                // Blocăm accesul la pagina de ștergere
                return Forbid();
            }
            // ----------------------------

            Comanda = comanda;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comanda = await _context.Comanda.FindAsync(id);
            if (comanda != null)
            {
                Comanda = comanda;
                _context.Comanda.Remove(Comanda);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
