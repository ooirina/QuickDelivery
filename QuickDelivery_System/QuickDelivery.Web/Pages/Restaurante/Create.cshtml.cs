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

namespace QuickDelivery.Web.Pages.Restaurante
{
    [Authorize(Roles = "Admin")]
    public class CreateModel : PageModel
    {
        private readonly QuickDelivery.Web.Data.QuickDeliveryWebContext _context;

        public CreateModel(QuickDelivery.Web.Data.QuickDeliveryWebContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Restaurant Restaurant { get; set; } = default!;

        [BindProperty]
        public IFormFile? Foto { get; set; } // Fișierul încărcat din formular

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("Restaurant.ImagineUrl");

            if (!ModelState.IsValid) return Page();

            if (Foto != null)
            {
                // 1. Definim calea unde salvăm poza (wwwroot/images)
                string folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

                // 2. Generăm un nume unic pentru fișier ca să nu se suprascrie
                string numeFisier = Guid.NewGuid().ToString() + "_" + Foto.FileName;
                string caleCompleta = Path.Combine(folder, numeFisier);

                // 3. Salvăm fișierul pe disc
                using (var stream = new FileStream(caleCompleta, FileMode.Create))
                {
                    await Foto.CopyToAsync(stream);
                }

                // 4. Salvăm în baza de date doar calea relativă
                Restaurant.ImagineUrl = "/images/" + numeFisier;
            }

            _context.Restaurant.Add(Restaurant);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
