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

namespace QuickDelivery.Web.Pages.Restaurante
{
    [Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly QuickDelivery.Web.Data.QuickDeliveryWebContext _context;

        public EditModel(QuickDelivery.Web.Data.QuickDeliveryWebContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Restaurant Restaurant { get; set; } = default!;

        [BindProperty]
        public IFormFile? Foto { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurant =  await _context.Restaurant.FirstOrDefaultAsync(m => m.Id == id);
            if (restaurant == null)
            {
                return NotFound();
            }
            Restaurant = restaurant;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            // 1. Căutăm restaurantul existent în baza de date fără a-l "urmări" (AsNoTracking) 
            // ca să extragem calea pozei vechi
            var restaurantDinDb = await _context.Restaurant.AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == Restaurant.Id);

            if (Foto != null)
            {
                // Utilizatorul a ales o POZĂ NOUĂ
                string folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                string numeFisier = Guid.NewGuid().ToString() + "_" + Foto.FileName;
                string caleCompleta = Path.Combine(folder, numeFisier);

                using (var stream = new FileStream(caleCompleta, FileMode.Create))
                {
                    await Foto.CopyToAsync(stream);
                }

                // Punem calea pozei noi
                Restaurant.ImagineUrl = "/images/" + numeFisier;
            }
            else
            {
                // Utilizatorul NU a ales o poză, deci o păstrăm pe cea existentă
                Restaurant.ImagineUrl = restaurantDinDb.ImagineUrl;
            }

            _context.Attach(Restaurant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RestaurantExists(Restaurant.Id)) return NotFound();
                else throw;
            }

            return RedirectToPage("./Index");
        }

        private bool RestaurantExists(int id)
        {
            return _context.Restaurant.Any(e => e.Id == id);
        }
    }
}
