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

namespace QuickDelivery.Web.Pages.Produse
{
    public class EditModel : PageModel
    {
        private readonly QuickDelivery.Web.Data.QuickDeliveryWebContext _context;

        public EditModel(QuickDelivery.Web.Data.QuickDeliveryWebContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Produs Produs { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produs =  await _context.Produs.FirstOrDefaultAsync(m => m.Id == id);
            if (produs == null)
            {
                return NotFound();
            }
            Produs = produs;
           ViewData["RestaurantId"] = new SelectList(_context.Set<Restaurant>(), "Id", "Adresa");
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

            _context.Attach(Produs).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProdusExists(Produs.Id))
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

        private bool ProdusExists(int id)
        {
            return _context.Produs.Any(e => e.Id == id);
        }
    }
}
