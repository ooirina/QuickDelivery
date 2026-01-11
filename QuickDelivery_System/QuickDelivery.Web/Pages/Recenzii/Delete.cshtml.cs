using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using QuickDelivery.Web.Data;
using QuickDelivery.Web.Models;

namespace QuickDelivery.Web.Pages.Recenzii
{
    public class DeleteModel : PageModel
    {
        private readonly QuickDelivery.Web.Data.QuickDeliveryWebContext _context;

        public DeleteModel(QuickDelivery.Web.Data.QuickDeliveryWebContext context)
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

            // Adăugăm .Include(r => r.Client) pentru a putea verifica email-ul autorului
            var recenzie = await _context.Recenzii
                .Include(r => r.Client)
                .Include(r => r.Restaurant)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (recenzie == null)
            {
                return NotFound();
            }

            // --- LOGICA DE SECURITATE ---
            // Preluăm email-ul utilizatorului logat
            var userEmail = User.Identity?.Name;

            // Dacă utilizatorul NU este Admin ȘI email-ul autorului recenziei NU coincide cu cel logat
            if (!User.IsInRole("Admin") && recenzie.Client?.Email != userEmail)
            {
                // Blocăm accesul la pagina de ștergere
                return Forbid();
            }
            // ----------------------------

            Recenzie = recenzie;

            return Page();
        }
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recenzie = await _context.Recenzii.FindAsync(id);
            if (recenzie != null)
            {
                Recenzie = recenzie;
                _context.Recenzii.Remove(Recenzie);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
