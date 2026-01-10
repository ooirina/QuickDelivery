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

namespace QuickDelivery.Web.Pages.Comenzi
{
    public class EditModel : PageModel
    {
        private readonly QuickDelivery.Web.Data.QuickDeliveryWebContext _context;

        public EditModel(QuickDelivery.Web.Data.QuickDeliveryWebContext context)
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

            // Folosim .Include(c => c.Client) pentru a putea accesa email-ul clientului asociat comenzii
            var comanda = await _context.Comanda
                .Include(c => c.Client)
                .Include(c => c.Restaurant)
                .Include(c => c.Produs)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (comanda == null)
            {
                return NotFound();
            }

            // --- LOGICA DE SECURITATE ---
            // Preluăm email-ul utilizatorului care este logat în acest moment
            var userEmail = User.Identity.Name;

            // Verificăm: dacă utilizatorul NU este Admin ȘI email-ul comenzii nu corespunde cu email-ul lui
            if (!User.IsInRole("Admin") && comanda.Client.Email != userEmail)
            {
                // Îi blocăm accesul dacă încearcă să editeze comanda altcuiva
                return Forbid();
            }
           
            Comanda = comanda;
           ViewData["ClientId"] = new SelectList(_context.Client, "Id", "Email");
            ViewData["RestaurantId"] = new SelectList(_context.Restaurant, "Id", "Nume");
            ViewData["ProdusId"] = new SelectList(_context.Produs, "Id", "Nume");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // 1. Preluăm comanda originală din baza de date pentru a verifica proprietarul
            // Folosim .AsNoTracking() pentru a nu intra în conflict cu obiectul care va fi atașat ulterior
            var comandaOriginala = await _context.Comanda
                .Include(c => c.Client)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == Comanda.Id);

            if (comandaOriginala == null)
            {
                return NotFound();
            }

            // 2. LOGICA DE SECURITATE: Verificăm dacă utilizatorul are dreptul să modifice această comandă
            var userEmail = User.Identity.Name;
            if (!User.IsInRole("Admin") && comandaOriginala.Client.Email != userEmail)
            {
                return Forbid(); // Clientul încearcă să modifice comanda altcuiva
            }

            // 3. Dacă utilizatorul este CLIENT, forțăm păstrarea valorilor pe care nu are voie să le schimbe
            if (!User.IsInRole("Admin"))
            {
                Comanda.Status = comandaOriginala.Status;   // Clientul nu poate schimba Statusul
                Comanda.ClientId = comandaOriginala.ClientId; // Clientul nu poate atribui comanda altcuiva
                Comanda.DataComanda = comandaOriginala.DataComanda; // De regulă, data rămâne neschimbată
            }

            // 4. Atașăm obiectul modificat și salvăm
            _context.Attach(Comanda).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComandaExists(Comanda.Id))
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

        private bool ComandaExists(int id)
        {
            return _context.Comanda.Any(e => e.Id == id);
        }
    }
}
